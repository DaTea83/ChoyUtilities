using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ChoyUtilities {

    public static partial class HelperCollection {

        public static Transform FloatsSerializeToTransform(this Transform obj, Floater data) {
            if (data.values.Length < 9) return obj;
            var euler = new Vector3(data.values[3], data.values[4], data.values[5]);
            obj.position = new Vector3(data.values[0], data.values[1], data.values[2]);
            obj.rotation = quaternion.Euler(euler);
            obj.localScale = new Vector3(data.values[6], data.values[7], data.values[8]);
            return obj;
        }

        public static Quaternion RotateTowards(this Transform obj, float3 target, float speed) {
            var dir = math.normalize(target - (float3)obj.position);
            var lookTowards = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

            return Quaternion.Slerp(obj.rotation, lookTowards, Time.deltaTime * speed);
        }

        public static bool FinishRotate(this Transform obj, Vector3 target, float threshold = 5f) {
            var dir = (target - obj.position).normalized;
            var angle = Vector3.Angle(obj.forward, dir);

            return angle < threshold;
        }

        public static Transform FindNearestTransform(this List<Transform> posList, in Transform currentPosition) {
            if (posList is null || currentPosition is null) return null;

            Transform nearest = null;
            var disToNearest = 0f;

            foreach (var pos in posList) {
                if (pos is null) continue;
                var distance = (currentPosition.position - pos.position).magnitude;

                if (nearest is not null && !(distance < disToNearest))
                    continue; //&& CurrentPosition.CanMoveThere(nearest))) 
                nearest = pos;
                disToNearest = distance;
            }

            return nearest;
        }

        public static Transform FindNearestTransform(this List<Transform> posList,
            in Transform currentPosition,
            in List<Transform> prevPos) {
            if (posList is null || currentPosition is null) return null;

            Transform nearest = null;
            var disToNearest = 0f;

            foreach (var pos in posList) {
                if (pos is null || prevPos is null) continue;
                if (prevPos.Contains(pos)) continue;
                var distance = (currentPosition.position - pos.position).magnitude;

                if (nearest is not null && !(distance < disToNearest))
                    continue; //&& CurrentPosition.CanMoveThere(nearest))) 
                nearest = pos;
                disToNearest = distance;
            }

            return nearest;
        }

        public static bool CanMoveThere(this Transform pos, float3 target, string tag) {
            var dir = math.normalize(target - (float3)pos.position);
            var ray = new Ray(pos.position, dir);

            if (!Physics.Raycast(ray, out var hitInfo)) return true;

            return !hitInfo.collider.CompareTag(tag);
        }

        public static Transform FindNearestGameObject(this GameObject bot, in List<GameObject> objectList) {
            Transform target = null;
            var disToNearest = 0f;

            foreach (var potentialTarget in objectList) {
                if (potentialTarget == bot) continue;
                var distance = (bot.transform.position - potentialTarget.transform.position).magnitude;

                if (target is not null && !(distance < disToNearest)) continue;
                target = potentialTarget.transform;
                disToNearest = distance;
            }

            return target;
        }

        public static GameObject FindNearestObjectInRange(this Transform ob, in List<GameObject> obList, float maxRange) {
            GameObject nearest = null;
            float distanceToNearest = 0;

            foreach (var spawned in obList) {
                if (spawned.transform == ob) continue;
                var distance = math.distance(ob.position, spawned.transform.position);

                if (!(distance <= maxRange)) continue;
                if (nearest is not null && !(distance < distanceToNearest)) continue;

                nearest = spawned;
                distanceToNearest = distance;
            }

            return nearest;
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