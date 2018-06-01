using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public abstract class AbstractElement : HasCustomFacts
    {
        public List<CustomFact> CustomFacts { get; set; } = new List<CustomFact>();

        public List<CustomFact> GetCustomFactsWithTag(string tag)
        {
            return CustomFacts.Where(x => x.Tag == tag).ToList();
        }
    }
}
