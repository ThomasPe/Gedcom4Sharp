using System.Collections.Generic;
using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class CitationListParser : AbstractParser<List<AbstractCitation>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public CitationListParser(GedcomParser gedcomParser, StringTree stringTree, List<AbstractCitation> loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            AbstractCitation citation;
            if (ReferencesAnotherNode(_stringTree))
            {
                citation = new CitationWithSource();
                LoadCitationWithSource(_stringTree, citation);
            }
            else
            {
                citation = new CitationWithoutSource();
                LoadCitationWithoutSource(_stringTree, citation);
            }
            _loadInto.Add(citation);
        }

        /// <summary>
        /// Load a DATA structure in a source citation from a string tree node
        /// </summary>
        /// <param name="data">the DATA (source-citation data) node</param>
        /// <param name="d">the CitationData structure</param>
        private void LoadCitationData(StringTree data, CitationData d)
        {
            if(data.Children != null)
            {
                foreach(var ch in data.Children)
                {
                    if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        d.EntryDate = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.TEXT.Desc().Equals(ch.Tag))
                    {
                        var ms = new MultiStringWithCustomFacts();
                        d.SourceText.Add(ms);
                        LoadMultiStringWithCustomFacts(ch, ms);
                    }
                    else
                    {
                        UnknownTag(ch, d);
                    }
                }
            }
        }

        /// <summary>
        /// Load the non-cross-referenced source citation from a string tree node
        /// </summary>
        /// <param name="sour">the SOUR (source-citation) node</param>
        /// <param name="citation">the citation to load into</param>
        private void LoadCitationWithoutSource(StringTree sour, AbstractCitation citation)
        {
            var cws = (CitationWithoutSource)citation;
            cws.Description.Add(sour.Value);
            if(sour.Children != null)
            {
                foreach(var ch in sour.Children)
                {
                    if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        cws.Description.Add(string.IsNullOrEmpty(ch.Value) ? "" : ch.Value);
                    }
                    else if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        if (!cws.Description.Any())
                        {
                            cws.Description.Add(ch.Value);
                        }
                        else
                        {
                            var iLast = cws.Description.Count - 1;
                            cws.Description[iLast] = cws.Description[iLast] + ch.Value;
                        }
                    }
                    else if (Tag.TEXT.Desc().Equals(ch.Tag))
                    {
                        var ls = new List<string>();
                        LoadMultiLinesOfText(ch, cws.TextFromSource, cws);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, cws.NoteStructures).Parse();
                    }
                    else if (Tag.QUALITY.Desc().Equals(ch.Tag))
                    {
                        cws.Certainty = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, cws.Multimedia);
                    }
                    else
                    {
                        UnknownTag(ch, citation);
                    }
                }
            }
        }

        /// <summary>
        /// Load a cross-referenced source citation from a string tree node
        /// </summary>
        /// <param name="sour">the SOUR (source-citation) node</param>
        /// <param name="citation">the citation to load into</param>
        private void LoadCitationWithSource(StringTree sour, AbstractCitation citation)
        {
            var cws = (CitationWithSource)citation;
            Source src = null;
            if (ReferencesAnotherNode(sour))
            {
                src = GetSource(sour.Value);
            }
            cws.Source = src;
            if(sour.Children != null)
            {
                foreach(var ch in sour.Children)
                {
                    if (Tag.PAGE.Desc().Equals(ch.Tag))
                    {
                        cws.WhereInSource = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.EVENT.Desc().Equals(ch.Tag))
                    {
                        cws.EventCited = new StringWithCustomFacts(ch.Value);
                        if(ch.Children != null)
                        {
                            foreach(var gc in ch.Children)
                            {
                                if (Tag.ROLE.Desc().Equals(gc.Tag))
                                {
                                    cws.RoleInEvent = ParseStringWithCustomFacts(gc);
                                }
                                else
                                {
                                    UnknownTag(gc, cws.EventCited);
                                }
                            }
                        }
                    }
                    // TODO Handle duplicate Tag
                    // else if (Tag.DATA_FOR_CITATION.Desc().Equals(ch.Tag))
                    else if (Tag.DATA_FOR_SOURCE.Desc().Equals(ch.Tag))
                    {
                        var d = new CitationData();
                        cws.Data.Add(d);
                        LoadCitationData(ch, d);
                    }
                    else if (Tag.QUALITY.Desc().Equals(ch.Tag))
                    {
                        cws.Certainty = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, cws.NoteStructures).Parse();
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, cws.Multimedia).Parse();
                    }
                    else
                    {
                        UnknownTag(ch, citation);
                    }
                }
            }
        }

    }
}