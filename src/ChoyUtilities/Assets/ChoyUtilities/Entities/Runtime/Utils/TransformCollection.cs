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
using Unity.Mathematics;
using Unity.Transforms;

namespace ChoyUtilities.Entities {
    [BurstCompile]
    public static class EntitiesCollection {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Floater Floater(this LocalTransform lt) {
            var set = new float[7];
            var euler = math.Euler(lt.Rotation);
            set[0] = lt.Position.x;
            set[1] = lt.Position.y;
            set[2] = lt.Position.z;
            set[3] = euler.x;
            set[4] = euler.y;
            set[5] = euler.z;
            set[6] = lt.Scale;

            return new Floater(set);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalTransform ToLocalTransform(this Floater fs) {
            if (fs.Length < 7) return default;

            return new LocalTransform {
                Position = new float3(fs[0], fs[1], fs[2]),
                Rotation = quaternion.Euler(fs[3], fs[4], fs[5]),
                Scale = fs[6]
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetDistanceAndDot(this LocalTransform player,
            in LocalTransform target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetDistanceAndDot(this LocalToWorld player,
            in LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward, math.normalize(dir));

            return dot >= 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetDistanceAndDot(this LocalTransform player,
            in LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetDistanceAndDot(this LocalToWorld player,
            in LocalTransform target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward, math.normalize(dir));

            return dot >= 0f;
        }
    }
}