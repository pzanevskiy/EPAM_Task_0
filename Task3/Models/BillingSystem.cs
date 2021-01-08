using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.Models
{
    //working in this
    public class BillingSystem
    {
        public List<CallInfo> Calls { get; set; }

        public BillingSystem(Station s)
        {
            Calls = new List<CallInfo>();
            RegisterHandlerForStation(s);
        }

        private void RegisterHandlerForStation(Station station)
        {
            station.Call += (sender, args) =>
            {
                Calls.Add(args);
            };
        }
    }
}
