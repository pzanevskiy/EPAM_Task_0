using System;
using System.Collections.Generic;
using System.Text;
using TextParser.Models.Interfaces;

namespace TextParser.Service.Interfaces
{
    public interface ITextService
    {
        public IEnumerable<IWord> GetInterrogativeSentencesWordsWithLength(ICollection<ISentence> sentences, int length);
        public void ReplaceWords(ISentence sentence, int length, string newWord);
        public ICollection<ISentence> SortSentences(ICollection<ISentence> sentences);
        public ICollection<ISentence> RemoveWordsStartsWithConsonants(ICollection<ISentence> sentences, int length);
    }
}
