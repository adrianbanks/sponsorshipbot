using System.Linq;
using System.Text;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("list", "all")]
    public class ListAllSponsorsCommand : CommandBase
    {
        private readonly ConferenceRepository conferenceRepository = new ConferenceRepository();
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public ListAllSponsorsCommand(SlackMessage message, string[] commandArguments) : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            var conference = conferenceRepository.GetCurrentConference();
            var allSponsors = sponsorRepository.GetAllSponsors(conference.Id).ToList();

            if (!allSponsors.Any())
            {
                return "No sponsors yet :worried:";
            }

            var text = new StringBuilder();
            text.AppendLine("Current sponsors:");
            text.AppendLine();
            decimal totalPledged = 0;
            decimal totalReceived = 0;

            foreach (var sponsor in allSponsors)
            {
                if (sponsor.AmountPledged.HasValue && sponsor.AmountReceived.HasValue && sponsor.AmountPledged.Value == sponsor.AmountReceived.Value)
                {
                    text.AppendFormat("    {0} has already paid `£{1}`", sponsor.Name, sponsor.AmountReceived.Value);
                    totalPledged += sponsor.AmountPledged.Value;
                    totalReceived += sponsor.AmountReceived.Value;
                }
                else if (sponsor.AmountPledged.HasValue && sponsor.AmountReceived.HasValue)
                {
                    text.AppendFormat("    {0} has pledged `£{1}` and already paid `£{2}`", sponsor.Name, sponsor.AmountPledged.Value, sponsor.AmountReceived.Value);
                    totalPledged += sponsor.AmountPledged.Value;
                    totalReceived += sponsor.AmountReceived.Value;
                }
                else if (sponsor.AmountPledged.HasValue)
                {
                    text.AppendFormat("    {0} has pledged `£{1}`", sponsor.Name, sponsor.AmountPledged.Value);
                    totalPledged += sponsor.AmountPledged.Value;
                }
                else
                {
                    text.AppendFormat("    {0} has agreed to be a sponsor", sponsor.Name);
                }

                text.AppendLine();
            }

            text.AppendLine();
            text.AppendLine("Total pledged: `£" + totalPledged + "`");
            text.AppendLine("Total received: `£" + totalReceived + "`");
            return text.ToString();
        }
    }
}
