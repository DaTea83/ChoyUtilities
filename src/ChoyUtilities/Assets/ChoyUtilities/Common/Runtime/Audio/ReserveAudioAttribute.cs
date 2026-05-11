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
using UnityEngine;
using UnityEngine.Audio;

namespace ChoyUtilities {

    [CreateAssetMenu(fileName = "Audio Reserve", menuName = "Choy Utilities/Audio Reserve")]
    public sealed class ReserveAudioAttribute : ScriptableObject {

        public Reserve<AudioResource> pools;
        public MixerSerialize serialize;

        [Serializable]
        public struct MixerReserve {
            public Reserve<AudioMixer> mixers;
            [Tooltip("The key is for identify in code, not the parameter name in the mixer, although is more encouraged to use the same name")]
            public string mixerName;
        }
        
        [Serializable]
        public struct MixerSerialize {
            public MixerReserve reserve;
            [Min(0.01f)]public float motionTime;
            public EMotion motionCurve;
        }
    }

}