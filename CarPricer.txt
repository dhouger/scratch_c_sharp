#region Instructions
/*
 * You are tasked with writing an algorithm that determines the value of a used car, 
 * given several factors.
 * 
 *    AGE:    Given the number of months of how old the car is, reduce its value one-half 
 *            (0.5) percent.
 *            After 10 years, it's value cannot be reduced further by age. This is not 
 *            cumulative.
 *            
 *    MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
 *              percent (0.2). Do not consider remaining miles. After 150,000 miles, it's 
 *              value cannot be reduced further by miles.
 *            
 *    PREVIOUS OWNER:    If the car has had more than 2 previous owners, reduce its value 
 *                       by twenty-five (25) percent. If the car has had no previous  
 *                       owners, add ten (10) percent of the FINAL car value at the end.
 *                    
 *    COLLISION:        For every reported collision the car has been in, remove two (2) 
 *                      percent of it's value up to five (5) collisions.
 *                    
 * 
 *    Each factor should be off of the result of the previous value in the order of
 *        1. AGE
 *        2. MILES
 *        3. PREVIOUS OWNER
 *        4. COLLISION
 *        
 *    E.g., Start with the current value of the car, then adjust for age, take that  
 *    result then adjust for miles, then collision, and finally previous owner. 
 *    Note that if previous owner, had a positive effect, then it should be applied 
 *    AFTER step 4. If a negative effect, then BEFORE step 4.
 */
#endregion

using System;
using NUnit.Framework;

namespace CarPricer
{
	public class Car
	{
		public decimal PurchaseValue { get; set; }
		public int AgeInMonths { get; set; }
		public int NumberOfMiles { get; set; }
		public int NumberOfPreviousOwners { get; set; }
		public int NumberOfCollisions { get; set; }
	}

	public class PriceDeterminator
	{
		public decimal DetermineCarPrice(Car car)
		{
			decimal value = car.PurchaseValue;

			// Calculate reduced value with age
			CalculateValueReduction(ref value, car.AgeInMonths, (12 * 10), (decimal)0.005, false, car);

			// Calculate reduced value with miles
			int thousandsOfMiles = (int)Math.Floor((decimal)car.NumberOfMiles / 1000);

			CalculateValueReduction(ref value, thousandsOfMiles, 150, (decimal)0.002, true);

			// Calculate reduced value with previous owners - negative effect
			if (car.NumberOfPreviousOwners > 2)
			{ 
				value += (value * (decimal)0.25);
			}

			// Calculate reduced value with number of collisions
			CalculateValueReduction(ref value, car.NumberOfCollisions, 5, (decimal)0.02, true);

			// Calculate increased value with previous owners - posative impact
			if (car.NumberOfPreviousOwners == 0)
			{
				value += (value * (decimal)0.10);
			}

			return Math.Round(value, 2, MidpointRounding.AwayFromZero);
		}

		
		private void CalculateValueReduction(ref decimal value, int itterations, int maxItterations, decimal percentage, bool useTotalPercentage = false, Car car = null)
		{
			// Check for max value
			if (itterations > maxItterations) { itterations = maxItterations; }

			// Temp percentage
			decimal tempPercent = percentage;

			// Temp itterations, used to exit out of an edge case when collisions = 0
			int tempItterations = itterations;

			while (itterations > 0)
			{
				if (useTotalPercentage && itterations > 1)
				{
					percentage += tempPercent;
				}
				else if (!useTotalPercentage)
				{
					if (car == null)
					{
						value -= value * percentage;
					}
					else
					{
						value -= car.PurchaseValue * percentage;
					}
				}
				itterations--;
			}

			// Calculate new value with a total modified percentage
			if (useTotalPercentage && tempItterations > 0 && car == null)
			{
				value -= value * percentage;
			}
			else if (useTotalPercentage && tempItterations > 0)
			{
				value -= car.PurchaseValue * percentage;
			}
		}
	}


	public class UnitTests
	{
		[TestCase]
		public void CalculateCarValue()
		{
			AssertCarValue(25313.40m, 35000m, 3 * 12, 50000, 1, 1);
			AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
			AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
			AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
			AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
		}

		private static void AssertCarValue(decimal expectValue, decimal purchaseValue,
		int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int
		numberOfCollisions)
		{
			Car car = new Car
			{
				AgeInMonths = ageInMonths,
				NumberOfCollisions = numberOfCollisions,
				NumberOfMiles = numberOfMiles,
				NumberOfPreviousOwners = numberOfPreviousOwners,
				PurchaseValue = purchaseValue
			};
			PriceDeterminator priceDeterminator = new PriceDeterminator();
			var carPrice = priceDeterminator.DetermineCarPrice(car);
			Assert.AreEqual(expectValue, carPrice);
		}
	}
}