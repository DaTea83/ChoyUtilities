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

using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Transforms;

// Purpose of these is to help encapsulates custom define systems and organize them
namespace ChoyUtilities.Entities {
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    [WorldSystemFilter(WorldSystemFilterFlags.Editor, WorldSystemFilterFlags.Editor)]
    public partial class EuCEditorSystemGroup : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems related to initialization logic that should be executed towards the beginning of
    ///     the frame.
    /// </summary>
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndInitializationEntityCommandBufferSystem))]
    internal partial class EuCInitializationSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(EuCInitializationSystemGroup))]
    public partial class EuCSpawnSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(EuCInitializationSystemGroup), OrderFirst = true)]
    public partial class EuCManagedComponentSystem : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems that deal with physics such as scheduling collision/trigger event jobs and
    ///     executing physics overlap queries.
    /// </summary>
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial class EuCPhysicsSystemGroup : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems that deal with moving entities via their LocalTransform component.
    /// </summary>
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial class EuCPreTransformSystemGroup : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems related to entity interactions
    /// </summary>
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial class EuCPostTransformSystemGroup : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems that deal with triggering visual and audio effects.
    /// </summary>
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EuCDestroySystemGroup))]
    public partial class EuCEffectSystemGroup : ComponentSystemGroup { }

    /// <summary>
    ///     System group containing systems related to entity destruction.
    /// </summary>
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial class EuCDestroySystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(EuCDestroySystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(DestroyEntityISystem))]
    public partial class EuCCleanupSystemGroup : ComponentSystemGroup { }
}