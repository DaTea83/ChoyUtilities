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
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;

namespace ChoyUtilities {
    [BurstCompile]
    [Serializable]
    public partial struct RawSet<T> : IDisposable, IEnumerable<T>, IFormattable,
        IComparable, IComparable<RawSet<T>>, IIndexable<T>
        where T : unmanaged {
        private NativeArray<T> _values;
        
        public T this[int index] { readonly get => _values[index]; set => _values[index] = value; }

        public int Length { get; set; }
        public ref T ElementAt(int index) {
            throw new NotImplementedException();
        }
        //public readonly int Length => _values.Length;
        public readonly bool IsCreated => _values.IsCreated;

        public RawSet(RawSet<T> other) {
            _values = other._values; 
            Length = other.Length;
        }

        public RawSet(NativeArray<T> values) {
            _values = values; 
            Length = values.Length;
        }

        public RawSet(NativeList<T> values, Allocator allocator = Allocator.Persistent) : this(
            values.ToArray(allocator)) { }

        public RawSet(byte size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
            Length = size;
        }

        public RawSet(ushort size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
            Length = size;
        }

        public RawSet(int size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
            Length = size;
        }

        public RawSet(T value, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(new []{ value }, allocator);
            Length = 1;
        }

        public RawSet(T[] values, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(values, allocator);
            Length = values.Length;
        }
        
        public RawSet(Span<T> values, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(values.ToArray(), allocator);
            Length = values.Length;
        }

        public RawSet(List<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }

        public RawSet(Stack<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }

        public RawSet(Queue<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }
        
        public void Dispose() {
            if (!IsCreated) return;
            _values.Dispose();
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var t in _values)
                yield return t;
        }

        public int CompareTo(RawSet<T> other) { return Length.CompareTo(other.Length); }

        public override int GetHashCode() { return _values.GetHashCode(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public override string ToString() { return _values.ToString(); }

        public int CompareTo(object obj) { return obj is RawSet<T> other ? CompareTo(other) : -1; }

        public string ToString(string format, IFormatProvider formatProvider) { return ToString(); }

    }
}