﻿using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class IndividualReference : AbstractEvent
    {
        public Individual Individual { get; set; }
    }
}