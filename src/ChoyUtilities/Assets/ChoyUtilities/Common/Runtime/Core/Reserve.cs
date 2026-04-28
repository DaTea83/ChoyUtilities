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

    /// <summary>
    ///     A dictionary-like structure that can store any type of data and uses string keys
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [BurstCompile]
    [Serializable]
    public partial struct Reserve<T> :
        IEnumerable<Vault<T>>,
        IComparable<Reserve<T>>,
        IEquatable<Reserve<T>> {

        [SerializeField] private Vault<T>[] values;
        public readonly Vault<T> this[byte index] => values[index];
        public readonly Vault<T> this[ushort index] => values[index];
        public readonly Vault<T> this[int index] => values[index];
        public readonly ushort Length => (ushort)values.Length;
        public readonly bool IsEmpty => values.Length == 0;

        public Reserve(Reserve<T> other) : this(other.values) { }

        public Reserve(Vault<T>[] bank) {
            values = bank;
        }

        public Reserve(Vault<T> bank) {
            values = new[] { bank };
        }

        public Reserve(string name, T value) {
            values = new[] { new Vault<T>(name, 0, value) };
        }

        public Reserve(T value) {
            values = new[] { new Vault<T>(string.Empty, 0, value) };
        }

        public Reserve(T[] values) {
            this.values = new Vault<T>[values.Length];
            for (var i = 0; i < values.Length; i++) this.values[i] = new Vault<T>(string.Empty, (ushort)i, values[i]);
        }

        public Reserve(ushort id, T value) {
            values = new Vault<T>[id];
            values[id] = new Vault<T>(id, value);
        }

        public Reserve((string, T)[] values) {
            this.values = new Vault<T>[values.Length];

            for (var i = 0; i < values.Length; i++)
                this.values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
        }

        public Reserve((FixedString32Bytes, T)[] values) {
            this.values = new Vault<T>[values.Length];

            for (var i = 0; i < values.Length; i++)
                this.values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
        }

        public Reserve((FixedString64Bytes, T)[] values) {
            this.values = new Vault<T>[values.Length];

            for (var i = 0; i < values.Length; i++)
                this.values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
        }

        public Reserve((FixedString128Bytes, T)[] values) {
            this.values = new Vault<T>[values.Length];

            for (var i = 0; i < values.Length; i++)
                this.values[i] = new Vault<T>(values[i].Item1, (ushort)i, values[i].Item2);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Reserve<T> other) {
            return values.Length.CompareTo(other.values.Length);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Reserve<T> other) {
            return values.Equals(other.values);
        }

        [BurstCompile]
        public IEnumerator<Vault<T>> GetEnumerator() {
            foreach (var vault in values) 
                yield return vault;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}