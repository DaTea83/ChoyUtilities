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

using UnityEngine;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace ChoyUtilities.Entities {
    [CreateAssetMenu(fileName = "AgentAttributes", menuName = "Choy Utilities/Agents", order = 0)]
    public class AgentAttributes : ScriptableObject {
        [Min(0.01f)] public float MoveSpeed = 10f;
        [Min(0.01f)] public float RotationSpeed = 3f;
        public bool HasRestTime = true;
        [Min(0.01f)] public float RestTime = 1f;

        [Tooltip("0 and below means it doesn't despawn")] [Min(0f)]
        public float ExistTime;
    }
}