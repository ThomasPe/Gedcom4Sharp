using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class ChangeDate : AbstractElement
    {
        public StringWithCustomFacts Date { get; set; }
        public StringWithCustomFacts Time { get; set; }

        public void setDate(string date)
        {
            Date = new StringWithCustomFacts { Value = date };
        }

        public void setTime(string time)
        {
            Time = new StringWithCustomFacts { Value = time };
        }
    }
}
