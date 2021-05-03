using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Task2.Models;
using Text_Analyzer.TextUtility.DataTransferObject;
using TextParser.Models.Interfaces;

namespace TextParser.Service.Interfaces
{
    public interface IFileService
    {
        public ICollection<string> GetData(string path, string contentType);
        public void Write(IText text, string filename);
        public void WriteData(IEnumerable<ConcordanceItemsDTO> items, string filename);
    }
}
