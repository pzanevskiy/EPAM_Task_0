using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Service.Interfaces;

namespace Task3.ATS.Models.Interfaces
{
    public interface IStation
    {
        public ICallService CallService { get; }
        public ITerminalService TerminalService { get; }
        public IPort GetFreePort();

        public void AddPort(IPort port);
        //public IPort GetPortByPhoneNumber(IPhoneNumber phone);       
    }
}
