using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;
using Task3.Enums;

namespace Task3.BillingSystems.Service.Interfaces
{
    public interface ICallService
    {
        public void AddCall(CallInfo info);
        public void SetAdditionalInfo(IUser user, CallInfo callInfo);
        public void GetUserCallsPerMonth(IUser user);
        public void GetUserCallsByCallStatePerMonth(IUser user, CallState callState);
        public void GetUserCallsByDate(IUser user, int days);
        public void GetUserCallsByDuration(IUser user, int minutes, int seconds);
        public void GetUserCallsByCost(IUser user, double cost);
        public void GetUserCallsByUser(IUser user, IPhoneNumber number);

    }
}
