using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public interface HasCitations
    {
        List<AbstractCitation> Citations { get; set; }
    }
}
