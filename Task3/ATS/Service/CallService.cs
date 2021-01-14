using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Service.Interfaces;

namespace Task3.ATS.Controllers
{
    public class CallService : ICallService
    {
        private ICollection<CallInfo> _calls;

        public event EventHandler<CallInfo> Call;

        public CallService()
        {
            _calls = new List<CallInfo>();
        }

        public void AddCall(CallInfo call)
        {
            _calls.Add(call);
        }

        public void RemoveCall(CallInfo call)
        {
            _calls.Remove(call);
        }

        public CallInfo GetCallInfo(Connection connection)
        {
            return _calls.FirstOrDefault(x => x.From.Equals(connection.From) && x.To.Equals(connection.To));
        }

        public CallInfo Copy(CallInfo callInfo)
        {
            return new CallInfo
            {
                From = callInfo.From,
                To = callInfo.To,
                DateTimeStart = callInfo.DateTimeStart,
                Duration = callInfo.Duration
            };
        }

        public void OnCall(object sender, CallInfo call)
        {
            Call?.Invoke(sender, call);
        }

    }
}
