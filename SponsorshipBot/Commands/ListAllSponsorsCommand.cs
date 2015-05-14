using System.Linq;
using System.Text;
using SponsorshipBot.DataAccess;

namespace SponsorshipBot.Commands
{
    public class ListAllSponsorsCommand : ICommand
    {
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public string Execute()
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
