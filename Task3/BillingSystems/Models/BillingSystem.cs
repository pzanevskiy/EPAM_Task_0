using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;
using Task3.Enums;

namespace Task3.BillingSystems.Models
{
    public class BillingSystem : IBillingSystem
    {
        private IDictionary<IPhoneNumber,bool> PhoneNumbers { get; set; }
        public IList<CallInfo> Calls { get; set; }
        public IList<IUser> Users { get; set; }
        public Tariff Tariff { get; private set; }

        public BillingSystem(IStation s, List<IPhoneNumber> phones)
        {
            Calls = new List<CallInfo>();
            Users = new List<IUser>();
            PhoneNumbers = new Dictionary<IPhoneNumber,bool>();
            Tariff = new Tariff(0.2);
            foreach(var item in phones)
            {
                PhoneNumbers.Add(item,true);
            }
            RegisterHandlerForStation(s);
        }

        private void RegisterHandlerForStation(IStation station)
        {
            station.CallService.Call += (sender, callInfo) =>
            {
                callInfo.User = GetUserByTerminal(sender as Terminal);
                if (callInfo.CallState == CallState.Outgoing)
                {
                    callInfo.Cost = callInfo.User.Tariff.CostPerSecond * (callInfo.Duration.Minutes*60+callInfo.Duration.Seconds);
                    callInfo.User.Money -= callInfo.Cost;
                }
                else
                {
                    callInfo.Cost = 0;
                }
                Calls.Add(callInfo);
            };
        }

        private IUser GetUserByTerminal(ITerminal terminal)
        {
            return Users.FirstOrDefault(x => x.Terminal.Equals(terminal));
        }

        public void RegisterUser(IUser user)
        {
            var freeNumber = PhoneNumbers.FirstOrDefault(x => x.Value.Equals(true)).Key;
            user.Terminal.Number = freeNumber;
            PhoneNumbers[freeNumber] = false;
            user.Tariff = Tariff;
            Users.Add(user);
        }

        public void GetUserCallsPerMonth(IUser user)
        {
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date>=DateTime.Now.AddMonths(-1).Date)
                .GroupBy(x => x.CallState);
            foreach(var item in userCalls)
            {
                Console.WriteLine($"{item.Key}\n");
                foreach (var x in item.OrderBy(x => x.DateTimeStart))
                {
                    Console.WriteLine($"{x}\n");
                }
                Console.WriteLine();
            }
        }

        public void GetUserCallsByCallStatePerMonth(IUser user, CallState callState)
        {
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date && x.CallState.Equals(callState));
            if (userCalls.Count()==0)
            {
                Console.WriteLine($"No {callState} calls");
            }
            else
            {
                Console.WriteLine($"{callState} calls");
                foreach (var item in userCalls.OrderBy(x => x.DateTimeStart))
                {
                    Console.WriteLine($"{item}\n");
                }
            }
        }

        public void GetUserCallsByDate(IUser user,int days)
        {
            days = days <= 0 ? days = 7 : days > 30 ? days = 30 : days;
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date >= DateTime.Now.AddDays(-days).Date)
                .GroupBy(x => x.CallState);
            if (userCalls.Count() == 0)
            {
                Console.WriteLine($"No calls between {DateTime.Now.AddDays(-days):d} and {DateTime.Now:d}");
            }
            else
            {
                foreach (var item in userCalls)
                {
                    Console.WriteLine($"{item.Key}\n");
                    foreach (var x in item.OrderBy(x=>x.DateTimeStart))
                    {
                        Console.WriteLine($"{x}\n");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void GetUserCallsByDuration(IUser user, int minutes,int seconds)
        {
            minutes = minutes < 0 ?  0 : minutes >= 60 ?  59 : minutes;
            seconds = seconds < 0 ?  1 : seconds >= 60 ? 59 : seconds;
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date && x.Duration <= TimeSpan.ParseExact($"{minutes}:{seconds}", "m\\:s", null))
                .GroupBy(x => x.CallState);
            if (userCalls.Count() == 0)
            {
                Console.WriteLine($"No calls up to {TimeSpan.ParseExact($"{minutes}:{seconds}", "m\\:s", null):mm\\:ss}");
            }
            else
            {
                foreach (var item in userCalls)
                {
                    Console.WriteLine($"{item.Key}\n");
                    foreach (var x in item.OrderBy(x => x.Duration))
                    {
                        Console.WriteLine($"{x}\n");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void GetUserCallsByCost(IUser user, double cost)
        {
            cost = cost <= 0 ? 0.15 : cost;
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.CallState.Equals(CallState.Outgoing) && x.Cost <= cost);
            if (userCalls.Count() == 0)
            {
                Console.WriteLine($"No calls up to {cost}$");
            }
            else
            {
                Console.WriteLine($"Calls up to {cost}$");
                foreach (var item in userCalls.OrderBy(x => x.DateTimeStart))
                {
                    Console.WriteLine($"{item}\n");
                }
            }
        }

        public void GetUserCallsByUser(IUser user,IPhoneNumber number)
        {
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date && (x.From.Equals(number) || x.To.Equals(number)))
                .GroupBy(x => x.CallState);
            if (userCalls.Count() == 0)
            {
                Console.WriteLine($"No calls with this number {number}");
            }
            else
            {
                foreach (var item in userCalls)
                {
                    Console.WriteLine($"{item.Key}\n");
                    foreach (var x in item.OrderBy(x => x.DateTimeStart))
                    {
                        Console.WriteLine($"{x}\n");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
