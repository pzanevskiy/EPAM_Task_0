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
    public class TextServices
    {
        public IEnumerable<IWord> getInterrogativeSentencesWordsWithLength(ICollection<Sentence> sentences, int length)
        {         
            return sentences
                .Where(x => x.TypeOfSentence.Equals(SentenceType.INTERROGATIVE)).ToList()
                .SelectMany(y => y.SentenceItems.Where(x => x is IWord word && word.Count == length))
                .Cast<IWord>()
                .Distinct(new WordComparer());

        }

        public void ReplaceWords(ISentence sentence, int length, string newWord)
        {       
            foreach(var word in sentence.Words.Where(x => x is IWord).ToList())
            {
                sentence.Replace(word, new Word(newWord));
            }
        }

        public ICollection<Sentence> SortSentences(ICollection<Sentence> sentences)
        {
            return sentences.OrderBy(sentence => sentence.Count).ToList();
        }

        public void RemoveWords(ICollection<Sentence> sentences)
        {
           
        }

    }
}
