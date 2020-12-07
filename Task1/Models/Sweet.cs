using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public abstract class Sweet : IPrintable
    {
        private string _name;
        private double _weight;
        private double _sugar;

        public string Name
        {
            get => _name;
            set => _name = value == "" ? "New sweet" : value;         
        }
        public double Weight
        {
            get => _weight;
            set => _weight = value < 0 ? 0 : value;            
        }
        public double Sugar
        {
            get => _sugar;
            set => _sugar = value < 0 ? 0 : value;
        }

        public Sweet()
        {

        }

        public Sweet(string name, double weight, double sugar)
        {
            Name = name;
            Weight = weight;
            Sugar = sugar;
        }

        public abstract void Print();       
    }
}
