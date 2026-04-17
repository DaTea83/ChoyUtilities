using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Test {
    
    public class FloaterOperatorTest {

        [Test]
        public void Floater_CastAddFloatTest() {
            var input = new Floater(new [] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new [] { 1.23456789f, 2.3456789f, 3.59024689f });
            
            var floater = input.Add(3.59024689f);
            Assert.AreEqual(expected, floater);
        }

        [Test]
        public void Floater_CastAddFloatArrayTest() {
            var input = new Floater(new [] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new [] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });
            
            var floater = input.Add(new [] { 3.59024689f, 4.70135799f });
            Assert.AreEqual(expected, floater);
        }
        
        [Test]
        public void Floater_CastAddFloaterTest() {
            var input = new Floater(new [] { 1.23456789f, 2.3456789f });
            var expected = new Floater(new [] { 1.23456789f, 2.3456789f, 3.59024689f, 4.70135799f });
            
            var floater = input.Add(new Floater(new [] { 3.59024689f, 4.70135799f }));
            Assert.AreEqual(expected, floater);
        }
    }
}