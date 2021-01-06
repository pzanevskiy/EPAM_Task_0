using System;
using System.Collections.Generic;
using System.Linq;
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
            PhoneNumber p1 = new PhoneNumber("+375291234567");
            PhoneNumber p2 = new PhoneNumber("+375298883230");
            Terminal t1 = new Terminal(p1);
            Terminal t2 = new Terminal(p2);
            station.AddTerminal(t1);
            station.AddTerminal(t2);
            t1.ConnectToPort(port);
            t2.ConnectToPort(station.GetFreePort());
            t1.Call(p2);
            t2.AcceptCall();
            t1.RejectCall();
           // port.ChangeState();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
