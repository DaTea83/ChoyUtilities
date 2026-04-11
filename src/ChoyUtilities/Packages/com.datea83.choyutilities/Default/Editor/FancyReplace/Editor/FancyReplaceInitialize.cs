using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    
    [InitializeOnLoad]
    internal static partial class FancyReplaceEditor {

        static FancyReplaceEditor() {
            _ = LoadOrCreate();
            
        }

        private static void OnProjectWindowItemOnGUI(string guid, Rect selectionRect) {
            
        }
    }
}