using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;
using System.Linq;

namespace Gedcom4Sharp.Parser
{
    internal class NoteRecordParser : AbstractParser<NoteRecord>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public NoteRecordParser(GedcomParser gedcomParser, StringTree stringTree, NoteRecord loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(!string.IsNullOrEmpty(_stringTree.Xref) && ReferencesAnotherNode(_stringTree))
            {
                _gedcomParser.Warnings.Add($"NOTE line has both an XREF_ID ({ _stringTree.Xref}) and SUBMITTER_TEXT ({_stringTree.Value}) value between @ signs - treating SUBMITTER_TEXT as string, not a cross-reference");
            }
            _loadInto.Lines.Add(_stringTree.Value);
            if(_stringTree.Children != null)
            {
                foreach (var ch in _stringTree.Children)
                {
                    if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        if (!_loadInto.Lines.Any())
                        {
                            _loadInto.Lines.Add(ch.Value);
                        }
                        else
                        {
                            var i = _loadInto.Lines.Count - 1;
                            var lastNote = _loadInto.Lines[i];
                            _loadInto.Lines[i] += ch.Value;
                        }
                    }
                    else if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Lines.Add(ch.Value ?? string.Empty);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.REFERENCE.Desc().Equals(ch.Tag))
                    {
                        var u = new UserReference();
                        _loadInto.UserReferences.Add(u);
                        new UserReferenceParser(_gedcomParser, ch, u);
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RecIdNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, _loadInto.ChangeDate);
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