using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class CustomFactParserTests
    {
        [TestMethod]
        public async Task Test()
        {
            var gp = new GedcomParser
            {
                StrictCustomTags = false
            };
            await gp.Load(@"Assets\Samples\ftmcustomtags.ged");
            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            foreach(var e in gp.Errors)
            {
                Debug.WriteLine(e);
            }
            Assert.AreEqual(0, gp.Errors.Count);

            foreach(var w in gp.Warnings)
            {
                Debug.WriteLine(w);
            }
            //Assert.AreEqual(0, gp.Warnings.Count);

            var john = g.Individuals["@I1@"];
            Assert.IsNotNull(john);
            Assert.AreEqual(19, john.CustomFacts.Count);

            var customFact = john.GetCustomFactsWithTag("_MDCL").FirstOrDefault();
            Assert.IsNotNull(customFact);
            Assert.AreEqual("Decapitated, but recovered", customFact.Description.Value);
            Assert.IsNotNull(customFact.Date);
            Assert.AreEqual("08 AUG 1978", customFact.Date.Value);
            Assert.AreEqual("San Antonio, Bexar, Texas, USA", customFact.Place.PlaceName);
        }
    }
}
