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
        private readonly TotalsRepository totalsRepository = new TotalsRepository();
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public TotalsCommand(SlackMessage message, string[] commandArguments) : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            if (commandArguments.Any())
            {
                if (commandArguments[0].StartsWith("total=", StringComparison.CurrentCultureIgnoreCase))
                {
                    var totalNeededStr = commandArguments[0].Substring(6);
                    decimal totalNeeded = DecimalEx.Parse(totalNeededStr);
                    totalsRepository.UpdateTotalNeeded(totalNeeded);
                }

                if (commandArguments[0].StartsWith("start=", StringComparison.CurrentCultureIgnoreCase))
                {
                    var startingBalanceStr = commandArguments[0].Substring(6);
                    decimal startingBalance = DecimalEx.Parse(startingBalanceStr);
                    totalsRepository.UpdateStartingBalance(startingBalance);
                }
            }

            return ShowTotals();
        }

        private string ShowTotals()
        {
            var totals = totalsRepository.GetTotals();
            var startingBalance = totals.StartingBalance;
            var amountNeeded = totals.AmountNeeded;

            var allSponsors = sponsorRepository.GetAllSponsors();
            var amountPledged = allSponsors.Where(sponsor => sponsor.AmountPledged.HasValue)
                                           .Sum(sponsor => sponsor.AmountPledged.Value);
            var shortfall = amountNeeded - startingBalance - amountPledged;

            var response = new StringBuilder();

            response.AppendLine("Total needed: `£" + amountNeeded + "`");
            response.AppendLine("Starting balance: `£" + startingBalance + "`");
            response.AppendLine("Total pledged: `£" + amountPledged + "`");
            response.AppendLine();
            response.AppendLine((shortfall > 0 ? "Shortfall" : "Surplus") + ": `£" + Math.Abs(shortfall) + "`");

            return response.ToString();
        }
    }
}