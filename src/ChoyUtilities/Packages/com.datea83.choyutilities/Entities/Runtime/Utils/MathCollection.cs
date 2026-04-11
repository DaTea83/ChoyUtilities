using Unity.Entities;
using Unity.Mathematics;

namespace ChoyUtilities {
    
    public static partial class EntitiesCollection {
        
        public static float RandomValue(this Entity entity, double et) {
            var ran = Random.CreateFromIndex(((uint)entity.Index + (uint)et) << (4 + 1));

            return ran.NextFloat();
        }

        public static float RandomValue(this Entity entity, double et, float min, float max) {
            var ran = Random.CreateFromIndex(((uint)entity.Index + (uint)et) << (4 + 1));

            return ran.NextFloat(min, max);
        }

        public static int RandomValue(this Entity entity, double et, int min, int max) {
            var ran = Random.CreateFromIndex(((uint)entity.Index + (uint)et) << (4 + 1));

            return ran.NextInt(min, max);
        }
    }
}