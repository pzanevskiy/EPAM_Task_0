using System;
using System.Collections.Generic;
using System.Linq;
using Task3.ATS.Models.Interfaces;
using Task3.ATS.Service;
using Task3.ATS.Service.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Models
{
    public class Station : IStation
    {
        private IPortService _portService;
        private ICallService _callService;
        private ITerminalService _terminalService;

        public ICallService CallService => _callService;
        public ITerminalService TerminalService => _terminalService;

        public Station()
        {
            _portService = new PortService();
            _callService = new CallService();
            _terminalService = new TerminalService();
        }

        public Station(ICollection<IPort> ports, ICollection<ITerminal> terminals) : this()
        {                       
            foreach(var item in ports)
            {
                AddPort(item);
            }
            foreach(var terminal in terminals)
            {
                AddTerminal(terminal);
            }
        }
       
        public void AddTerminal(ITerminal terminal)
        {
            RegisterEventHandlersForTerminal(terminal);
            _terminalService.AddTerminal(terminal);
        }

        public IPort GetFreePort()
        {
            return _portService.GetFreePort();
        }

        public void AddPort(IPort port)
        {
            RegisterEventHandlersForPort(port);
            _portService.AddPort(port);
        }

        private void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingCall += OnOutgoingCall;
            terminal.IncomingCall += OnIncomingCall;
            terminal.Accept += OnAccept;
            terminal.End += OnEnd;
            terminal.Reject += OnReject;
        }

        private void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, eventArgs) =>
            {
                Console.WriteLine($"Port #{(sender as Port).Id} state changed to {eventArgs}");
            };
        }

        private void OnOutgoingCall(object sender, IPhoneNumber phone)
        {
            var answerer = _terminalService.FindTerminalByNumber(phone);
            var caller = sender as Terminal;

            if (answerer != null && caller != null)
            {
                caller.RememberConnection(caller.Number, phone);
                _portService.ChangeState(caller.Port, PortState.Busy);
                CallInfo info = new CallInfo
                {
                    From = caller.Number,
                    To = phone,
                    DateTimeStart = DateTime.Now,
                    Duration = TimeSpan.Zero
                };
                _callService.AddCall(info);

                if (answerer.Port.State != PortState.Busy)
                {                   
                    answerer.GetCall(caller.Number);
                }
                else
                {                   
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

        private void OnIncomingCall(object sender, IPhoneNumber phone)
        {
            var answerer = sender as Terminal;
            _portService.ChangeState(answerer.Port, PortState.Busy);
            answerer.RememberConnection(phone, answerer.Number);
        }

        private void OnAccept(object sender, EventArgs e)
        {
            var caller = _terminalService.FindTerminalByNumber((sender as Terminal).Connection.From);
            var info = _callService.GetCallInfo(caller.Connection);
            info.DateTimeStart = DateTime.Now;
        }

        private void OnEnd(object sender, EventArgs e)
        {
            var caller = _terminalService.FindTerminalByNumber((sender as Terminal).Connection.From);
            var info = _callService.GetCallInfo(caller.Connection);
            info.Duration = TimeSpan.ParseExact($"{DateTime.Now - info.DateTimeStart:mm\\:ss}","m\\:s",null);
            info.CallState = CallState.Outgoing;
            _callService.SaveCall(caller, info);
            var answerer = _terminalService.FindTerminalByNumber(caller.Connection.To);
            CallInfo info1 = _callService.Copy(info);
            info1.CallState = CallState.Incoming;
            _callService.SaveCall(answerer, info1);
            _callService.RemoveCall(info);
            _portService.ChangeState(caller.Port, PortState.ConnectedTerminal);
            _portService.ChangeState(answerer.Port, PortState.ConnectedTerminal);
        }

        private void OnReject(object sender, EventArgs e)
        {
            if ((sender as Terminal).Connection != null)
            {
                var caller = _terminalService.FindTerminalByNumber((sender as Terminal).Connection.From);
                var info = _callService.GetCallInfo(caller.Connection);
                var answerer = _terminalService.FindTerminalByNumber(caller.Connection.To);

                if (caller.Connection.Equals(answerer.Connection))
                {
                    if (caller.Equals(sender))
                    {
                        info.CallState = CallState.NoAnswer;
                        _callService.SaveCall(caller, info);
                        CallInfo info1 = _callService.Copy(info);
                        info1.CallState = CallState.Missed;
                        _callService.SaveCall(answerer, info1);
                    }
                    else
                    {
                        info.CallState = CallState.Rejected;
                        _callService.SaveCall(answerer, info);
                        CallInfo info1 = _callService.Copy(info);
                        info1.CallState = CallState.NoAnswer;
                        _callService.SaveCall(caller, info1);
                    }
                    caller.ClearConnection();
                    answerer.ClearConnection();
                    _callService.RemoveCall(info);
                    _portService.ChangeState(caller.Port, PortState.ConnectedTerminal);
                    _portService.ChangeState(answerer.Port, PortState.ConnectedTerminal);
                }
                else
                {
                    info.CallState = CallState.NoAnswer;
                    _callService.SaveCall(caller, info);
                    CallInfo info1 = _callService.Copy(info);
                    info1.CallState = CallState.Missed;
                    _callService.SaveCall(answerer, info1);
                    caller.ClearConnection();
                    _portService.ChangeState(caller.Port, PortState.ConnectedTerminal);
                }
            }
        }
    }
}
