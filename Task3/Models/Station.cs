using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.Models.Controllers;
using Task3.Models.Enums;

namespace Task3.Models
{
    public class Station
    {
        //private ICollection<Port> _portsCollection;
        //private ICollection<Terminal> _terminalsCollection;

        private PortController _portController;
        private TerminalController _terminalController;
        private CallController _callController;

        public event EventHandler<CallInfo> Call;

        public Station()
        {
            _portController = new PortController();
            _terminalController = new TerminalController();
            _callController = new CallController();
        }

        public Station(ICollection<Port> ports, ICollection<Terminal> terminals) : this()
        {                       
            foreach(var item in ports)
            {
                _portController.AddPort(item);
            }
        }
        
        protected virtual void OnCall(object sender, CallInfo call)
        {
            Call?.Invoke(sender, call);
        }

        public Port GetFreePort()
        {
            return _portController.GetFreePort();
        }

        public void AddPort(Port port)
        {
            _portController.AddPort(port);
        }

        public void AddTerminal(Terminal terminal)
        {
            RegisterEventHandlersForTerminal(terminal);
            _terminalController.AddTerminal(terminal);
        }

        public ICollection<Terminal> GetTerminals()
        {
            return _terminalController.Terminals;
        }
        
        private void RegisterEventHandlersForTerminal(Terminal terminal)
        {
            terminal.OutgoingCall += OnOutgoingCall;
            terminal.IncomingCall += OnIncomingCall;
            terminal.Accept += OnAccept;
            terminal.End += OnEnd;
            terminal.Reject += OnReject;
        }

        private void OnOutgoingCall(object sender,PhoneNumber phone)
        {
            var answerer = _terminalController.FindTerminalByNumber(phone);
            var caller = sender as Terminal;

            if (answerer != null && caller != null)
            {
                if (answerer.Port.State != PortState.Busy)
                {
                    caller.Port.ChangeState(PortState.Busy);
                    caller.RememberConnection(caller.Number, phone);
                    CallInfo info = new CallInfo
                    {
                        From = caller.Number,
                        To = phone,
                        DateTimeStart = DateTime.Now,
                    };
                    _callController.AddCall(info);
                    answerer.GetCall(caller.Number);
                }
                else
                {
                    caller.Port.ChangeState(PortState.Busy);
                    caller.RememberConnection(caller.Number, phone);
                    CallInfo info = new CallInfo
                    {
                        From = caller.Number,
                        To = phone,
                        DateTimeStart = DateTime.Now,
                    };
                    _callController.AddCall(info);
                    Console.WriteLine($"Terminal is busy");
                    caller.RejectCall();
                }

            }
            else
            {
                Console.WriteLine($"Phone not binded to terminal");
                caller.RejectCall();
            }
        }

        private void OnIncomingCall(object sender,PhoneNumber phone)
        {
            var answerer = sender as Terminal;
            answerer.Port.ChangeState(PortState.Busy);
            answerer.RememberConnection(phone, answerer.Number);
        }

        private void OnAccept(object sender, EventArgs e)
        {
            var caller = _terminalController.FindTerminalByNumber((sender as Terminal).Connection.From);
            var info = _callController.GetCallInfo(caller.Connection);
            info.DateTimeStart = DateTime.Now;
        }

        private void OnEnd(object sender, EventArgs e)
        {
            var caller = _terminalController.FindTerminalByNumber((sender as Terminal).Connection.From);
            var info = _callController.GetCallInfo(caller.Connection);
            info.Duration = DateTime.Now - info.DateTimeStart;
            info.Terminal = caller;
            info.CallState = CallState.Outgoing;
            OnCall(this, info);
            var answerer = _terminalController.FindTerminalByNumber(caller.Connection.To);
            CallInfo info1 = info.Copy();
            info1.Terminal = answerer;
            info1.CallState = CallState.Incoming;
            OnCall(this, info1);
            _callController.RemoveCall(info);
            caller.Port.ChangeState(PortState.ConnectedTerminal);
            answerer.Port.ChangeState(PortState.ConnectedTerminal);
        }

        private void OnReject(object sender, EventArgs e)
        {
            if ((sender as Terminal).Connection != null)
            {
                var caller = _terminalController.FindTerminalByNumber((sender as Terminal).Connection.From);
                var info = _callController.GetCallInfo(caller.Connection);
                var answerer = _terminalController.FindTerminalByNumber(caller.Connection.To);
                info.Duration = TimeSpan.Zero;

                if (caller.Equals(sender))
                {
                    info.CallState = CallState.NoAnswer;
                    info.Terminal = caller;
                    OnCall(this, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = CallState.Missed;
                    info1.Terminal = answerer;
                    OnCall(this, info1);
                }
                else
                {
                    info.CallState = CallState.Rejected;
                    info.Terminal = answerer;
                    OnCall(this, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = CallState.NoAnswer;
                    info1.Terminal = caller;
                    OnCall(this, info1);
                }

                _callController.RemoveCall(info);
                caller.Port.ChangeState(PortState.ConnectedTerminal);
                answerer.Port.ChangeState(PortState.ConnectedTerminal);
            }
        }
    }
}
