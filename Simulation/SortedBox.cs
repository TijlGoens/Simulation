using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation
{    

    public class SortedBox
    {
        public double MinWeightInKg { get; }
        public double MaxWeightInKg { get; }
        private int NumOfItems { get; set; }
        private FoodType FoodType { get; set; }
        public List<Product> Products { get; set; }

        public string ProductWeightClassificationName { get; }

        public event EventHandler BoxIsFull;

        public SortedBox(double minWeight, double maxWeight, int numOfItems, List<Product> products, string productWeightClassificationName)
        {
            MinWeightInKg = minWeight;
            MaxWeightInKg = maxWeight;
            NumOfItems = numOfItems;            
            Products = products;
            ProductWeightClassificationName = productWeightClassificationName;
            FoodType = FoodType.Salmon;
        }        

        public void AddProduct(Product product)
        {
            Products.Add(product);
            if(Products.Count == NumOfItems)
            {
                //Box is full                
                BoxIsFull?.Invoke(this, EventArgs.Empty); //send box is full event to Graderstation
            }            
        }

        
    }
}
