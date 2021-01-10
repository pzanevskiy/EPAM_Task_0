using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;

namespace Task3.BillingSystems.Service.Interfaces
{
    public interface IUserService
    {
        public void ConnectToPort(IUser user, IPort port);
        public void DisconnectFromPort(IUser user);
        public void Call(IUser user, IPhoneNumber phoneNumber);
        public void Reject(IUser user);
        public void Answer(IUser user);
        public void EndCall(IUser user);
    }
}
