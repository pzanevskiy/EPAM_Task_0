using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.Models.Enums;

namespace Task3.Models.Controllers
{
    public class PortController
    {
        private ICollection<Port> _ports;

        public PortController()
        {
            _ports = new List<Port>();
        }

        public void AddPort(Port port)
        {
            port.State = PortState.Free;
            _ports.Add(port);
        }

        public Port GetFreePort()
        {
            return _ports.Where(x => x.State == PortState.Free).FirstOrDefault();
        }
        
        public Port GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _ports.FirstOrDefault(x => x.Terminal.Number.Equals(phoneNumber));
        }

        public void CreatePort()
        {
            AddPort(new Port());
        }
    }
}
