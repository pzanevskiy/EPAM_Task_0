using System;
using System.Collections.Generic;
using System.Text;

namespace TextParser.Models.Separators
{
    public class SentenceSeparators : Separator
    { 
        string[] sentenceSeparators = { ".", "…", "?!", "?", "!" };

        public override string[] GetSeparators()
        {
            return sentenceSeparators;
        }
    }
}
