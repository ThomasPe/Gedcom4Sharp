using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class IndividualEvent : AbstractEvent
    {
        public FamilyChild Family { get; set; }
        public IndividualEventType Type { get; set; }
    }
}