using System;
using System.Linq;
using System.Text;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("total", "totals")]
    public class TotalsCommand : CommandBase
    {
        private readonly ConferenceRepository conferenceRepository = new ConferenceRepository();
        private readonly TotalsRepository totalsRepository = new TotalsRepository();
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public TotalsCommand(SlackMessage message, string[] commandArguments) : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            if (commandArguments.Any())
            {
                var conference = conferenceRepository.GetCurrentConference();

                var monetaryArgument = commandArguments[0];

                if (monetaryArgument.StartsWith("total=", StringComparison.CurrentCultureIgnoreCase))
                {
                    var totalNeededStr = monetaryArgument.Substring(6);
                    decimal totalNeeded = DecimalEx.Parse(totalNeededStr);
                    totalsRepository.UpdateTotalNeeded(conference.Id, totalNeeded);
                }

                if (monetaryArgument.StartsWith("start=", StringComparison.CurrentCultureIgnoreCase))
                {
                    var startingBalanceStr = monetaryArgument.Substring(6);
                    decimal startingBalance = DecimalEx.Parse(startingBalanceStr);
                    totalsRepository.UpdateStartingBalance(conference.Id, startingBalance);
                }
            }

            return ShowTotals();
        }

        private string ShowTotals()
        {
            var conference = conferenceRepository.GetCurrentConference();

            var totals = totalsRepository.GetTotals(conference.Id);
            var startingBalance = totals.StartingBalance;
            var amountNeeded = totals.AmountNeeded;

            var allSponsors = sponsorRepository.GetAllSponsors(conference.Id);
            var amountPledged = allSponsors.Where(sponsor => sponsor.AmountPledged.HasValue)
                                           .Sum(sponsor => sponsor.AmountPledged.Value);
            var shortfall = amountNeeded - startingBalance - amountPledged;

            var response = new StringBuilder();

            response.AppendLine("Total needed: `£" + amountNeeded + "`");
            response.AppendLine("Starting balance: `£" + startingBalance + "`");
            response.AppendLine("Total pledged: `£" + amountPledged + "`");
            response.AppendLine();

            if (shortfall > 0)
            {
                response.AppendLine("Shortfall: `£" + Math.Abs(shortfall) + "` :worried:");
            }
            else
            {
                response.AppendLine("Surplus: `£" + Math.Abs(shortfall) + "` :relaxed:");
            }

            return response.ToString();
        }
    }
}