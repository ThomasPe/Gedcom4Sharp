using System.Collections.Generic;
using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Place : AbstractNotesElement, HasCitations
    {
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();
        public StringWithCustomFacts Latitude { get; set; }
        public StringWithCustomFacts Longitude { get; set; }
        public List<AbstractNameVariation> Phonetic { get; set; } = new List<AbstractNameVariation>();
        public StringWithCustomFacts PlaceFormat { get; set; }
        public string PlaceName { get; set; }
        public List<AbstractNameVariation> Romanized { get; set; } = new List<AbstractNameVariation>();
    }
}