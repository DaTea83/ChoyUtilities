using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Test {
    
    [TestFixture]
    public class EnumCollectionTest {
        
        [Flags]
        private enum ETest : byte {
            A = 1, 
            B = 1 << 1, 
            C = 1 << 2, 
            D = 1 << 3
        }
        
        private enum ETest2 : byte {
            A = 1, 
        }

        [Test]
        public void Enum_SingleFlagTest() {
            const ETest insert1 = ETest.A | ETest.B;
            const ETest insert2 = ETest.A;
            const ETest2 insert3 = ETest2.A;
            
            var final1 = insert1.IsSingleFlag();
            Assert.IsFalse(final1);
            
            var final2 = insert2.IsSingleFlag();
            Assert.IsTrue(final2);
            
            Assert.Throws<ArgumentException>(() => insert3.IsSingleFlag());
        }
        
        [Test]
        public void Enum_HighestFlagTest() {
            
            const ETest insert = ETest.A | ETest.C | ETest.D;
            var final = insert.GetHighestFlag();
            Assert.AreEqual(ETest.D, final);
        }
    }
}