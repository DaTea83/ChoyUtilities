using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;

namespace ChoyUtilities {
    
    [BurstCompile]
    public partial struct RawSet<T> : IDisposable, 
        IEquatable<RawSet<T>>, IEnumerable<T>
    where T : unmanaged {
        
        private NativeArray<T> _values;
        
        public RawSet(RawSet<T> other) { _values = other._values; }
        public RawSet(NativeArray<T> values, Allocator allocator = Allocator.Persistent) { _values = values; }
        public RawSet(NativeList<T> values, Allocator allocator = Allocator.Persistent) : this (values.ToArray(allocator)){ }
        public RawSet(byte size, Allocator allocator = Allocator.Persistent) { _values = new NativeArray<T>(size, allocator); }
        public RawSet(ushort size, Allocator allocator = Allocator.Persistent) { _values = new NativeArray<T>(size, allocator); }
        public RawSet(int size, Allocator allocator = Allocator.Persistent) { _values = new NativeArray<T>(size, allocator); }
        public RawSet(T value, int size = 1, Allocator allocator = Allocator.Persistent) { _values = new NativeArray<T>(size, allocator) { [0] = value }; }
        public RawSet(T[] values, Allocator allocator = Allocator.Persistent) { _values = new NativeArray<T>(values, allocator); }
        public RawSet(List<T> values, Allocator allocator = Allocator.Persistent) : this (values.ToArray(), allocator) { }
        public RawSet(Stack<T> values, Allocator allocator = Allocator.Persistent) : this (values.ToArray(), allocator) { }
        public RawSet(Queue<T> values, Allocator allocator = Allocator.Persistent) : this (values.ToArray(), allocator) { }
        
        public readonly T this[int index] => _values[index];
        public readonly int Length => _values.Length;
        public readonly bool IsCreated => _values.IsCreated;
        
        public void Dispose() {
            if (!IsCreated) return;
            _values.Dispose();
        }

        public bool Equals(RawSet<T> other) {
            return _values.Equals(other._values);
        }

        public IEnumerator<T> GetEnumerator() {
            return _values.GetEnumerator();
        }

        public override bool Equals(object obj) {
            return obj is RawSet<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return _values.GetHashCode();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}