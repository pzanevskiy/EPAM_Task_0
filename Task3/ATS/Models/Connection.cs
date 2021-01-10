using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models.Interfaces;

namespace Task3.ATS.Models
{
    public class Connection
    {
        public IPhoneNumber From { get; set; }
        public IPhoneNumber To { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Connection connection &&
                   EqualityComparer<IPhoneNumber>.Default.Equals(From, connection.From) &&
                   EqualityComparer<IPhoneNumber>.Default.Equals(To, connection.To);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }

        public override string ToString()
        {
            return $"{From} --> {To}";
        }
    }
}
