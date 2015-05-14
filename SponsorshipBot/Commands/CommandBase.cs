using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly SlackMessage message;
        protected readonly string[] commandArguments;

        protected CommandBase(SlackMessage message, string[] commandArguments)
        {
            this.message = message;
            this.commandArguments = commandArguments;
        }

        public abstract string Execute();
    }
}
