using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class RepositoryParser : AbstractParser<Repository>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public RepositoryParser(GedcomParser gedcomParser, StringTree stringTree, Repository loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.NAME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Name = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Address = new Address();
                        new AddressParser(_gedcomParser, ch, _loadInto.Address).Parse();
                    }
                    else if (Tag.PHONE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PhoneNumbers.Add(ParseStringWithCustomFacts(ch));
                    }
                    else if (Tag.WEB_ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.WwwUrls.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but WWW URL was specified on repository {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.FAX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FaxNumbers.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but fax was specified on repository {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.EMAIL.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Emails.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but email was specified on repository {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.REFERENCE.Desc().Equals(ch.Tag))
                    {
                        var u = new UserReference();
                        _loadInto.UserReferences.Add(u);
                        new UserReferenceParser(_gedcomParser, ch, u).Parse();
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RecIdNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, _loadInto.ChangeDate).Parse();
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