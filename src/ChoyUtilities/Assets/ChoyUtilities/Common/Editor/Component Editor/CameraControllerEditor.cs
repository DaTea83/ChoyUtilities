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

using UnityEditor;

// ReSharper disable CheckNamespace

namespace ChoyUtilities.Editor {
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var instance = (CameraController)target;

            EditorGUILayout.HelpBox(
                "Currently in ECS you can't just attach the camera to a subscene entity and called it a day",
                MessageType.Info);
            EditorGUILayout.HelpBox("That's where this singleton comes into play", MessageType.Info);
            EditorGUILayout.HelpBox("Attach this to the camera object", MessageType.Info);

            EditorGUILayout.HelpBox("Don't put the camera in the subscene, put it in normal hierarchy",
                MessageType.Warning);

            EditorGUILayout.HelpBox(
                "Keep Singleton true or not doesn't matter, if true it will just override the other scene's camera",
                MessageType.Info);

            base.OnInspectorGUI();
        }
    }
}