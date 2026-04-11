using System;

// ReSharper disable once CheckNamespace

namespace ChoyUtilities.Editor {
    
    public static partial class FancyReplaceEditor {
        
        internal const string DEFAULT_NAME = "FancyReplace";
        private const string TEXTURES = "Textures";
        private const string EDITOR = "Editor";
        private const string SAVE = "Save";
        
        private static string FullSavePath {
            get {
                var name = EditorCollection.FindPathByName(DEFAULT_NAME + EDITOR);
                if (name != string.Empty)
                    return name.Replace($"{EDITOR}/{DEFAULT_NAME}{EDITOR}.cs", 
                        $"{SAVE}/{EditorCollection.ProjectFolderName}_{SAVE}.json");
                throw new Exception("FancyScriptableEditor: save path not found");
            }
        }

        private static string FullTexturePath {
            get {
                var name = EditorCollection.FindPathByName(DEFAULT_NAME + EDITOR);
                if (name != string.Empty)
                    return name.Replace($"{EDITOR}/{DEFAULT_NAME}{EDITOR}.cs", 
                        $"{TEXTURES}");
                throw new Exception("FancyScriptableEditor: resource path not found");
            }
        }
    }
}