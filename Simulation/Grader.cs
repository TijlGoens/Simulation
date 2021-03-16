using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Simulation
{
    public class Grader
    {
        private int GraderNumber { get; set; }
        private double BoxesPerMinute { get; set; }
        private List<GraderStation> Stations { get; set; }
        private Queue<Product> InputConveyor { get; set; }
        //A list so that each weightclass has a Queue of items and a string (its name)
        public List<Tuple<string,Queue<SortedBox>>> SortedOutputConveyors { get; } 
        private List<Product> Rejects { get; set; }
        public event EventHandler UpdateChartView;
        private Random Rand { get; set; }

        public Grader(int graderNumber)
        {
            GraderNumber = graderNumber;
            InputConveyor = new Queue<Product>();
            Stations = new List<GraderStation>();
            SortedOutputConveyors = new List<Tuple<string, Queue<SortedBox>>>();
            Rejects = new List<Product>();            
            //Grader objects created simultaniously need to have different random seeds
            Rand = new Random(Guid.NewGuid().GetHashCode());            
            //Make 10 graderstations
            for (int i = 1; i <= 10; i++)
            {
                GraderStation graderStation = new GraderStation(i, 4, i - 0.5, i + 0.5);
                Stations.Add(graderStation);
                //Add the string to the outputconveyor
                SortedOutputConveyors.Add(new Tuple<string, Queue<SortedBox>>(graderStation.ProductWeightClassificationName, new Queue<SortedBox>()));
            }
            foreach (var station in Stations)
            {
                station.BoxUnderThisStationIsFull += BoxUnderThisStationIsFull;
            }
        }

        public void FillInputConveyor(int amountOfItems)
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                //Add a new product between 0 and 10 kg
                InputConveyor.Enqueue(new Product(FoodType.Salmon, Rand.NextDouble() * 10));
            }
        }

        // event handler
        public void BoxUnderThisStationIsFull(object sender, EventArgs e)
        {
            int index = SortedOutputConveyors.FindIndex(x => x.Item1 == ((GraderStation)sender).ProductWeightClassificationName);
            if(index == -1)
            {
                //First time a product of this weightclass is added we add the weightclass to SortedOutputConveyors
                SortedOutputConveyors.Add(new Tuple<string, Queue<SortedBox>>(((GraderStation)sender).ProductWeightClassificationName, new Queue<SortedBox>()));
                //Add the box to the newly added weightclass
                SortedOutputConveyors[SortedOutputConveyors.Count-1].Item2.Enqueue(((GraderStation)sender).BoxUnderThisStation);
                //Sort SortedOutputConveyors based on Minweight of the first box of each class
                SortedOutputConveyors.Sort((x, y) => y.Item2.Peek().MinWeightInKg.CompareTo(x.Item2.Peek().MinWeightInKg));
            }
            else
            {
                //Else just add the box to the queue of the corresponding weightclass
                SortedOutputConveyors[index].Item2.Enqueue(((GraderStation)sender).BoxUnderThisStation);
            }
            //Update the chartView
            if(UpdateChartView != null)
            {
                UpdateChartView.Invoke(this, EventArgs.Empty);
            }            
            Trace.WriteLine($"Box filled under grader {GraderNumber}");
        }        

        public void Run()
        {
            while (InputConveyor.Count != 0)
            {
                bool gotGraded = false;
                Product currentProduct = InputConveyor.Dequeue();
                
                int sleepTime = Rand.Next(200, 300); //Lets imagine it takes a quarter of a second to grade the product on average
                Thread.Sleep(sleepTime);
                foreach (var station in Stations)
                {
                    if (currentProduct.WeightInKg >= station.MinWeightInKg && currentProduct.WeightInKg <= station.MaxWeightInKg)
                    {
                        station.BoxUnderThisStation.AddProduct(currentProduct);
                        Trace.WriteLine($"Single item processed grader: {GraderNumber} Station: {station.ProductWeightClassificationName} Count: {station.BoxUnderThisStation.Products.Count}");
                        gotGraded = true;
                        break; //The product has been graded, stop the loop
                    }
                }
                if (gotGraded == false)
                {
                    Rejects.Add(currentProduct);
                }
            }
            Console.WriteLine($"{DateTime.Now}: There is no more product on the inputconveyor of grader: {this.GraderNumber}");
        }
    }
}
