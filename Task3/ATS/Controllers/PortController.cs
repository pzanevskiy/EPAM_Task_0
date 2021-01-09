using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.Models.Enums;

namespace Task3.Models.Controllers
{
    public class PortController
    {
        private ICollection<IPort> _ports;

        public PortController()
        {
            _ports = new List<IPort>();
        }

        public void AddPort(IPort port)
        {
            port.State = PortState.Free;
            _ports.Add(port);
        }

        public IPort GetFreePort()
        {
            return _ports.Where(x => x.State == PortState.Free).FirstOrDefault();
        }
        
        public IPort GetPortByPhoneNumber(IPhoneNumber phoneNumber)
        {
            return _ports.FirstOrDefault(x => x.Terminal.Number.Equals(phoneNumber));
        }

        public void CreatePort()
        {
            AddPort(new Port());
        }
    }
}
