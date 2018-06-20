using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

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
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Date = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.PLACE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Place = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.STATUS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Status = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.TEMPLE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Temple = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else
                    {
                        UnknownTag(ch, _loadInto);
                    }
                }
            }
        }
    }
}