using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TextParser.Models.Enums;

namespace TextParser.Models
{
    public class Sentence
    {
        private IList<ISentenceItem> _sentenceItems;
        private SentenceType _sentenceType;

        public IList<ISentenceItem> SentenceItems
        {
            get => _sentenceItems;
            set => _sentenceItems = value;
        }

        public SentenceType TypeOfSentence
        {
            get => _sentenceType;
            set => _sentenceType = value.Equals(null) ? SentenceType.NARRATIVE : value;
        }

        public Sentence()
        {
            SentenceItems = new List<ISentenceItem>();
            TypeOfSentence = SentenceType.NARRATIVE;
        }

        public Sentence(IList<ISentenceItem> words)
        {
            SentenceItems = words;           
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();            
            for(int i = 0; i < SentenceItems.Count; i++)
            {
                if(SentenceItems[i] is Word && SentenceItems[i+1] is Punctuation)
                {
                    stringBuilder.Append(SentenceItems[i]);
                    continue;
                }
                stringBuilder.Append(SentenceItems[i]);
                stringBuilder.Append(" ");                   
            }
            return stringBuilder.ToString();
        }
    }
}
