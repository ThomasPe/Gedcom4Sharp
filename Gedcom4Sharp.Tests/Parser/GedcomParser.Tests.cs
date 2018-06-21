using System;
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
            var parser = new GedcomParser();
            parser.Load(@"Assets\TortureFiles\EnglishTudorHouse.ged");
        }
    }
}
