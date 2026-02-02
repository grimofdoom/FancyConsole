using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands {
    public interface ICommand {
        /// <summary>Name of command, used for information not for calling</summary>
        public abstract string Name { get; }
        /// <summary>Description of command used for more information</summary>
        public abstract string Description { get; }
        /// <summary>Let the user/player know what kind of arguments are needed to perform command</summary>
        public abstract string ArgsDescription { get; }
        /// <summary>All the ways in which a command can be called from</summary>
        public abstract string[] Invokers { get; }
        /// <summary>Minimum amount of parameters provided by user/player, to invoke the command</summary>
        public abstract int MinimumParameterCount { get; }



        /// <summary>What will be called when the command is performed</summary>
        /// <param name="args">Parameters provided by user/player</param>
        /// <param name="context">Information to directly pass to command</param>
        /// <returns>End results of the command, return null if not needed</returns>
        public abstract CommandResults? Perform(string[] args, CommandContextBase context);
    }
}
