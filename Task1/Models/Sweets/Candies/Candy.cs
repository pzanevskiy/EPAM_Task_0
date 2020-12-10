using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Interfaces;

namespace Task1.Models
{
    public abstract class Candy : Sweet, ISugarable
    {
        private double _sugar;
        public double Sugar
        {
            get => _sugar;
            set => _sugar = value < 0 ? 0 : value > 100 ? 100 : value;            
        }       

        public Candy()
        {

        }

        public Candy(string name, double weight, double sugar) : base(name, weight)
        {
            Sugar = sugar;
        }

        public abstract override void Print();
        
    }
}
