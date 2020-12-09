using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public abstract class Cookie : Sweet
    {
        private string _cookieType;

        public string CookieType { get => _cookieType; set => _cookieType = value.Equals("") ? "butter" : value; }

        public Cookie() { }
        public Cookie(string name, double weight, string cookieType) : base(name, weight)
        {
            CookieType = cookieType;
        }

        public abstract override void Print();
    }
}
