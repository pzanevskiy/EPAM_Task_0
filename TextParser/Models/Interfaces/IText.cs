using System;
using System.Collections.Generic;
using System.Text;

namespace TextParser.Models.Interfaces
{
    public interface IText
    {
        public ICollection<ISentence> Sentences { get; set; }

        public void Add(ISentence sentence);
    }
}
