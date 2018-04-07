using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class FamilySpouse : AbstractNotesElement
    {
        public Family Family { get; set; }
    }
}
