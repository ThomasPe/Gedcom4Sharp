using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class NoteStructure
    {
        public List<string> Lines { get; set; }
        public NoteRecord NoteReference { get; set; }
    }
}
