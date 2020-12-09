using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Interfaces;

namespace Task1.Models.Sweets.Cookies
{
    public class ChocolateCookie : Cookie, IChocolable
    {
        private double _chocolatePercentage;
      
        public double PercentageOfChocolate 
        {
            get => _chocolatePercentage;
            set => _chocolatePercentage = value < 0 ? 0 : value > 90 ? 90 : value;
        }

        public ChocolateCookie()
        {

        }
        public ChocolateCookie(string name, double weight, string cookieType, double chocoPercentage) : base(name, weight, cookieType)
        {
            PercentageOfChocolate = chocoPercentage;
        }

        public override void Print()
        {
            Console.WriteLine($"ChocoCookie Name: {Name}, Weight: {Weight}g, CoockieType: {CookieType}, Chocolate percentage: {PercentageOfChocolate}%");
        }
    }
}
