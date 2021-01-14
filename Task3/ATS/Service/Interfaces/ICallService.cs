using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;

namespace Task3.ATS.Service.Interfaces
{
    public interface ICallService
    {
        public event EventHandler<CallInfo> Call;

        public void AddCall(CallInfo call);
        public void RemoveCall(CallInfo call);
        public CallInfo GetCallInfo(Connection connection);
        public CallInfo Copy(CallInfo callInfo);
        public void SaveCall(object sender, CallInfo call);
    }
}
