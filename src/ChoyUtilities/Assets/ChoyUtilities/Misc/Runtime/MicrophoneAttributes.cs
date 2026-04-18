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

using UnityEngine;

namespace ChoyUtilities {
    [CreateAssetMenu(fileName = "Microphone", menuName = "Choy Utilities/Microphone")]
    public class MicrophoneAttributes : ScriptableObject {
        [Range(0, 300)] public int recMaxLength = 60;
        public bool loop = true;
        [Range(0, 48000)] public int frequency = 16000;
    }
}