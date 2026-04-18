// Copyright 2026 DaTea83
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

            public static void Clear() { Actions.Clear(); }
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

            public static void Clear() { Actions.Clear(); }
        }
    }
}