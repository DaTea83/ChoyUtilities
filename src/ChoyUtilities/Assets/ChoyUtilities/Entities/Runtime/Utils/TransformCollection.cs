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
            if (fs.Length < 7) throw new FloaterException("Value must be more than 7 floats");
            return new LocalTransform {
                Position = new float3(fs[0], fs[1], fs[2]),
                Rotation = quaternion.Euler(fs[3], fs[4], fs[5]),
                Scale = fs[6]
            };
        }
        
        public static bool GetDistanceAndDot(this LocalTransform player,
            in LocalTransform target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

        public static bool GetDistanceAndDot(this LocalToWorld player,
            in LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward, math.normalize(dir));

            return dot >= 0f;
        }

        public static bool GetDistanceAndDot(this LocalTransform player,
            in LocalToWorld target,
            out float distanceSqr,
            out float dot) {
            var dir = target.Position - player.Position;
            distanceSqr = math.lengthsq(dir);
            dot = math.dot(player.Forward(), math.normalize(dir));

            return dot >= 0f;
        }

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