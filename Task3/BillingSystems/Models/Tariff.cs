using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.BillingSystems.Models
{
    public class Tariff
    {
        private double _cost;

        public double CostPerSecond
        {
            get => _cost;
            set => _cost = value < 0 ? 0 : value;
        }

        public Tariff(double cost)
        {
            CostPerSecond = cost;
        }
    }
}
