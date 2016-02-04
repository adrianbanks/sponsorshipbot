using System;
using System.Linq;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("add", "new")]
    public class AddSponsorCommand : CommandBase
    {
        private readonly ConferenceRepository conferenceRepository = new ConferenceRepository();
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public AddSponsorCommand(SlackMessage message, string[] commandArguments) : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            if (!commandArguments.Any())
            {
                return "C'mon - you at least need to give me a name when adding a sponsor :unamused:";
            }

            var sponsor = new Sponsor
            {
                Name = commandArguments[0],
                AddedBy = message.user_name
            };

            if (commandArguments.Length == 2)
            {
                var amountPledgedStr = commandArguments[1];
                var amountPledged = DecimalEx.Parse(amountPledgedStr);
                sponsor.AmountPledged = amountPledged;
            }

            var conference = conferenceRepository.GetCurrentConference();
            sponsor.ConferenceId = conference.Id;

            sponsorRepository.AddSponsor(sponsor);
            return ":thumbsup: Sponsor added";
        }
    }
}
