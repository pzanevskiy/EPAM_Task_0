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
        private Dictionary<IPhoneNumber,bool> PhoneNumbers { get; set; }
        public List<CallInfo> Calls { get; set; }
        public List<IUser> Users { get; set; }

        public BillingSystem(Station s, List<IPhoneNumber> phones)
        {
            Calls = new List<CallInfo>();
            Users = new List<IUser>();
            PhoneNumbers = new Dictionary<IPhoneNumber,bool>();
            foreach(var item in phones)
            {
                PhoneNumbers.Add(item,true);
            }
            RegisterHandlerForStation(s);
        }



        private void RegisterHandlerForStation(Station station)
        {
            station.CallService.Call += (sender, callInfo) =>
            {
                callInfo.User = GetUserByTerminal(sender as Terminal);
                if (callInfo.CallState == CallState.Outgoing)
                {
                    callInfo.Cost = callInfo.User.Tariff.CostPerSecond * callInfo.Duration.Seconds;
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
            user.Tariff = new Tariff();
            Users.Add(user);
        }

        public void GetUserInfo(IUser user)
        {
            //Calls.Add(new CallInfo() { User = user, From = user.Terminal.Number, DateTimeStart = DateTime.Now.AddDays(-29) });
            //Calls.Add(new CallInfo() { User = user, From = user.Terminal.Number, DateTimeStart = DateTime.Now.AddDays(-30) });
            //Calls.Add(new CallInfo() { User = user, From = user.Terminal.Number, DateTimeStart = DateTime.Now.AddDays(-31) });
            //Calls.Add(new CallInfo() { User = user, From = user.Terminal.Number, DateTimeStart = DateTime.Now.AddDays(-32) });
            var userCalls = Calls
                .Where(x => x.User.Equals(user) && x.DateTimeStart.Date>=DateTime.Now.AddMonths(-1).Date)
                .GroupBy(x => x.CallState);
            foreach(var item in userCalls)
            {
                Console.WriteLine($"{item.Key}\n");
                foreach (var x in item)
                {
                    Console.WriteLine($"{x}\n");
                }
                Console.WriteLine();
            }
        }
    }
}
