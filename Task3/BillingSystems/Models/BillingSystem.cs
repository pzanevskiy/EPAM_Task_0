using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;

namespace Task3.BillingSystems.Models
{
    public class BillingSystem : IBillingSystem
    {
        public Dictionary<IPhoneNumber,bool> PhoneNumbers { get; private set; }
        public List<CallInfo> Calls { get; set; }
        public List<IUser> Users { get; set; }

        public BillingSystem(Station s,List<IPhoneNumber> phones)
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
            station.Call += (sender, callInfo) =>
            {
                callInfo.User = GetUserByTerminal(sender as Terminal);
                if (callInfo.CallState == Enums.CallState.Outgoing)
                {
                    callInfo.Cost = callInfo.User.Tariff * callInfo.Duration.Seconds;
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
            user.Tariff = Tariff.tariff;
            Users.Add(user);
        }

        public void GetUserInfo(IUser user)
        {
            var userCalls = Calls.Select(x => x).Where(x => x.User.Equals(user));
            var userCallGroups = userCalls.GroupBy(x => x.CallState);
            foreach(var item in userCallGroups)
            {
                Console.WriteLine($"{item.Key}\n");
                foreach (var x in item.OrderBy(x=>x.Duration))
                {
                    Console.WriteLine($"{x}");
                }
                Console.WriteLine();
            }
        }
    }
}
