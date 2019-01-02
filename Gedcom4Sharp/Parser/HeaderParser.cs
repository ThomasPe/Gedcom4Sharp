using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    public class HeaderParser : AbstractParser<Header>
    {
        public HeaderParser(GedcomParser gedcomParser, StringTree stringTree, Header loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        var sourceSystem = new SourceSystem();
                        _loadInto.SourceSystem = sourceSystem;
                        new SourceSystemParser(_gedcomParser, ch, _loadInto.SourceSystem).Parse();
                    }
                    else if (Tag.DESTINATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.DestinationSystem = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Date = ParseStringWithCustomFacts(ch);
                        if (ch.Children != null)
                        {
                            foreach (var gch in ch.Children)
                            {
                                if (gch.Tag.Equals("TIME"))
                                {
                                    _loadInto.Time = ParseStringWithCustomFacts(gch);
                                }
                                else
                                {
                                    UnknownTag(gch, _loadInto.Date);
                                }
                            }
                        }
                    }
                    else if (Tag.CHARACTER_SET.Desc().Equals(ch.Tag))
                    {
                        _loadInto.CharacterSet = new CharacterSet();
                        _loadInto.CharacterSet.CharacterSetName = ParseStringWithCustomFacts(ch);
                        // one optional version subitem is the only standard possibility here, but there can be custom tags
                        if (ch.Children != null)
                        {
                            foreach (var gch in ch.Children)
                            {
                                if (gch.Tag.Equals("VERS"))
                                {
                                    _loadInto.CharacterSet.VersionNum = ParseStringWithCustomFacts(gch);
                                }
                                else
                                {
                                    UnknownTag(gch, _loadInto.CharacterSet);
                                }
                            }
                        }
                    }
                    else if (Tag.SUBMITTER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SubmitterReference = new SubmitterReference(GetSubmitter(ch.Value));
                        RemainingChildrenAreCustomTags(ch, _loadInto.SubmitterReference);
                    }
                    else if (Tag.FILE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FileName = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.GEDCOM_VERSION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.GedcomVersion = new GedcomVersion();
                        new GedcomVersionParser(_gedcomParser, ch, _loadInto.GedcomVersion).Parse();
                    }
                    else if (Tag.COPYRIGHT.Desc().Equals(ch.Tag))
                    {
                        LoadMultiLinesOfText(ch, _loadInto.CopyrightData, _loadInto);
                        // TODO this sometimes gets called before the GEDCOM version has been read
                        if (G55() && _loadInto.CopyrightData.Count > 1)
                        {
                            _gedcomParser.Warnings.Add("GEDCOM version is 5.5, but multiple lines of copyright data were specified, which is only allowed in GEDCOM 5.5.1. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.SUBMISSION.Desc().Equals(ch.Tag))
                    {
                        if (_loadInto.SubmissionReference == null)
                        {
                            /*
                                 * There can only be one SUBMISSION record per GEDCOM, and it's found at the root level, but the HEAD
                                 * structure has a cross-reference to that root-level structure, so we're setting it here (if it hasn't
                                 * already been loaded, which it probably isn't yet)
                            */
                            _loadInto.SubmissionReference = new SubmissionReference(_gedcomParser.Gedcom.Submission);
                            RemainingChildrenAreCustomTags(ch, _loadInto.SubmissionReference);
                        }
                    }
                    else if (Tag.LANGUAGE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Language = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.PLACE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PlaceHierarchy = ParseStringWithCustomFacts(ch.Children.FirstOrDefault());
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
