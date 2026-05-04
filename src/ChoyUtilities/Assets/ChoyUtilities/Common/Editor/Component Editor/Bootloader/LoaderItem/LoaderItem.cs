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

using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ChoyUtilities.Editor {

    [UxmlElement]
    public partial class LoaderItem : VisualElement {

        private static ToolkitData? _toolkitData;
        private static ToolkitData ToolkitData => _toolkitData ??= new ToolkitData("LoaderItem");

        public ObjectField Field { get; }

        public LoaderItem() {
            ToolkitData.Clone(this);
            Field = this.Q<ObjectField>("field");
        }

        [UxmlAttribute]
        public string Order {
            get => Field.label;
            set => Field.label = $"Order no. {value}";
        }
    }

}