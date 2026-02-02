using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands {
    public sealed class Layer (string ID) {
        public string ID { get; } = ID;


        private static Dictionary<string, Layer> Registry { get; } = new(StringComparer.OrdinalIgnoreCase);

        public static Layer Define(string ID) {
            //If layer already exists, get the exact object instead handling string perfect checks
            //This should incredibly rarely ever happen
            if (Registry.TryGetValue(ID, out Layer? foundLayer) && foundLayer is not null) {
                return foundLayer;
            }

            //No layer exists, so one is properly made
            Layer newLayer = new(ID);
            Registry.Add(ID, newLayer);
            return newLayer;
        }
    }

    public static class CommonLayers {
        public static Layer All { get; } = Layer.Define("All");
        public static Layer Settings { get; } = Layer.Define("Settings");
    }
}
