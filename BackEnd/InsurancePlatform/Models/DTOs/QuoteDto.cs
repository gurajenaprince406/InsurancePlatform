public class QuoteDto
{
    public DateTime CoverStartDate { get; set; }
    public DateTime CoverEndDate { get; set; }
    public string PolicyType { get; set; } // "Monthly" or "Annual"
    public string Branch { get; set; } // e.g., "Cape Town", "Durban"
}
