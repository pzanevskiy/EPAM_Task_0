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
            Console.WriteLine("All sweets");
            giftService.PrintAll(gift.Sweets);
            Console.WriteLine();
            Console.WriteLine("All sweets min max sugar");
            giftService.PrintAll(giftService.SearchSugar(gift.Sweets, 40, 50));
            Console.WriteLine();
            Console.WriteLine("All sweets sorted by weight");
            giftService.PrintAll(giftService.SortByWeight(gift.Sweets));
            Console.WriteLine();
            Console.WriteLine("Gift weight");
            Console.WriteLine($"{giftService.CalculateWeight(gift.Sweets)}g");
            Console.WriteLine();

            giftService.PrintAll(giftService.SortOnlySugarable(gift.Sweets));
            Console.WriteLine();
            giftService.PrintAll(giftService.SortOnlyChocolable(gift.Sweets));

        }
    }
}
