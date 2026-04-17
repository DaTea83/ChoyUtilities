using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;

namespace ChoyUtilities {

    [BurstCompile]
    [Serializable]
    public partial struct RawSet<T> : IDisposable, IEnumerable<T>, IFormattable,
        IComparable, IComparable<RawSet<T>>
        where T : unmanaged {

        private NativeArray<T> _values;

        public RawSet(RawSet<T> other) {
            _values = other._values;
        }

        public RawSet(NativeArray<T> values) {
            _values = values;
        }

        public RawSet(NativeList<T> values, Allocator allocator = Allocator.Persistent) : this(
            values.ToArray(allocator)) { }

        public RawSet(byte size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
        }

        public RawSet(ushort size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
        }

        public RawSet(int size, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator);
        }

        public RawSet(T value, int size = 1, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(size, allocator) { [0] = value };
        }

        public RawSet(T[] values, Allocator allocator = Allocator.Persistent) {
            _values = new NativeArray<T>(values, allocator);
        }

        public RawSet(List<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }

        public RawSet(Stack<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }

        public RawSet(Queue<T> values, Allocator allocator = Allocator.Persistent) :
            this(values.ToArray(), allocator) { }

        public readonly T this[int index] => _values[index];
        public readonly int Length => _values.Length;
        public readonly bool IsCreated => _values.IsCreated;

        public void Dispose() {
            if (!IsCreated) return;
            _values.Dispose();
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var t in _values)
                yield return t;
        }

        public int CompareTo(RawSet<T> other) {
            return Length.CompareTo(other.Length);
        }

        public override int GetHashCode() {
            return _values.GetHashCode();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override string ToString() {
            return $"{nameof(_values)}: {_values}, {nameof(Length)}: {Length}, {nameof(IsCreated)}: {IsCreated}";
        }

        public int CompareTo(object obj) {
            return obj is RawSet<T> other ? CompareTo(other) : -1;
        }

        public string ToString(string format, IFormatProvider formatProvider) {
            return ToString();
        }

    }

}