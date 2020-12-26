using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextParser.Models.Interfaces;

namespace TextParser.Service.Interfaces
{
    public interface IParser
    {
        public IText ParseText(StreamReader reader);
    }
}
