using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    
    internal class FancyReplaceWindow : EditorWindow {
        
        private const byte FONT_SIZE = 24;
        private const byte BUTTON_PER_ROW = 8;
        private const float BUTTON_PADDING = 10f;
        private const float BUTTON_HEIGHT = 100f;
        private const float HEAD_BUTTON_HEIGHT = 36f;
        
        private static string _assetPath;
        private static EReplaceType _replaceType;
        
        private void OnGUI() {
            var matColor = EditorGUILayout.ColorField("Tint Color", FancyReplaceEditor.Asset.color, new [] {
                GUILayout.Width(position.width),
                GUILayout.Height(HEAD_BUTTON_HEIGHT)
            });
            FancyReplaceEditor.Asset.color = new Floater(matColor);
            
            var clearButtonStyle = new GUIStyle(GUI.skin.button) {
                fontSize = FONT_SIZE,
                alignment = TextAnchor.MiddleCenter
            };
            if (GUI.Button(GetStartPos(1, new Vector2(position.width, HEAD_BUTTON_HEIGHT)), "Reset Color", clearButtonStyle)) {
                FancyReplaceEditor.Asset.color = new Floater(Color.white);
                _ = FancyReplaceEditor.SaveData();
                Close();
            }
            if (GUI.Button(GetStartPos(2, new Vector2(position.width, HEAD_BUTTON_HEIGHT)), "Reset Selection", clearButtonStyle)) {
                if(TryRemove())
                    _ = FancyReplaceEditor.SaveData();
                Close();
            }
            
            // Display own type textures first before global
            var typePath = FancyReplaceEditor.GetTypePath(_replaceType);
            var fullPath = FancyReplaceEditor.FullTexturePath;
            var textures = AssetDatabase.FindAssets("t:Texture2D", new[] {
                typePath,
                fullPath
            });
            if (textures is null || textures.Length == 0) return;

            for (var i = 0; i < textures.Length; i++) {
                var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(textures[i]));
                
                // x and y only set the start position, but doesn't care whether all elements fit or not
                // do BUTTON_PER_ROW * BUTTON_PADDING to take account for that
                var width = (position.width - BUTTON_PER_ROW * BUTTON_PADDING) / BUTTON_PER_ROW;
                var rect = GetStartPos(3, new Vector2(width, BUTTON_HEIGHT));
                
                var pos = rect.position;
                pos.x += (i % BUTTON_PER_ROW) * (width + BUTTON_PADDING);
                pos.y += math.floor(i / (float)BUTTON_PER_ROW) * (BUTTON_HEIGHT + BUTTON_PADDING);
                rect.position = pos;
                
                if (GUI.Button(rect, tex)) {
                    TryRemove();
                    var newEntry = new MenuItemSerialize() {
                        idPath = _assetPath,
                        texturePath = AssetDatabase.GUIDToAssetPath(textures[i]),
                        idType = new Floater(_replaceType)
                    };
                    FancyReplaceEditor.AddNew(newEntry);
                    _ = FancyReplaceEditor.SaveData();
                    Close();
                }
            }
            
            return;
            Rect GetStartPos(int index, Vector2 size) =>
            new Rect(0, (BUTTON_PADDING * (1 + index)) + (HEAD_BUTTON_HEIGHT * index), size.x, size.y);
        }

        internal static void ShowWindow(string assetPath, EReplaceType type) {
            var window = GetWindow<FancyReplaceWindow>(FancyReplaceEditor.DEFAULT_NAME);
            _assetPath = assetPath;
            _replaceType = type;
            window.Show();
        }
        
        private static int FindPathIndex() {
            if (FancyReplaceEditor.Asset.assetModified is null || FancyReplaceEditor.Asset.assetModified.Length == 0) return -1;
            return Array.FindIndex(FancyReplaceEditor.Asset.assetModified, 
                data => data.idPath == _assetPath);
        }

        private static bool TryRemove() {
            var index = FindPathIndex();
            if (index == -1) return false;
            FancyReplaceEditor.RemoveAt(index);
            return true;
        }
    }
}