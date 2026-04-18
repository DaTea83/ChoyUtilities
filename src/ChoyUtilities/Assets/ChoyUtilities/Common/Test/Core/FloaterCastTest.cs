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
using Unity.Mathematics;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class FloaterCastTest {
        [Test]
        public void Floater_CastFloatTest() {
            const float input = 1.23456789f;
            const float expected = 1.23456789f;

            float floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastFloatArrayTest() {
            var input = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var expected = new[] { 1.23456789f, 2.3456789f, 3.456789f };

            float[] floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastFloatListTest() {
            var input = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var expected = new List<float> { 1.23456789f, 2.3456789f, 3.456789f };

            List<float> floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastIntArrayTest() {
            var input = new[] { 1, 2, 3 };
            var expected = new[] { 1, 2, 3 };

            int[] floater = new Floater(input);
            Assert.AreEqual(expected, floater);

            Assert.Throws<FloaterException>(() => {
                int[] _ = new Floater(Array.Empty<float>());
            });
        }

        [Test]
        public void Floater_CastFloat2Test() {
            var input = new[] { 1.23456789f, 2.3456789f };
            var expected = new float2(1.23456789f, 2.3456789f);

            float2 floater = new Floater(input);
            Assert.AreEqual(expected, floater);

            var input2 = new[] { 1.23456789f };
            Assert.Throws<FloaterException>(() => {
                float2 _ = new Floater(input2);
            });
        }

        [Test]
        public void Floater_CastFloat3Test() {
            var input = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var expected = new float3(1.23456789f, 2.3456789f, 3.456789f);

            float3 floater = new Floater(input);
            Assert.AreEqual(expected, floater);

            var input2 = new[] { 1.23456789f };
            Assert.Throws<FloaterException>(() => {
                float3 _ = new Floater(input2);
            });
        }

        [Test]
        public void Floater_CastFloat4Test() {
            var input = new[] { 1.23456789f, 2.3456789f, 3.456789f, 4.56789f };
            var expected = new float4(1.23456789f, 2.3456789f, 3.456789f, 4.56789f);

            float4 floater = new Floater(input);
            Assert.AreEqual(expected, floater);

            var input2 = new[] { 1.23456789f };
            Assert.Throws<FloaterException>(() => {
                float4 _ = new Floater(input2);
            });
        }
    }
}