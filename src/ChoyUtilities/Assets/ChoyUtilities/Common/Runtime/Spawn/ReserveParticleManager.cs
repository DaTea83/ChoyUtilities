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

using System.Threading.Tasks;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

namespace ChoyUtilities {

    public class ReserveParticleManager : ReservePoolingManager<ParticleSystem, ReserveParticleManager> {
        
        protected override bool InitializeImmediately => true;

        public override Task Init() {
            if (attribute.pools.IsEmpty || attribute is null) return base.Init();

            foreach (var p in attribute.pools) {
                var particle = p.Value;
                var particleMain = particle.main;
                particleMain.playOnAwake = false;
            }

            return base.Init();
        }

        public ParticleSystem PlayEffectAtPosition(FixedString128Bytes id, float3 position) {
            if (!attribute.pools.TryGet(id, out var index, out var particle)) return null;
            
            particle.transform.position = position;
            particle.Play();

            RuntimePools[index].previousIndex = RuntimePools[index].currentIndex;
            RuntimePools[index].currentIndex++;
            RuntimePools[index].currentIndex %= RuntimePools[index].spawn.Length;
            return particle;
        }

        public void PauseAllEffects() {
            PauseIndexes = ListPool<int>.Get();

            for (var i = 0; i < RuntimePools.Length; i++) {
                var system = RuntimePools[i];

                foreach (var p in system.spawn)
                    if (p.isPlaying)
                        p.Pause();
                PauseIndexes.Add(i);
            }
        }

        public void ResumeAllEffects() {
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