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

using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class FloaterTest {
        [Test]
        public void Floater_ValueTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert[1], floater[1]);
        }

        [Test]
        public void Floater_LengthTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert.Length, floater.Length);
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
            Assert.AreEqual(insert, list);
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
            Assert.AreEqual(insert, floater);
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
            var floater = new Floater(insert);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CharArrayTest() {
            var insert = new[] {
                'H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!'
            };
            const string expected = "Hello World!";
            string floater = new Floater(insert);
            Assert.AreEqual(expected, floater);
        }
    }
}