namespace SponsorshipBot.Models
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? AmountPledged { get; set; }
        public decimal? AmountReceived { get; set; }
        public string AddedBy { get; set; }
        public int ConferenceId { get; set; }
    }
}
