using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public class Marshmallow : Sweet
    {
        private string _flavor;

        public string Flavor
        {
            get => _flavor;
            set => _flavor = value == "" ? "unknown flavor" : value;            
        }
        public Marshmallow()
        {
                
        }
        public Marshmallow(string name, double weight, double sugar, string flavor) : base(name, weight, sugar)
        {
            Flavor = flavor;
        }

        public override void Print()
        {
            Console.WriteLine($"Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, Flavor: {Flavor}");

        }
    }
}
