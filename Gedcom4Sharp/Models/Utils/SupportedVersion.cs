﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Utils
{
    public static class SupportedVersion
    {
        public const string V5_5 = "5.5";
        public const string V5_5_1 = "5.5.1";

        public static bool TestSupportedVersion(string value)
        {
            return value.Equals(V5_5) || value.Equals(V5_5_1);
        }
    }
}
