using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;

namespace Task1.Service
{
    public interface IGiftService 
    {
        public void PrintAll(ICollection<Sweet> sweets);
        public double CalculateWeight(ICollection<Sweet> sweets);
        public ICollection<Sweet> SearchSugar(ICollection<Sweet> sweets,double min,double max);
        public ICollection<Sweet> SortByWeight(ICollection<Sweet> sweets);
        public ICollection<Sweet> SortOnlySugarable(ICollection<Sweet> sweets);
        public ICollection<Sweet> SortOnlyChocolable(ICollection<Sweet> sweets);
        public void AddSweetsToGift(Gift gift,ICollection<Sweet> sweets);
    }
}
