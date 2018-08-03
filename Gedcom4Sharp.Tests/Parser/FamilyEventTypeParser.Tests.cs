using System.Diagnostics;
using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class FamilyEventTypeParser
    {
        private Gedcom g;

        [TestInitialize]
        public void TestInitialize()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\TGC551.ged");
            g = gp.Gedcom;
        }

        /// <summary>
        /// Positive test case for google code issue 2
        /// </summary>
        [TestMethod]
        public void TestIssue2()
        {
            int familyCount = g.Families.Count;
            int events = 0;
            foreach(var fam in g.Families.Values)
            {
                if(fam.Events != null)
                {
                    foreach(var ev in fam.Events)
                    {
                        Assert.IsNotNull(ev.Type);
                        events++;
                    }
                }
            }
            Assert.IsTrue(events > 0);
            Assert.AreEqual(7, familyCount);
        }

    }
}
