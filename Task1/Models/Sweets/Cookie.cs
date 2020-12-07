using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models.Enums;

namespace Task1.Models
{
    public class Cookie : Sweet
    {
        private CookieType _cookieType;

        public CookieType CookieType { get => _cookieType; set => _cookieType = value; }

        public Cookie() { }
        public Cookie(string name, double weight, double sugar, CookieType cookieType) : base(name, weight, sugar)
        {
            CookieType = cookieType;
        }

        public override void Print()
        {
            Console.WriteLine($"Name: {Name}, Weight: {Weight}g, Sugar: {Sugar}g, Type: {CookieType}");

        }
    }
}
