using FancyConsole.Logging;
using FancyConsole.Text;

namespace FancyConsole.Commands {
    public abstract class CommandSetBase {
        /// <summary>What layers are these commands going to be accessible from</summary>
        public LayerSet Layers { get; } = new();

        /// <summary>Name of the command set</summary>
        public abstract string Name { get; }

        /// <summary>All commands registered, by individual invokers</summary>
        private Dictionary<string, ICommand> Commands { get; } = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>Register a new command to the command set</summary>
        public void Register(ICommand command) {
            foreach (string invoker in command.Invokers) {
                if (Commands.ContainsKey(invoker)) {
                    Log.Error($"Could not register <{command.Name}> Command to <{Name}> command set under invoker <{invoker}>, as invoker already exists.");
                }

                //Sanitize and add
                string sanitizeInvoker = CommandManager.Sanitize(invoker);
                Commands[sanitizeInvoker] = command;
            }
        }

        /// <summary>Remove just a single invoker for a command</summary>
        public void Remove(string invoker) {
            Commands.Remove(invoker);
        }

        /// <summary>Remove all instances of a command</summary>
        public void Remove(ICommand command) {
            foreach(string invoker in command.Invokers) {
                Commands.Remove(invoker);
            }
        }

        /// <summary>Get command by invoker</summary>
        /// <returns>Null if no command found</returns>
        public ICommand? GetCommand(string invoker) {
            //If command found, and not null, return command
            if (Commands.TryGetValue(invoker, out ICommand? command) && command != null) return command;
            //No command found, return null
            return null;
        }
    }
}
