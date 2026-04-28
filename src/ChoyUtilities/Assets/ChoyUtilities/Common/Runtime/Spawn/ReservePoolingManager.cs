// Copyright 2026 DeTea83
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace ChoyUtilities {

    public abstract class ReservePoolingManager<TObj, TMono> : GenericSingleton<TMono>, IBootloader 
    where TMono : MonoBehaviour
    where TObj : Component{
        
        [SerializeField] protected Attribute attribute;
        [SerializeField] protected byte poolCount = 32;
        protected List<int> PauseIndexes;

        protected ObjectPool<TObj>[] Pools;
        protected RuntimePoolSerialize[] RuntimePools;
        
        /// <summary>
        ///     Determines if you want to spawn all prefabs as child on start
        /// </summary>
        protected abstract bool InitializeImmediately { get; }

        public virtual async Task Init() {
            await Awaitable.NextFrameAsync(Token);
            if (attribute is null) throw new SingletonException("Attribute is not set");
            if (attribute.pools.IsEmpty) return;
            Pools = new ObjectPool<TObj>[attribute.pools.Length];
            RuntimePools = new RuntimePoolSerialize[attribute.pools.Length];

            for (var i = 0; i < Pools.Length; i++) {
                if (attribute.pools[i].Value is null) continue;

                RuntimePools[i].spawn = new TObj[poolCount];
                Pools[i] = InitPool(attribute.pools[i].Value);

                if (!InitializeImmediately) continue;

                for (var j = 0; j < poolCount; j++) {
                    var spawnObj = Pools[i].Get();
                    spawnObj.gameObject.transform.SetSiblingIndex(j);
                    RuntimePools[i].spawn[j] = spawnObj;
                }
            }
        }
        
        public virtual async Task Shutdown() {
            foreach (var pool in Pools) {
                pool?.Clear();
                pool?.Dispose();
            }
            await Awaitable.EndOfFrameAsync(Token);
        }
        
        protected virtual ObjectPool<TObj> InitPool(Component prefab) {
            return InitPool(() => {
                _ = Instantiate(prefab.gameObject, transform)
                    .TryGetComponent<TObj>(out var component)
                    ? component
                    : throw new SingletonException("Prefab must have a component of type " + typeof(TObj));

                return component;
            });
        }

        public virtual ObjectPool<TObj> InitPool(Func<TObj> createFunc) {
            var pool = new ObjectPool<TObj>(
                createFunc,
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                Destroy,
                false,
                attribute.pools.Length,
                poolCount
            );

            return pool;
        }

        public abstract class Attribute : ScriptableObject {

            public Reserve<TObj> pools;

        }
        
        [Serializable]
        public struct RuntimePoolSerialize {
            public TObj[] spawn;
            public int currentIndex;
            public int previousIndex;
        }
    }

}