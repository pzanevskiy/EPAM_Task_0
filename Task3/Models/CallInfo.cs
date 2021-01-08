using System;
using System.Collections.Generic;
using System.Text;
using Task3.Models.Enums;

namespace Task3.Models
{
    public class CallInfo
    {
        public Terminal Terminal { get; set; }
        public PhoneNumber From { get; set; }
        public PhoneNumber To { get; set; }
        public DateTime DateTimeStart { get; set; }
        public TimeSpan Duration { get; set; }
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
            return $"From {From} " +
                $"to {To}\t" +
                $"Started at: {string.Format("{0:F}", DateTimeStart)}\t" +
                $"Duration: {string.Format("{0:hh\\:mm\\:ss}", Duration)}\t" +
                $"State: {CallState}";
        }
    }
}
