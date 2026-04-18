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

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class UnmanagedCollectionTest {
        private enum ETest : byte {
            A,
            B,
            C,
            D
        }

        [Test]
        public void Float_FirstMatchTest() {
            var insert = new[] { 1.23456789f, 2.3456789f, 3.456789f };
            var index1 = insert.FirstMatchUnmanaged(2.3456789f);
            Assert.AreEqual(1, index1);

            var index2 = insert.FirstMatchUnmanaged(2f);
            Assert.AreEqual(-1, index2);
        }

        [Test]
        public void Enum_FirstMatchTest() {
            var insert = new[] { ETest.A, ETest.B, ETest.C };
            var index1 = insert.FirstMatchUnmanaged(ETest.B);
            Assert.AreEqual(1, index1);

            var index2 = insert.FirstMatchUnmanaged(ETest.D);
            Assert.AreEqual(-1, index2);
        }

        [Test]
        public void Enum_AddIfNotContainTest() {
            var insert = new[] { ETest.A, ETest.B, ETest.C };
            var final = new[] { ETest.A, ETest.B, ETest.C, ETest.D };

            var (set1, success) = insert.AddIfNotContainUnmanaged(ETest.A);
            Assert.IsFalse(success);
            Assert.AreEqual(insert, set1);

            var (set2, success2) = insert.AddIfNotContainUnmanaged(ETest.D);
            Assert.IsTrue(success2);
            Assert.AreEqual(final, set2);
        }

        [Test]
        public void Enum_RemoveIfContainTest() {
            var insert = new[] { ETest.A, ETest.B, ETest.C };
            var final = new[] { ETest.A, ETest.B };

            var (set1, success) = insert.RemoveIfContainUnmanaged(ETest.C);
            Assert.IsTrue(success);
            Assert.AreEqual(final, set1);

            var (set2, success2) = insert.RemoveIfContainUnmanaged(ETest.D);
            Assert.IsFalse(success2);
            Assert.AreEqual(insert, set2);
        }
    }
}