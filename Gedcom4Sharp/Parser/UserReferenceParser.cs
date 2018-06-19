using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class UserReferenceParser : AbstractParser<UserReference>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public UserReferenceParser(GedcomParser gedcomParser, StringTree stringTree, UserReference loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.ReferenceNum = new StringWithCustomFacts(_stringTree.Value);
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.TYPE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Type = ParseStringWithCustomFacts(ch);
                    }
                    else
                    {
                        UnknownTag(ch, _loadInto.ReferenceNum);
                    }
                }
            }
        }
    }
}