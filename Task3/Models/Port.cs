using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models.Enums;

namespace Task3.Models
{
    public class Port
    {
        //guid???
        public Guid Id { get; set; }

        private PortState _portState;
        //private Terminal _terminal;

        public PortState State
        {
            get => _portState;
            set
            {               
                _portState = value;
                OnStateChanged(this, _portState);
            }
        }
        //public Terminal Terminal
        //{
        //    get => _terminal;
        //    set 
        //    {
        //        _terminal = value;
        //    }
        //}

        public event EventHandler<PortState> StateChanged;
        //public event EventHandler CurrentCallAdd;
        //public event EventHandler CurentCallRemove;
        //public event EventHandler CurrentCallGet;
        //public event EventHandler CurrentCallSave;

        public Port()
        {
            Id = Guid.NewGuid();
            RegisterEventHandlerForPort();
            State = PortState.Disconnected;
        }
        //public Port(Guid id)
        //{
        //    Id = id;
        //    RegisterEventHandlerForPort();           
        //    State = PortState.Disconnected;
        //}

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

        //protected virtual void OnCurrentCallAdd(object sender, EventArgs e)
        //{
        //    CurrentCallAdd.Invoke(sender, e);
        //}
        //protected virtual void OnCurrentCallRemove(object sender, EventArgs e)
        //{
        //    CurentCallRemove.Invoke(sender, e);
        //}
        //protected virtual void OnCurrentCallGet(object sender, EventArgs e)
        //{
        //    CurrentCallGet.Invoke(sender, e);
        //}
        //protected virtual void OnCurrentCallSave(object sender, EventArgs e)
        //{
        //    CurrentCallSave.Invoke(sender, e);
        //}
    }
}
