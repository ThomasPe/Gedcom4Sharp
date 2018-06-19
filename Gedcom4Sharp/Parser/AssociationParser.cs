using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Gedcom.Models;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class AssociationParser : AbstractParser<Association>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public AssociationParser(GedcomParser gedcomParser, StringTree stringTree, Association loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.AssociatedEntityXref = _stringTree.Value;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.RELATIONSHIP.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Relationship = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.TYPE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AssociatedEntityType = ParseStringWithCustomFacts(ch);
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