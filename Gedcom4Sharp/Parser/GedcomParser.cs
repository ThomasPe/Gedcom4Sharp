using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    /// <summary>
    /// Class for parsing GEDCOM 5.5 files and creating a Gedcom structure from them.
    /// 
    /// TODO: Parser Progress Obeservers / Notifications
    /// </summary>
    public class GedcomParser
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
        private bool _cancelled;

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
        private bool _insideCustomTag;

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


        public void Load(string filePath)
        {
            Gedcom = new Gedcom();
            LineNum = 0;
            Errors.Clear();
            Warnings.Clear();
            _cancelled = false;
            _stringTreeBuilder = new StringTreeBuilder(this);

            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    if(line[0] == '0')
                    {
                        // parseAndLoadPreviousStringTree();
                    }
                    LineNum++;
                    _stringTreeBuilder.AppendLine(line);
                    if (_cancelled)
                    {
                        throw new Exception("File load/parse is cancelled");
                    }
                    // TODO Notify Parser Status
                    //if (lineNum % parseNotificationRate == 0)
                    //{
                    //    notifyParseObservers(new ParseProgressEvent(this, gedcom, false, lineNum));
                    //}
                }
                ParseAndLoadPreviousStringTree();
            }
            else
            {
                throw new FileNotFoundException($"{filePath} was not found");
            }


        }

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
                if(Gedcom.Header == null)
                {
                    Gedcom.Header = new Header();
                }
            }
        }
    }
}
