using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Services {
    public sealed class Service(object t, string identifier) {
        private object classObj = t;
        public string identifier { get; } = identifier;

        public Type type = t.GetType();

        public T GetObj<T>() {
            return (T)classObj;
        }
    }
}
