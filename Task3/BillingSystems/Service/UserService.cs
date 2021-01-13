using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;
using Task3.BillingSystems.Service.Interfaces;

namespace Task3.BillingSystems.Service
{
    public class UserService : IUserService
    {
        public void ConnectToPort(IUser user, IPort port)
        {
            user.Terminal.ConnectToPort(port);
        }

        public void DisconnectFromPort(IUser user)
        {
            user.Terminal.DisconnectFromPort();
        }

        public void Call(IUser user, IPhoneNumber phoneNumber)
        {
            user.Terminal.Call(phoneNumber);
        }

        public void Reject(IUser user)
        {
            user.Terminal.RejectCall();
        }

        public void Answer(IUser user)
        {
            user.Terminal.AcceptCall();
        }

        public void EndCall(IUser user)
        {
            user.Terminal.EndCall();
        }
       
    }
}
