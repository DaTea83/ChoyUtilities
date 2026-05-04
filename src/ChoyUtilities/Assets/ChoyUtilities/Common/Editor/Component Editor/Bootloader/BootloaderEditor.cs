// Copyright 2026 DeTea83
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChoyUtilities.Editor {

    [CustomEditor(typeof(BootLoader))]
    public sealed class BootloaderEditor : UnityEditor.Editor {

        [SerializeField] private VisualTreeAsset visualTreeAsset;

        public override VisualElement CreateInspectorGUI() {
            if (visualTreeAsset is null) return null;

            var root = visualTreeAsset.CloneTree();
            var content = root.Q<ListView>("content")
                ?? throw new InvalidOperationException("Visual tree asset does not contain a ListView with name 'content'");
            var loadersProp = serializedObject.FindProperty("<Loaders>k__BackingField");

            content.makeItem = () => new LoaderItem();
            content.bindItem = (element, i) => {
                if (element is not LoaderItem item) return;
                item.Order = (i + 1).ToString();
                item.Field.BindProperty(loadersProp.GetArrayElementAtIndex(i));
            };
            content.unbindItem = (element, i) => {
                if (element is LoaderItem item) item.Field.Unbind();
            };
            content.BindProperty(loadersProp);

            root.Q<Button>("add-btn").clicked += () => {
                loadersProp.arraySize++;
                loadersProp.GetArrayElementAtIndex(loadersProp.arraySize - 1).objectReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
            };

            root.Q<Button>("remove-btn").clicked += () => {
                var index = content.selectedIndex;
                if (index < 0 || index >= loadersProp.arraySize) {
                    loadersProp.arraySize--;
                }
                else {
                    loadersProp.DeleteArrayElementAtIndex(index);
                }

                serializedObject.ApplyModifiedProperties();
            };

            return root;
        }

    }

}