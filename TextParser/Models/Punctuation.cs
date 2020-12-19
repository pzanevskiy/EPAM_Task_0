using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextParser.Models
{
    public class Punctuation : ISentenceItem
    {
        private Symbol _punctuationMark;

        public Symbol PunctuationMark
        {
            get => _punctuationMark;
            set => _punctuationMark = value;
        }

        public Punctuation(string punctuation)
        {
            this.PunctuationMark = new Symbol(punctuation);
        }

        public override string ToString()
        {
            return PunctuationMark.ToString();
        }
    }
}
