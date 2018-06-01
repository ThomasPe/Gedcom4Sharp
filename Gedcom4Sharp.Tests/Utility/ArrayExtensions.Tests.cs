using System;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void ArrayExtensions_Positive()
        {
            var array = new String[100];
			foreach(var a in array)
            {
                Assert.IsNull(a);
            }

            array.Fill(0, 99, "Test");
            foreach (var a in array)
            {
                Assert.AreEqual("Test", a);
            }
        }

		[TestMethod]
		public void ArrayExtensions_NegativeFromIndex()
        {
            var array = new String[100];
            foreach (var a in array)
            {
                Assert.IsNull(a);
            }
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.Fill(50, 49, "Test"));
        }

        [TestMethod]
        public void ArrayExtensions_NegativeOutOfBounds()
        {
            var array = new String[100];
            foreach (var a in array)
            {
                Assert.IsNull(a);
            }
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.Fill(90, 100, "Test"));
        }
    }
}
