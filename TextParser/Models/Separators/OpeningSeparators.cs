using System;
using System.Collections.Generic;
using System.Text;
using TextParser.Models.Separators;

namespace Task2.Models.Separators
{
    public class OpeningSeparators : Separator
    {
        string[] openingSeparators = { "(", "[", "{", "«", "“", "‘" };

        public override string[] GetSeparators()
        {
            return openingSeparators;
        }
    }
}
