using FancyConsole.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands.BaseCommands {
    public class HelpCommand : ICommand {
        public string Name => "Help";
        public string Description => "Get information about a command";
        public string[] Invokers => ["info", "help"];
        public string ArgsDescription => "What command to get information about";
        public int MinimumParameterCount => 1;



        public CommandResults? Perform(string[] args, CommandContextBase context) {
            string infoCommand = CommandManager.Sanitize(args[1]);

            //If parameter for command info is empty, then it is going to be ignored
            if (infoCommand == null || infoCommand == "" || infoCommand == string.Empty) return null;

            if (CommandManager.FindCommand(infoCommand) is ICommand foundCMD && foundCMD is not null) {
                Print.Pause($"[{foundCMD.Name}] {foundCMD.Description}\n" +
                    $"        [required info]{ArgsDescription}");
            }

            return null;
        }
    }
}
