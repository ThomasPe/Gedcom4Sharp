using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Submission : AbstractElement, HasXref
    {
        /// <summary>
        /// The xref for this submitter
        /// </summary>
        public string Xref { get; set; }

        /// <summary>
        /// The number of ancestor generations
        /// </summary>
        public StringWithCustomFacts AncestorsCount { get; set; }

        /// <summary>
        /// The number of descendant generations
        /// </summary>
        public StringWithCustomFacts DescendandsCount { get; set; }

        /// <summary>
        /// The name of the family file
        /// </summary>
        public StringWithCustomFacts NameOfFamilyFile { get; set; }

        /// <summary>
        /// The ordinance process flag
        /// </summary>
        public StringWithCustomFacts OrdinanceProcessFlag { get; set; }

        /// <summary>
        /// The record ID number
        /// </summary>
        public StringWithCustomFacts RecIdNumber { get; set; }

        /// <summary>
        /// The submitter of this submission
        /// </summary>
        public Submitter Submitter { get; set; }

        /// <summary>
        /// The temple code for this submission
        /// </summary>
        public StringWithCustomFacts TempleCode { get; set; }

        public Submission(string xref)
        {
            Xref = xref;
        }
    }
}