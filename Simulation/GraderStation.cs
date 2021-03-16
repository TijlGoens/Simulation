using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation
{
    public class GraderStation
    {
        private int StationNumber { get; }
        private int ProductsPerBox { get; }
        public double MinWeightInKg { get; }
        public double MaxWeightInKg { get; }

        public string ProductWeightClassificationName { get; }
        public SortedBox BoxUnderThisStation { get; private set; }

        public event EventHandler BoxUnderThisStationIsFull;

        public GraderStation(int stationNumber, int productsPerBox, double minWeight, double maxWeight)
        {
            StationNumber = stationNumber;
            ProductsPerBox = productsPerBox;
            MinWeightInKg = minWeight;
            MaxWeightInKg = maxWeight;
            ProductWeightClassificationName = $"{MinWeightInKg} - {MaxWeightInKg}";
            BoxUnderThisStation = new SortedBox(minWeight, maxWeight, 4, new List<Product>(), ProductWeightClassificationName);
            BoxUnderThisStation.BoxIsFull += OnBoxIsFull;
        }

        // event handler
        public void OnBoxIsFull(object sender, EventArgs e)
        {
            //Send box is full event to Grader first
            BoxUnderThisStationIsFull.Invoke(this, EventArgs.Empty); 
            //Prepare the new box
            this.BoxUnderThisStation = new SortedBox(this.MinWeightInKg, this.MaxWeightInKg, 4, new List<Product>(), ProductWeightClassificationName);
            BoxUnderThisStation.BoxIsFull += OnBoxIsFull;            
        }
    }
}
