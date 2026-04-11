using System;

// ReSharper disable once CheckNamespace

namespace ChoyUtilities.Editor {
    
    internal static partial class FancyReplaceEditor {

        internal enum EReplaceType {
            Script,
            Prefab,
            Scene,
            Folder,
            ScriptableObject,
        }
         
        internal const string DEFAULT_NAME = "FancyReplace";
        private const string TEXTURES = "Textures";
        private const string EDITOR = "Editor";
        private const string SAVE = "Save";
        
        private static string ScriptPath => $"{EDITOR}/{DEFAULT_NAME}{EDITOR}.cs";
        
        private static string FullSavePath {
            get {
                var name = EditorCollection.FindPathByName(DEFAULT_NAME + EDITOR);
                return name != string.Empty 
                    ? name.Replace(ScriptPath, $"{SAVE}/{EditorCollection.ProjectFolderName}_{SAVE}.json") 
                    : string.Empty;
            }
        }

        internal static string FullTexturePath {
            get {
                var name = EditorCollection.FindPathByName(DEFAULT_NAME + EDITOR);
                return name != string.Empty 
                    ? name.Replace(ScriptPath, $"{TEXTURES}/All") 
                    : string.Empty;
            }
        }

        internal static string GetTypePath(EReplaceType type) {
            var name =  type switch {
                EReplaceType.Script => "Scripts",
                EReplaceType.ScriptableObject => "ScriptableObject",
                EReplaceType.Prefab => "Prefabs",
                EReplaceType.Scene => "Scenes",
                EReplaceType.Folder => "Folder",
                _ => string.Empty
            };
            return FullTexturePath.Replace("All", name);
        }
    }
}