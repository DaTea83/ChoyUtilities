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

using Unity.Entities;
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DestroyAuthoring))]
    public class AgentStatsAuthoring : MonoBehaviour {
        [SerializeField] private AgentAttributes attributes;

        private class AgentStatsAuthoringBaker : Baker<AgentStatsAuthoring> {
            public override void Bake(AgentStatsAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);
                var stats = authoring.attributes;

                AddComponent(e, new AgentStatsIData {
                    MoveSpeed = stats.MoveSpeed,
                    RotationSpeed = stats.RotationSpeed,
                    RestTime = stats.RestTime,
                    HasRestTime = stats.HasRestTime,
                    ExistTime = stats.ExistTime
                });
            }
        }
    }

    public struct AgentStatsIData : IComponentData {
        public float MoveSpeed;
        public float RotationSpeed;
        public float ExistTime;
        public float RestTime;
        public bool HasRestTime;
    }
}