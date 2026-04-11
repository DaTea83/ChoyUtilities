using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    internal class FancyReplaceWindow : EditorWindow {
        private const byte FONT_SIZE = 24;
        private const byte BUTTON_PER_ROW = 5;
        private const float BUTTON_PADDING = 10f;
        private const float BUTTON_HEIGHT = 100f;
        private const float HEAD_BUTTON_HEIGHT = 36f;

        private static string _assetPath;
        private static EReplaceType _replaceType;

        private static Vector2 _scrollPos;

        private void OnGUI() {
            GUILayout.Space(BUTTON_PADDING);
            var matColor = EditorGUILayout.ColorField("Global Tint Color", FancyReplaceEditor.Asset.color, new[] {
                GUILayout.Width(position.width),
                GUILayout.Height(HEAD_BUTTON_HEIGHT),
            });
            GUILayout.Space(BUTTON_PADDING);
            FancyReplaceEditor.Asset.color = new Floater(matColor);

            var clearButtonStyle = new GUIStyle(GUI.skin.button) {
                fontSize = FONT_SIZE,
                alignment = TextAnchor.MiddleCenter
            };
            if (GUILayout.Button("Reset Color", clearButtonStyle, GUILayout.Height(HEAD_BUTTON_HEIGHT))) {
                FancyReplaceEditor.Asset.color = new Floater(Color.white);
                _ = FancyReplaceEditor.SaveData();
                Close();
            }

            GUILayout.Space(BUTTON_PADDING);

            if (GUILayout.Button("Reset Selection", clearButtonStyle, GUILayout.Height(HEAD_BUTTON_HEIGHT))) {
                if (TryRemove())
                    _ = FancyReplaceEditor.SaveData();
                Close();
            }

            GUILayout.Space(BUTTON_PADDING);

            // Display own type textures first before global
            var typePath = FancyReplaceEditor.GetTypePath(_replaceType);
            var fullPath = FancyReplaceEditor.FullTexturePath;
            var textures = AssetDatabase.FindAssets("t:Texture2D", new[] {
                typePath,
                fullPath
            });
            if (textures is null || textures.Length == 0) return;

            EditorGUILayout.BeginVertical();
            _scrollPos =
                EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

            if (textures.Length > 0) {
                var width = (position.width - BUTTON_PER_ROW * BUTTON_PADDING) / BUTTON_PER_ROW;

                for (var i = 0; i < textures.Length; i += BUTTON_PER_ROW) {
                    EditorGUILayout.BeginHorizontal();

                    for (var j = 0; j < BUTTON_PER_ROW; j++) {
                        var index = i + j;

                        if (index >= textures.Length) {
                            GUILayout.Space(width);
                            continue;
                        }

                        var texPath = AssetDatabase.GUIDToAssetPath(textures[index]);
                        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);

                        if (GUILayout.Button(tex, GUILayout.Width(width), GUILayout.Height(BUTTON_HEIGHT))) {
                            TryRemove();
                            var newEntry = new MenuItemSerialize() {
                                idPath = _assetPath,
                                texturePath = texPath,
                                idType = new Floater(_replaceType)
                            };
                            FancyReplaceEditor.AddNew(newEntry);
                            _ = FancyReplaceEditor.SaveData();
                            Close();
                        }

                        GUILayout.Space(BUTTON_PADDING);
                    }

                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(BUTTON_PADDING);
                }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        internal static void ShowWindow(string assetPath, EReplaceType type) {
            var window = GetWindow<FancyReplaceWindow>(FancyReplaceEditor.DEFAULT_NAME);
            _assetPath = assetPath;
            _replaceType = type;
            window.Show();
        }

        private static int FindPathIndex() {
            if (FancyReplaceEditor.Asset.assetModified is null ||
                FancyReplaceEditor.Asset.assetModified.Length == 0) return -1;
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