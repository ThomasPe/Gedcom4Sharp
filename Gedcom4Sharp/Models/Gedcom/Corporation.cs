using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Corporation : AbstractAddressableElement
    {
        public string BusinessName { get; set; } = "UNSPECIFIED";
    }
}