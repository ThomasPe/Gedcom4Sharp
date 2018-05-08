using Gedcom4Sharp.Models.Gedcom;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly List<string> errors = new List<string>();

        /// <summary>
        /// The warnings issued during the parsing of the gedcom file
        /// </summary>
        private readonly List<string> warnings = new List<string>();

        /// <summary>
        /// Is the load/parse process being cancelled
        /// </summary>
        private bool cancelled;

        /// <summary>
        ///  The StringTreeBuilder that is assisting this class
        /// </summary>
        private StringTreeBuilder stringTreeBuilder = new StringTreeBuilder();

        /// <summary>
        /// The 1-based line number that we've most recently read, so starts at zero (when we haven't read any lines yet)
        /// </summary>
        private int lineNum;

        /// <summary>
        /// Are we currently parsing somewhere inside a custom tag?
        /// </summary>
        private bool insideCustomTag;

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


        public void Load(Stream stream)
        {
            Gedcom = new Gedcom();
            lineNum = 0;
            errors.Clear();
            warnings.Clear();
            cancelled = false;

            
        }
    }
}
