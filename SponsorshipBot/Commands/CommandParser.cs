﻿using System;
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

            var matchingCommands = FindAllCommands().Where(t => MatchesCommandText(t, commandText)).ToList();

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

        private bool MatchesCommandText(Type commandType, string commandText)
        {
            var attribute = commandType.GetCustomAttribute<CommandAttribute>();
            return attribute != null && string.Equals(commandText, attribute.CommandText);
        }

        private string GetMainCommandText(string text)
        {
            var parts = text.Split(' ');
            return parts[0];
        }

        private IEnumerable<Type> FindAllCommands()
        {
            var allCommands = GetType().Assembly
                                       .GetTypes()
                                       .Where(t => typeof(ICommand).IsAssignableFrom(t));
            return allCommands;
        }
    }
}
