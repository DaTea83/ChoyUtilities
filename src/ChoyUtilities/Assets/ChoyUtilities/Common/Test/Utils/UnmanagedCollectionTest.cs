using NUnit.Framework;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Test {
    
    [TestFixture]
    public class UnmanagedCollectionTest {
        
        [Test]
        public void Float_FirstMatchTest() {
            var insert = new [] { 1.23456789f, 2.3456789f, 3.456789f };
            var index1 = insert.FirstMatchUnmanaged(2.3456789f);
            Assert.AreEqual(1, index1);
            
            var index2 = insert.FirstMatchUnmanaged(2f);
            Assert.AreEqual(-1, index2);
        }
        
    }
}