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
            var expected = new List<float>() { 1.23456789f, 2.3456789f, 3.456789f };

            List<float> floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastIntArrayTest() {
            var input = new[] { 1, 2, 3 };
            var expected = new[] { 1, 2, 3 };
            
            int[] floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastIntArrayTest_Exception() {
            Assert.That(() => {
                int[] floater = new Floater(Array.Empty<float>());
            }, Throws.TypeOf<FloaterException>());
        }

        [Test]
        public void Floater_CastFloat2Test() {
            var input = new []{ 1.23456789f, 2.3456789f };
            var expected = new float2(1.23456789f, 2.3456789f);
            
            float2 floater = new Floater(input);
            Assert.AreEqual(expected, floater);
        }
        
        [Test]
        public void Floater_CastFloat2Test_Exception() {
            var input = new []{ 1.23456789f };
            
            Assert.That(() => {
                float2 floater = new Floater(input);
            }, Throws.TypeOf<FloaterException>());
        }
    }
}