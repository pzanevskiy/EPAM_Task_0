using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;

namespace Task3.ATS.Service.Interfaces
{
    public interface ITerminalService
    {
        public void AddTerminal(ITerminal terminal);
        public void RemoveTerminal(ITerminal terminal);
        public ITerminal FindTerminalByNumber(IPhoneNumber number);
        public ITerminal GetFreeTerminal();
    }
}
