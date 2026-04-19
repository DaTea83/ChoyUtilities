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
using Unity.Collections;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Test {
    [TestFixture]
    public class RawSetTest {
        
        [Test]
        public void RawSet_Test() {
            var expected = new NativeArray<int>(3, Allocator.Temp);
            var input = new RawSet<int>(3, Allocator.Temp);

            try {
                expected[0] = 1;
                expected[1] = 2;
                expected[2] = 3;

                input[0] = 1;
                input[1] = 2;
                input[2] = 3;
                AssertNativeArrayEqual(expected, input);
            }
            finally {
                if (expected.IsCreated) expected.Dispose();
                if (input.IsCreated) input.Dispose();
            }
        }

        [Test]
        public void RawSet_LengthTest() {
            var input = new RawSet<int>(3, Allocator.Temp);
            try {
                input[0] = 1;
                input[1] = 2;
                input[2] = 3;
                
                Assert.AreEqual(3, input.Length);
            }
            finally {
                if (input.IsCreated) input.Dispose();
            }
        }
        
        private static void AssertNativeArrayEqual<T>(NativeArray<T> a, NativeArray<T> b)
            where T : unmanaged, System.IEquatable<T>
        {
            Assert.AreEqual(a.Length, b.Length, "NativeArray lengths differ.");

            for (var i = 0; i < a.Length; i++) {
                Assert.IsTrue(a[i].Equals(b[i]), $"Mismatch at index {i}: {a[i]} != {b[i]}");
            }
        }
    }
}