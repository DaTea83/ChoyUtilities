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
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

namespace ChoyUtilities {

    public abstract class GenericMixerManager<TEnum, TMono> : GenericSingleton<TMono>
    where TEnum : struct, Enum
    where TMono : MonoBehaviour {

        [Serializable]
        public struct AudioMixerSerialize {

            public TEnum id;
            public string name;
            public AudioMixer mixer;
            public EMotion motionCurve;

        }

        [SerializeField] protected AudioMixerSerialize[] mixers;

        private int GetMixers(TEnum id) {
            var index = Array.FindIndex(mixers, i => EqualityComparer<TEnum>.Default.Equals(i.id, id));
            return index == -1 ? throw new SingletonException("Undefined mixer") : index;
        }

        public async Awaitable RaiseMixer(TEnum id, float value, float duration = 1f) {
            var index = GetMixers(id);
            var success= mixers[index].mixer.GetFloat(mixers[index].name, out var currentValue);
            if (!success) throw new SingletonException("Mixer not found");
            var time = 0f;
            while (time < duration) {
                mixers[index].mixer.SetFloat(mixers[index].name, math.lerp(value, currentValue, mixers[index].motionCurve.Evaluate(time / duration)));
                time++;
                await Awaitable.NextFrameAsync(Token);
            }
        }
    }

}