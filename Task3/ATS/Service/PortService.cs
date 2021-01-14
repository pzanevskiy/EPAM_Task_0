using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models;
using Task3.ATS.Models.Interfaces;
using Task3.ATS.Service.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Service
{
    public class PortService : IPortService
    {
        private ICollection<IPort> _ports;

        public PortService()
        {
            _ports = new List<IPort>();
        }

        public void AddPort(IPort port)
        {            
            ChangeState(port,PortState.Free);
            _ports.Add(port);
        }

        public IPort GetFreePort()
        {
            return _ports.Where(x => x.State == PortState.Free).FirstOrDefault();
        }
        
        //public IPort GetPortByPhoneNumber(IPhoneNumber phoneNumber)
        //{
        //    return _ports.FirstOrDefault(x => x.Terminal.Number.Equals(phoneNumber));
        //}

        public void ChangeState(IPort port,PortState portState)
        {
            port.State = portState;
        }

        public void CreatePort()
        {
            AddPort(new Port());
        }
    }
}
