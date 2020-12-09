using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models.Sweets.Cookies
{
    public class OatCookie : Cookie
    {
        public OatCookie()
        {

        }
        public OatCookie(string name, double weight, string cookieType) : base(name, weight, cookieType)
        {

        }

        public override void Print()
        {
            Console.WriteLine($"Oat Cookie Name: {Name}, Weight: {Weight}g, CoockieType: {CookieType}");
        }
    }
}
