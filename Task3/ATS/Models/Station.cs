using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Controllers;
using Task3.ATS.Models.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Models
{
    public class Station : IStation
    {
        private PortService _portService;
        private CallService _callService;

        public CallService CallService => _callService;


        public Station()
        {
            _portService = new PortService();
            _callService = new CallService();
        }

        public Station(ICollection<IPort> ports) : this()
        {                       
            foreach(var item in ports)
            {
                AddPort(item);
            }
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

        private void RegisterEventHandlersForPort(IPort port)
        {
            port.OutgoingCall += OnOutgoingCall;
            port.IncomingCall += OnIncomingCall;
            port.Accept += OnAccept;
            port.End += OnEnd;
            port.Reject += OnReject;
        }

        public IPort GetPortByPhoneNumber(IPhoneNumber phone)
        {
            return _portService.GetPortByPhoneNumber(phone);
        }

        public void AddCall(CallInfo info)
        {
            _callService.AddCall(info);
        }

        public void RemoveCall(CallInfo info)
        {
            _callService.RemoveCall(info);
        }

        public CallInfo GetCallInfo(Connection connection)
        {
            return _callService.GetCallInfo(connection);
        }

        private void OnOutgoingCall(object sender, IPhoneNumber phone)
        {
            var answerer = GetPortByPhoneNumber(phone).Terminal;
            var caller = sender as Terminal;

            if (answerer != null && caller != null)
            {
                caller.Port.ChangeState(PortState.Busy);
                caller.RememberConnection(caller.Number, phone);
                CallInfo info = new CallInfo
                {
                    From = caller.Number,
                    To = phone,
                    DateTimeStart = DateTime.Now,
                    Duration = TimeSpan.Zero
                };
                AddCall(info); 

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
            answerer.Port.ChangeState(PortState.Busy);
            answerer.RememberConnection(phone, answerer.Number);
        }

        private void OnAccept(object sender, EventArgs e)
        {
            var caller = GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
            var info = GetCallInfo(caller.Connection);
            info.DateTimeStart = DateTime.Now;
        }

        private void OnEnd(object sender, EventArgs e)
        {
            var caller = GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
            var info = GetCallInfo(caller.Connection);
            info.Duration = TimeSpan.ParseExact($"{DateTime.Now - info.DateTimeStart:mm\\:ss}","m\\:s",null);
            info.CallState = CallState.Outgoing;
            _callService.OnCall(caller, info);
            var answerer = GetPortByPhoneNumber(caller.Connection.To).Terminal;
            CallInfo info1 = _callService.Copy(info);
            info1.CallState = CallState.Incoming;
            _callService.OnCall(answerer, info1);
            RemoveCall(info);
            caller.Port.ChangeState(PortState.ConnectedTerminal);
            answerer.Port.ChangeState(PortState.ConnectedTerminal);
        }

        private void OnReject(object sender, EventArgs e)
        {
            if ((sender as Terminal).Connection != null)
            {
                var caller = GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
                var info = GetCallInfo(caller.Connection);
                var answerer = GetPortByPhoneNumber(caller.Connection.To).Terminal;

                if (caller.Connection.Equals(answerer.Connection))
                {
                    if (caller.Equals(sender))
                    {
                        info.CallState = CallState.NoAnswer;
                        _callService.OnCall(caller, info);
                        CallInfo info1 = _callService.Copy(info);
                        info1.CallState = CallState.Missed;
                        _callService.OnCall(answerer, info1);
                    }
                    else
                    {
                        info.CallState = CallState.Rejected;
                        _callService.OnCall(answerer, info);
                        CallInfo info1 = _callService.Copy(info);
                        info1.CallState = CallState.NoAnswer;
                        _callService.OnCall(caller, info1);
                    }
                    caller.ClearConnection();
                    answerer.ClearConnection();
                    RemoveCall(info);
                    caller.Port.ChangeState(PortState.ConnectedTerminal);
                    answerer.Port.ChangeState(PortState.ConnectedTerminal);
                }
                else
                {
                    info.CallState = CallState.NoAnswer;
                    _callService.OnCall(caller, info);
                    CallInfo info1 = _callService.Copy(info);
                    info1.CallState = CallState.Missed;
                    _callService.OnCall(answerer, info1);
                    caller.ClearConnection();
                    caller.Port.ChangeState(PortState.ConnectedTerminal);
                }
            }
        }
    }
}
