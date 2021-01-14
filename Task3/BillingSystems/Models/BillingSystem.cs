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
        private IStation Station { get; set; }
        public ICallService CallService { get; private set; }

        public BillingSystem(IStation s, List<IPhoneNumber> phones)
        {
            CallService = new CallService();
            Users = new List<IUser>();
            PhoneNumbers = new Dictionary<IPhoneNumber, bool>();
            Tariff = new Tariff(0.2);
            Station = s;
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
                var user = GetUserByTerminal(sender as Terminal);
                CallService.SetAdditionalInfo(user, callInfo);
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
            user.Terminal = Station.TerminalService.GetFreeTerminal();
            user.Terminal.Number = freeNumber;
            PhoneNumbers[freeNumber] = false;
            user.Tariff = Tariff;
            Users.Add(user);
        }

        public IPort GetFreePort()
        {
            return Station.GetFreePort();
        }
    }
}
