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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

namespace ChoyUtilities {
    [BurstCompile]
    [Serializable]
    public partial struct Reserve<T> :
        IEnumerable<Vault<T>>,
        IComparable<Reserve<T>>,
        IEquatable<Reserve<T>>{
        
        [field: SerializeField] 
        public Vault<T>[] Values { get; set; }
        public Vault<T> this[ushort id] => Values[id];
        public ushort Length => (ushort)Values.Length;

        public Reserve(Reserve<T> other) : this(other.Values) { }
        public Reserve(Vault<T>[] bank) { Values = bank; }
        public Reserve(Vault<T> bank) { Values = new[] { bank }; }
        public Reserve(string name, T content) { Values = new[] { new Vault<T>(name, 0, content) }; }

        public Reserve(ushort id, T content) {
            Values = new Vault<T>[id];
            Values[id] = new Vault<T>(id, content);
        }
        
        public Reserve((string, T)[] values) {
            Values = new Vault<T>[values.Length];
            for (var i = 0; i < values.Length; i++) {
                Values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
            }
        }
        
        public Reserve((FixedString128Bytes, T)[] values) {
            Values = new Vault<T>[values.Length];
            for (var i = 0; i < values.Length; i++) {
                Values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
            }
        }

        public Reserve(T[] values) {
            Values = new Vault<T>[values.Length];
            for (var i = 0; i < values.Length; i++) {
                Values[i] = new Vault<T>(string.Empty, (ushort)i, values[i]);
            }
        }

        [BurstCompile]
        public IEnumerator<Vault<T>> GetEnumerator() {
            foreach (var b in Values) 
                yield return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Reserve<T> other) { return Values.Length.CompareTo(other.Values.Length); }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Reserve<T> other) { return Values.Equals(other.Values); }
    }
}