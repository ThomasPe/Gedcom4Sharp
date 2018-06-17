using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class FileReference511Parser : AbstractParser<FileReference>
    {
        public FileReference511Parser(GedcomParser gedcomParser, StringTree stringTree, FileReference loadInto) : base (gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.ReferenceToFile = new StringWithCustomFacts(_stringTree.Value);
            var fileChildren = _stringTree.Children;
            if(fileChildren != null)
            {
                foreach(var fileChild in fileChildren)
                {
                    if (Tag.FORM.Desc().Equals(fileChild.Tag))
                    {
                        LoadForm(fileChild);
                    }
                    else if (Tag.TITLE.Desc().Equals(fileChild.Tag))
                    {
                        _loadInto.Title = ParseStringWithCustomFacts(fileChild);
                    }
                    else
                    {
                        UnknownTag(fileChild, _loadInto.ReferenceToFile);
                    }
                }
            }
            if(_loadInto.Format != null)
            {
                _gedcomParser.Warnings.Add($"FORM tag not found under FILE reference on line {_stringTree.Parent.LineNum} - technically required by spec");
            }
        }

        /// <summary>
        /// Load the form tag and its children
        /// </summary>
        /// <param name="form">the form string tree</param>
        private void LoadForm(StringTree form)
        {
            _loadInto.Format = new StringWithCustomFacts(form.Value);
            var formChildren = form.Children;
            if(formChildren != null)
            {
                int typeCount = 0;
                foreach (var formChild in formChildren)
                {
                    if (Tag.TYPE.Desc().Equals(formChild.Tag))
                    {
                        _loadInto.MediaType = ParseStringWithCustomFacts(formChild);
                        typeCount++;
                    } 
                    else if (Tag.MEDIA.Desc().Equals(formChild.Tag))
                    {
                        _loadInto.MediaType = ParseStringWithCustomFacts(formChild);
                        typeCount++;
                    }
                    else
                    {
                        UnknownTag(formChild, _loadInto.Format);
                    }
                }
                if(typeCount > 1)
                {
                    _gedcomParser.Errors.Add($"Media type was specified more than once for the FORM tag on line {form.LineNum}");
                }
            }
        }
    }
}