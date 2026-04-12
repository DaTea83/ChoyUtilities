using Unity.Mathematics;
using Unity.Transforms;

namespace ChoyUtilities.Entities {
    public static class EntitiesCollection {
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

        public static LocalTransform ToLocalTransform(this Floater fs) {
            if (fs.values.Length < 7) throw new FloaterException("Value must be more than 7 floats");
            return new LocalTransform {
                Position = new float3(fs.values[0], fs.values[1], fs.values[2]),
                Rotation = quaternion.Euler(fs.values[3], fs.values[4], fs.values[5]),
                Scale = fs.values[6]
            };
        }
        
        public static bool GetDistanceAndDot(this LocalTransform player,
            LocalTransform target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

        public static bool GetDistanceAndDot(this LocalToWorld player,
            LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward, math.normalize(dir));

            return dot >= 0f;
        }

        public static bool GetDistanceAndDot(this LocalTransform player,
            LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

        public static bool GetDistanceAndDot(this LocalToWorld player,
            LocalTransform target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward, math.normalize(dir));

            return dot >= 0f;
        }
    }
}