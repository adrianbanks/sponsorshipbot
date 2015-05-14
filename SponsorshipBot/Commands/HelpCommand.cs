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
<required parameter>
(optional parameter)

`totals` Show the sponsorship totals so far
`all` Show all sponsors
";
            return helpText;
        }
    }
}
