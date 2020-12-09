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
            set => _sweets = value;           
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
