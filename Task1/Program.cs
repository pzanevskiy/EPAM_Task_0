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
            Sweet chocolateCandy=new ChocolateCandy("",23,45,55);
            Sweet chocolateCandy1=new ChocolateCandy("",22,40,555);
            Sweet brittleCandy = new BrittleСandy("", 10, 42, 11);
            Sweet brittleCandy1 = new BrittleСandy("", 8, 80, 5);
            Sweet liquorCandy = new LiquorCandy("", 7, 5, 555);
            Sweet curdCookie = new CurdCookie("", 111, "", 2);
            Sweet chocolateCookie = new ChocolateCookie("", 75, "", 10);
            Sweet oatCoockie = new OatCookie("", 30, "");
            Sweet chocolate = new Chocolate("",200,25,50);
            Gift gift = new Gift("", new List<Sweet>());
            gift.Sweets.Add(new Marshmallow("",1,"",10));
            gift.Sweets.Add(chocolateCandy);
            gift.Sweets.Add(chocolateCandy1);
            gift.Sweets.Add(brittleCandy);
            gift.Sweets.Add(brittleCandy1);
            gift.Sweets.Add(liquorCandy);
            gift.Sweets.Add(curdCookie);
            gift.Sweets.Add(chocolateCookie);
            gift.Sweets.Add(oatCoockie);
            gift.Sweets.Add(chocolate);
            IGiftService giftService = new GiftService();
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
        }
    }
}
