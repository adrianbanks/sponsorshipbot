namespace SponsorshipBot.Models
{
    public class Totals
    {
        public int Id { get; set; }
        public decimal AmountNeeded { get; set; }
        public decimal StartingBalance { get; set; }
        public int ConferenceId { get; set; }
    }
}
