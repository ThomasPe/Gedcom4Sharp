using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public abstract class AbstractNotesElement : AbstractElement, HasNotes
    {
        public List<NoteStructure> NoteStructures { get; set; } = new List<NoteStructure>();
    }
}
