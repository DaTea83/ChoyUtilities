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
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace ChoyUtilities {

    public partial class ReserveAudioManager : GenericSingleton<ReserveAudioManager>, IBootloader {

        public enum EAudioPriority : byte {
            Highest = 0,
            UltraHigh = 1 << 0,
            VeryHigh = 1 << 1,
            High = 1 << 2,
            AboveAverage = 1 << 3,
            Average = 1 << 4,
            BelowAverage = 1 << 5,
            Low = 1 << 6,
            VeryLow = 1 << 7,
            Lowest = byte.MaxValue
        }
        
        [SerializeField] protected ReserveAudioAttribute reserveAudioAttribute;
        [SerializeField] protected AudioSource audioSourcePrefab;
        [SerializeField] protected byte poolCount = 32;
        [SerializeField] protected bool loop;
        [SerializeField] protected EAudioPriority priority = EAudioPriority.High;

        [SerializeField] protected AudioMixerGroup mixerGroup;
        private byte _currentIndex;

        private byte _previousIndex;
        protected AudioSource[] AudioSources;
        protected List<int> PauseIndexes;

        protected ObjectPool<AudioSource> Pool;

        private void Start() {
            if (BootLoader.Instance is null)
                _ = Init();
        }

        public async Task Init() {
            await Awaitable.NextFrameAsync(Token);

            if (audioSourcePrefab is null) throw new SingletonException("Audio Source Prefab is not set");
            AudioSources = new AudioSource[poolCount];

            Pool = new ObjectPool<AudioSource>(
                () => Instantiate(audioSourcePrefab, transform),
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                Destroy,
                false,
                poolCount,
                poolCount << 1);

            for (var i = 0; i < poolCount; i++) {
                var spawnAudio = Pool.Get();
                spawnAudio.gameObject.transform.SetSiblingIndex(i);
                spawnAudio.loop = loop;
                spawnAudio.outputAudioMixerGroup = mixerGroup;
                spawnAudio.priority = (int)priority;
                AudioSources[i] = spawnAudio;
            }
        }
        
        public async Task Shutdown() {
            ListPool<int>.Release(PauseIndexes);
            await Awaitable.EndOfFrameAsync(Token);
        }

    }

}