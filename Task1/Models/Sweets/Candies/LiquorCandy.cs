using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models.Sweets.Candies
{
    public class LiquorCandy : Candy
    {
        private int _percentageOfAlcohol;
        public int PercentageOfAlcohol
        {
            get => _percentageOfAlcohol;
            set => _percentageOfAlcohol = value < 0 ? 0 : value >= 100 ? 99 : value;
        }
        public LiquorCandy()
        {

        }
        public LiquorCandy(string name, double weight, double sugar, int percentageOfAlco):base(name, weight, sugar)
        {
            PercentageOfAlcohol = percentageOfAlco;
        }

        public override void Print()
        {
            Console.WriteLine($"LiqourCandy Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, Alcohol: {PercentageOfAlcohol}%");

        }
    }
}
