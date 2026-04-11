using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    
    [InitializeOnLoad]
    internal static partial class FancyReplaceEditor {

        static FancyReplaceEditor() {
            _ = LoadOrCreate();
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;
        }

        private static void OnProjectWindowItemOnGUI(string guid, Rect selectionRect) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            
            if (Asset.assetModified is null || Asset.assetModified.Length == 0) return;
            var index = Array.FindIndex(Asset.assetModified, data => data.idPath == path);
            if (index == -1) return;
            
            var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(Asset.assetModified[index].texturePath);
            if (tex is null) return;

            var newRect = selectionRect.height <= 16 
                ? new Rect(selectionRect.x + 1.5f, selectionRect.y, selectionRect.height, selectionRect.height) 
                : new Rect(selectionRect.x, selectionRect.y, selectionRect.height - 10, selectionRect.height - 10);
            
            var mul = EditorGUIUtility.isProSkin // Means are you dark mode or light mode
                ? EditorCollection.ProjectDarkColor : EditorCollection.ProjectLightColor;
            Color col = Asset.color;
            
            GUI.backgroundColor = col;
            //For mul since all values are the same so just choose any of them
            col *= mul.r;
            col.a = 1;
            // Cover the original
            EditorGUI.DrawRect(newRect, col);
            // Put new texture on top
            GUI.DrawTexture(newRect, tex);
        }
    }
}