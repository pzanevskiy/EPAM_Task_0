using System;
using System.Collections.Generic;
using System.Text;

namespace TextParser.Models
{
    public class Text
    {
        private ICollection<Sentence> _sentences;

        public ICollection<Sentence> Sentences
        {
            get => _sentences;
            set => _sentences = value;
        }

        public Text()
        {
            Sentences = new List<Sentence>();
        }        

        public Text(ICollection<Sentence> sentences)
        {
            Sentences = sentences;
        }

        public void Add(Sentence sentence)
        {
            if (Sentences != null)
            {
                Sentences.Add(sentence);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var sentence in Sentences)
            {
                sb.Append(sentence);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
