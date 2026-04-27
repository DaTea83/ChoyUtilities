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
using UnityEngine;

namespace ChoyUtilities {

    [Serializable]
    public struct Banker<T> : 
        IComparable<Banker<T>>, 
        IComparable<Locker<ushort>>, 
        IEquatable<Banker<T>>,
        IEquatable<Locker<ushort>> {

        [SerializeField] private Locker<ushort> id;
        [SerializeField] private T value;
        
        public string Key => id.Key.ToString();
        public ushort Id => id.Value;
        public T Value => value;

        public Banker(ushort id, T value) {
            this.id = new Locker<ushort>(id);
            this.value = value;
        }

        public Banker(string key, ushort id, T value) {
            this.id = new Locker<ushort>(key, id);
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Banker<T> other) {
            return id.CompareTo(other.id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Locker<ushort> other) {
            return id.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Banker<T> other) {
            return id.Equals(other.id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Locker<ushort> other) {
            return id.Equals(other);
        }

    }

}