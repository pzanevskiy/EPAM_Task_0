using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextParser.Models;
using TextParser.Models.Enums;
using TextParser.Models.Interfaces;

namespace TextParser.Service
{
    public class TextService
    {
        public IEnumerable<IWord> GetInterrogativeSentencesWordsWithLength(ICollection<ISentence> sentences, int length)
        {         
            return sentences
                .Where(x => x.TypeOfSentence.Equals(SentenceType.INTERROGATIVE)).ToList()
                .SelectMany(y => y.Words.Where(x => x is IWord word && word.Count == length))
                .Cast<IWord>()
                .Distinct(new WordComparer());

        }

        public void ReplaceWords(ISentence sentence, int length, string newWord)
        {       
            foreach(var word in sentence.Words.Where(x => x is IWord word && word.Count==length).ToList())
                sentence.Replace(word, new Word(newWord));
        }

        public ICollection<ISentence> SortSentences(ICollection<ISentence> sentences)
        {
            return sentences.OrderBy(sentence => sentence.Count).ToList();
        }

        public ICollection<ISentence> RemoveWordsStartsWithConsonants(ICollection<ISentence> sentences)
        {
            string pattern = @"[\daeiou]";            
            var words= sentences
                .SelectMany(x => x.Words
                .Where(y => y is IWord word && word.Count == 5 && !Regex.IsMatch(word.FirstChar, pattern)));
            foreach (var sentence in sentences)
                foreach (var word in words.ToList())
                    sentence.Remove(word);
            return sentences;
        }

    }
}
