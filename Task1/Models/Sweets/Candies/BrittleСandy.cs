using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models.Sweets.Candies
{
    public class BrittleСandy : Candy
    {
        private int _numberOfNuts;
        public int NumberOfNuts
        {
            get => _numberOfNuts;
            set => _numberOfNuts = value < 0 ? 0 : value > 10 ? 10 : value;
        }
        public BrittleСandy()
        {

        }
        public BrittleСandy(string name, double weight, double sugar,int numOfNuts):base(name, weight, sugar)
        {
            NumberOfNuts = numOfNuts;
        }

        public override void Print()
        {
            Console.WriteLine($"BrittleCandy Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, NumberOfNuts: {NumberOfNuts}");
        }
    }
}
