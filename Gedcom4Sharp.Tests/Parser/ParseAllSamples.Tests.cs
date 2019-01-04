using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class ParseAllSamples
    {

        [TestMethod]
        public void TestLoadAllSamples()
        {
            GedcomParser gp = new GedcomParser();
            gp.StrictCustomTags = false;
            gp.StrictLineBreaks = false;

            var folderPath = @"Assets\Samples\";
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.ged"))
            {
                try
                {
                    gp.Gedcom = null;
                    gp.Load(file);
                    Assert.IsNotNull(gp.Gedcom);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
                
            }
        }
    }
}
