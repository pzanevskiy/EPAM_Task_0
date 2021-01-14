using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Service.Interfaces;
using Task3.Enums;

namespace Task3.BillingSystems.Models.Interfaces
{
    public interface IBillingSystem
    {
        public IList<IUser> Users { get; }
        public ICallService CallService { get; }

        public void RegisterUser(IUser user);
    }
}
