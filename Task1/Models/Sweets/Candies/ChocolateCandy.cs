using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Interfaces;

namespace Task1.Models.Sweets.Candies
{
    public class ChocolateCandy : Candy, IChocolable
    {
        private double _percentageOfChocolate;
        public double PercentageOfChocolate
        {
            get => _percentageOfChocolate;
            set => _percentageOfChocolate = value < 0 ? 0 : value > 100 ? 100 : value;
        }

        public ChocolateCandy() { }
        public ChocolateCandy(string name, double weight, double sugar,double perecentageOfChocolate) : base(name, weight, sugar)
        {
            PercentageOfChocolate = perecentageOfChocolate;
        }

        public override void Print()
        {
            Console.WriteLine($"ChocoCandy Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, PercentOfChoco: {PercentageOfChocolate}%");
        }
    }
}
