using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.Models.BillingSystem
{
    public class User
    {
        public string Name { get; set; }
        public Terminal Terminal { get; set; }
        public double Money { get; set; }

        public User()
        {

        }

        public User(string name, Terminal terminal, double money)
        {
            Name = name;
            Terminal = terminal;
            Money = money;
        }
    }
}
