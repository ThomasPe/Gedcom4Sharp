﻿using Gedcom4Sharp.IO;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gedcom4Sharp.Parser
{
    /// <summary>
    /// Class for parsing GEDCOM 5.5 files and creating a Gedcom structure from them.
    /// 
    /// TODO: Parser Progress Obeservers / Notifications
    /// </summary>
    public class GedcomParser : AbstractParser<Gedcom>
    {
        /// <summary>
        /// The things that went wrong while parsing the gedcom file
        /// </summary>
        public readonly List<string> Errors = new List<string>();

        /// <summary>
        /// The warnings issued during the parsing of the gedcom file
        /// </summary>
        public readonly List<string> Warnings = new List<string>();

        /// <summary>
        /// Is the load/parse process being cancelled
        /// </summary>
        public bool IsCancelled;

        /// <summary>
        ///  The StringTreeBuilder that is assisting this class
        /// </summary>
        private StringTreeBuilder _stringTreeBuilder;

        /// <summary>
        /// The 1-based line number that we've most recently read, so starts at zero (when we haven't read any lines yet)
        /// </summary>
        public int LineNum;

        /// <summary>
        /// Are we currently parsing somewhere inside a custom tag?
        /// </summary>
        public bool InsideCustomTag;

        /// <summary>
        /// The encoding detected by either the file bytes or gedcom CHAR tag
        /// </summary>
        public Encoding Encoding;

        /// <summary>
        /// Default Constructor
        /// This is the root level parser, so there are no parent or other root nodes to hook up to (yet)
        /// </summary>
        public GedcomParser() : base(null, null, null)
        {
        }

        /// <summary>
        /// The content of the gedcom file
        /// </summary>
        public Gedcom Gedcom { get; set; }

        /// <summary>
        /// Indicates whether handling of custom tags should be strict - that is, must an unrecognized tag begin with an underscore to be
        /// loaded into the custom facts collection? If false, unrecognized tags will be treated as custom tags even if they don't begin
        /// with underscores, and no errors will be issued. If true, unrecognized tags that do not begin with underscores will be
        /// discarded, with errors added to the errors collection.
        /// </summary>
        public bool StrictCustomTags { get; set; } = true;

        /// <summary>
        /// Should the parser ignore custom tags?
        /// </summary>
        public bool IgnoreCustomTags { get; set; } = false;

        /// <summary>
        /// Indicates whether non-compliant GEDCOM files with actual line breaks in text values (rather than CONT tags) should be parsed
        /// (with some loss of data) rather than fail with an exception.
        /// </summary>
        public bool StrictLineBreaks { get; set; } = true;


        /// <summary>
        /// Are we currently parsing somewhere inside a custom tag?
        /// </summary>
        public bool IsInsideCustomTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public async Task Load(string filePath, Encoding encoding = null, IProgress<int> progress = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (File.Exists(filePath))
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    await Load(fileStream, encoding, progress, cancellationToken);
                }
            }
            else
            {
                throw new FileNotFoundException($"{filePath} was not found");
            }
        }

        public async Task Load(Stream fileStream, Encoding encoding = null, IProgress<int> progress = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Encoding = encoding;
            Gedcom = new Gedcom();
            LineNum = 0;
            Errors.Clear();
            Warnings.Clear();
            IsCancelled = false;
            _stringTreeBuilder = new StringTreeBuilder(this);


            if (Encoding == null)
            {
                Encoding = await EncodingHelper.GetEncoding(fileStream);
            }
            await Task.Run(async () =>
            {
                using (var reader = new StreamReader(fileStream, Encoding))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = await reader.ReadLineAsync();
                        ReadLine(line);
                        if (progress != null && LineNum % 100 == 0)
                        {
                            progress.Report(LineNum);
                        }
                        if (cancellationToken != CancellationToken.None)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }
                }
                ParseAndLoadPreviousStringTree();
            });
        }

        private void ReadLine(string line)
        {
            line = line.TrimStart();
            if (line[0] == '0')
            {
                ParseAndLoadPreviousStringTree();
            }
            LineNum++;
            _stringTreeBuilder.AppendLine(line);
            if (IsCancelled)
            {
                throw new Exception("File load/parse is cancelled");
            }
        }

        /// <summary>
        /// Parse the {@link StringTreeBuilder}'s string tree in memory, load it into the object model, then discard that string tree buffer
        /// </summary>
        private void ParseAndLoadPreviousStringTree()
        {
            var tree = _stringTreeBuilder.WrapperNode;
            if(tree != null && tree.Level == -1 && tree.Children.Count == 1)
            {
                // We've still got the prior root node in memory - parse it and add to object model
                var rootLevelItem = _stringTreeBuilder.WrapperNode.Children.First();
                if(rootLevelItem.Level != 0)
                {
                    throw new Exception($"Expected a root level item in the buffer, but found {rootLevelItem.Level} {rootLevelItem.Tag} from line {LineNum}");
                }
                LoadRootItem(rootLevelItem);
                _stringTreeBuilder = new StringTreeBuilder(this);
            }
        }

        private void LoadRootItem(StringTree rootLevelItem)
        {
            if (Tag.HEADER.Desc().Equals(rootLevelItem.Tag))
            {
                if (Gedcom.Header == null)
                {
                    Gedcom.Header = new Header();
                }
                new HeaderParser(this, rootLevelItem, Gedcom.Header).Parse();
            }
            else if (Tag.SUBMITTER.Desc().Equals(rootLevelItem.Tag))
            {
                var submitter = GetSubmitter(rootLevelItem.Xref);
                new SubmitterParser(this, rootLevelItem, submitter).Parse();
            }
            else if (Tag.INDIVIDUAL.Desc().Equals(rootLevelItem.Tag))
            {
                var i = GetIndividual(rootLevelItem.Xref);
                new IndividualParser(this, rootLevelItem, i).Parse();
            }
            else if (Tag.SUBMISSION.Desc().Equals(rootLevelItem.Tag))
            {
                var s = new Submission(rootLevelItem.Xref);
                Gedcom.Submission = s;
                if (Gedcom.Header == null)
                {
                    Gedcom.Header = new Header();
                }
                if (Gedcom.Header.SubmissionReference == null)
                {
                    /**
                     * The GEDCOM spec puts a cross reference to the root-level SUBN element in the HEAD structure. Now that we have a
                     * submission object, represent that cross reference in the header object
                     */
                    Gedcom.Header.SubmissionReference = new SubmissionReference(s);
                }
                new SubmissionParser(this, rootLevelItem, s).Parse();
            }
            else if (Tag.NOTE.Desc().Equals(rootLevelItem.Tag))
            {
                var nr = GetNoteRecord(rootLevelItem.Xref);
                new NoteRecordParser(this, rootLevelItem, nr).Parse();
            }
            else if (Tag.FAMILY.Desc().Equals(rootLevelItem.Tag))
            {
                var f = GetFamily(rootLevelItem.Xref);
                new FamilyParser(this, rootLevelItem, f).Parse();
            }
            else if (Tag.TRAILER.Desc().Equals(rootLevelItem.Tag))
            {
                Gedcom.Trailer = new Trailer();
            }
            else if (Tag.SOURCE.Desc().Equals(rootLevelItem.Tag))
            {
                var s = GetSource(rootLevelItem.Xref);
                new SourceParser(this, rootLevelItem, s).Parse();
            }
            else if (Tag.REPOSITORY.Desc().Equals(rootLevelItem.Tag))
            {
                var r = GetRepository(rootLevelItem.Xref);
                new RepositoryParser(this, rootLevelItem, r).Parse();
            }
            else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(rootLevelItem.Tag))
            {
                var multimedia = GetMultimedia(rootLevelItem.Xref);
                new MultimediaParser(this, rootLevelItem, multimedia).Parse();
            }
            else
            {
                UnknownTag(rootLevelItem, Gedcom);
            }
        }

        public override void Parse()
        {
            throw new NotImplementedException();
        }
    }
}
