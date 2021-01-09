using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models;

namespace Task3.ATS.Models.Interfaces
{
    public interface IStation
    {
        public event EventHandler<CallInfo> Call;

        void OnCall(object sender, CallInfo call);
        public IPort GetFreePort();

        public void AddPort(IPort port);
        public IPort GetPortByPhoneNumber(IPhoneNumber phone);
        public void AddCall(CallInfo info);
        public void RemoveCall(CallInfo info);
        public CallInfo GetCallInfo(Connection connection);
    }
}
