using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3.Models
{
    public class Station
    {
        //private ICollection<Port> _portsCollection;
        //private ICollection<Terminal> _terminalsCollection;

        public ICollection<Port> Ports { get; set; }
        public ICollection<Terminal> Terminals { get; set; }

        public Station(ICollection<Port> ports, ICollection<Terminal> terminals)
        {
            Ports = ports;
            Terminals = terminals;
            foreach(var item in Ports)
            {
                item.State = Enums.PortState.Free;
            }
        }
        
        public Port GetFreePort()
        {
            return Ports.Where(x => x.State == Enums.PortState.Free).FirstOrDefault();
        }

        public void AddPort(Port port)
        {          
            port.State = Enums.PortState.Free;            
            Ports.Add(port);
        }

        public void AddTerminal(Terminal terminal)
        {
            RegisterEventHandlerForTerminal(terminal);      
            Terminals.Add(terminal);
        }
        public virtual void RegisterEventHandlerForTerminal(Terminal terminal)
        {
            terminal.OutgoingCall += (sender, phone) =>
            {
                var caller = sender as Terminal;
                Console.WriteLine($"{caller.Number} calls to {phone.Number}");
            };
            terminal.OutgoingCall+= (sender, phone) =>
            {
                
                var answerer = Terminals.Where(x => x.Number == phone).FirstOrDefault();
                var caller = sender as Terminal;
                
                    answerer.GetCall(caller.Number);
            
            };
            terminal.IncomingCall += (sender, phone) => {
                var answerer = sender as Terminal;
                Console.WriteLine($"{answerer.Number} is calling {phone.Number}");
            };
            terminal.Accept += (sender, e) =>
            {
                Console.WriteLine("Call is started");
            };
            terminal.Reject += (sender, e) =>
            {
                Console.WriteLine("Call is ended");
                terminal.Port.ChangeState(Enums.PortState.Free);
            };
        }
        private void OnOutgoingCall(object sender, PhoneNumber to)
        {
            
        }
    }
}
