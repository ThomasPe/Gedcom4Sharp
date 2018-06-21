using System;
using System.Collections.Generic;
using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class MultimediaParser : AbstractParser<Multimedia>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
        public MultimediaParser(GedcomParser gedcomParser, StringTree stringTree, Multimedia loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            int fileTagCount = 0;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.FILE.Desc().Equals(ch.Tag))
                    {
                        fileTagCount++;
                        break;
                    }
                }
            }
            if(fileTagCount > 0)
            {
                if (G55())
                {
                    _gedcomParser.Warnings.Add($"GEDCOM version was 5.5, but a 5.5.1-style multimedia record was found at line {_stringTree.LineNum}. Data will be loaded, but might have problems being written until the version is for the data is changed to 5.5.1");
                }
                LoadMultimediaRecord551(_stringTree);
            }
            else
            {
                if (!G55())
                {
                    _gedcomParser.Warnings.Add($"GEDCOM version is 5.5.1, but a 5.5-style multimedia record was found at line {_stringTree.LineNum}. Data will be loaded, but might have problems being written until the version is for the data is changed to 5.5.1");
                }
                LoadMultimediaRecord55(_stringTree);
            }
        }

        /// <summary>
        /// Load a GEDCOM 5.5-style multimedia record (that could be referenced from another object) from a string tree node. 
        /// This corresponds to the MULTIMEDIA_RECORD structure in the GEDCOM 5.5 spec.
        /// </summary>
        /// <param name="obje">the OBJE node being loaded</param>
        private void LoadMultimediaRecord55(StringTree obje)
        {
            if(obje.Children == null)
            {
                _gedcomParser.Errors.Add($"Root level multimedia record at line {obje.LineNum} had no child records");
            }
            else
            {
                foreach(var ch in obje.Children)
                {
                    if (Tag.FORM.Desc().Equals(ch.Tag))
                    {
                        _loadInto.EmbeddedMediaFormat = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.TITLE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.EmbeddedTitle = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.BLOB.Desc().Equals(ch.Tag))
                    {
                        LoadMultiLinesOfText(ch, _loadInto.Blob, _loadInto);
                        if (!G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5.1, but a BLOB tag was found at line {ch.LineNum}. Data will be loaded but will not be writeable unless GEDCOM version is changed to 5.5.1");
                        }
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        var continuedObjects = new List<MultimediaReference>();
                        new MultimediaLinkParser(_gedcomParser, ch, continuedObjects).Parse();
                        _loadInto.ContinuedObject = continuedObjects.First();
                        if (!G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5.1, but a chained OBJE tag was found at line {ch.LineNum}. Data will be loaded but will not be writeable unless GEDCOM version is changed to 5.5.1");
                        }
                    }
                    else if (Tag.REFERENCE.Desc().Equals(ch.Tag))
                    {
                        var u = new UserReference();
                        _loadInto.UserReferences.Add(u);
                        new UserReferenceParser(_gedcomParser, ch, u).Parse();
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch))
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
        /// Load a GEDCOM 5.5.1-style multimedia record (that could be referenced from another object) from a string tree node.
        /// This corresponds to the MULTIMEDIA_RECORD structure in the GEDCOM 5.5.1 spec.
        /// </summary>
        /// <param name="stringTree">the OBJE node being loaded</param>
        private void LoadMultimediaRecord551(StringTree obje)
        {
            var m = GetMultimedia(obje.Xref);
            if(obje.Children != null)
            {
                foreach(var ch in obje.Children)
                {
                    if (Tag.FILE.Desc().Equals(ch.Tag))
                    {
                        var fr = new FileReference();
                        m.FileReferences.Add(fr);
                        new FileReference511Parser(_gedcomParser, ch, fr).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, m.NoteStructures);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, m.Citations).Parse();
                    }
                    else if (Tag.REFERENCE.Desc().Equals(ch.Tag))
                    {
                        var u = new UserReference();
                        m.UserReferences.Add(u);
                        new UserReferenceParser(_gedcomParser, ch, u).Parse();
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        m.RecIdNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        m.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, m.ChangeDate).Parse();
                    }
                    else
                    {
                        UnknownTag(ch, m);
                    }
                }
            }
        }
    }
}