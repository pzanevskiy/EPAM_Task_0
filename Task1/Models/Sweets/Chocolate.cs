using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Interfaces;

namespace Task1.Models.Sweets
{
    public class Chocolate : Sweet, ISugarable, IChocolable
    {
        private double _sugar;
        private double _percentageOfChocolate;
        
        public double Sugar
        {
            get => _sugar;
            set => _sugar = value < 0 ? 0 : value > 100 ? 100 : value;
        }
        public double PercentageOfChocolate 
        {
            get => _percentageOfChocolate;
            set => _percentageOfChocolate = value < 0 ? 0 : value > 100 ? 100 : value;
        }
        public Chocolate()
        {

        }
        public Chocolate(string name, double weight, double sugar, double percentOfChocolate) : base(name, weight)
        {
            Sugar = sugar;
            PercentageOfChocolate = percentOfChocolate;
        }
        public override void Print()
        {
            Console.WriteLine($"Chocolate name: {Name}, weight: {Weight}g, sugar: {Sugar}, percentage of chocolate: {PercentageOfChocolate}%");
        }
    }
}
