using System;
using System.Threading;
using Unity.Entities;
using UnityEngine;

namespace ChoyUtilities {
#if UNITY_2023_1_OR_NEWER    
    public static partial class EntitiesCollection {
        private const ushort MAX_FRAME = 200;

        public static async Awaitable<World> TryGetWorld(this CancellationToken token, ushort maxAllowed = MAX_FRAME) {
            var frame = 0;
            
            while (frame < maxAllowed) {
                var world = World.DefaultGameObjectInjectionWorld;

                if (world is not null)
                    return world;
                frame++;
                await Awaitable.NextFrameAsync(token);
            }
            return null;
        }

        public static async Awaitable<TComponent> GetSingletonEntity<TComponent>(this CancellationToken token,
            World world,
            ushort maxAllowed = MAX_FRAME)
            where TComponent : unmanaged, IComponentData {
            var query = world.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<TComponent>());

            TComponent singleton = default;
            var validSingleton = false;
            var frame = 0;

            while (frame < maxAllowed) {
                validSingleton = query.TryGetSingleton(out singleton);

                if (validSingleton) break;
                Debug.Log($"Waiting for Singleton at frame at {frame}");
                frame++;
                await Awaitable.NextFrameAsync(token);
            }

            return !validSingleton ? throw new Exception("Singleton not found") : singleton;
        }

        public static async Awaitable<DynamicBuffer<TBuffer>> GetSingletonBuffer<TBuffer>(this CancellationToken token,
            World world,
            ushort maxAllowed = MAX_FRAME)
            where TBuffer : unmanaged, IBufferElementData {
            var query = world.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<TBuffer>());

            DynamicBuffer<TBuffer> buffer = default;
            var validSingleton = false;
            var frame = 0;

            while (frame < maxAllowed) {
                validSingleton = query.TryGetSingletonBuffer(out buffer);

                if (validSingleton) break;
                Debug.Log($"Waiting for buffer at frame at {frame}");
                frame++;
                await Awaitable.NextFrameAsync(token);
            }

            return !validSingleton ? throw new Exception("Buffer not found") : buffer;
        }
    }
#endif    
}