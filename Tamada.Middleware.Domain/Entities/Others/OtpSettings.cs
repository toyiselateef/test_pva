using System;
 
    public class OtpSettings
    {
        public int DurationInSeconds { get; set; }
        public int OtpSize { get; set; }
        public IFormatProvider? SmsTemplate { get; set; }
    }
 