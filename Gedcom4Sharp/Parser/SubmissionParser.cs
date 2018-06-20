using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class SubmissionParser : AbstractParser<Submission>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public SubmissionParser(GedcomParser gedcomParser, StringTree stringTree, Submission loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.SUBMITTER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Submitter = GetSubmitter(ch.Value);
                    }
                    else if (Tag.FAMILY_FILE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.NameOfFamilyFile = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.NameOfFamilyFile);
                    }
                    else if (Tag.TEMPLE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.TempleCode = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.TempleCode);
                    }
                    else if (Tag.ANCESTORS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AncestorsCount = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.AncestorsCount);
                    }
                    else if (Tag.DESCENDANTS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.DescendandsCount = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.DescendandsCount);
                    }
                    else if (Tag.ORDINANCE_PROCESS_FLAG.Desc().Equals(ch.Tag))
                    {
                        _loadInto.OrdinanceProcessFlag = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.OrdinanceProcessFlag);
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RecIdNumber = ParseStringWithCustomFacts(ch);
                        RemainingChildrenAreCustomTags(ch, _loadInto.RecIdNumber);
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