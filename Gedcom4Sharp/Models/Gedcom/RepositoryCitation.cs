using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class RepositoryCitation : AbstractNotesElement
    {
        public List<SourceCallNumber> CallNumbers { get; set; } = new List<SourceCallNumber>();
        public string RepositoryXref { get; set; }
    }
}