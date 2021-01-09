using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task3.ATS.Models.Interfaces;
using Task3.Models;
using Task3.Models.BillingSystem;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {           
            IPort port = new Port();
            Station station = new Station(new List<IPort>() { new Port(), new Port() });
            station.AddPort(port);
            BillingSystem system = new BillingSystem(station);
            IPhoneNumber p1 = new PhoneNumber("11111111");
            IPhoneNumber p2 = new PhoneNumber("22222222");
            IPhoneNumber p3 = new PhoneNumber("33333333");
            ITerminal t1 = new Terminal(p1);
            ITerminal t2 = new Terminal(p2);
            ITerminal t3 = new Terminal(p3);
            //station.AddTerminal(t1);
            //station.AddTerminal(t2);
            //station.AddTerminal(t3);
            t1.ConnectToPort(port);
            //t1.ConnectToPort(station.GetFreePort());
            t2.ConnectToPort(station.GetFreePort());
            t3.ConnectToPort(station.GetFreePort());

            t1.Call(p2);
            t2.AcceptCall();
            Thread.Sleep(2000);
            t2.EndCall();
            Console.WriteLine();
            t2.Call(p1);
            t3.Call(p1);
            t2.RejectCall();
            Console.WriteLine();
            t2.Call(p1);
            t1.RejectCall();
            Console.WriteLine();

            foreach(var item in new List<ITerminal>() { t1,t2,t3})
            {
                Console.WriteLine($"{item.Number} history");
                var callsGroups = system.Calls.Where(x => x.Terminal.Equals(item)).GroupBy(x => x.CallState);
                foreach (var item1 in callsGroups)
                {
                    Console.WriteLine($"{item1.Key}\n");
                    foreach (var x in item1)
                    {
                        Console.WriteLine($"\t{x}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to continue...\n\n\n");
            Console.ReadLine();
        }
    }
}
