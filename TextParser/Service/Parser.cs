using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Models;
using TextParser.Models.Separators;
using TextParser.Models.Enums;
using System.IO;
using TextParser.Models.Interfaces;
using TextParser.Service.Interfaces;

namespace TextParser.Service
{
    public class Parser : IParser
    {
        private WordSeparators _wordSeparators;
        private SentenceSeparators _sentenceSeparators;

        public Parser()
        {
            _wordSeparators = new WordSeparators();
            _sentenceSeparators = new SentenceSeparators();
        }

        public IText ParseText(StreamReader reader)
        {
            IText text = new Text();
            StringBuilder sb = new StringBuilder();
            using (reader)
            {
               
                while (reader.Peek() >= 0)
                {
                    StringBuilder line = new StringBuilder(reader.ReadLine());
                    if (sb.ToString() !="")
                    {
                        line.Insert(0,sb);
                        sb.Clear();
                    }
                    var sentences = line.ToString().Split(_sentenceSeparators.GetSeparators(), StringSplitOptions.RemoveEmptyEntries);
                    var separators = line.ToString().Split(sentences, StringSplitOptions.RemoveEmptyEntries);
                    if (sentences.Length > separators.Length)
                    {
                        sb.Append(sentences[sentences.Length - 1]);
                        sb.Append(" ");
                    }
                    IEnumerable<Tuple<string, string>> tuples = sentences.Zip(separators, (sentence, separator) => new Tuple<string, string>(sentence, separator));
                    foreach (var tuple in tuples)
                    {
                        text.Add(ParseSentence(tuple));
                    }
                }
            }
            return text;
        }

        private ISentence ParseSentence(Tuple<string,string> sentenceTuple)
        {
            ISentence sentence = new Sentence();
            var words = sentenceTuple.Item1.Split(new string[] { " ","\t"},StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {               
                var separator = _wordSeparators.GetSeparators().Where(x => word.IndexOf(x) >= 0).FirstOrDefault();
                
                if (separator!=null)
                {
                    string wordTemp = word.Replace(separator, "");
                    sentence.Add(new Word(wordTemp));
                    sentence.Add(new Punctuation(separator));
                    continue;
                }                
                sentence.Add(new Word(word));   
            }

            SetSentenceType(sentence,sentenceTuple.Item2);
            sentence.Add(new Punctuation(sentenceTuple.Item2));
            return sentence;
        }

        private void SetSentenceType(ISentence sentence, string endMark)
        {
            switch (endMark)
            {
                case ".":
                    {
                        sentence.TypeOfSentence = SentenceType.NARRATIVE;
                        break;
                    }
                case "?":
                    {
                        sentence.TypeOfSentence = SentenceType.INTERROGATIVE;
                        break;
                    }
                case "!":
                    {
                        sentence.TypeOfSentence = SentenceType.EXCLAMATION;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
