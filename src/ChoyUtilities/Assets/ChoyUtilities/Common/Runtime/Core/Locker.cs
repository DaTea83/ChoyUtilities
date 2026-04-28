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

    [BurstCompile]
    [Serializable]
    public partial struct Locker<T> : 
        IComparable<FixedString128Bytes>, 
        IComparable<FixedString64Bytes>, 
        IComparable<FixedString32Bytes>, 
        IComparable<string>, 
        IComparable<Locker<T>>, 
        IEquatable<FixedString128Bytes>, 
        IEquatable<FixedString64Bytes>, 
        IEquatable<FixedString32Bytes>, 
        IEquatable<string>, 
        IEquatable<Locker<T>>, 
        IFormattable
        where T : IComparable, IComparable<T>, IEquatable<T>,
        IFormattable, IConvertible {

        [field: SerializeField]
        public FixedString128Bytes Key { get ; set ;}
        [field: SerializeField]
        public T Value { get ; set ;}
        public bool IsKeyEmpty => Key.IsEmpty;

        public Locker(FixedString128Bytes key, T value) {
            Key = key;
            Value = value;
        }
        
        public Locker(FixedString64Bytes key, T value) {
            Key = key;
            Value = value;
        }

        public Locker(FixedString32Bytes key, T value) {
            Key = key;
            Value = value;
        }
        
        public Locker(string key, T value) {
            Key = key;
            Value = value;
        }

        public Locker(T value) {
            Key = string.Empty;
            Value = value;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Locker<T> other) => Key.CompareTo(other.Key);
        
        [BurstDiscard]
        public override string ToString() {
            return $"{nameof(Key)}: {Key}, {nameof(Value)}: {Value}";
        }

        [BurstDiscard]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => ToString();

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Locker<T> other) {
            return Key.Equals(other.Key) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(FixedString128Bytes other) => Key.CompareTo(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(FixedString128Bytes other) => Key.Equals(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(string other) => Key.CompareTo(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(string other) => Key.Equals(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(FixedString64Bytes other) => Key.CompareTo(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(FixedString64Bytes other) => Key.Equals(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(FixedString32Bytes other) => Key.CompareTo(other);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(FixedString32Bytes other) => Key.Equals(other);

        [BurstCompile]
        public override int GetHashCode() => HashCode.Combine(Key, Value);

    }

}