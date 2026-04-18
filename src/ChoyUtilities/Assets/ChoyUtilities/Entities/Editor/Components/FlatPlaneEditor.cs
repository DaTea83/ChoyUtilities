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
using UnityEngine;

// ReSharper disable CheckNamespace

namespace ChoyUtilities.Entities.Editor {
    [CustomEditor(typeof(FlatPlaneAuthoring))]
    public class FLatPlaneEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var authoring = (FlatPlaneAuthoring)target;

            authoring.planePrefab =
                (GameObject)EditorGUILayout.ObjectField("Prefab", authoring.planePrefab, typeof(GameObject), false);
            authoring.planeSize = (uint)EditorGUILayout.IntField("Size", (int)authoring.planeSize);
            authoring.unitsPerPlane = EditorGUILayout.FloatField("Units per Plane", authoring.unitsPerPlane);
            authoring.planeOffset = EditorGUILayout.Vector3Field("Offset", authoring.planeOffset);
            authoring.seed = (byte)EditorGUILayout.IntField("Seed", authoring.seed);

            if (GUILayout.Button("This is a button")) authoring.seed = (byte)Random.Range(0, byte.MaxValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}