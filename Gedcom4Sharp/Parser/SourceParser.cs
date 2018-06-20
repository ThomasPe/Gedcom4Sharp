using System;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class SourceParser : AbstractParser<Source>
    {

        public SourceParser(GedcomParser gedcomParser, StringTree stringTree, Source loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.DATA_FOR_SOURCE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Data = new SourceData();
                        LoadSouceData(ch, _loadInto.Data);
                    } 
                    else if (Tag.TITLE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Title = new MultiStringWithCustomFacts();
                        LoadMultiStringWithCustomFacts(ch, _loadInto.Title);
                    }
                    else if (Tag.PUBLICATION_FACTS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PublicationFacts = new MultiStringWithCustomFacts();
                        LoadMultiStringWithCustomFacts(ch, _loadInto.PublicationFacts);
                    }
                    else if (Tag.TEXT.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SourceText = new MultiStringWithCustomFacts();
                        LoadMultiStringWithCustomFacts(ch, _loadInto.SourceText);
                    }
                    else if (Tag.ABBREVIATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SourceFiledBy = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.AUTHORS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.OriginatorsAuthors = new MultiStringWithCustomFacts();
                        LoadMultiStringWithCustomFacts(ch, _loadInto.OriginatorsAuthors);
                    }
                    else if (Tag.REPOSITORY.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RepositoryCitation = LoadRepositoryCitation(ch);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, _loadInto.Multimedia).Parse();
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

        /// <summary>
        /// Load a reference to a repository in a source, from a string tree node
        /// </summary>
        /// <param name="repo">the node</param>
        /// <returns>the RepositoryCitation loaded</returns>
        private RepositoryCitation LoadRepositoryCitation(StringTree repo)
        {
            var r = new RepositoryCitation();
            r.RepositoryXref = repo.Value;
            if(repo.Children != null)
            {
                foreach(var ch in repo.Children)
                {
                    if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, r.NoteStructures).Parse();
                    }
                    else if (Tag.CALL_NUMBER.Desc().Equals(ch.Tag)){
                        var scn = new SourceCallNumber();
                        r.CallNumbers.Add(scn);
                        scn.CallNumber = new StringWithCustomFacts(ch.Value);
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                if (Tag.MEDIA.Desc().Equals(gch.Tag))
                                {
                                    scn.MediaType = ParseStringWithCustomFacts(gch);
                                }
                                else
                                {
                                    UnknownTag(gch, scn.CallNumber);
                                }
                            }
                        }
                    }
                    else
                    {
                        UnknownTag(ch, r);
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// Load data for a source from a string tree node into a source data structure
        /// </summary>
        /// <param name="dataNode">the node</param>
        /// <param name="sourceData">the source data structure</param>
        private void LoadSouceData(StringTree dataNode, SourceData sourceData)
        {
            if(dataNode.Children != null)
            {
                foreach(var ch in dataNode.Children)
                {
                    if (Tag.EVENT.Desc().Equals(ch.Tag))
                    {
                        LoadSourceDataEventRecorded(ch, sourceData);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, sourceData.NoteStructures).Parse();
                    }
                    else if (Tag.AGENCY.Desc().Equals(ch.Tag))
                    {
                        sourceData.RespAgency = ParseStringWithCustomFacts(ch);
                    }
                    else
                    {
                        UnknownTag(ch, sourceData);
                    }
                }
            }
        }

        /// <summary>
        /// Load the data for a recorded event from a string tree node
        /// </summary>
        /// <param name="dataNode">the node</param>
        /// <param name="sourceData">the source data</param>
        private void LoadSourceDataEventRecorded(StringTree dataNode, SourceData sourceData)
        {
            var e = new EventRecorded();
            sourceData.EventsRecorded.Add(e);
            e.EventType = new StringWithCustomFacts(dataNode.Value);
            if(dataNode.Children != null)
            {
                foreach(var ch in dataNode.Children)
                {
                    if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        e.DatePeriod = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.PLACE.Desc().Equals(ch.Tag))
                    {
                        e.Jurisdiction = ParseStringWithCustomFacts(ch);
                    }
                    else
                    {
                        UnknownTag(ch, e);
                    }
                }
            }
        }
    }
}