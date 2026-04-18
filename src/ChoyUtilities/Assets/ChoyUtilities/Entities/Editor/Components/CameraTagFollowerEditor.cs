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

using ChoyUtilities.Entities;
using UnityEditor;

// ReSharper disable CheckNamespace

namespace ChoyUtilities.Editor {
    [CustomEditor(typeof(CameraTagAuthoring))]
    public class CameraTagFollowerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            EditorGUILayout.HelpBox("Don't put this in the camera", MessageType.Warning);
            EditorGUILayout.HelpBox("For camera use CameraTrackerController", MessageType.Warning);

            EditorGUILayout.HelpBox("This is used for the camera to track the attached entity's transform",
                MessageType.Info);

            EditorGUILayout.HelpBox("Make sure only have a single entity have this component in any given runtime!!!",
                MessageType.Warning);
        }
    }
}