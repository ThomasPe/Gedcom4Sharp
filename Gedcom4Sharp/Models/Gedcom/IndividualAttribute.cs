﻿using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class IndividualAttribute : AbstractEvent
    {
        public IndividualAttributeType Type { get; set; }
    }
}