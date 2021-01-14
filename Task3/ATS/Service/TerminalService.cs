using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.ATS.Service.Interfaces;

namespace Task3.ATS.Service
{
    public class TerminalService : ITerminalService
    {
        private ICollection<ITerminal> _terminals;

        public TerminalService()
        {
            _terminals = new List<ITerminal>();
        }

        public void AddTerminal(ITerminal terminal)
        {
            _terminals.Add(terminal);
        }

        public void RemoveTerminal(ITerminal terminal)
        {
            _terminals.Remove(terminal);
        }

        public ITerminal FindTerminalByNumber(IPhoneNumber number)
        {
            return _terminals.FirstOrDefault(x => x.Number.Equals(number));
        }

        public ITerminal GetFreeTerminal()
        {
            var freeTerminal = _terminals.FirstOrDefault(x => x.IsFree.Equals(true));
            freeTerminal.IsFree = false;
            return freeTerminal;
        }
    }
}
