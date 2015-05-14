using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    [Command("help")]
    public class HelpCommand : CommandBase
    {
        public HelpCommand(SlackMessage message) : base(message)
        {
        }

        public override string Execute()
        {
            const string helpText = @"
`total` Show the total amount pledged so far
`needed` Set the total amount of sponsorship needed
`all` Show all sponsors
`add <name> (amount)` Add a new sponsor
";
            return helpText;
        }
    }
}