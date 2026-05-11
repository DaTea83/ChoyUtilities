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
using UnityEngine;
using UnityEngine.UIElements;

namespace ChoyUtilities.Editor {

    [UxmlElement]
    public partial class FloatThreeElement : VisualElement {

        private static ToolkitData? _toolkitData;
        private static ToolkitData ToolkitData => _toolkitData ??= new ToolkitData("FloatThreeElement");

        public Vector3Field Field { get; }
        private bool IsScale => Field.label.Contains("Scale");

        private readonly Button _resetButton;
        private readonly Button _syncButton;
        private Vector3 _previousValue;
        private bool _isSyncing;

        private readonly string _mimicPath;
        private Texture2D MimicTexture => AssetDatabase.LoadAssetAtPath<Texture2D>(_mimicPath);
        private readonly string _unmimicPath;
        private Texture2D UnmimicTexture => AssetDatabase.LoadAssetAtPath<Texture2D>(_unmimicPath);
        
        public FloatThreeElement() {
            ToolkitData.Clone(this);
            Field = this.Q<Vector3Field>("field");

            _resetButton = this.Q<Button>("btn-reset");
            _resetButton.clicked += () => Field.value = IsScale ? Vector3.one : Vector3.zero;

            _syncButton = this.Q<Button>("btn-sync");
            _syncButton.clicked += () => {
                _isSyncing = !_isSyncing;
                _syncButton.iconImage = _isSyncing ? MimicTexture : UnmimicTexture;
            };
            
            _mimicPath = EditorCollection.FindPathByName("mimic", "texture2D");
            _unmimicPath = EditorCollection.FindPathByName("unmimic", "texture2D");

            Field.RegisterValueChangedCallback(OnFieldValueChanged);
        }

        private void OnFieldValueChanged(ChangeEvent<Vector3> vecEvent) {
            if (!_isSyncing) {
                _previousValue = vecEvent.newValue;
                return;
            }

            var prev = _previousValue;
            var next = vecEvent.newValue;

            var dx = next.x - prev.x;
            var dy = next.y - prev.y;
            var dz = next.z - prev.z;

            float delta;
            if      (dx != 0 && dy == 0 && dz == 0) delta = dx;
            else if (dy != 0 && dx == 0 && dz == 0) delta = dy;
            else if (dz != 0 && dx == 0 && dy == 0) delta = dz;
            else {
                _previousValue = next;
                return;
            }

            _previousValue = new Vector3(prev.x + delta, prev.y + delta, prev.z + delta);
            Field.SetValueWithoutNotify(_previousValue);
        }
        
        [UxmlAttribute]
        public string Label {
            get => Field.label;
            set => Field.label = value;
        }
        
        [UxmlAttribute]
        public Vector3 Value {
            get => Field.value;
            set => Field.value = value;
        }
        
        
    }

}