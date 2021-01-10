using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models;

namespace Task3.ATS.Controllers
{
    public class CallController
    {
        private ICollection<CallInfo> _calls;

        public CallController()
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
    }
}
