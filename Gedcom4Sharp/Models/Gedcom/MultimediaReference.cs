using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class MultimediaReference : AbstractElement
    {
        public Multimedia Multimedia { get; set; }

        public MultimediaReference() { }

        public MultimediaReference(Multimedia multimedia)
        {
            Multimedia = multimedia;
        }
    }
}