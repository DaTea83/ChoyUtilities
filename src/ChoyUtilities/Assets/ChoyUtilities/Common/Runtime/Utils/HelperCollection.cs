using System;
using System.Collections.Generic;

namespace ChoyUtilities {

    public static partial class HelperCollection {

        public static class Broadcaster {

            private static readonly Dictionary<string, Action> Actions = new();
            
            public static int Count => Actions.Count;

            public static void Register(string key, Action action) {
                if (string.IsNullOrEmpty(key) || action == null) return;
                Actions[key] = action;
            }

            public static void UnRegister(string key) {
                if (string.IsNullOrEmpty(key)) return;
                Actions.Remove(key);
            }

            public static void Call(string key) {
                if (string.IsNullOrEmpty(key)) return;

                if (Actions.TryGetValue(key, out var action))
                    action();
            }

            public static void Clear() {
                Actions.Clear();
            }

        }

        public static class Broadcaster<T> {

            private static readonly Dictionary<string, Action<T>> Actions = new();
            
            public static int Count => Actions.Count;

            public static void Register(string key, Action<T> action) {
                if (string.IsNullOrEmpty(key) || action == null) return;
                Actions[key] = action;
            }

            public static void UnRegister(string key) {
                if (string.IsNullOrEmpty(key)) return;
                Actions.Remove(key);
            }

            public static void Call(string key, T param) {
                if (string.IsNullOrEmpty(key)) return;

                if (Actions.TryGetValue(key, out var action))
                    action(param);
            }

            public static void Clear() {
                Actions.Clear();
            }

        }

    }

}