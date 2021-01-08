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

        public void CreatePort(int id)
        {
            AddPort(new Port(id));
        }
    }
}
