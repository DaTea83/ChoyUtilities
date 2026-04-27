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
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

namespace ChoyUtilities {

    [Serializable]
    public partial struct Locker<T> : 
        IComparable<FixedString128Bytes>, 
        IComparable<string>, 
        IComparable<Locker<T>>, 
        IEquatable<FixedString128Bytes>, 
        IEquatable<string>, 
        IEquatable<Locker<T>>, 
        IFormattable
        where T : IComparable, IComparable<T>, IEquatable<T>,
        IFormattable {

        [SerializeField] private FixedString128Bytes key;
        [SerializeField] private T value;

        public FixedString128Bytes Key => key;
        public T Value => value;
        public bool IsKeyEmpty => key.IsEmpty;

        public Locker(FixedString128Bytes key, T value) {
            this.key = key;
            this.value = value;
        }

        public Locker(string key, T value) {
            this.key = key;
            this.value = value;
        }

        public Locker(T value) {
            key = string.Empty;
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Locker<T> other) {
            var keyComparison = key.CompareTo(other.key);
            return keyComparison != 0 ? keyComparison : value.CompareTo(other.value);
        }
        
        [BurstDiscard]
        public override string ToString() {
            return $"{nameof(key)}: {key}, {nameof(value)}: {value}, {nameof(Key)}: {Key}, {nameof(Value)}: {Value}";
        }

        [BurstDiscard]
        public string ToString(string format, IFormatProvider formatProvider) {
            return ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Locker<T> other) {
            return key.Equals(other.key) && EqualityComparer<T>.Default.Equals(value, other.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(FixedString128Bytes other) {
            var keyComparison = key.CompareTo(other);

            return keyComparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(FixedString128Bytes other) {
            return key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(string other) {
            var keyComparison = key.CompareTo(other);

            return keyComparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(string other) {
            return key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) {
            return obj is Locker<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(key, value);
        }

    }

}