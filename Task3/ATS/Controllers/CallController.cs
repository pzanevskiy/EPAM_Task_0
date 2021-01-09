using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3.Models.Controllers
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
