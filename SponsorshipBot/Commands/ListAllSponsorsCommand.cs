using System.Linq;
using System.Text;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("all")]
    public class ListAllSponsorsCommand : CommandBase
    {
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public ListAllSponsorsCommand(SlackMessage message) : base(message)
        {
        }

        public override string Execute()
        {
            var allSponsors = sponsorRepository.GetAllSponsors().ToList();

            if (!allSponsors.Any())
            {
                return "No sponsors yet!";
            }

            var text = new StringBuilder();
            text.AppendLine("Current sponsors:");
            text.AppendLine();
            decimal total = 0;

            foreach (var sponsor in allSponsors)
            {
                if (sponsor.AmountReceived.HasValue)
                {
                    text.AppendFormat("    {0} has already paid `£{1}`", sponsor.Name, sponsor.AmountReceived.Value);
                    total += sponsor.AmountReceived.Value;
                }
                else if (sponsor.AmountPledged.HasValue)
                {
                    text.AppendFormat("    {0} has pledged `£{1}`", sponsor.Name, sponsor.AmountPledged.Value);
                    total += sponsor.AmountPledged.Value;
                }
                else
                {
                    text.AppendFormat("    {0} has agreed to be a sponsor", sponsor.Name);
                }
            }

            text.AppendLine();
            text.AppendLine();
            text.AppendLine("Total: `£" + total + "`");
            return text.ToString();
        }
    }
}
