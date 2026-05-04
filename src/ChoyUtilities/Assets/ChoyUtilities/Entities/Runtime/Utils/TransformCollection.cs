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
            var euler = math.Euler(lt.Rotation);
            return new Floater(lt.Position.x, lt.Position.y, lt.Position.z, euler.x, euler.y, euler.z, lt.Scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalTransform ToLocalTransform(this Floater floater) {

            return new LocalTransform {
                Position = new float3(floater[0], floater[1], floater[2]),
                Rotation = quaternion.Euler(floater[3], floater[4], floater[5]),
                Scale = floater[6]
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