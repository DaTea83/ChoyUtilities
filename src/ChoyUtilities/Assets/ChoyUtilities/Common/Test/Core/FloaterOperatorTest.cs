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

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Test {
    public class FloaterOperatorTest {
        [Test]
        public void Floater_CastAddFloatTest() {
            var input = new Floater(new[] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new[] { 1.23456789f, 2.3456789f, 3.59024689f });

            var floater = input.Add(3.59024689f);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastAddFloatArrayTest() {
            var input = new Floater(new[] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new[] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });

            var floater = input.Add(new[] { 3.59024689f, 4.70135799f });
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastAddFloaterTest() {
            var input = new Floater(new[] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new[] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });

            var floater = input.Add(new Floater(new[] { 3.59024689f, 4.70135799f }));
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_FirstMatchTest() {
            var input = new Floater(new[] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });
            var result = input.FirstMatch(2.3456789f);
            Assert.AreEqual(1, result);

            var result2 = input.FirstMatch(2f);
            Assert.AreEqual(-1, result2);
        }

        [Test]
        public void Floater_RemoveTest() {
            var input = new Floater(new[] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });
            var expected = new Floater(new[] { 1.23456789f, 3.59024689f, 4.70135799f });

            var result = input.Remove(2.3456789f);
            var result2 = input.Remove(2f);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(input, result2);
        }
    }
}