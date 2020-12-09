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
            Sweet sweet=new ChocolateCandy("",23,45,55);
            Sweet sweet1=new ChocolateCandy("",22,40,555);
            Sweet sweet2 = new BrittleСandy("", 10, 42, 11);
            Sweet sweet3 = new BrittleСandy("", 8, 80, 5);
            Sweet sweet4 = new LiquorCandy("", 7, 5, 555);
            Sweet sweet5 = new CurdCookie("", 111, "", 2);
            Sweet sweet6 = new ChocolateCookie("", 75, "", 10);
            Sweet sweet7 = new OatCookie("", 30, "");
            Sweet sweet8 = new Chocolate("",200,25,50);
            Gift gift = new Gift("", new List<Sweet>());
            gift.Sweets.Add(new Marshmallow("",1,""));
            gift.Sweets.Add(sweet);
            gift.Sweets.Add(sweet1);
            gift.Sweets.Add(sweet2);
            gift.Sweets.Add(sweet3);
            gift.Sweets.Add(sweet4);
            gift.Sweets.Add(sweet5);
            gift.Sweets.Add(sweet6);
            gift.Sweets.Add(sweet7);
            gift.Sweets.Add(sweet8);
            IGiftService giftService = new GiftService();
            Console.WriteLine("All sweets");
            giftService.PrintAll(gift.Sweets);
            Console.WriteLine();
            Console.WriteLine("All sweets min max sugar");
            giftService.PrintAll(giftService.SearchSugar(gift.Sweets, 0, 100));
            Console.WriteLine();
            Console.WriteLine("All sweets sorted by weight");
            giftService.PrintAll(giftService.SortByWeight(gift.Sweets));
            Console.WriteLine();
            Console.WriteLine("Gift weight");
            Console.WriteLine(giftService.CalculateWeight(gift.Sweets));
            Console.WriteLine();
        }
    }
}
