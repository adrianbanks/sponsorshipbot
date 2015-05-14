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
            var commandParts = GetCommandParts(message.text);
            var mainCommand = commandParts.Item1;
            var commandArguments = commandParts.Item2;

            if (string.IsNullOrWhiteSpace(mainCommand))
            {
                return new ListAllSponsorsCommand(message, commandArguments);
            }

            var matchingCommands = FindAllCommandTypes().Where(t => MatchesCommandText(t, mainCommand)).ToList();

            if (!matchingCommands.Any())
            {
                throw new Exception("Unknown command: " + mainCommand);
            }

            if (matchingCommands.Count > 1)
            {
                throw new Exception("Multiple possible commands found: " + mainCommand);
            }

            var command = (ICommand)Activator.CreateInstance(matchingCommands.Single(), message, commandArguments);
            return command;
        }

        private Tuple<string, string[]> GetCommandParts(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Tuple.Create(string.Empty, new string[0]);
            }

            var parts = text.Split(' ');
            return Tuple.Create(parts[0], parts.Skip(1).ToArray());
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
