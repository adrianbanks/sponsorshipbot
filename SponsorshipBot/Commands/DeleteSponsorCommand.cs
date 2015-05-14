using System.Linq;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("delete", "remove")]
    public class DeleteSponsorCommand : CommandBase
    {
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public DeleteSponsorCommand(SlackMessage message, string[] commandArguments)
            : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            if (!commandArguments.Any())
            {
                return "How d'you expect me to remove a sponsor when you don't tell me which one? :confused:";
            }

            var sponsorName = commandArguments[0];
            sponsorRepository.DeleteSponsor(sponsorName);

            return ":thumbsup: Sponsor removed";
        }
    }
}
