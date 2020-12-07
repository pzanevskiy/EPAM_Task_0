using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public class Gift
    {
        private string _name;
        private ICollection<Sweet> _sweets;

        public string Name
        {
            get => _name;
            set => _name = value.Equals("") ? "New gift" : value;            
        }

        public ICollection<Sweet> Sweets
        {
            get => _sweets;
            set => _sweets = value.Count == 0 ? _sweets = new List<Sweet> { new Candy("",2,3,4) } : value;            
        }

        public Gift()
        {

        }

        public Gift(string name, List<Sweet> sweets)
        {
            Name = name;
            Sweets = sweets;
        }
    }
}
