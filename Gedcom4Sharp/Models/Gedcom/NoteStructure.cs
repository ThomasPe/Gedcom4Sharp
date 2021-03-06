﻿using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class NoteStructure : AbstractElement
    {
        public List<string> Lines { get; set; } = new List<string>();
        public NoteRecord NoteReference { get; set; }
    }
}
