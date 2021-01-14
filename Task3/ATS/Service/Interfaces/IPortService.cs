using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Service.Interfaces
{
    public interface IPortService
    {
        public void AddPort(IPort port);
        public IPort GetFreePort();
        public IPort GetPortByPhoneNumber(IPhoneNumber phoneNumber);
        public void ChangeState(IPort port, PortState portState);
    }
}
