using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Commands {
    public sealed class LayerSet {
        public HashSet<Layer> layers = [];

        public void Add(Layer layer) => layers.Add(layer);
        public void Remove(Layer layer) => layers.Remove(layer);
        public void Clear() => layers.Clear();
        public bool Contains(Layer layer) => layers.Contains(layer);
    }
}
