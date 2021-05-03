using System;
using System.IO;
using TextParser.Models;
using TextParser.Service;
using System.Linq;
using System.Collections.Generic;
using TextParser.Models.Interfaces;
using TextParser.Service.Interfaces;
using System.Configuration;
using DeepMorphy;
using Task2.Models;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = ConfigurationManager.AppSettings;

            IParser parser = new Parser();
            IText text = new Text();
            IFileService fileService = new FileService();
            ITextService textService = new TextService();
            int choose;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("1-Get text\n" +
                    "2-get words in interrogative sentences of given length\n" +
                    "3-sort sentences\n" +
                    "4-replace words\n" +
                    "5-remove words start with consonant of given length\n" +
                    "0-exit");
                choose = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choose)
                {
                    case 1:
                        {
                            //if (text != null)
                            //{
                            ICollection<string> s = fileService.GetData(app["text"], "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                            //}

                            text = parser.ParseText(s);
                            var conc = textService.Concordance(text).ToDictionary(x => x.Word.ToString());
                            var m = new MorphAnalyzer(withLemmatization: true);
                            var results = m.Parse(conc.Select(x => x.Key)).ToList();
                            Dictionary<string, IList<ConcordanceItem>> pairs = new Dictionary<string, IList<ConcordanceItem>>();

                            foreach (var morphInfo in results)
                            {
                                var mainWord = morphInfo;
                                bool checkout = false;
                                foreach (var item in pairs)
                                {
                                    if (item.Value.Select(x => x.Word.ToString()).Contains(mainWord.Text))
                                    {
                                        checkout = true;
                                        break;
                                    }
                                }
                                if (checkout)
                                {
                                    continue;
                                }
                                IList<ConcordanceItem> strings = new List<ConcordanceItem>();
                                foreach (var item in results)
                                {
                                    if (mainWord.CanBeSameLexeme(item))
                                    {
                                        //Console.WriteLine($"{mainWord.Text} --- {item.Text}");
                                        strings.Add(conc[item.Text]);
                                    }
                                }
                                pairs.Add(mainWord.Text, strings);
                            }

                            foreach (var item in pairs)
                            {
                                Console.WriteLine($"{string.Join('/', item.Value.Select(x => x.Word.ToString()))} --- " +
                                    $"{item.Value.Select(x => x.Count).Sum()}");
                            }
                            foreach (var item in text.Sentences)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            var temp = textService.GetInterrogativeSentencesWordsWithLength(text.Sentences, length);
                            if (temp != null)
                            {
                                foreach (var item in temp)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not found");
                            }
                            break;
                        }
                    case 3:
                        {
                            text.Sentences = textService.SortSentences(text.Sentences);
                            Console.WriteLine(text);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            Console.Write("Enter word to replace ");
                            string newWord = Console.ReadLine();
                            foreach (var item in text.Sentences)
                                textService.ReplaceWords(item, length, newWord);
                            Console.WriteLine(text);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            text.Sentences = textService.RemoveWordsStartsWithConsonants(text.Sentences, length);
                            Console.WriteLine(text);
                            break;
                        }
                    case 0:
                        {
                            flag = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            fileService.Write(text, app["answer"]);
        }
    }
}
