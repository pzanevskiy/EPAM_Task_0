using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;
using Task3.BillingSystems.Service;
using Task3.BillingSystems.Service.Interfaces;
using Task3.Enums;

namespace Task3.BillingSystems.Models
{
    public class BillingSystem : IBillingSystem
    {
        private IDictionary<IPhoneNumber,bool> PhoneNumbers { get; set; }
        public IList<IUser> Users { get; set; }
        public Tariff Tariff { get; private set; }

        public ICallService CallService { get; private set; }

        public BillingSystem(IStation s, List<IPhoneNumber> phones)
        {
            CallService = new CallService();
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
                CallService.AddCall(callInfo);
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
    }
}
