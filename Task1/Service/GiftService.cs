using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task1.Models;
using Task1.Models.Interfaces;

namespace Task1.Service
{
    public class GiftService : IGiftService
    {
        public void PrintAll(ICollection<Sweet> sweets)
        {
            foreach (IPrintable printable in sweets)
            {
                printable.Print();
            }
        }

        public double CalculateWeight(ICollection<Sweet> sweets)
        {
            return sweets.Sum(x => x.Weight);
        }

        public ICollection<Sweet> SearchSugar(ICollection<Sweet> sweets, double min, double max)
        {
            return sweets.Where(x => x is ISugarable sugarable && sugarable.Sugar >= min && sugarable.Sugar <= max).ToList();
        }

        public ICollection<Sweet> SortByWeight(ICollection<Sweet> sweets)
        {
            return sweets.OrderBy(x => x.Weight).ToList();
        }
    }
}
