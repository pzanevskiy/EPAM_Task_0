using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Models;
using Task1.Service;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sweet sweet=new Candy();
            Gift gift = new Gift("", new List<Sweet>());
            gift.Sweets.Add(new Marshmallow("",1,9,""));
            IGiftService giftService = new GiftService();
            Console.WriteLine("All sweets");
            giftService.PrintAll(gift.Sweets);
            Console.WriteLine("All sweets min max sugar");
            giftService.PrintAll(giftService.SearchSugar(gift.Sweets,2,5));
            Console.WriteLine("All sweets sorted by weight");
            giftService.PrintAll(giftService.SortByWeight(gift.Sweets));
            Console.WriteLine("Gift weight");
            Console.WriteLine(giftService.CalculateWeight(gift.Sweets));
            Console.WriteLine();
        }
    }
}
