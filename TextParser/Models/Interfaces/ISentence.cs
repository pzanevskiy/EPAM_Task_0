using System;
using System.Collections.Generic;
using System.Text;
using TextParser.Models.Enums;

namespace TextParser.Models.Interfaces
{
    public interface ISentence
    {
        public int Count { get; }  
        public SentenceType TypeOfSentence { get; set; }
        public IList<ISentenceItem> Words { get; }

        public void Add(ISentenceItem item);
        public void Remove(ISentenceItem item);
        public void Replace(ISentenceItem oldItem, ISentenceItem newItem);
    }
}
