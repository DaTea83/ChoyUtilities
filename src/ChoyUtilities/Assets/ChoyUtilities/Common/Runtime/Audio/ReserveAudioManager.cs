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

using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace ChoyUtilities {

    public class ReserveAudioManager : GenericSingleton<ReserveAudioManager>, IBootloader {

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
        
        [SerializeField] protected Attribute attribute;
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
        
        public (float, AudioSource) PlayClipAtPos(string id, float3 pos, byte audioPriority = (byte)EAudioPriority.Average) {
            return !attribute.pools.TryGet(id, out var resource) 
                ? (0f, null) : PlayClipAtPos(resource, pos, audioPriority);
        }
        
        public (float, AudioSource) PlayClipAtPos(AudioResource resource,
            float3 pos,
            byte audioPriority = (byte)EAudioPriority.Average) {
            var currentSource = AudioSources[_currentIndex];

            currentSource.transform.localPosition = pos;
            currentSource.resource = resource;
            currentSource.priority = audioPriority;
            currentSource.Play();

            var lengthSeconds = currentSource.clip?.length ?? 0f;

            _previousIndex = _currentIndex;
            _currentIndex++;
            _currentIndex %= (byte)AudioSources.Length;

            return (lengthSeconds, currentSource);
        }
        
        public (float, AudioSource) PlayClip(string id, byte audioPriority = (byte)EAudioPriority.Average) {
            return PlayClipAtPos(id, float3.zero, audioPriority);
        }

        public (float, AudioSource) PlayClip(AudioResource resource, byte audioPriority = (byte)EAudioPriority.Average) {
            return PlayClipAtPos(resource, float3.zero, audioPriority);
        }
        
        public bool StopClip(int idx = -1) {
            idx = idx == -1 ? _previousIndex : idx;
            var source = AudioSources[idx];

            if (!source.isPlaying) return false;
            source.Stop();

            return true;
        }

        public bool PauseAllClips(bool isStop = false) {
            PauseIndexes = ListPool<int>.Get();

            for (var i = 0; i < AudioSources.Length; i++) {
                var currentSource = AudioSources[i];

                if (!currentSource.isPlaying) continue;

                if (!isStop)
                    currentSource.Pause();
                else
                    currentSource.Stop();
                PauseIndexes.Add(i);
            }

            return PauseIndexes.Count == AudioSources.Length;
        }

        public bool ResumeClips() {
            if (PauseIndexes is null) return false;

            foreach (var index in PauseIndexes)
                AudioSources[index].Play();

            ListPool<int>.Release(PauseIndexes);

            return true;
        }

        public abstract class Attribute : ScriptableObject {

            public Reserve<AudioResource> pools;

        }
    }

}