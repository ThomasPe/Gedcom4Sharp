using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;

namespace Gedcom4Sharp.Parser
{
    internal class LdsSpouseSealingParser : AbstractParser<LdsSpouseSealing>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public LdsSpouseSealingParser(GedcomParser gedcomParser, StringTree stringTree, LdsSpouseSealing loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            // TODO
        }
    }
}