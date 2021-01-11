using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Models
{
    public class Port : IPort
    {      
        private PortState _portState;
        private ITerminal _terminal;

        public Guid Id { get; set; }
        public PortState State
        {
            get => _portState;
            private set
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

        public event EventHandler<PortState> StateChanged;
        public event EventHandler<IPhoneNumber> OutgoingCall;
        public event EventHandler<IPhoneNumber> IncomingCall;
        public event EventHandler Accept;
        public event EventHandler Reject;
        public event EventHandler End;

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
            terminal.OutgoingCall += (sender, phone) =>
            {
                OutgoingCall?.Invoke(sender, phone);
            };        
            terminal.IncomingCall += (sender, phone) =>
            {
                IncomingCall?.Invoke(sender, phone);
            };
            terminal.Accept += (sender, e) =>
            {
                Accept.Invoke(sender, e);
            };
            terminal.End += (sender, e) =>
            {
                End.Invoke(sender, e);
            };
            terminal.Reject += (sender, e )=>
            {
                Reject.Invoke(sender, e);
            };
        }

        public void ChangeState(PortState state)
        {
            State = state;
        }
    }
}
