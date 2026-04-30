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
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class FloaterTest {
        private const float TOLERANCE = 0.0001f;

        private static void AssertFloaterValues(float[] expected, Floater actual) {
            Assert.IsNotNull(actual.values);
            Assert.GreaterOrEqual(actual.values.Length, expected.Length);

            for (var i = 0; i < expected.Length; i++) {
                Assert.AreEqual(expected[i], actual.values[i], TOLERANCE, $"Index {i}");
            }

            for (var i = expected.Length; i < actual.values.Length; i++) {
                Assert.AreEqual(0f, actual.values[i], TOLERANCE, $"Index {i}");
            }
        }

        [Test]
        public void Floater_ValueTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert[1], floater[1]);
        }

        [Test]
        public void Floater_IsCreatedTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.IsTrue(floater.IsCreated);
        }

        [Test]
        public void Floater_GetEnumeratorTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            var list = new List<float>(floater);
            Assert.AreEqual(floater, list);
        }

        [Test]
        public void Floater_ToStringTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert.ToString(), floater.ToString());
        }

        [Test]
        public void Floater_EqualsTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            var test = new[] {
                floater[0], floater[1], floater[2],
            };
            AssertFloaterValues(insert, floater);
        }

        [Test]
        public void Floater_Float3ArrayTest() {
            var insert = new[] {
                new float3(1, 2, 3),
                new float3(4, 5, 6),
                new float3(7, 8, 9)
            };
            var expected = new[] {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            };
#pragma warning disable CS0618
            var floater = new Floater(insert);
#pragma warning restore CS0618
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CharArrayTest() {
            var insert = new[] {
                'H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!'
            };
            const string expected = "Hello World!\0\0\0\0";
            string floater = new Floater(insert);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_FloatArrayConstructorTest() {
            var insert = new[] { 1f, 2f, 3f };
            var floater = new Floater(insert);
            AssertFloaterValues(insert, floater);
        }

        [Test]
        public void Floater_IntArrayConstructorTest() {
            var insert = new[] { 1, 2, 3 };
            var expected = new[] { 1f, 2f, 3f };
            var floater = new Floater(insert);
            AssertFloaterValues(expected, floater);
        }

        [Test]
        public void Floater_FloaterConstructorTest() {
            var source = new Floater(1f, 2f, 3f);
            var floater = new Floater(source);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
            Assert.AreNotSame(source.values, floater.values);
        }

        [Test]
        public void Floater_Float1ConstructorTest() {
            var floater = new Floater(1f);
            AssertFloaterValues(new[] { 1f }, floater);
        }

        [Test]
        public void Floater_Float2ConstructorTest() {
            var floater = new Floater(1f, 2f);
            AssertFloaterValues(new[] { 1f, 2f }, floater);
        }

        [Test]
        public void Floater_Float3ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_Float4ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f }, floater);
        }

        [Test]
        public void Floater_Float5ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f }, floater);
        }

        [Test]
        public void Floater_Float6ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f }, floater);
        }

        [Test]
        public void Floater_Float7ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f }, floater);
        }

        [Test]
        public void Floater_Float8ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f }, floater);
        }

        [Test]
        public void Floater_Float9ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f }, floater);
        }

        [Test]
        public void Floater_Float10ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f }, floater);
        }

        [Test]
        public void Floater_Float11ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f }, floater);
        }

        [Test]
        public void Floater_Float12ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f }, floater);
        }

        [Test]
        public void Floater_Float13ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f }, floater);
        }

        [Test]
        public void Floater_Float14ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f }, floater);
        }

        [Test]
        public void Floater_Float15ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f }, floater);
        }

        [Test]
        public void Floater_Float16ConstructorTest() {
            var floater = new Floater(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f }, floater);
        }

        [Test]
        public void Floater_Float2ValueConstructorTest() {
            var insert = new float2(1f, 2f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f }, floater);
        }

        [Test]
        public void Floater_Float3ValueConstructorTest() {
            var insert = new float3(1f, 2f, 3f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_Float4ValueConstructorTest() {
            var insert = new float4(1f, 2f, 3f, 4f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f }, floater);
        }

        [Test]
        public void Floater_ColorConstructorTest() {
            var insert = new Color(0.1f, 0.2f, 0.3f, 0.4f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 0.1f, 0.2f, 0.3f, 0.4f }, floater);
        }

        [Test]
        public void Floater_Vector2ConstructorTest() {
            var insert = new Vector2(1f, 2f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f }, floater);
        }

        [Test]
        public void Floater_Vector3ConstructorTest() {
            var insert = new Vector3(1f, 2f, 3f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_Vector4ConstructorTest() {
            var insert = new Vector4(1f, 2f, 3f, 4f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f }, floater);
        }

        [Test]
        public void Floater_IntConstructorTest() {
            var floater = new Floater(1);
            AssertFloaterValues(new[] { 1f }, floater);
        }

        [Test]
        public void Floater_Int2ConstructorTest() {
            var insert = new int2(1, 2);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f }, floater);
        }

        [Test]
        public void Floater_Int3ConstructorTest() {
            var insert = new int3(1, 2, 3);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_Int4ConstructorTest() {
            var insert = new int4(1, 2, 3, 4);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f }, floater);
        }

        [Test]
        public void Floater_ListConstructorTest() {
            var insert = new List<float> { 1f, 2f, 3f };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_ReadOnlyListConstructorTest() {
            IReadOnlyList<float> insert = new List<float> { 1f, 2f, 3f };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_StackConstructorTest() {
            var insert = new Stack<float>();
            insert.Push(1f);
            insert.Push(2f);
            insert.Push(3f);

            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_QueueConstructorTest() {
            var insert = new Queue<float>();
            insert.Enqueue(1f);
            insert.Enqueue(2f);
            insert.Enqueue(3f);

            float[] floater = new Floater(insert);
            
            Assert.AreEqual(new[] { 1f, 2f, 3f, 0,0,0,0,0,0,0,0,0,0,0,0,0 }, floater);
        }

        [Test]
        public void Floater_SpanConstructorTest() {
            var source = new[] { 1f, 2f, 3f };
            Span<float> insert = source;
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_ReadOnlySpanConstructorTest() {
            var source = new[] { 1f, 2f, 3f };
            ReadOnlySpan<float> insert = source;
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_NativeArrayConstructorTest() {
            var insert = new NativeArray<float>(new[] { 1f, 2f, 3f }, Allocator.Temp);

            try {
                var floater = new Floater(insert);
                AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
            }
            finally {
                insert.Dispose();
            }
        }

        [Test]
        public void Floater_NativeListConstructorTest() {
            var insert = new NativeList<float>(Allocator.Temp);

            try {
                insert.Add(1f);
                insert.Add(2f);
                insert.Add(3f);

                var floater = new Floater(insert);
                AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
            }
            finally {
                insert.Dispose();
            }
        }

        [Test]
        public void Floater_ByteConstructorTest() {
            var floater = new Floater((byte)1);
            AssertFloaterValues(new[] { 1f }, floater);
        }

        [Test]
        public void Floater_ByteArrayConstructorTest() {
            var insert = new byte[] { 1, 2, 3 };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_SByteConstructorTest() {
            var floater = new Floater((sbyte)-1);
            AssertFloaterValues(new[] { -1f }, floater);
        }

        [Test]
        public void Floater_SByteArrayConstructorTest() {
            var insert = new sbyte[] { -1, 2, 3 };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { -1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_UShortConstructorTest() {
            var floater = new Floater((ushort)1);
            AssertFloaterValues(new[] { 1f }, floater);
        }

        [Test]
        public void Floater_UShortArrayConstructorTest() {
            var insert = new ushort[] { 1, 2, 3 };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_ShortConstructorTest() {
            var floater = new Floater((short)-1);
            AssertFloaterValues(new[] { -1f }, floater);
        }

        [Test]
        public void Floater_ShortArrayConstructorTest() {
            var insert = new short[] { -1, 2, 3 };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { -1f, 2f, 3f }, floater);
        }

        [Test]
        public void Floater_BoolConstructorTest() {
            var floater = new Floater(true);
            AssertFloaterValues(new[] { 1f }, floater);
        }

        [Test]
        public void Floater_Bool2ConstructorTest() {
            var insert = new bool2(true, false);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 0f }, floater);
        }

        [Test]
        public void Floater_BoolArrayConstructorTest() {
            var insert = new[] { true, false, true };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 0f, 1f }, floater);
        }

        [Test]
        public void Floater_Float4x4ConstructorTest() {
            var insert = new float4x4(
                new float4(1f, 2f, 3f, 4f),
                new float4(5f, 6f, 7f, 8f),
                new float4(9f, 10f, 11f, 12f),
                new float4(13f, 14f, 15f, 16f));
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f }, floater);
        }

        [Test]
        public void Floater_Matrix4x4ConstructorTest() {
            var insert = new Matrix4x4();
            insert.SetColumn(0, new Vector4(1f, 2f, 3f, 4f));
            insert.SetColumn(1, new Vector4(5f, 6f, 7f, 8f));
            insert.SetColumn(2, new Vector4(9f, 10f, 11f, 12f));
            insert.SetColumn(3, new Vector4(13f, 14f, 15f, 16f));

            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f }, floater);
        }

        [Test]
        public void Floater_DoubleConstructorTest() {
            var floater = new Floater(1.25d);
            AssertFloaterValues(new[] { 1.25f }, floater);
        }

        [Test]
        public void Floater_DoubleArrayConstructorTest() {
            var insert = new[] { 1.25d, 2.5d, 3.75d };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { 1.25f, 2.5f, 3.75f}, floater);
        }

        [Test]
        public void Floater_HalfConstructorTest() {
            var insert = new half(1.5f);
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { (float)insert }, floater);
        }

        [Test]
        public void Floater_HalfArrayConstructorTest() {
            var insert = new[] { new half(1.5f), new half(2.5f), new half(3.5f) };
            var expected = new[] {
                (float)insert[0], (float)insert[1], (float)insert[2], 0,
                0,0,0,0,0,0,0,0,0,0,0,0
            };
            var floater = new Floater(insert);
            AssertFloaterValues(expected, floater);
        }

        [Test]
        public void Floater_CharConstructorTest() {
            var floater = new Floater('A');
            AssertFloaterValues(new[] { (float)'A' }, floater);
        }

        [Test]
        public void Floater_CharArrayConstructorValueTest() {
            var insert = new[] { 'A', 'B', 'C' };
            var floater = new Floater(insert);
            AssertFloaterValues(new[] { (float)'A', (float)'B', (float)'C' }, floater);
        }

        [Test]
        public void Floater_StringConstructorTest() {
            var floater = new Floater("ABC");
            AssertFloaterValues(new[] { (float)'A', (float)'B', (float)'C' }, floater);
        }

        [Test]
        public void Floater_TransformConstructorTest() {
            var obj = new GameObject("Floater Transform Test");

            try {
                obj.transform.position = new Vector3(1f, 2f, 3f);
                obj.transform.rotation = Quaternion.identity;
                obj.transform.localScale = new Vector3(4f, 5f, 6f);

                var floater = new Floater(obj.transform);
                AssertFloaterValues(new[] { 1f, 2f, 3f, 0f, 0f, 0f, 4f, 5f, 6f }, floater);
            }
            finally {
                Object.DestroyImmediate(obj);
            }
        }

        [Test]
        public void Floater_Float3Float3ConstructorTest() {
            var floater = new Floater(new float3(1f, 2f, 3f), new float3(4f, 5f, 6f));
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 1f, 1f, 1f }, floater);
        }

        [Test]
        public void Floater_Float3Float3Float3ConstructorTest() {
            var floater = new Floater(new float3(1f, 2f, 3f), new float3(4f, 5f, 6f), new float3(7f, 8f, 9f));
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f }, floater);
        }

        [Test]
        public void Floater_Float3QuaternionConstructorTest() {
            var floater = new Floater(new float3(1f, 2f, 3f), quaternion.identity);
            AssertFloaterValues(new[] { 1f, 2f, 3f, 0f, 0f, 0f, 1f, 1f, 1f }, floater);
        }

        [Test]
        public void Floater_QuaternionConstructorTest() {
            var floater = new Floater(quaternion.identity);
            AssertFloaterValues(new[] { 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f }, floater);
        }

        [Test]
        public void Floater_UnityQuaternionConstructorTest() {
            var floater = new Floater(Quaternion.identity);
            AssertFloaterValues(new[] { 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f }, floater);
        }

        [Test]
        public void Floater_Float3QuaternionFloat3ConstructorTest() {
            var floater = new Floater(new float3(1f, 2f, 3f), quaternion.identity, new float3(4f, 5f, 6f));
            AssertFloaterValues(new[] { 1f, 2f, 3f, 0f, 0f, 0f, 4f, 5f, 6f }, floater);
        }

        [Test]
        public void Floater_ObsoleteFloat3ArrayConstructorTest() {
            var insert = new[] {
                new float3(1f, 2f, 3f),
                new float3(4f, 5f, 6f),
                new float3(7f, 8f, 9f)
            };
#pragma warning disable CS0618
            var floater = new Floater(insert);
#pragma warning restore CS0618
            AssertFloaterValues(new[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f }, floater);
        }
    }
}
