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

using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace ChoyUtilities {
    [BurstCompile]
    public static partial class EntitiesCollection {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomValue(this Entity entity, double et) {
            var ran = Random.CreateFromIndex(CreateSeed(entity, et));

            return ran.NextFloat();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomValue(this Entity entity, double et, float min, float max) {
            var ran = Random.CreateFromIndex(CreateSeed(entity, et));

            return ran.NextFloat(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomValue(this Entity entity, double et, int min, int max) {
            var ran = Random.CreateFromIndex(CreateSeed(entity, et));

            return ran.NextInt(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint CreateSeed(Entity entity, double et) {
            // unchecked: Expect the number to overflow, but it will round it back to 0
            unchecked {
                var seed = (uint)entity.Index;
                seed ^= (uint)et << 3;
                // Both random prime numbers
                seed = seed * 2722380223u + 1137611711u;

                if (seed == 0) seed = 1;

                return seed;
            }
        }
    }
}