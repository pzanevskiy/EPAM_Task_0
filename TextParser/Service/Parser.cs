using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Models;
using TextParser.Models.Separators;

namespace TextParser.Service
{
    public class Parser
    {
        private WordSeparators _wordSeparators;
        private SentenceSeparators _sentenceSeparators;

        public Parser()
        {
            _wordSeparators = new WordSeparators();
            _sentenceSeparators = new SentenceSeparators();
        }

        public Text ParseText(string source)
        {
            Text text = new Text();
            StringBuilder sb = new StringBuilder();
            var sentences = source.Split(_sentenceSeparators.GetSeparators(), StringSplitOptions.RemoveEmptyEntries);
            var separators = source.Split(sentences, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<Tuple<string, string>> tuples = sentences.Zip(separators, (sentence, separator) => new Tuple<string, string>(sentence, separator));
            foreach(var tuple in tuples)
            {
                text.Add(ParseSentence(tuple));
            }
            Console.WriteLine(text.ToString());
            Console.WriteLine();
            return text;
        }

        private Sentence ParseSentence(Tuple<string,string> sentenceTuple)
        {
            Sentence sentence = new Sentence();
            var words = sentenceTuple.Item1.Split(new string[] { " ","\t"},StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                bool flag = false;
                string tempPunct = "";
                foreach (string separator in _wordSeparators.GetSeparators())
                {
                    if (word.Contains(separator))
                    {
                        flag = true;
                        tempPunct = separator;
                        break;
                    }
                }
                if (flag)
                {
                    string wordTemp = word.Replace(tempPunct, "");
                    sentence.SentenceItems.Add(new Word(wordTemp));
                    sentence.SentenceItems.Add(new Punctuation(tempPunct));
                    continue;
                }                
                sentence.SentenceItems.Add(new Word(word));   
            }
            switch (sentenceTuple.Item2)
            {
                case ".":
                    {
                        sentence.TypeOfSentence = Models.Enums.SentenceType.NARRATIVE;
                        break;
                    }
                case "?":
                    {
                        sentence.TypeOfSentence = Models.Enums.SentenceType.INTERROGATIVE;
                        break;
                    }
                case "!":
                    {
                        sentence.TypeOfSentence = Models.Enums.SentenceType.EXCLAMATION;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            sentence.SentenceItems.Add(new Punctuation(sentenceTuple.Item2));
            return sentence;
        }
    }
}
