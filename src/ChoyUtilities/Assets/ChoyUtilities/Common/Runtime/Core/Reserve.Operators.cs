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

using System;
using Unity.Collections;

namespace ChoyUtilities {

    public partial struct Reserve<T> {

        public bool TryGet(FixedString128Bytes key, out T value) {
            value = default;

            foreach (var v in values) {
                if (v.IsKeyEmpty) continue;
                if (v.Key != key) continue;
                value = v.Value;

                return true;
            }

            return false;
        }
        
        public bool TryGet(FixedString128Bytes key, out ushort id, out T value) {
            value = default;
            id = ushort.MaxValue;

            foreach (var v in values) {
                if (v.IsKeyEmpty) continue;
                if (v.Key != key) continue;
                
                value = v.Value;
                id = v.Id;
                return true;
            }

            return false;
        }

        public bool TryGet(string key, out T value) {
            return TryGet((FixedString128Bytes)key, out value);
        }

        public bool TryGet(string key, out ushort id, out T value) {
            return TryGet((FixedString128Bytes)key, out id, out value);
        }

        /// <summary>
        ///     This is only allowed if the key is empty
        /// </summary>
        public bool TryGetFromId(ushort id, out T value) {
            value = default;

            foreach (var v in values) {
                if (!v.IsKeyEmpty) continue;
                if (v.Id != id) continue;
                value = v.Value;

                return true;
            }

            return false;
        }

        public bool TryGetFromId(int id, out T value) {
            return TryGetFromId((ushort)id, out value);
        }

        /// <summary>
        ///     Adds a key if the key is empty
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddKey(FixedString128Bytes key, ushort id) {
            foreach (var t in values) {
                var v = t;

                if (!v.IsKeyEmpty) continue;
                if (v.Id != id) continue;
                v.Key = key;

                return true;
            }

            return false;
        }

        public bool AddKey(string key, ushort id) {
            return AddKey((FixedString128Bytes)key, id);
        }

        public T Replace(FixedString128Bytes key, T value) {
            foreach (var t in values) {
                var v = t;

                if (v.IsKeyEmpty) continue;
                if (v.Key != key) continue;
                v.Value = value;

                return v.Value;
            }

            return default;
        }

        public T Replace(string key, T value) {
            return Replace((FixedString128Bytes)key, value);
        }

        /// <summary>
        ///     If finds the existing key and replaces the value,
        ///     otherwise will add the new key-value pair
        /// </summary>
        public Reserve<T> Insert(FixedString128Bytes key, T value) {
            foreach (var t in values) {
                var v = t;

                if (v.IsKeyEmpty) continue;
                if (v.Key != key) continue;
                v.Value = value;

                return this;
            }

            var newReserve = new Vault<T>[values.Length + 1];
            Array.Copy(values, newReserve, values.Length);
            newReserve[values.Length] = new Vault<T>(key, (ushort)values.Length, value);
            values = newReserve;

            return this;
        }

        public Reserve<T> Insert(string key, T value) {
            return Insert((FixedString128Bytes)key, value);
        }

    }

}