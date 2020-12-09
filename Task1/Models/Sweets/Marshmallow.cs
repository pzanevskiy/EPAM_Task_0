using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Interfaces;

namespace Task1.Models
{
    public class Marshmallow : Sweet, ISugarable
    {
        private string _flavor;
        private double _sugar;

        public string Flavor
        {
            get => _flavor;
            set => _flavor = value == "" ? "unknown flavor" : value;            
        }
        public double Sugar 
        {
            get => _sugar;
            set => _sugar = value < 0 ? 0 : value > 100 ? 100 : value;
        }

        public Marshmallow()
        {
                
        }
        public Marshmallow(string name, double weight, string flavor) : base(name, weight)
        {
            Flavor = flavor;
        }

        public override void Print()
        {
            Console.WriteLine($"Marshmallow Name: {Name}, Weight: {Weight}g, Flavor: {Flavor}, Sugar: {Sugar}g");

        }
    }
}
