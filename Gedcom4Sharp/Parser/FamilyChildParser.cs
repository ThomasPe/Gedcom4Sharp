using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class FamilyChildParser : AbstractParser<FamilyChild>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public FamilyChildParser(GedcomParser gedcomParser, StringTree stringTree, FamilyChild loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            var f = GetFamily(_stringTree.Value);
            _loadInto.Family = f;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures);
                    }
                    else if (Tag.PEDIGREE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Pedigree = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADOPTION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AdoptedBy = new StringWithCustomFacts(EnumExtensions.ParseDescriptionToEnum<AdoptedByWhichParent>(ch.Value).Desc());
                    }
                    else if (Tag.STATUS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Status = ParseStringWithCustomFacts(ch);
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but status was specified for child-to-family link on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
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