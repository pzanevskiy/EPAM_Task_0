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
        public ICollection<CallInfo> Calls { get; set; }

        public event EventHandler<CallInfo> Call;

        public Station(ICollection<Port> ports, ICollection<Terminal> terminals)
        {
            Ports = ports;
            Terminals = terminals;
            Calls = new List<CallInfo>();
            foreach(var item in Ports)
            {
                item.State = Enums.PortState.Free;
            }
        }
        
        protected virtual void OnCall(object sender, CallInfo call)
        {
            Call?.Invoke(sender, call);
        }

        public Terminal FindTerminalByNumber(PhoneNumber number)
        {
            return Terminals.FirstOrDefault(x => x.Number == number);
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
            terminal.OutgoingCall+= (sender, phone) =>
            {                
                var answerer = FindTerminalByNumber(phone);
                var caller = sender as Terminal;
                
                if (answerer != null && caller!=null)
                {
                   // answerer.Port.ChangeState(Enums.PortState.Busy);
                    caller.Port.ChangeState(Enums.PortState.Busy);
                   // answerer.RememberConnection(phone, caller.Number);
                    caller.RememberConnection(caller.Number, phone);
                    CallInfo info = new CallInfo
                    {
                        From = caller.Number,
                        To = phone,
                        DateTimeStart = DateTime.Now,
                    };
                    AddCall(info);
                    //add callinfo to billing system
                    answerer.GetCall(caller.Number);                    
                }
                else
                {
                    Console.WriteLine($"{new Exception("Phone not binded to terminal")}");
                    caller.RejectCall();
                }

            };
            terminal.IncomingCall += (sender, phone) =>
            {
                var answerer = sender as Terminal;
                //var info=
                answerer.Port.ChangeState(Enums.PortState.Busy);
                answerer.RememberConnection(phone,answerer.Number);
            };
            terminal.Accept += (sender, e) =>
            {

            };
            terminal.End+=(sender, e) =>
            {

                var caller = FindTerminalByNumber((sender as Terminal).Connection.From);
                var info = GetCallInfo(caller.Connection);
                info.Duration = DateTime.Now - info.DateTimeStart;
                info.Terminal = caller;
                info.CallState = Enums.CallState.Outgoing;
                OnCall(this, info);
                var answerer = FindTerminalByNumber(caller.Connection.To);
                CallInfo info1 = info.Copy();
                info1.Terminal = answerer;
                info1.CallState = Enums.CallState.Incoming;               
                OnCall(this, info1);
                RemoveCall(info);
                // Console.WriteLine("{0:hh\\:mm\\:ss}",info.Duration);
                caller.Port.ChangeState(Enums.PortState.ConnectedTerminal);
                answerer.Port.ChangeState(Enums.PortState.ConnectedTerminal);

            };
            terminal.Reject += (sender, e) =>
            {
                var caller = FindTerminalByNumber((sender as Terminal).Connection.From);
                var info = GetCallInfo(caller.Connection);
                var answerer = FindTerminalByNumber(caller.Connection.To);
                info.Duration = TimeSpan.Zero;

                if (caller.Equals(sender))
                {
                    info.CallState = Enums.CallState.NoAnswer;
                    info.Terminal = caller;
                    OnCall(this, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = Enums.CallState.Missed;
                    info1.Terminal = answerer;
                    OnCall(this, info1);
                }
                else
                {
                    info.CallState = Enums.CallState.Rejected;
                    info.Terminal =answerer;
                    OnCall(this, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = Enums.CallState.Missed;
                    info1.Terminal = caller; 
                    OnCall(this, info1);
                }

                RemoveCall(info);
                // Console.WriteLine("{0:hh\\:mm\\:ss}",info.Duration);
                caller.Port.ChangeState(Enums.PortState.ConnectedTerminal);
                answerer.Port.ChangeState(Enums.PortState.ConnectedTerminal);

            };
            Terminals.Add(terminal);
        }
        
        private void AddCall(CallInfo call)
        {
            Calls.Add(call);
        }
       
        private void RemoveCall(CallInfo call)
        {
            Calls.Remove(call);
        }
        private CallInfo GetCallInfo(Connection connection)
        {
            return Calls.FirstOrDefault(x => x.From.Equals(connection.From) && x.To.Equals(connection.To));
        }
    }
}
