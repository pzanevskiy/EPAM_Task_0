using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Controllers;

namespace Task3.ATS.Models.Interfaces
{
    public interface IStation
    {
        public CallService CallService { get; }

        public IPort GetFreePort();

        public void AddPort(IPort port);
        public IPort GetPortByPhoneNumber(IPhoneNumber phone);       
    }
}
