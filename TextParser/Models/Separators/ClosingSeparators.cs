using System;
using System.Collections.Generic;
using System.Text;
using TextParser.Models.Separators;

namespace Task2.Models.Separators
{
    public class ClosingSeparators : Separator
    {
        string[] closingSeparators = { ")", "]", "}", "»", "”", "’" };

        public override string[] GetSeparators()
        {
            return closingSeparators;
        }
    }
}
