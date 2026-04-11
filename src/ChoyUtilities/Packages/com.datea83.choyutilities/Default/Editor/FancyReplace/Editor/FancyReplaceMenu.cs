using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    internal static partial class FancyReplaceEditor {
        [MenuItem("Assets/Fancy Replace", false, 100)]
        private static void CustomModificationMenuItem() {
            if (!TryGetSelectedObjectType(out var path, out var type)) return;
            FancyReplaceWindow.ShowWindow(path, type);
        }

        [MenuItem("Assets/Fancy Replace", true)]
        private static bool ValidateCustomModificationMenuItem() {
            if (!TryGetSelectedObjectType(out _, out var type)) return false;
            return type != EReplaceType.None;
        }

        private static bool TryGetSelectedObjectType(out string path, out EReplaceType type) {
            type = EReplaceType.None;
            path = string.Empty;
            if (Selection.assetGUIDs == null || Selection.assetGUIDs.Length == 0)
                return false;

            var selected = Selection.activeObject;
            if (selected is null) {
                type = EReplaceType.None;
                path = string.Empty;
                return false;
            }

            path = AssetDatabase.GetAssetPath(selected);
            if (string.IsNullOrEmpty(path)) {
                path = string.Empty;
                return false;
            }

            // Put at most top, if click at project empty space it will return the current directory
            if (AssetDatabase.IsValidFolder(path)) {
                type = EReplaceType.Folder;
                return true;
            }

            switch (selected) {
                case MonoScript:
                    type = EReplaceType.Script;
                    return true;
                case ScriptableObject:
                    type = EReplaceType.ScriptableObject;
                    return true;
                case SceneAsset:
                    type = EReplaceType.Scene;
                    return true;
                case Material:
                    type = EReplaceType.Materials;
                    return true;
                case TextAsset:
                    type = EReplaceType.TextAsset;
                    return true;
            }

            // Definition for this is super board, also every single object can fall to this definition
            // If fail to return previously it will return for this
            if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(selected) is not null) {
                type = EReplaceType.Prefab;
                return true;
            }

            return false;
        }
    }
}