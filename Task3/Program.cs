using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task3.Models;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Port port = new Port(3);
            Station station = new Station(new List<Port>() { new Port(1), new Port(2) },new List<Terminal>());
            station.AddPort(port);
            BillingSystem system = new BillingSystem(station);
            PhoneNumber p1 = new PhoneNumber("+375291234567");
            PhoneNumber p2 = new PhoneNumber("+375298883230");
            PhoneNumber p3 = new PhoneNumber("111");
            Terminal t1 = new Terminal(p1);
            Terminal t2 = new Terminal(p2);
            station.AddTerminal(t1);
            station.AddTerminal(t2);
            t1.ConnectToPort(port);
            t2.ConnectToPort(port);
            t2.ConnectToPort(station.GetFreePort());
            t2.DisconnectFromPort();
            t2.DisconnectFromPort();
            t2.ConnectToPort(station.GetFreePort());
            t1.Call(p2);
            //t2.AcceptCall();
            Thread.Sleep(2000);
            t2.EndCall();
            t2.Call(p1);
            t2.RejectCall();
            t1.Call(p2);
            t2.RejectCall();
           // port.ChangeState();
            Console.WriteLine("Press any key to continue...\n\n\n");
            Console.WriteLine($"{t1.Number} history");
            var callsGroups = system.Calls.Where(x => x.Terminal.Equals(t1)).GroupBy(x => x.CallState);
            foreach (var item in callsGroups)
            {
                Console.WriteLine($"{item.Key}\n");
                foreach (var x in item)
                {
                    Console.WriteLine($"\t{x}");
                }
            }
            Console.WriteLine($"\n\n{t2.Number} history");
            var callsGroups1 =system.Calls.Where(x=>x.Terminal.Equals(t2)).GroupBy(x => x.CallState);
            foreach(var item in callsGroups1)
            {
                Console.WriteLine($"{item.Key}\n");
                foreach(var x in item)
                {
                    Console.WriteLine($"\t{x}");
                }
            }
            Console.ReadLine();
        }
    }
}
