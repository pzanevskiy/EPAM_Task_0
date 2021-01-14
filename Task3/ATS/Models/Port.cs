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
        //private ITerminal _terminal;

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
        //public ITerminal Terminal
        //{
        //    get => _terminal;
        //    set
        //    {
        //        _terminal = value;
        //        RegisterEventHandlersForTerminal(_terminal);
        //    }
        //}     

        public event EventHandler<PortState> StateChanged;
        //public event EventHandler<IPhoneNumber> OutgoingCall;
        //public event EventHandler<IPhoneNumber> IncomingCall;
        //public event EventHandler Accept;
        //public event EventHandler Reject;
        //public event EventHandler End;

        public Port()
        {
            Id = Guid.NewGuid();
            State = PortState.Disconnected;
        }

        protected virtual void OnStateChanged(object sender, PortState state)
        {
            StateChanged?.Invoke(sender,state);
        }
        

        public void ChangeState(PortState state)
        {
            State = state;
        }
    }
}
