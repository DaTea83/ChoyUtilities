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

namespace ChoyUtilities.Editor {

    [UxmlElement]
    public partial class StatusElement : VisualElement {

        public enum EStatus : byte {

            Present,
            NotFound,
        }

        private static ToolkitData? _toolkitData;
        private static ToolkitData ToolkitData => _toolkitData ??= new ToolkitData("StatusElement");

        private readonly Label _description;
        private readonly Label _status;
        private readonly Button _button;
        
        public StatusElement() {
            ToolkitData.Clone(this);
            var root = this.Q<VisualElement>("Root");
            _description = root.Q<Label>("Text");
            _status = root.Q<Label>("Status");
            _button = root.Q<Button>("Execute");
            _button.clicked += () => OnClicked?.Invoke();
        }

        public event Action OnClicked;
        
        [UxmlAttribute]
        public string Description {
            get => _description.text;
            set => _description.text = value;
        }

        public void SetStatus(EStatus status) {
            switch (status) {
                case EStatus.Present:
                    _status.text = "Present";
                    _button.text = "";
                    _button.SetEnabled(false);
                    break;
                case EStatus.NotFound:
                    _status.text = "Not Found";
                    _button.text = "Create";
                    _button.SetEnabled(true);
                    break;
            }
        }
    }

}