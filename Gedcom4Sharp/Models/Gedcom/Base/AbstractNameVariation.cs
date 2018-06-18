namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public abstract class AbstractNameVariation : AbstractElement
    {
        public string Variation { get; set; }
        public StringWithCustomFacts VariationType { get; set; }

        public void SetVariationType(string variationType)
        {
            VariationType = new StringWithCustomFacts { Value = variationType };
        }
    }
}
