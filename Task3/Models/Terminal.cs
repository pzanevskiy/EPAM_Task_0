using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.Models
{
    public class Terminal
    {
        private PhoneNumber _phoneNumber;
        private Port _port;

        public Connection Connection { get; set; }

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
        public event EventHandler End;
        public event EventHandler<Port> ConnectingToPort;
        public event EventHandler<Port> DisconnectingFromPort;

        public Terminal()
        {

        }

        public Terminal(PhoneNumber phoneNumber)
        {
            RegisterEventHandlerForTerminal();
            Number = phoneNumber;
        }

        public virtual void RegisterEventHandlerForTerminal()
        {
            OutgoingCall += (sender, phone) =>
            {
                var caller = sender as Terminal;
                Console.WriteLine($"{caller.Number} calls to {phone.Number}");
            };
            IncomingCall += (sender, phone) =>
            {
                var answerer = sender as Terminal;
                Console.WriteLine($"{answerer.Number} is calling {phone.Number}");
            };
            Accept += (sender, e) =>
            {
                Console.WriteLine($"Call is started by {(sender as Terminal).Number}");
            };
            Reject += (sender, e) =>
            {                
                Console.WriteLine($"Call is rejected by {(sender as Terminal).Number}");
            };
            End += (sender, e) =>
            {
                Console.WriteLine($"Call is ended by {(sender as Terminal).Number}");
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
            }
        }

        public void GetCall(PhoneNumber from)
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
            else
            {

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

        public void ConnectToPort(Port port)
        {
            if (port.State == Enums.PortState.Free)
            {
                Port = port;
                Port.ChangeState(Enums.PortState.ConnectedTerminal);
                ConnectingToPort?.Invoke(this, port);
            }
            else
            {
                Console.WriteLine($"{new Exception("Port is busy")}");
            }
        }

        public void DisconnectFromPort()
        {
            if (Port != null && Port.State.Equals(Enums.PortState.ConnectedTerminal))
            {
                DisconnectingFromPort?.Invoke(this, Port);
                Port.ChangeState(Enums.PortState.Free);
                Port = null;
            }
            else
            {
                Console.WriteLine($"{new Exception("Port is null")}");
            }

        }

        public void RememberConnection(PhoneNumber from, PhoneNumber to)
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
                   EqualityComparer<PhoneNumber>.Default.Equals(_phoneNumber, terminal._phoneNumber) &&
                   EqualityComparer<Port>.Default.Equals(_port, terminal._port);
        }
    }
}
