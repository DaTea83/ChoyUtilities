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

using NUnit.Framework;
using Unity.Mathematics;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class FloaterExtensionTest {
        private enum ETest : byte {
            A,
            B,
            C,
            D
        }

        [Test]
        public void Floater_PositionFromTransformTest() {
            var insert1 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f, -90, 90, 45, 1, 1, 1 });
            var insert2 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f });

            var expected = new float3(1.23456789f, 2.3456789f, 3.456789f);

            var result1 = insert1.PositionFromTransform();
            var result2 = insert2.PositionFromTransform();

            Assert.AreEqual(expected, result1);
            Assert.AreEqual(float3.zero, result2);
        }

        [Test]
        public void Floater_RotationFromTransformTest() {
            var insert1 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f, -90, 90, 45, 1, 1, 1 });
            var insert2 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f });

            var expected = quaternion.Euler(new float3(-90, 90, 45));

            var result1 = insert1.RotationFromTransform();
            var result2 = insert2.RotationFromTransform();

            Assert.AreEqual(expected, result1);
            Assert.AreEqual(quaternion.identity, result2);
        }

        [Test]
        public void Floater_ScaleFromTransformTest() {
            var insert1 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f, -90, 90, 45, 2, 3, 4 });
            var insert2 = new Floater(new[] { 1.23456789f, 2.3456789f, 3.456789f });

            var expected = new float3(2, 3, 4);

            var result1 = insert1.ScaleFromTransform();
            var result2 = insert2.ScaleFromTransform();

            Assert.AreEqual(expected, result1);
            Assert.AreEqual(new float3(1, 1, 1), result2);
        }

        [Test]
        public void Floater_EnumTest() {
            const ETest insert = ETest.A;
            var result = insert.Floater();
            Assert.AreEqual(insert, result.GetEnum<ETest>());
        }

        [Test]
        public void Floater_EnumArrayTest() {
            var insert = new[] { ETest.A, ETest.B, ETest.C };
            var result = insert.Floater();
            Assert.AreEqual(insert, result.GetEnums<ETest>());
        }
    }
}