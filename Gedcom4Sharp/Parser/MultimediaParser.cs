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
                // TODO
            }
        }
    }
}