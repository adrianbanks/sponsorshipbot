using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    public class CommandParser
    {
        public ICommand ParseCommand(SlackMessage message)
        {
            return new ListAllSponsorsCommand();
        }
    }
}
