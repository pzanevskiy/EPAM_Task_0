using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public class Candy : Sweet
    {
        private double _calories;
        public double Calories
        {
            get => _calories;
            set => _calories = value < 0 ? 0 : value;
            
        }
        public Candy()
        {

        }

        public Candy(string name, double weight, double sugar, double calories) : base(name, weight, sugar)
        {
            Calories = calories;
        }

        public override void Print()
        {
            Console.WriteLine($"Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, Calories: {Calories}");
        }
    }
}
