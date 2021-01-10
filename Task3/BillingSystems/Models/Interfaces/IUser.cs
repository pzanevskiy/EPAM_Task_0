using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;

namespace Task3.BillingSystems.Models.Interfaces
{
    public interface IUser
    {
        public string Name { get; set; }
        public ITerminal Terminal { get; set; }
        public double Money { get; set; }
        public Tariff Tariff { get; set; }
    }
}
