using System;
using System.Collections.Generic;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class MultimediaLinkParser : AbstractParser<List<MultimediaReference>>
    {
        public MultimediaLinkParser(GedcomParser gedcomParser, StringTree stringTree, List<MultimediaReference> loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            Multimedia m;
            MultimediaReference mr;
            if (ReferencesAnotherNode(_stringTree))
            {
                m = GetMultimedia(_stringTree.Value);
                mr = new MultimediaReference(m);
                RemainingChildrenAreCustomTags(_stringTree, mr);
            }
            else
            {
                m = new Multimedia();
                LoadFileReferences(m, _stringTree);
                mr = new MultimediaReference(m);
            }
            _loadInto.Add(mr);
        }

        /// <summary>
        /// Load all the file references in the current OBJE tag
        /// </summary>
        /// <param name="m">the multimedia object being with the reference</param>
        /// <param name="obje">the OBJE node being parsed</param>
        private void LoadFileReferences(Multimedia m, StringTree obje)
        {
            int fileTagCount = 0;
            int formTagCount = 0;

            if(obje.Children != null)
            {
                foreach(var ch in obje.Children)
                {
                    // Count up the number of files referenced for this object - GEDCOM 5.5.1 allows multiple, 5.5 only allows 1
                    if (Tag.FILE.Desc().Equals(ch.Tag))
                    {
                        fileTagCount++;
                    }

                    /*
                     * Count the number of formats referenced per file - GEDCOM 5.5.1 has them as children of FILEs (so should be zero),
                     * 5.5 pairs them with the single FILE tag (so should be one)
                     */
                    if (Tag.FORM.Desc().Equals(ch.Tag))
                    {
                        formTagCount++;
                    }
                }
            }
            if (G55())
            {
                if(fileTagCount > 1)
                {
                    _gedcomParser.Warnings.Add($"GEDCOM version is 5.5, but multiple files referenced in multimedia reference on line {obje.LineNum} which is only allowed in 5.5.1. Data will be loaded, but cannot be written back out unless the GEDCOM version is changed to 5.5.1");
                }
                if(formTagCount == 0)
                {
                    _gedcomParser.Warnings.Add($"GEDCOM version is 5.5, but there is not a FORM tag in the multimedia link on line {obje.LineNum} a scenario which is only allowed in 5.5.1. Data will be loaded, but cannot be written back out unless the GEDCOM version is changed to 5.5.1");
                }
            }
            if(formTagCount > 1)
            {
                _gedcomParser.Errors.Add($"Multiple FORM tags were found for a multimedia file reference at line {obje.LineNum} this is not compliant with any GEDCOM standard - data not loaded");
                return;
            }

            if(fileTagCount > 1 || formTagCount < fileTagCount)
            {
                LoadFileReferences551(m, obje.Children);
            } else
            {
                LoadFileReferences55(m, obje.Children);
            }
        }

        /// <summary>
        /// Load a single 5.5-style file reference
        /// </summary>
        /// <param name="m">The multimedia object to contain the new file reference</param>
        /// <param name="objeChildren">the sub-tags of the OBJE tag</param>
        private void LoadFileReferences55(Multimedia m, List<StringTree> objeChildren)
        {
            var currentFileRef = new FileReference();
            m.FileReferences.Add(currentFileRef);
            if(objeChildren != null)
            {
                foreach(var ch in objeChildren)
                {
                    if (Tag.FORM.Desc().Equals(ch.Tag))
                    {
                        currentFileRef.Format = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.TITLE.Desc().Equals(ch.Tag))
                    {
                        m.EmbeddedTitle = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.FILE.Desc().Equals(ch.Tag))
                    {
                        currentFileRef.ReferenceToFile = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        var notes = m.NoteStructures;
                        new NoteStructureListParser(_gedcomParser, ch, notes).Parse();
                    }
                    else
                    {
                        UnknownTag(ch, m);
                    }
                }
            }
        }

        /// <summary>
        /// Load one or more 5.5.1-style references
        /// </summary>
        /// <param name="m">the multimedia object to which we are adding the file references</param>
        /// <param name="objeChildren">the sub-tags of the OBJE tag</param>
        private void LoadFileReferences551(Multimedia m, List<StringTree> objeChildren)
        {
            if(objeChildren != null)
            {
                foreach(var ch in objeChildren)
                {
                    if (Tag.FILE.Equals(ch.Tag))
                    {
                        var fr = new FileReference();
                        m.FileReferences.Add(fr);
                        new FileReference511Parser(_gedcomParser, ch, fr).Parse();
                    }
                    else if (Tag.TITLE.Equals(ch.Tag))
                    {
                        if(m.FileReferences != null)
                        {
                            foreach (var fr in m.FileReferences)
                            {
                                fr.Title = ParseStringWithCustomFacts(ch);
                            }
                        }
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        var notes = m.NoteStructures;
                        new NoteStructureListParser(_gedcomParser, ch, notes).Parse();
                        if (!G55())
                        {
                            _gedcomParser.Warnings.Add($"Gedcom version was 5.5.1, but a NOTE was found on a multimedia link on line {ch.LineNum}, which is no longer supported. Data will be loaded, but cannot be written back out unless the GEDCOM version is changed to 5.5");
                        }
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