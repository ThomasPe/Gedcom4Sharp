using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public interface HasNotes
    {
        List<NoteStructure> NoteStructures { get; set; }
    }
}
