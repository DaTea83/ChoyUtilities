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
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

namespace ChoyUtilities {

    [BurstCompile]
    [Serializable]
    public struct Vault<T> : 
        IComparable<Vault<T>>, 
        IComparable<Locker<ushort>>, 
        IEquatable<Vault<T>>,
        IEquatable<Locker<ushort>> {

        [SerializeField] private Locker<ushort> locker;
        public string Key { get => locker.Key.ToString(); set => locker.Key = value; }
        public ushort Id { get => locker.Value; set => locker.Value = value; }
        
        [field: SerializeField]
        public T Value { get; set; }

        public Vault(ushort id, T value) {
            this.locker = new Locker<ushort>(id);
            Value = value;
        }

        public Vault(string key, ushort id, T value) {
            this.locker = new Locker<ushort>(key, id);
            Value = value;
        }
        
        public Vault(FixedString128Bytes key, ushort id, T value) {
            this.locker = new Locker<ushort>(key, id);
            Value = value;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Vault<T> other) {
            return locker.CompareTo(other.locker);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Locker<ushort> other) {
            return locker.CompareTo(other);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vault<T> other) {
            return locker.Equals(other.locker);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Locker<ushort> other) {
            return locker.Equals(other);
        }

    }

}