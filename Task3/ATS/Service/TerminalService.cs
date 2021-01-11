using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task3.ATS.Models;

namespace Task3.ATS.Controllers
{
    public class TerminalController
    {
        private ICollection<Terminal> _terminals;

        public ICollection<Terminal> Terminals => _terminals.ToList();

        public TerminalController()
        {
            _terminals = new List<Terminal>();
        }

        public void AddTerminal(Terminal terminal)
        {
            _terminals.Add(terminal);
        }

        public void RemoveTerminal(Terminal terminal)
        {
            _terminals.Remove(terminal);
        }

        public Terminal FindTerminalByNumber(PhoneNumber number)
        {
            return _terminals.FirstOrDefault(x => x.Number.Equals(number));
        }
    }
}
