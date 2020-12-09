using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models.Sweets.Cookies
{
    public class CurdCookie : Cookie
    {
        private int _proportionOfFat;
        public int ProportionOfFat
        {
            get => _proportionOfFat;
            set => _proportionOfFat = value < 0 ? 0 : value > 100 ? 100 : value;
        }

        public CurdCookie()
        {

        }
        public CurdCookie(string name, double weight, string cookieType, int proportionOfFat) : base(name, weight, cookieType)
        {
            ProportionOfFat = proportionOfFat;
        }

        public override void Print()
        {
            Console.WriteLine($"CurdCookie Name: {Name}, Weight: {Weight}g, CoockieType: {CookieType}, Proportion of fat: {ProportionOfFat}%");
        }
    }
}
