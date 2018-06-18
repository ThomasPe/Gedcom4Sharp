using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;

namespace Gedcom4Sharp.Parser
{
    /// <summary>
    /// A base class for an event parser.
    /// </summary>
    /// <typeparam name="T">The type of event that is being parsed</typeparam>
    public abstract class AbstractEventParser<T> : AbstractParser<T> where T : AbstractEvent
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public AbstractEventParser(GedcomParser gedcomParser, StringTree stringTree, T loadInto): base (gedcomParser, stringTree, loadInto)
        {
        }

        /// <summary>
        /// Parse the Y or Null portion of the event after the tag
        /// </summary>
        protected void ParseYNull()
        {
            if ("Y".Equals(_stringTree.Value))
            {
                _loadInto.Ynull = _stringTree.Value;
                _loadInto.Description = new StringWithCustomFacts(string.Empty);
            }
            else if (string.IsNullOrWhiteSpace(_stringTree.Value))
            {
                _loadInto.Ynull = null;
                _loadInto.Description = new StringWithCustomFacts(string.Empty);
            }
            else
            {
                _loadInto.Ynull = null;
                _loadInto.Description = new StringWithCustomFacts(_stringTree.Value);
                _gedcomParser.Warnings.Add($"{_stringTree.Tag} tag had description rather than [Y|<NULL>] - violates standard");
            }
        }

    }
}
