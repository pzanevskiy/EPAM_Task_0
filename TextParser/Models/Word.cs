using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextParser.Models
{
    public class Word : ISentenceItem
    {
        private ICollection<Symbol> _symbols;

        public ICollection<Symbol> Symbols
        {
            get => _symbols;
            set => _symbols=value;
        }

        public Word(string word)
        {
           
            if (word != null)
            {
                this.Symbols = word.Select(x => new Symbol(x)).ToList();
            }
            else
            {
                this.Symbols = null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var symbol in Symbols)
            {
                sb.Append(symbol);
            }
            return sb.ToString();
        }
    }
}
