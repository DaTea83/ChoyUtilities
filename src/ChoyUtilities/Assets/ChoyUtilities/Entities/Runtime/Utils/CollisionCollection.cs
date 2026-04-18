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

using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace ChoyUtilities {
    public static partial class EntitiesCollection {
        [BurstCompile]
        public static (Entity, Entity) GetSimulationEntities<TA, TB>(this TriggerEvent triggerEvent,
            ComponentLookup<TA> aLookup,
            ComponentLookup<TB> bLookup)
            where TA : unmanaged, IComponentData
            where TB : unmanaged, IComponentData {
            Entity aEntity, bEntity;

            if (aLookup.HasComponent(triggerEvent.EntityA) &&
                bLookup.HasComponent(triggerEvent.EntityB)) {
                aEntity = triggerEvent.EntityA;
                bEntity = triggerEvent.EntityB;

                return (aEntity, bEntity);
            }

            if (aLookup.HasComponent(triggerEvent.EntityB) &&
                bLookup.HasComponent(triggerEvent.EntityA)) {
                aEntity = triggerEvent.EntityB;
                bEntity = triggerEvent.EntityA;

                return (aEntity, bEntity);
            }

            aEntity = Entity.Null;
            bEntity = Entity.Null;

            return (aEntity, bEntity);
        }

        [BurstCompile]
        public static (Entity, Entity, int) GetSimulationEntities<TA, TB, TC>(this TriggerEvent triggerEvent,
            ComponentLookup<TA> aLookup,
            ComponentLookup<TB> bLookup,
            ComponentLookup<TC> cLookup)
            where TA : unmanaged, IComponentData
            where TB : unmanaged, IComponentData
            where TC : unmanaged, IComponentData {
            var (aEntity, bEntity) = triggerEvent.GetSimulationEntities(aLookup, bLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 1);

            (aEntity, bEntity) = triggerEvent.GetSimulationEntities(bLookup, cLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 2);

            (aEntity, bEntity) = triggerEvent.GetSimulationEntities(aLookup, cLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 3);

            return (aEntity, bEntity, -1);
        }

        [BurstCompile]
        public static (Entity, Entity) GetSimulationEntities<TA, TB>(this CollisionEvent collisionEvent,
            ComponentLookup<TB> aLookup,
            ComponentLookup<TA> bLookup)
            where TB : unmanaged, IComponentData
            where TA : unmanaged, IComponentData {
            Entity aEntity, bEntity;

            if (aLookup.HasComponent(collisionEvent.EntityA) &&
                bLookup.HasComponent(collisionEvent.EntityB)) {
                aEntity = collisionEvent.EntityA;
                bEntity = collisionEvent.EntityB;

                return (aEntity, bEntity);
            }

            if (aLookup.HasComponent(collisionEvent.EntityB) &&
                bLookup.HasComponent(collisionEvent.EntityA)) {
                aEntity = collisionEvent.EntityB;
                bEntity = collisionEvent.EntityA;

                return (aEntity, bEntity);
            }

            aEntity = Entity.Null;
            bEntity = Entity.Null;

            return (aEntity, bEntity);
        }

        [BurstCompile]
        public static (Entity, Entity, int) GetSimulationEntities<TA, TB, TC>(this CollisionEvent collisionEvent,
            ComponentLookup<TA> aLookup,
            ComponentLookup<TB> bLookup,
            ComponentLookup<TC> cLookup)
            where TA : unmanaged, IComponentData
            where TB : unmanaged, IComponentData
            where TC : unmanaged, IComponentData {
            var (aEntity, bEntity) = collisionEvent.GetSimulationEntities(aLookup, bLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 1);

            (aEntity, bEntity) = collisionEvent.GetSimulationEntities(bLookup, cLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 2);

            (aEntity, bEntity) = collisionEvent.GetSimulationEntities(aLookup, cLookup);

            if (aEntity != Entity.Null && bEntity != Entity.Null) return (aEntity, bEntity, 3);

            return (aEntity, bEntity, -1);
        }
    }
}