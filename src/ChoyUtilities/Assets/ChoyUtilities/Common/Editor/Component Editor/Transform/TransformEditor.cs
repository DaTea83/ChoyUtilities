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

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChoyUtilities.Editor {

    [CustomEditor(typeof(Transform))]
    public class TransformEditor : UnityEditor.Editor {

        [SerializeField] private VisualTreeAsset visualTreeAsset;
        
        public override VisualElement CreateInspectorGUI() {
            if (visualTreeAsset is null) return base.CreateInspectorGUI();

            try {
                var root = visualTreeAsset.CloneTree();
                var pos = root.Q<FloatThreeElement>("position");
                var rot = root.Q<FloatThreeElement>("rotation");
                var scale = root.Q<FloatThreeElement>("scale");
                var transform = (Transform)target;

                pos.Field.BindProperty(serializedObject.FindProperty("m_LocalPosition"));
                scale.Field.BindProperty(serializedObject.FindProperty("m_LocalScale"));

                rot.Field.SetValueWithoutNotify(transform.localEulerAngles);
                rot.Field.RegisterValueChangedCallback(evt => {
                    Undo.RecordObject(transform, "Rotate");
                    transform.localEulerAngles = evt.newValue;
                });
                rot.Field.TrackPropertyValue(serializedObject.FindProperty("m_LocalRotation"), _ => {
                    rot.Field.SetValueWithoutNotify(transform.localEulerAngles);
                });
                return root;
            }
            catch {
                return base.CreateInspectorGUI();
            }
        }
    }
}