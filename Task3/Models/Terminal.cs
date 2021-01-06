using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.Models
{
    public class Terminal
    {
        private PhoneNumber _phoneNumber;
        private Port _port;

        public PhoneNumber Number 
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public Port Port
        {
            get => _port;
            set => _port = value;
        }

        public event EventHandler<PhoneNumber> OutgoingCall; 
        public event EventHandler<PhoneNumber> IncomingCall; 
        public event EventHandler Accept; 
        public event EventHandler Reject;

        public Terminal()
        {

        }

        public Terminal(PhoneNumber phoneNumber)
        {
            Number = phoneNumber;
        }

        

        protected virtual void OnOnutgoingCall(object sender, PhoneNumber number)
        {
            OutgoingCall?.Invoke(sender, number);
        }

        protected virtual void OnIncomingCall(object sender, PhoneNumber number)
        {
            IncomingCall?.Invoke(sender, number);
        }

        public void Call(PhoneNumber to)
        {
            if (Port != null)
            {
                OnOnutgoingCall(this, to);
                //GetCall(this.Number);
            }
        }

        public void GetCall(PhoneNumber from)
        {
            if (Port != null)
            {
                OnIncomingCall(this, from);
            }
        }

        protected virtual void OnAcceptCall(object sender,EventArgs args)
        {
            Accept.Invoke(sender, args);
        }

        protected virtual void OnRejectCall(object sender, EventArgs args)
        {
            Reject.Invoke(sender, args);
        }

        public void AcceptCall()
        {
            OnAcceptCall(this, null);
        }

        public void RejectCall()
        {
            OnRejectCall(this,null);
        }

        public void ConnectToPort(Port port)
        {
            Port = port;
            Port.ChangeState(Enums.PortState.Busy);
        }
    }
}
