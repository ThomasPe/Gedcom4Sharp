using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Utils;

namespace Gedcom4Sharp.Parser.Base
{
    internal class CustomFactParser
    {
        private GedcomParser _gedcomParser;
        private StringTree _gch;
        private CustomFact _cf;

        public CustomFactParser(GedcomParser gedcomParser, StringTree gch, CustomFact cf)
        {
            _gedcomParser = gedcomParser;
            _gch = gch;
            _cf = cf;
        }
    }
}