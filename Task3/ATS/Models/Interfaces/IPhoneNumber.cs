using System;
using System.Collections.Generic;
using System.Text;
using Task3.ATS.Models;

namespace Task3.ATS.Models.Interfaces
{
    public interface IPhoneNumber  :IEquatable<PhoneNumber>
    {
        string Number { get; }
    }
}
