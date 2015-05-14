using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SponsorshipBot.Models;

namespace SponsorshipBot.Commands
{
    public class CommandParser
    {
        public ICommand ParseCommand(SlackMessage message)
        {
            var commandText = GetMainCommandText(message.text);

            if (string.IsNullOrWhiteSpace(commandText))
            {
                return new ListAllSponsorsCommand(message);
            }

            var matchingCommands = FindAllCommandTypes().Where(t => MatchesCommandText(t, commandText)).ToList();

            if (!matchingCommands.Any())
            {
                throw new Exception("Unknown command: " + commandText);
            }

            if (matchingCommands.Count > 1)
            {
                throw new Exception("Multiple possible commands found: " + commandText);
            }

            var command = (ICommand) Activator.CreateInstance(matchingCommands.Single(), message);
            return command;
        }

        private string GetMainCommandText(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? string.Empty : text.Split(' ')[0];
        }

        private bool MatchesCommandText(Type commandType, string commandText)
        {
            var attribute = commandType.GetCustomAttribute<CommandAttribute>();
            return attribute != null && attribute.CommandTexts.Any(t => string.Equals(commandText, t, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<Type> FindAllCommandTypes()
        {
            var allCommands = GetType().Assembly
                                       .GetTypes()
                                       .Where(t => typeof(ICommand).IsAssignableFrom(t));
            return allCommands;
        }
    }
}
