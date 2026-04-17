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