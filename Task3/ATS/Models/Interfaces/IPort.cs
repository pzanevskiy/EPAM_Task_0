using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models.Enums;

namespace Task3.ATS.Models.Interfaces
{
    public interface IPort
    {
        public Guid Id { get; set; }
        public PortState State { get; set; }        
        public ITerminal Terminal { get; set; }
        public IStation Station { get; set; }

        public event EventHandler<PortState> StateChanged;

        public void ChangeState(PortState state);

    }
}
