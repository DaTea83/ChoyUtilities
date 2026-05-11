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

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace ChoyUtilities {

    public partial class ReserveAudioManager {

        public (float, AudioSource) PlayClipAtPos(string id, float3 pos,
            byte audioPriority = (byte)EAudioPriority.Average) {
            return !reserveAudioAttribute.pools.TryGet(id, out var resource)
                ? (0f, null)
                : PlayClipAtPos(resource, pos, audioPriority);
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

        public (float, AudioSource) PlayClip(AudioResource resource,
            byte audioPriority = (byte)EAudioPriority.Average) {
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

    }

}