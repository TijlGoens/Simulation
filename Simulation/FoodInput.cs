using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation
{
	public enum TypeOfMeat { Chicken, Pork, Beef, Trout, Salmon };
	public class FoodInput
	{
		
		double WeightInKg { get; }

		TypeOfMeat TypeOfMeat { get; }

		FoodInput(double weigtInKg, TypeOfMeat typeOfMeat)
        {
			WeightInKg = weigtInKg;
			TypeOfMeat = TypeOfMeat;
        }		
	}
}

