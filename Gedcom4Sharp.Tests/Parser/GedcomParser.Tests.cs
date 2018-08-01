using System;
using System.Diagnostics;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
	[TestClass]
    public class GedcomParserTests
    {
		[TestMethod]
		public void Test()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\EnglishTudorHouse.ged");
            Debug.WriteLine("done");
        }

        [TestMethod]
        public void BadCustomTag()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\Bad_custom_tag.ged");
            Assert.IsNotNull(gp.Errors);
            Assert.AreEqual(1, gp.Errors.Count);
            Assert.IsNotNull(gp.Errors[0]);
            Assert.IsTrue(gp.Errors[0].Contains("Line 28: Cannot handle tag BUST"));
        }
    }
}
