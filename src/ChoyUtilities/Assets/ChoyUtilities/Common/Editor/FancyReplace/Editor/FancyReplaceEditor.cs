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

// ReSharper disable once CheckNamespace

namespace ChoyUtilities.Editor {
    internal enum EReplaceType : byte {
        None = 0,
        Script,
        Prefab,
        Scene,
        Folder,
        ScriptableObject,
        Materials,
        TextAsset
    }

    internal static partial class FancyReplaceEditor {
        internal const string DEFAULT_NAME = "FancyReplace";
        private const string EDITOR = "Editor";

        private static string ScriptPath => $"{EDITOR}/{DEFAULT_NAME}{EDITOR}.cs";
        private static string FullSavePath => $"Assets/{DEFAULT_NAME}/{EditorCollection.ProjectFolderName}_Save.json";

        internal static string FullTexturePath {
            get {
                var name = EditorCollection.FindPathByName(DEFAULT_NAME + EDITOR);

                return name != string.Empty
                    ? name.Replace(ScriptPath, "Textures/All")
                    : string.Empty;
            }
        }

        internal static string GetTypePath(EReplaceType type) {
            var name = type switch {
                EReplaceType.Script => "Scripts",
                EReplaceType.ScriptableObject => "ScriptableObject",
                EReplaceType.Prefab => "Prefab",
                EReplaceType.Scene => "Scenes",
                EReplaceType.Folder => "Folder",
                EReplaceType.Materials => "Materials",
                EReplaceType.TextAsset => "TextAsset",
                _ => string.Empty
            };

            return FullTexturePath.Replace("All", name);
        }
    }
}