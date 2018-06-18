using System;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gedcom4Sharp.Utility.Extensions;
using Gedcom4Sharp.Models.Gedcom.Enums;
using System.Collections.Generic;

namespace Gedcom4Sharp.Tests.Utility
{
    [TestClass]
    public class EnumExtensionsTests
    {
		[TestMethod]
		public void EnumExtensions_Positive()
        {
            var e = EnumExtensions.ParseDescriptionToEnum<IndividualEventType>("BIRT");
            Assert.IsNotNull(e);
            Assert.AreEqual(IndividualEventType.BIRTH, e);

            var success = EnumExtensions.TryParseDescriptionToEnum("ADOP", out Tag tag);
            Assert.IsTrue(success);
            Assert.AreEqual(Tag.ADOPTION, tag);
        }

        [TestMethod]
        public void EnumExtension_Negative()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => EnumExtensions.ParseDescriptionToEnum<IndividualEventType>("1234"));

            var success = EnumExtensions.TryParseDescriptionToEnum("1234", out Tag tag);
            Assert.IsFalse(success);
            Assert.AreEqual(default(Tag), tag);
        }

    }
}
