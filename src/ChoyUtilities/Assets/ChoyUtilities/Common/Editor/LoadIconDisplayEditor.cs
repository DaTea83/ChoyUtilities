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

using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace

//Origin: https://www.youtube.com/watch?v=EFh7tniBqkk

//Read this:
//In case of debugging and this is blocking the way just comment out [InitializeOnLoad]
//After finish debugging uncomment back

namespace ChoyUtilities.Editor {
#if UNITY_EDITOR

    [InitializeOnLoad]
    internal static class LoadIconDisplayEditor {
        private static bool _hierarchyHasFocus;
        private static EditorWindow _window;

        static LoadIconDisplayEditor() {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate() {
            _window ??= Resources.FindObjectsOfTypeAll<EditorWindow>()
                .FirstOrDefault(window => window.GetType().Name == "SceneHierarchyWindow");

            _hierarchyHasFocus = EditorWindow.focusedWindow is not null && EditorWindow.focusedWindow == _window;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
#if UNITY_6000_3_OR_NEWER
            if (EditorUtility.EntityIdToObject(instanceID) is not GameObject obj) return;
#else
            if (EditorUtility.InstanceIDToObject(instanceID) is not GameObject obj) return;
#endif

            //Is this a prefab? If yes return
            if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj) is not null) return;

            //Check if the game object has any component, if null return
            var components = obj.GetComponents<Component>();

            if (components is null || components.Length == 0) return;

            // Filter out missing scripts
            components = components.Where(c => c is not null).ToArray();

            if (components.Length == 0) return;

            var component = components.Length > 1 ? components[1] : components[0];

            //Get what type of the component
            var type = component.GetType();

            //Tell unity to get the icon of the component, but will also return back the text, so set that as null
            var content = EditorGUIUtility.ObjectContent(component, type);
            content.text = null;
            //On mouse hover gives the context of the icon
            content.tooltip = type.Name;

            if (content.image is null) return;

            //Cover up the default box

#if UNITY_6000_3_OR_NEWER
            var isSelected = Selection.entityIds.Contains(instanceID);
#else
            var isSelected = Selection.instanceIDs.Contains(instanceID);
#endif
            var isHovering = selectionRect.Contains(Event.current.mousePosition);

            var color = EditorCollection.GetHierarchyColor(isSelected, isHovering, _hierarchyHasFocus);
            var background = selectionRect;
            background.width = 18.5f;
            EditorGUI.DrawRect(background, color);

            EditorGUI.LabelField(selectionRect, content);
        }
    }

#endif
}