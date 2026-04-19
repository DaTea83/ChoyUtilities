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

namespace ChoyUtilities.Entities {
    public struct InitializeAgentIData : IComponentData {
        public Entity Spawn;
    }

    public struct AgentMoveIEnableable : IComponentData, IEnableableComponent {
        public Entity CurrentNode;
        public float CurrentRestTime;
    }

    public struct AgentMoveICleanupTag : ICleanupComponentData { }

    public struct ConnectedNodeIBuffer : IBufferElementData {
        public Entity ConnectedNode;
    }

    public struct SpawnNodeIBuffer : IBufferElementData {
        public Entity Prefab;
    }

    public struct SpawnNodeIEnableable : IComponentData, IEnableableComponent {
        public bool SpawnOnce;
        public float DefaultSpawnDelay;
        public float CurrentSpawnDelay;
    }

    public struct AgentISingleton : IComponentData {
        public ushort TotalSpawnCount;
        public ushort CurrentSpawnCount;
        public ushort SpawnLimit;
    }
}