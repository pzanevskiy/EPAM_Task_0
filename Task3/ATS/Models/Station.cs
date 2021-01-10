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
        private protected PortController _portController;
        private protected CallController _callController;

        public event EventHandler<CallInfo> Call;

        public Station()
        {
            _portController = new PortController();
            _callController = new CallController();
        }

        public Station(ICollection<IPort> ports) : this()
        {                       
            foreach(var item in ports)
            {
                AddPort(item);
            }
        }
       
        public void OnCall(object sender, CallInfo call)
        {
            Call?.Invoke(sender, call);
        }

        public IPort GetFreePort()
        {
            return _portController.GetFreePort();
        }

        public void AddPort(IPort port)
        {
            port.Station = this;
            RegisterEventHandlersForTerminal(port);
            _portController.AddPort(port);
        }

        private void RegisterEventHandlersForTerminal(IPort port)
        {
            port.OutgoingCall += OnOutgoingCall;
            port.IncomingCall += OnIncomingCall;
            port.Accept += OnAccept;
            port.End += OnEnd;
            port.Reject += OnReject;
        }

        public IPort GetPortByPhoneNumber(IPhoneNumber phone)
        {
            return _portController.GetPortByPhoneNumber(phone);
        }

        public void AddCall(CallInfo info)
        {
            _callController.AddCall(info);
        }

        public void RemoveCall(CallInfo info)
        {
            _callController.RemoveCall(info);
        }

        public CallInfo GetCallInfo(Connection connection)
        {
            return _callController.GetCallInfo(connection);
        }

        private void OnOutgoingCall(object sender, IPhoneNumber phone)
        {
            var answerer = GetPortByPhoneNumber(phone).Terminal;
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
                    AddCall(info);
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
                    AddCall(info);
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
            info.Duration = DateTime.Now - info.DateTimeStart;
            info.CallState = CallState.Outgoing;
            OnCall(caller, info);
            var answerer = GetPortByPhoneNumber(caller.Connection.To).Terminal;
            CallInfo info1 = info.Copy();
            info1.CallState = CallState.Incoming;
            OnCall(answerer, info1);
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
                info.Duration = TimeSpan.Zero;

                if (caller.Connection.Equals(answerer.Connection))
                {
                    if (caller.Equals(sender))
                    {
                        info.CallState = CallState.NoAnswer;
                        OnCall(caller, info);
                        CallInfo info1 = info.Copy();
                        info1.CallState = CallState.Missed;
                        OnCall(answerer, info1);
                    }
                    else
                    {
                        info.CallState = CallState.Rejected;
                        OnCall(answerer, info);
                        CallInfo info1 = info.Copy();
                        info1.CallState = CallState.NoAnswer;
                        OnCall(caller, info1);
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
                    OnCall(caller, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = CallState.Missed;
                    OnCall(answerer, info1);
                    caller.ClearConnection();
                    caller.Port.ChangeState(PortState.ConnectedTerminal);
                }
            }
        }
    }
}
