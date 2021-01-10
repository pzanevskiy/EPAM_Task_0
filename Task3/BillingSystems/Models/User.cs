using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;

namespace Task3.BillingSystems.Models
{
    public class User : IUser
    {
        public string Name { get; set; }
        public ITerminal Terminal { get; set; }
        public double Money { get; set; }
        public double Tariff { get; set; }

        public User()
        {

        }

        public User(string name, ITerminal terminal, double money)
        {
            Name = name;
            Terminal = terminal;
            Money = money;
        }

        public override string ToString()
        {
            return $"{Name} has {Terminal}, money: {Money}";
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Name == user.Name &&
                   EqualityComparer<ITerminal>.Default.Equals(Terminal, user.Terminal) &&
                   Money == user.Money;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Terminal, Money);
        }

    }
}
