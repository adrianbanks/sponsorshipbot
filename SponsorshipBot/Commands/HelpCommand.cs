using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("?", "help")]
    public class HelpCommand : CommandBase
    {
        public HelpCommand(SlackMessage message, string[] commandArguments) : base(message, commandArguments)
        {
        }

        public override string Execute()
        {
            const string helpText = @"
`<required parameter>`
`(optional parameter)`

`totals` Show the sponsorship totals so far
`totals total=<amount>` Update the total amount needed
`totals start=<amount>` Update the starting balance
`add <name> (amount pledged)` Add a new sponsor
`update <name> pledged=<amount>` Update the amount pledged by a sponsor
`update <name> received=<amount>` Update the amount received from a sponsor
`remove <name> Remove a sponsor
`all` Show all sponsors
";
            return helpText;
        }
    }
}
