using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation
{
    public enum FoodType { Chicken, Pork, Beef, Salmon, Cod };
    public class Product
    {       
        public double WeightInKg { get; }
        public FoodType FoodType { get; }
        public Product(FoodType foodType, double weightInKg)
        {
            WeightInKg = weightInKg;
            FoodType = foodType;
        }
    }
}
