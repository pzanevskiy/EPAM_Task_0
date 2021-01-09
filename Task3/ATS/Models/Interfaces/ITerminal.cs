using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models;

namespace Task3.ATS.Models.Interfaces
{
    public interface ITerminal
    {
        public Connection Connection { get; set; }

        public IPhoneNumber Number { get; set; }       

        public IPort Port { get; set; }

        public event EventHandler<IPhoneNumber> OutgoingCall;
        public event EventHandler<IPhoneNumber> IncomingCall;
        public event EventHandler Accept;
        public event EventHandler Reject;
        public event EventHandler End;
        public event EventHandler<IPort> ConnectingToPort;
        public event EventHandler<IPort> DisconnectingFromPort;

        void RegisterEventHandlerForTerminal();

        public void Call(IPhoneNumber to);
        public void GetCall(IPhoneNumber from);
        public void AcceptCall();      
        public void RejectCall();
        public void EndCall();

        public void ConnectToPort(IPort port);
        public void DisconnectFromPort();
        public void RememberConnection(IPhoneNumber from, IPhoneNumber to);

    }
}
