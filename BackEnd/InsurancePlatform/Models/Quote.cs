
using System.Collections.Generic;


    public class Quote
    {
        public int QuoteId { get; set; }
        public DateTime CoverStartDate { get; set; }
        public DateTime CoverEndDate { get; set; }
        public DateTime QuoteCreationDate { get; set; } = DateTime.Now;
        public string PolicyType { get; set; }
        public string Branch { get; set; }
        public string QuoteNumber { get; set; }
        public List<MotorRisk> MotorRisks { get; set; } // Relationship
    }



