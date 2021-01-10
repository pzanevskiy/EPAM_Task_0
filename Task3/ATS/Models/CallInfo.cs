using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Task3.ATS.Models.Interfaces;
using Task3.BillingSystems.Models.Interfaces;
using Task3.Enums;

namespace Task3.ATS.Models
{
    public class CallInfo
    {
        public IUser User { get; set; }
        public IPhoneNumber From { get; set; }
        public IPhoneNumber To { get; set; }
        public DateTime DateTimeStart { get; set; }
        public TimeSpan Duration { get; set; }
        public double Cost { get; set; }
        public CallState CallState { get; set; }

        public CallInfo Copy()
        {
            return new CallInfo
            {
                From=From,
                To=To,
                DateTimeStart=DateTimeStart,
                Duration=Duration
            };
        }

        public override string ToString()
        {
            return $"From: {From}\t" +
                $"To: {To}\n" +
                $"Started at: {DateTimeStart:F}\t" +
                $"Duration: {Duration:hh\\:mm\\:ss}\n" +
                $"State: {CallState}\t" +
                $"Cost: {Cost:F2}";
        }
    }
}
