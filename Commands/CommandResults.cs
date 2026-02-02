using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands {
    public class CommandResults {
        public CommandStatus Status { get; }

        public string Message { get; } = "";

        public CommandResults(CommandStatus status) {
            this.Status = status;
        }

        public CommandResults(CommandStatus status, string message) {
            this.Status = status;
            this.Message = message;
        }
    }

    public enum CommandStatus {
        //Good
        PERFORMED,

        //Issues/errors
        FAILED,
        NOTFOUND,
        NOLAYER,
        NOINPUT,
        LOWARGS
    }
}
