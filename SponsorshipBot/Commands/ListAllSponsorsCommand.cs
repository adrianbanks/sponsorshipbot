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

            foreach (var sponsor in allSponsors)
            {
                text.AppendLine("    " + sponsor.Name);
            }

            return text.ToString();
        }
    }
}
