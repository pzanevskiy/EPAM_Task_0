using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextParser.Models
{
    public class Sentence
    {
        private ICollection<ISentenceItem> _sentenceItems;

        public ICollection<ISentenceItem> SentenceItems
        {
            get => _sentenceItems;
            set => _sentenceItems = value;
        }

        public Sentence(string sentence)
        {
            SentenceItems = new List<ISentenceItem>();
            string[] words = sentence.Split(new string[] { " ","\t" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string word in words)
            {
                bool flag = false;
                string tempPunct="";
                foreach(char ch in new char[] { ',',':'})
                {
                    if (word.Contains(ch))
                    {
                        flag = true;
                        tempPunct = String.Format("{0}",ch);
                        break;                      
                    }
                }
                if (flag)
                {
                    string wordTemp = word.Replace(tempPunct, "");
                    SentenceItems.Add(new Word(wordTemp));
                    SentenceItems.Add(new Punctuation(tempPunct));
                }
                else
                {
                    SentenceItems.Add(new Word(word));
                }
                
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var item in SentenceItems)
            {
                if(item is Word)
                {
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }
    }
}
