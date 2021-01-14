using System;
using System.Collections.Generic;
using System.Text;
using Task3.Enums;

namespace Task3.ATS.Models.Interfaces
{
    public interface IPort
    {
        public Guid Id { get; }
        public PortState State { get; set; }        

        public event EventHandler<PortState> StateChanged;

        public void ChangeState(PortState state);

    }
}
