using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextParser.Models.Interfaces;

namespace TextParser.Service.Interfaces
{
    public interface IFileService
    {
        public StreamReader GetReader(string filename);
        public void Write(IText text, string filename);
    }
}
