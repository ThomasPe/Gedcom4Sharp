using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class SubmissionReference : AbstractElement
    {
        public Submission Submission { get; set; }

        public SubmissionReference(Submission submission)
        {
            Submission = submission;
        }
    }
}