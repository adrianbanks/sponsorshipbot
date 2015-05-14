using System;

namespace SponsorshipBot.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CommandAttribute : Attribute
    {
        public string CommandText { get { return commandText; } }
        private readonly string commandText;

        public CommandAttribute(string commandText)
        {
            this.commandText = commandText;
        }
    }
}
