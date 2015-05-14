using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly SlackMessage message;

        protected CommandBase(SlackMessage message)
        {
            this.message = message;
        }

        public abstract string Execute();
    }
}
