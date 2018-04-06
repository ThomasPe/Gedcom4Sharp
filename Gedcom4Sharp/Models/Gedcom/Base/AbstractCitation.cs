using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public class AbstractCitation : AbstractNotesElement
    {
        public StringWithCustomFacts Certainty { get; set; }
        public List<MultimediaReference> Multimedia { get; set; }

        public void SetCertainty(string certainty)
        {
            Certainty = new StringWithCustomFacts { Value = certainty };
        }
    }
}
