﻿using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class SubmitterReference : AbstractElement
    {
        public Submitter Submitter { get; set; }

        public SubmitterReference()
        {
        }

        public SubmitterReference(Submitter submitter)
        {
            Submitter = submitter;
        }
    }
}