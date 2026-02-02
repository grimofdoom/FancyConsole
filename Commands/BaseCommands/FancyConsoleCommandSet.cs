using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands.BaseCommands {
    public class FancyConsoleCommandSet : CommandSetBase {
        public override string Name => "FancyConsole";

        public FancyConsoleCommandSet() {
            Layers.Add(CommonLayers.All);

            Register(new HelpCommand());
        }
    }
}
