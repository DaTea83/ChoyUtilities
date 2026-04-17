using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    
    [TestFixture]
    public class FloaterTest {
        
        [Test]
        public void Floater_ValueTest() {
            
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert[1], floater[1]);
        }
        
        [Test]
        public void Floater_LengthTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert.Length, floater.Length);
        }

        [Test]
        public void Floater_IsCreatedTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.IsTrue(floater.IsCreated);
        }
        
        [Test]
        public void Floater_GetEnumeratorTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            var list = new List<float>(floater);
            Assert.AreEqual(insert, list);
        }
        
        [Test]
        public void Floater_ToStringTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert.ToString(), floater.ToString());
        }
        
        [Test]
        public void Floater_EqualsTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var floater = new Floater(insert);
            Assert.AreEqual(insert, floater);
        }
        
        
    }
}