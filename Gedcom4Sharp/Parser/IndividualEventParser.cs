using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class IndividualEventParser : AbstractEventParser<IndividualEvent>
    {
        /// <summary>
        /// a reference to the root GedcomParser
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public IndividualEventParser(GedcomParser gedcomParser, StringTree stringTree, IndividualEvent loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.Type = EnumExtensions.ParseDescriptionToEnum<IndividualEventType>(_stringTree.Tag);
            ParseYNull();
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.TYPE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SubType = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Date = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.PLACE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Place = new Place();
                        new PlaceParser(_gedcomParser, ch, _loadInto.Place).Parse();
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, _loadInto.Multimedia).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.AGE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Age = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CAUSE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Cause = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Address = new Address();
                        new AddressParser(_gedcomParser, ch, _loadInto.Address).Parse();
                    }
                    else if (Tag.AGENCY.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RespAgency = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.RESTRICTION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RestrictionNotice = ParseStringWithCustomFacts(ch);
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but restriction notice was specified for individual event on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.RELIGION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ReligiousAffiliation = ParseStringWithCustomFacts(ch);
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but religious affiliation  was specified for individual event on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
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
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but WWW URL was specified for individual event on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.FAX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FaxNumbers.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but fax was specified for individual event on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.EMAIL.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Emails.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but email was specified for individual event on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        if(_loadInto.Description == null)
                        {
                            _loadInto.Description = ParseStringWithCustomFacts(ch);
                        }
                        else
                        {
                            _loadInto.Description.Value += ch.Value;
                        }
                    }
                    else if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        if (_loadInto.Description == null)
                        {
                            _loadInto.Description = ParseStringWithCustomFacts(ch);
                        }
                        else
                        {
                            _loadInto.Description.Value += ch.Value;
                        }
                    }
                    else if (Tag.FAMILY_WHERE_CHILD.Desc().Equals(ch.Tag))
                    {
                        var fc = new FamilyChild();
                        _loadInto.Family = fc;
                        new FamilyChildParser(_gedcomParser, ch, fc).Parse();
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