using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models.Enums;

namespace Task3.Models
{
    public class Port
    {

        private PortState _portState;
        //guid???
        public int Id { get; set; }

        public PortState State
        {
            get => _portState;
            set
            {               
                _portState = value;
                OnStateChanged(this, _portState);
            }
        }

        public event EventHandler<PortState> StateChanged;

        public Port()
        {
            RegisterEventHandlerForPort();
            State = PortState.Disconnected;
        }
        public Port(int id)
        {
            Id = id;
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
        

        public void ChangeState(PortState state)
        {
            State = state;
        }
    }
}
