using System;

namespace SponsorshipBot.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CommandAttribute : Attribute
    {
        public string[] CommandTexts { get { return commandTexts; } }
        private readonly string[] commandTexts;

        public CommandAttribute(params string[] commandTexts)
        {
            this.commandTexts = commandTexts;
        }
    }
}
