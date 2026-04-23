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

#if UNITY_2023_1_OR_NEWER
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

namespace ChoyUtilities {
    public abstract class GenericParticleManager<TEnum, TMono> : GenericPoolingManager<TEnum, ParticleSystem, TMono>
        where TEnum : struct, Enum
        where TMono : MonoBehaviour {
        protected override void Awake() {
            base.Awake();

            foreach (var p in poolAttributes.poolPrefabs) {
                var particle = p.prefab;
                var particleMain = particle.main;
                particleMain.playOnAwake = false;
            }
        }

        public virtual ParticleSystem PlayEffectAtPosition(TEnum id, float3 position) {
            var index = GetPoolIndex(id);

            if (index == -1) return null;

            var particle = RuntimePools[index].spawn[RuntimePools[index].currentIndex];
            particle.transform.position = position;
            particle.Play();

            RuntimePools[index].previousIndex = RuntimePools[index].currentIndex;
            RuntimePools[index].currentIndex++;
            RuntimePools[index].currentIndex %= RuntimePools[index].spawn.Length;
            return particle;
        }

        public virtual void PauseAllEffects() {
            PauseIndexes = ListPool<int>.Get();

            for (var i = 0; i < RuntimePools.Length; i++) {
                var system = RuntimePools[i];

                foreach (var p in system.spawn)
                    if (p.isPlaying)
                        p.Pause();
                PauseIndexes.Add(i);
            }
        }

        public virtual void ResumeAllEffects() {
            if (PauseIndexes is null) return;

            foreach (var i in PauseIndexes) {
                var system = RuntimePools[i];

                foreach (var p in system.spawn)
                    if (p.isPaused)
                        p.Play();
            }

            ListPool<int>.Release(PauseIndexes);
        }
    }
}
#endif