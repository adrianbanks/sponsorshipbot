using System;
using System.Linq;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("update")]
    public class UpdateSponsorCommand : CommandBase
    {
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public UpdateSponsorCommand(SlackMessage message, string[] commandArguments)
            : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            if (!commandArguments.Any())
            {
                return "How d'you expect me to update a sponsor when you don't tell me which one? :confused:";
            }

            if (commandArguments.Length < 2)
            {
                return "How d'you expect me to update a sponsor when you don't tell me what to update? :confused:";
            }

            var sponsorName = commandArguments[0];
            var monetaryAmountParameter = commandArguments[1];

            var sponsor = sponsorRepository.Get(sponsorName);

            if (monetaryAmountParameter.StartsWith("pledged=", StringComparison.CurrentCultureIgnoreCase))
            {
                var pledgedAmountStr = monetaryAmountParameter.Substring(8);
                sponsor.AmountPledged = DecimalEx.ParseNullable(pledgedAmountStr);
            }

            if (monetaryAmountParameter.StartsWith("received=", StringComparison.CurrentCultureIgnoreCase))
            {
                var receivedAmountStr = monetaryAmountParameter.Substring(9);
                sponsor.AmountReceived = DecimalEx.ParseNullable(receivedAmountStr);
            }

            sponsorRepository.UpdateSponsor(sponsor);
            return ":thumbsup: Sponsor updated";
        }
    }
}
