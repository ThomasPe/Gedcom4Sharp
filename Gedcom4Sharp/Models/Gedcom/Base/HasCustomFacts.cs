using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public interface HasCustomFacts
    {
        List<CustomFact> CustomFacts { get; set; }

        List<CustomFact> GetCustomFactsWithTag(string tag);
    }
}
