using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models;
using Task3.BillingSystems.Models.Interfaces;
using Task3.BillingSystems.Service;
using Task3.BillingSystems.Service.Interfaces;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            IPort port = new Port();

            ITerminal t1 = new Terminal();
            ITerminal t2 = new Terminal();
            ITerminal t3 = new Terminal();

            IPhoneNumber p1 = new PhoneNumber("11111111");
            IPhoneNumber p2 = new PhoneNumber("22222222");
            IPhoneNumber p3 = new PhoneNumber("33333333");

            IStation station = new Station(new List<IPort>() { new Port(), new Port() }, new List<ITerminal>() { t1, t2, t3 });
            station.AddPort(port);

            IBillingSystem system = new BillingSystem(station, new List<IPhoneNumber>() { p1, p2, p3 });

            IUser user1 = new User("Ivan", 100);
            IUser user2 = new User("Petya", 100);
            IUser user3 = new User("Dima", 100);
            
            system.RegisterUser(user1);
            system.RegisterUser(user2);
            system.RegisterUser(user3);
            IUserService userService = new UserService();
            userService.ConnectToPort(user1, system.GetFreePort());
            userService.ConnectToPort(user2, system.GetFreePort());
            userService.ConnectToPort(user3, system.GetFreePort());

            userService.Call(user1, p2);
            userService.Answer(user2);
            Thread.Sleep(2000);
            userService.EndCall(user2);
            Console.WriteLine();

            userService.Call(user1, p2);
            userService.Answer(user2);
            Thread.Sleep(3000);
            userService.EndCall(user1);
            Console.WriteLine();

            userService.Call(user1, p2);
            userService.Answer(user2);
            Thread.Sleep(1000);
            userService.EndCall(user2);
            Console.WriteLine();

            userService.Call(user2, p1);
            userService.Call(user3, p1);
            userService.Reject(user1);
            Console.WriteLine();

            userService.Call(user2, p1);
            userService.Reject(user2);
            Console.WriteLine();

            userService.Call(user2, p1);
            userService.Answer(user1);
            Thread.Sleep(1000);
            userService.EndCall(user1);
            Console.WriteLine(); 
            
            userService.Call(user2, p3);
            userService.Answer(user3);
            Thread.Sleep(1000);
            userService.EndCall(user2);
            Console.WriteLine();

            foreach (var item in system.Users)
            {
                Console.WriteLine($"{item.Name} history");
                system.CallService.GetUserCallsByCallStatePerMonth(item, Enums.CallState.NoAnswer);
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to continue...\n\n\n");
            Console.ReadLine();
        }
    }
}
