using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    internal static partial class FancyReplaceEditor {
        internal static AssetModificationSerialize Asset;

        private static AssetModificationSerialize DefaultAsset => new AssetModificationSerialize() {
            assetModified = Array.Empty<MenuItemSerialize>(),
            color = new Floater(Color.white)
        };

        internal static async Task SaveData() {
            var json = JsonUtility.ToJson(Asset, true);
            await File.WriteAllTextAsync(FullSavePath, json);
            AssetDatabase.Refresh();
        }

        private static async Task LoadOrCreate() {
            if (!File.Exists(FullSavePath)) {
                Asset = DefaultAsset;
                await SaveData();
                return;
            }

            try {
                var json = await File.ReadAllTextAsync(FullSavePath);
                Asset = JsonUtility.FromJson<AssetModificationSerialize>(json);
            }
            catch (Exception e) {
                Debug.LogError($"Failed to load {FullSavePath}, creating new. Error: {e}");
                Asset = DefaultAsset;
                await SaveData();
            }
        }

        // Yes not a fan of LINQ and List how could you tell?
        internal static void RemoveAt(int removeIndex) {
            if (Asset.assetModified is null || Asset.assetModified.Length == 0) return;

            if (removeIndex < 0 || removeIndex >= Asset.assetModified.Length)
                throw new ArgumentOutOfRangeException(nameof(removeIndex));

            var newArray = new MenuItemSerialize[Asset.assetModified.Length - 1];
            var newIndex = 0;

            for (var i = 0; i < Asset.assetModified.Length; i++) {
                if (i == removeIndex)
                    continue;

                newArray[newIndex] = Asset.assetModified[i];
                newIndex++;
            }

            Asset.assetModified = newArray;
        }

        internal static void AddNew(MenuItemSerialize entry) {
            Asset.assetModified ??= Array.Empty<MenuItemSerialize>();
            var newArray = new MenuItemSerialize[Asset.assetModified.Length + 1];
            Array.Copy(Asset.assetModified, newArray, Asset.assetModified.Length);
            newArray[^1] = entry;
            Asset.assetModified = newArray;
        }
    }
}