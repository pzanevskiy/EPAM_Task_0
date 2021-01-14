using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;

namespace Task3.ATS.Models
{
    public class Terminal : ITerminal
    {
        private IPhoneNumber _phoneNumber;
        private IPort _port;

        public Connection Connection { get; private set; }
        public IPhoneNumber Number { get; set; }
        public IPort Port { get; private set; }

        public bool IsFree { get; set; }

        public event EventHandler<IPhoneNumber> OutgoingCall;
        public event EventHandler<IPhoneNumber> IncomingCall;
        public event EventHandler Accept;
        public event EventHandler Reject;
        public event EventHandler End;
        public event EventHandler<IPort> ConnectingToPort;
        public event EventHandler<IPort> DisconnectingFromPort;

        public Terminal()
        {
            IsFree = true;
            RegisterEventHandlerForTerminal();
        }

        public Terminal(IPhoneNumber phoneNumber):this()
        {
            Number = phoneNumber;
        }

        protected virtual void RegisterEventHandlerForTerminal()
        {
            OutgoingCall += (sender, phone) =>
            {
                var caller = sender as Terminal;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{caller.Number} calls to {phone.Number}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            IncomingCall += (sender, phone) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var answerer = sender as Terminal;
                Console.WriteLine($"{answerer.Number} is calling {phone.Number}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            Accept += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Call is started by {(sender as Terminal).Number}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            Reject += (sender, e) =>
            {                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Call is rejected by {(sender as Terminal).Number}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            End += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Call is ended by {(sender as Terminal).Number}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            ConnectingToPort += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                var terminal = sender as Terminal;
                Console.WriteLine($"Terminal {terminal.Number} connected to port #{e.Id}");
                Console.ForegroundColor = ConsoleColor.White;
            };
            DisconnectingFromPort += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                var terminal = sender as Terminal;
                Console.WriteLine($"Terminal {terminal.Number} disconnected from port #{e.Id}");
                Console.ForegroundColor = ConsoleColor.White;
            };
        }

        protected virtual void OnOnutgoingCall(object sender, IPhoneNumber number)
        {
            OutgoingCall?.Invoke(sender, number);
        }

        protected virtual void OnIncomingCall(object sender, IPhoneNumber number)
        {
            IncomingCall?.Invoke(sender, number);
        }

        public void Call(IPhoneNumber to)
        {
            if (Port != null)
            {
                OnOnutgoingCall(this, to);
            }
        }

        public void GetCall(IPhoneNumber from)
        {
            if (Port != null)
            {
                OnIncomingCall(this, from);
            }
        }

        protected virtual void OnAcceptCall(object sender, EventArgs args)
        {
            Accept.Invoke(sender, args);
        }

        protected virtual void OnRejectCall(object sender, EventArgs args)
        {
            Reject.Invoke(sender, args);
        }

        public void AcceptCall()
        {
            if (Port != null)
            {
                OnAcceptCall(this, null);
            }
          
        }

        public void RejectCall()
        {
            OnRejectCall(this, null);
        }

        protected virtual void OnEndCall(object sender,EventArgs args)
        {
            End.Invoke(sender, args);
        }

        public void EndCall()
        {
            OnEndCall(this, null);
        }

        protected virtual void OnConnect(IPort port)
        {
            ConnectingToPort?.Invoke(this, port);
        }

        protected virtual void OnDisconnect(IPort port)
        {
            DisconnectingFromPort?.Invoke(this, Port);
        }

        public void ConnectToPort(IPort port)
        {            
            if (port.State == Enums.PortState.Free && Port==null)
            {
                Port = port;
                Port.ChangeState(Enums.PortState.ConnectedTerminal);
                OnConnect(port);
            }
            else
            {
                Console.WriteLine($"Port is busy");
            }
        }

        public void DisconnectFromPort()
        {
            if (Port != null && Port.State.Equals(Enums.PortState.ConnectedTerminal))
            {
                OnDisconnect(Port);
                Port.ChangeState(Enums.PortState.Free);
                Port = null;
            }
            else
            {
                Console.WriteLine($"Port is null");
            }

        }

        public void RememberConnection(IPhoneNumber from, IPhoneNumber to)
        {
            Connection = new Connection()
            {
                From = from,
                To = to
            };
        }

        public void ClearConnection()
        {
            Connection = null;
        }

        public override string ToString()
        {
            return $"Terminal number {Number} connected to port #{Port.Id}";
        }

        public override bool Equals(object obj)
        {
            return obj is Terminal terminal &&
                   EqualityComparer<IPhoneNumber>.Default.Equals(Number, terminal.Number) &&
                   EqualityComparer<IPort>.Default.Equals(Port, terminal.Port) &&
                   IsFree == terminal.IsFree;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_phoneNumber);
        }
    }
}
