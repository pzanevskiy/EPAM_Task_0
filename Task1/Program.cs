using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Models;
using Task1.Models.Sweets;
using Task1.Models.Sweets.Candies;
using Task1.Models.Sweets.Cookies;
using Task1.Service;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<Sweet> sweets = new List<Sweet>
            {
                new ChocolateCandy("",23,45,55),
                new ChocolateCandy("",22,40,555),
                new BrittleСandy("", 10, 42, 11),
                new BrittleСandy("", 8, 80, 5),
                new LiquorCandy("", 7, 5, 555),
                new CurdCookie("", 111, "", 2),
                new ChocolateCookie("", 75, "", 10),
                new OatCookie("", 30, ""),
                new Chocolate("", 200, 25, 50),
                new Marshmallow("",1,"",10)
            };    
            
            Gift gift = new Gift();           
            IGiftService giftService = new GiftService();
            giftService.AddSweetsToGift(gift, sweets);
            int choose;
            bool flag = true;
            while (flag)
            {
                
                Console.WriteLine("1-print all sweets in gift\n" +
                    "2-Search by sugar value\n" +
                    "3-Sort by weight\n" +
                    "4-Get total gift weight\n" +
                    "5-Sort sweets which have sugar\n" +
                    "6-Sort sweets which have chocolate\n");

                choose = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choose)
                {                  
                    case 1:
                        {
                            Console.WriteLine("All sweets");
                            giftService.PrintAll(gift.Sweets);
                            break;
                        }
                    case 2:
                        {                            
                            Console.Write("Min value");
                            int min = int.Parse(Console.ReadLine());
                            Console.Write("Max value"); 
                            int max = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            giftService.PrintAll(giftService.SearchSugar(gift.Sweets, min, max));
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("All sweets sorted by weight");
                            giftService.PrintAll(giftService.SortByWeight(gift.Sweets));
                            Console.WriteLine();
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Gift weight");
                            Console.WriteLine($"{giftService.CalculateWeight(gift.Sweets)}g");
                            Console.WriteLine();
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Sort sweets which have sugar");
                            giftService.PrintAll(giftService.SortOnlySugarable(gift.Sweets));
                            Console.WriteLine();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Sort sweets which have chocolate");
                            giftService.PrintAll(giftService.SortOnlyChocolable(gift.Sweets));
                            Console.WriteLine();
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
            
            Console.WriteLine();
            
        }
    }
}
