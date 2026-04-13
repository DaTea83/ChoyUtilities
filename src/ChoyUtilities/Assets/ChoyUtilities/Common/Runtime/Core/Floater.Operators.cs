using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace ChoyUtilities {
    
    public partial struct Floater {
        
        private const float EPSILON = 0.0001f;
        
        #region Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Add(float value) {
            var set = new float[Values.Length + 1];
            if (set == null) throw new FloaterException(nameof(set));
            Array.Copy(Values, set, Values.Length);
            set[^1] = value;

            return new Floater(set);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Add(float[] extras) {
            if (extras == null || extras == Array.Empty<float>()) return this;
            var set = new float[Values.Length + extras.Length];
            if (set == null) throw new FloaterException(nameof(set));

            for (var i = 0; i < Values.Length; i++)
                set[i] = Values[i];
            for (var i = 0; i < extras.Length; i++)
                set[i + Values.Length] = extras[i];

            return new Floater(set);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Add(Floater extra) {
            if (extra.Values == null || extra.Values == Array.Empty<float>()) return this;
            var set = new float[Values.Length + extra.Values.Length];
            if (set == null) throw new FloaterException(nameof(set));

            for (var i = 0; i < Values.Length; i++)
                set[i] = Values[i];
            for (var i = 0; i < extra.Values.Length; i++)
                set[i + Values.Length] = extra.Values[i];

            return new Floater(set);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FirstMatch(float value) {
            var index = -1;
            for (var i = 0; i < Values.Length; i++) {
                if (!(math.abs(Values[i] - value) < EPSILON)) continue;
                index = i;
                break;
            }
            return index;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        ///     If true, will remove all values that are equal to <paramref name="value" />,
        ///     otherwise will remove only the first value that is equal to <paramref name="value" />.
        /// </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Remove(float value, bool removeAllEqual = false) {
            if (Values == null || Values.Length == 0) return new Floater(Array.Empty<float>());

            if (removeAllEqual) {
                var keepCount = 0;

                foreach (var t in Values)
                    if (math.abs(t - value) >= EPSILON)
                        keepCount++;

                if (keepCount == Values.Length) return this;
                if (keepCount == 0) return new Floater(Array.Empty<float>());

                var set = new float[keepCount];
                var j = 0;

                foreach (var t in Values) {
                    if (math.abs(t - value) < EPSILON) continue;
                    set[j] = t;
                    j++;
                }

                return new Floater(set);
            }
            else {
                var removeIndex = FirstMatch(value);

                if (removeIndex < 0) return this;
                if (Values.Length == 1) return new Floater(Array.Empty<float>());
                var set = new float[Values.Length - 1];

                if (removeIndex > 0)
                    Array.Copy(Values, 0, set, 0, removeIndex);
                if (removeIndex < Values.Length - 1)
                    Array.Copy(Values, removeIndex + 1, set, removeIndex, Values.Length - removeIndex - 1);

                return new Floater(set);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        ///     If true, will remove all values that <paramref name="value" /> contains,
        ///     otherwise will remove only the first value that in <paramref name="value" />.
        /// </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Remove(float[] value, bool removeAllEqual = false) {
            return removeAllEqual ? RemoveAllMatches(value) : RemoveFirstMatches(value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        ///     If true, will remove all values that <paramref name="value" /> contains,
        ///     otherwise will remove only the first value that in <paramref name="value" />.
        /// </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater Remove(Floater value, bool removeAllEqual = false) {
            return removeAllEqual ? RemoveAllMatches(value.Values) : RemoveFirstMatches(value.Values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Floater RemoveFirstMatches(float[] valuesToRemove) {
            if (Values == null || Values.Length == 0) return new Floater(Array.Empty<float>());
            if (valuesToRemove == null || valuesToRemove.Length == 0) return this;
            
            var consumed = new bool[valuesToRemove.Length];
            var keepCount = 0;

            foreach (var t in Values) {
                var shouldRemove = false;

                for (var j = 0; j < valuesToRemove.Length; j++) {
                    if (consumed[j]) continue;

                    if (!(math.abs(t - valuesToRemove[j]) < EPSILON)) continue;
                    consumed[j] = true;
                    shouldRemove = true;
                    break;
                }

                if (!shouldRemove) keepCount++;
            }

            if (keepCount == Values.Length) return this;
            if (keepCount == 0) return new Floater(Array.Empty<float>());

            var set = new float[keepCount];
            consumed = new bool[valuesToRemove.Length];
            var index = 0;

            foreach (var t in Values) {
                var shouldRemove = false;

                for (var j = 0; j < valuesToRemove.Length; j++) {
                    if (consumed[j]) continue;

                    if (!(math.abs(t - valuesToRemove[j]) < EPSILON)) continue;
                    consumed[j] = true;
                    shouldRemove = true;
                    break;
                }

                if (shouldRemove) continue;

                set[index] = t;
                index++;
            }

            return new Floater(set);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Floater RemoveAllMatches(float[] valuesToRemove) {
            if (Values == null || Values.Length == 0) return new Floater(Array.Empty<float>());
            if (valuesToRemove == null || valuesToRemove.Length == 0) return this;
            var keepCount = 0;

            foreach (var t in Values) {
                var shouldRemove = false;
                foreach (var t1 in valuesToRemove) {
                    if (!(math.abs(t - t1) < EPSILON)) continue;
                    shouldRemove = true;
                    break;
                }

                if (!shouldRemove) keepCount++;
            }

            if (keepCount == Values.Length) return this;
            if (keepCount == 0) return new Floater(Array.Empty<float>());

            var set = new float[keepCount];
            var index = 0;

            foreach (var t in Values) {
                var shouldRemove = false;
                foreach (var t1 in valuesToRemove) {
                    if (!(math.abs(t - t1) < EPSILON)) continue;
                    shouldRemove = true;
                    break;
                }

                if (shouldRemove)
                    continue;

                set[index] = t;
                index++;
            }

            return new Floater(set);
        }

        #endregion
    }
}