using System.IO;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace

namespace ChoyUtilities.Editor {
    internal static partial class EditorCollection {
        public const string UtilityWindow = "ChoyUtilities/";

        public static string ProjectFolderName => Directory.GetParent(Application.dataPath)?.Name;

        public static string FindPathByName(string name) {
            var guids = AssetDatabase.FindAssets($"{name} t:script");

            if (guids is null || guids.Length == 0)
                return string.Empty;

            return AssetDatabase.GUIDToAssetPath(guids[0]);
        }
    }
}