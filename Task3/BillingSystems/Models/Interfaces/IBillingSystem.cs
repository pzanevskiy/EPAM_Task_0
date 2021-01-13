using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.Enums;

namespace Task3.BillingSystems.Models.Interfaces
{
    public interface IBillingSystem
    {
        public IList<CallInfo> Calls { get; set; }
        public IList<IUser> Users { get; set; }

        public void RegisterUser(IUser user);
        public void GetUserCallsPerMonth(IUser user);
        public void GetUserCallsByCallStatePerMonth(IUser user, CallState callState);
        public void GetUserCallsByDate(IUser user, int days);     
        public void GetUserCallsByDuration(IUser user, int minutes, int seconds); 
        public void GetUserCallsByCost(IUser user, double cost);        
        public void GetUserCallsByUser(IUser user, IPhoneNumber number);       
    }
}
