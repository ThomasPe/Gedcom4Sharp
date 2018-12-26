using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class GedcomParser551
    {

        /// <summary>
        /// Test that header copyright data is now multi-line capable
        /// </summary>
        [TestMethod]
        public void TestContinuationForCopyright()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\john_of_sea_20101009.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it"

			// TODO: Can't check if its 5.5 as the Version Tag comes after the Copyright Tag
            // Assert.AreNotEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Header);
            Assert.IsNotNull(g.Header.CopyrightData);
            Assert.AreEqual(3, g.Header.CopyrightData.Count);
            Assert.AreEqual("License: Creative Commons Attribution-ShareAlike 3.0", g.Header.CopyrightData[1]);
        }
    }
}
