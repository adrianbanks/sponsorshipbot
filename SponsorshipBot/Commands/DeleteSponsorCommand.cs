using System.Linq;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("delete", "remove")]
    public class DeleteSponsorCommand : CommandBase
    {
        private readonly ConferenceRepository conferenceRepository = new ConferenceRepository();
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

            var conference = conferenceRepository.GetCurrentConference();
            sponsorRepository.DeleteSponsor(conference.Id, sponsorName);

            return ":thumbsup: Sponsor removed";
        }
    }
}
