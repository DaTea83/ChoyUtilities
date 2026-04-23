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

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace ChoyUtilities.Entities {
    /// <summary>
    ///     Any entity with URP lit material will be randomized with a color
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UrpRandomColorSingletonAuthoring : MonoBehaviour {
        [SerializeField] private Color[] colors;

        private class Baker : Baker<UrpRandomColorSingletonAuthoring> {
            public override void Bake(UrpRandomColorSingletonAuthoring singletonAuthoring) {
                if (singletonAuthoring.colors is null || singletonAuthoring.colors.Length == 0) return;
                var e = GetEntity(TransformUsageFlags.None);
                var buffer = AddBuffer<UrpRandomColorISingletonBuffer>(e);

                foreach (var color in singletonAuthoring.colors)
                    buffer.Add(new UrpRandomColorISingletonBuffer
                        { Value = new float4(color.r, color.g, color.b, color.a) });
            }
        }
    }

    public struct UrpRandomColorISingletonBuffer : IBufferElementData {
        public float4 Value;
    }

    public struct UrpColorChangedITag : IComponentData { }

    [BurstCompile]
    [UpdateInGroup(typeof(TeaManagedComponentSystem))]
    public partial struct UrpRandomColorISystem : ISystem {
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<UrpRandomColorISingletonBuffer>(); }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var buffer = SystemAPI.GetSingletonBuffer<UrpRandomColorISingletonBuffer>();
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (material, random, entity)
                     in SystemAPI.Query<RefRW<URPMaterialPropertyBaseColor>, RefRW<RandomIData>>()
                         .WithNone<UrpColorChangedITag>().WithEntityAccess()) {
                if (random.ValueRO.Value.state == 0) continue;

                var index = random.ValueRW.Value.NextInt(0, buffer.Length);
                material.ValueRW.Value = buffer[index].Value;

                ecb.AddComponent<UrpColorChangedITag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}