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
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {

    public sealed partial class UtilitiesMenuWindow {

        private static void SetupSceneTools(VisualElement root) {
            var generateButton = root.Q<Button>(GetName("GenerateSceneStructure"))
                ?? throw new InvalidOperationException("GenerateSceneStructure button not found");
            generateButton.clicked += SceneTemplateEditor.GenerateSceneStructure;
            
            var sortButton = root.Q<Button>(GetName("SortSceneObjects"))
                ?? throw new InvalidOperationException("SortSceneObjects button not found");
            sortButton.clicked += SceneTemplateEditor.SortSceneObjects;
            
            var findMissingButton = root.Q<Button>(GetName("FindMissingScripts"))
                ?? throw new InvalidOperationException("FindMissingScripts button not found");
            findMissingButton.clicked += RemoveMissingScriptsEditor.FindMissingScripts;
            
            var removeMissingButton = root.Q<Button>(GetName("RemoveMissingScripts"))
                ?? throw new InvalidOperationException("RemoveMissingScripts button not found");
            removeMissingButton.clicked += RemoveMissingScriptsEditor.RemoveMissingScripts;
            
            return;
            string GetName(string uiName) => "Button-"+ uiName;
        }

    }

}