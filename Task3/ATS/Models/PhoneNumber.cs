using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Task3.ATS.Models.Interfaces;

namespace Task3.Models
{
    public class PhoneNumber : IPhoneNumber
    {
        private string _phoneNumber;

        public string Number => _phoneNumber;

        public PhoneNumber()
        {

        }

        public PhoneNumber(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
        }

        public bool Equals([AllowNull] PhoneNumber other)
        {
            return this._phoneNumber == other._phoneNumber;           
        }

        public override bool Equals(object obj)
        {
            return this._phoneNumber == (obj as PhoneNumber)._phoneNumber ? true : false;
        }

        public override int GetHashCode()
        {
            return _phoneNumber.GetHashCode();
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
