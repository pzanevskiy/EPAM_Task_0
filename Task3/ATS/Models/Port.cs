using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.Models.Enums;

namespace Task3.Models
{
    public class Port : IPort
    {      
        private PortState _portState;
        private ITerminal _terminal;

        public Guid Id { get; set; }
        public PortState State
        {
            get => _portState;
            set
            {               
                _portState = value;
                OnStateChanged(this, _portState);
            }
        }
        public ITerminal Terminal
        {
            get => _terminal;
            set
            {
                _terminal = value;
                RegisterEventHandlersForTerminal(_terminal);
            }
        }
        public IStation Station { get; set; }

        public event EventHandler<PortState> StateChanged;

        public Port()
        {
            Id = Guid.NewGuid();
            RegisterEventHandlerForPort();
            State = PortState.Disconnected;
        }

        protected virtual void OnStateChanged(object sender, PortState state)
        {
            StateChanged?.Invoke(sender,state);
        }

        protected virtual void RegisterEventHandlerForPort()
        {
            StateChanged += (sender, eventArgs) =>
            {
                Console.WriteLine($"Port #{Id} state changed to {eventArgs}");
            };
        }

        private void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingCall += OnOutgoingCall;
            terminal.IncomingCall += OnIncomingCall;
            terminal.Accept += OnAccept;
            terminal.End += OnEnd;
            terminal.Reject += OnReject;
        }

        public void ChangeState(PortState state)
        {
            State = state;
        }

        private void OnOutgoingCall(object sender, IPhoneNumber phone)
        {
            var answerer = Station.GetPortByPhoneNumber(phone).Terminal;
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
                    Station.AddCall(info);
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
                    Station.AddCall(info);
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
            var caller = Station.GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
            var info = Station.GetCallInfo(caller.Connection);
            info.DateTimeStart = DateTime.Now;
        }

        private void OnEnd(object sender, EventArgs e)
        {
            var caller = Station.GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
            var info = Station.GetCallInfo(caller.Connection);
            info.Duration = DateTime.Now - info.DateTimeStart;
            info.Terminal = caller;
            info.CallState = CallState.Outgoing;
            Station.OnCall(this, info);
            var answerer = Station.GetPortByPhoneNumber(caller.Connection.To).Terminal;
            CallInfo info1 = info.Copy();
            info1.Terminal = answerer;
            info1.CallState = CallState.Incoming;
            Station.OnCall(this, info1);
            Station.RemoveCall(info);
            caller.Port.ChangeState(PortState.ConnectedTerminal);
            answerer.Port.ChangeState(PortState.ConnectedTerminal);
        }

        private void OnReject(object sender, EventArgs e)
        {
            if ((sender as Terminal).Connection != null)
            {
                var caller = Station.GetPortByPhoneNumber((sender as Terminal).Connection.From).Terminal;
                var info = Station.GetCallInfo(caller.Connection);
                var answerer = Station.GetPortByPhoneNumber(caller.Connection.To).Terminal;
                info.Duration = TimeSpan.Zero;

                if (caller.Connection.Equals(answerer.Connection))
                {
                    if (caller.Equals(sender))
                    {
                        info.CallState = CallState.NoAnswer;
                        info.Terminal = caller;
                        Station.OnCall(this, info);
                        CallInfo info1 = info.Copy();
                        info1.CallState = CallState.Missed;
                        info1.Terminal = answerer;
                        Station.OnCall(this, info1);
                    }
                    else
                    {
                        info.CallState = CallState.Rejected;
                        info.Terminal = answerer;
                        Station.OnCall(this, info);
                        CallInfo info1 = info.Copy();
                        info1.CallState = CallState.NoAnswer;
                        info1.Terminal = caller;
                        Station.OnCall(this, info1);
                    }

                    Station.RemoveCall(info);
                    caller.Port.ChangeState(PortState.ConnectedTerminal);
                    answerer.Port.ChangeState(PortState.ConnectedTerminal);
                }
                else
                {
                    info.CallState = CallState.NoAnswer;
                    info.Terminal = caller;
                    Station.OnCall(this, info);
                    CallInfo info1 = info.Copy();
                    info1.CallState = CallState.Missed;
                    info1.Terminal = answerer;
                    Station.OnCall(this, info1);
                    caller.Port.ChangeState(PortState.ConnectedTerminal);
                }
            }
        }
    }
}
