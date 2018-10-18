using System.ComponentModel;

namespace Gedcom4Sharp.Models.Gedcom.Enums
{
    public enum Tag
    {
        /** Abbreviation */
        [Description("ABBR")]
        ABBREVIATION,

        /** Address */
        [Description("ADDR")]
        ADDRESS,

        /** Address 1 */
        [Description("ADR1")]
        ADDRESS_1,

        /** Address 2 */
        [Description("ADR2")]
        ADDRESS_2,

        /** Address line 3 */
        [Description("ADR3")]
        ADDRESS_3,

        /** Adoption */
        [Description("ADOP")]
        ADOPTION,

        /** Age */
        [Description("AGE")]
        AGE,
        /** Agency */
        [Description("AGNC")]
        AGENCY,

        /** Alias */
        [Description("ALIA")]
        ALIAS,

        /** Interest in an ancestor */
        [Description("ANCI")]
        ANCESTOR_INTEREST,

        /** Ancestors */
        [Description("ANCE")]
        ANCESTORS,

        /** Ancestral File Number */
        [Description("AFN")]
        ANCESTRAL_FILE_NUMBER,

        /** Association */
        [Description("ASSO")]
        ASSOCIATION,

        /** Authors */
        [Description("AUTH")]
        AUTHORS,

        /** BLOB (Binary Large OBject) - embedded media */
        [Description("BLOB")]
        BLOB,

        /** Call number */
        [Description("CALN")]
        CALL_NUMBER,

        /** Cause */
        [Description("CAUS")]
        CAUSE,

        /** Date/time of change */
        [Description("CHAN")]
        CHANGED_DATETIME,

        /** Character set */
        [Description("CHAR")]
        CHARACTER_SET,

        /** Child */
        [Description("CHIL")]
        CHILD,

        /** City */
        [Description("CITY")]
        CITY,

        /** Concatenation - more text, but do not start on a new line */
        [Description("CONC")]
        CONCATENATION,

        /** Continuation - more text, but start on a new line */
        [Description("CONT")]
        CONTINUATION,

        /** Copyright */
        [Description("COPR")]
        COPYRIGHT,

        /** Corporation */
        [Description("CORP")]
        CORPORATION,

        /** Country */
        [Description("CTRY")]
        COUNTRY,

        /** TODO: How to handle duplicate DATA */
        /** Data for a citation */
        //[Description("DATA")]
        //DATA_FOR_CITATION,

        /** Data for a source */
        [Description("DATA")]
        DATA_FOR_SOURCE,
        /** Date */
        [Description("DATE")]
        DATE,

        /** Interest in a descendant */
        [Description("DESI")]
        DESCENDANT_INTEREST,

        /** Descendants */
        [Description("DESC")]
        DESCENDANTS,

        /** Destination */
        [Description("DEST")]
        DESTINATION,

        /** Email address */
        [Description("EMAIL")]
        EMAIL,

        /** Event */
        [Description("EVEN")]
        EVENT,

        /** Family */
        [Description("FAM")]
        FAMILY,

        /** Family File */
        [Description("FAMF")]
        FAMILY_FILE,

        /** Family in which individual is a child */
        [Description("FAMC")]
        FAMILY_WHERE_CHILD,

        /** Family in which individual is a spouse */
        [Description("FAMS")]
        FAMILY_WHERE_SPOUSE,

        /** Fax number */
        [Description("FAX")]
        FAX,

        /** File */
        [Description("FILE")]
        FILE,

        /** Form */
        [Description("FORM")]
        FORM,

        /** GEDCOM spec version */
        [Description("GEDC")]
        GEDCOM_VERSION,

        /** Given name */
        [Description("GIVN")]
        GIVEN_NAME,

        /** Header */
        [Description("HEAD")]
        HEADER,

        /** Husband */
        [Description("HUSB")]
        HUSBAND,

        /** Individual */
        [Description("INDI")]
        INDIVIDUAL,

        /** Language */
        [Description("LANG")]
        LANGUAGE,

        /** Latitude */
        [Description("LATI")]
        LATITUDE,

        /** Longitude */
        [Description("LONG")]
        LONGITUDE,

        /** Map */
        [Description("MAP")]
        MAP,

        /** Media */
        [Description("MEDI")]
        MEDIA,

        /** Name */
        [Description("NAME")]
        NAME,

        /** Name Prefix */
        [Description("NPFX")]
        NAME_PREFIX,

        /** Name Suffix */
        [Description("NSFX")]
        NAME_SUFFIX,

        /** Nickname */
        [Description("NICK")]
        NICKNAME,

        /** Note */
        [Description("NOTE")]
        NOTE,

        /** Number of children */
        [Description("NCHI")]
        NUM_CHILDREN,

        /** Multimedia object */
        [Description("OBJE")]
        OBJECT_MULTIMEDIA,

        /** Ordinance Process Flag */
        [Description("ORDI")]
        ORDINANCE_PROCESS_FLAG,

        /** Page */
        [Description("PAGE")]
        PAGE,

        /** Pedigree */
        [Description("PEDI")]
        PEDIGREE,

        /** Phone number */
        [Description("PHON")]
        PHONE,

        /** Phonetic form/spelling */
        [Description("FONE")]
        PHONETIC,

        /** Place */
        [Description("PLAC")]
        PLACE,

        /** Postal Code */
        [Description("POST")]
        POSTAL_CODE,

        /** Publication facts */
        [Description("PUBL")]
        PUBLICATION_FACTS,

        /** Quality */
        [Description("QUAY")]
        QUALITY,

        /** Record ID Number */
        [Description("RIN")]
        RECORD_ID_NUMBER,

        /** Reference */
        [Description("REFN")]
        REFERENCE,

        /** Registration file number */
        [Description("RFN")]
        REGISTRATION_FILE_NUMBER,

        /** Relationship */
        [Description("RELA")]
        RELATIONSHIP,

        /** Religious Affiliation */
        [Description("RELI")]
        RELIGION,

        /** Repository */
        [Description("REPO")]
        REPOSITORY,

        /** Restriction */
        [Description("RESN")]
        RESTRICTION,

        /** Role */
        [Description("ROLE")]
        ROLE,

        /** Romanicized form/spelling */
        [Description("ROMN")]
        ROMANIZED,

        /** Sealing - Spouse */
        [Description("SLGS")]
        SEALING_SPOUSE,

        /** Sex */
        [Description("SEX")]
        SEX,

        /** Source */
        [Description("SOUR")]
        SOURCE,

        /** State */
        [Description("STAE")]
        STATE,

        /** Status */
        [Description("STAT")]
        STATUS,

        /** Submission */
        [Description("SUBN")]
        SUBMISSION,

        /** Submitter */
        [Description("SUBM")]
        SUBMITTER,

        /** Surname */
        [Description("SURN")]
        SURNAME,

        /** Surname prefix */
        [Description("SPFX")]
        SURNAME_PREFIX,

        /** Temple */
        [Description("TEMP")]
        TEMPLE,

        /** Text */
        [Description("TEXT")]
        TEXT,

        /** Title */
        [Description("TITL")]
        TITLE,

        /** Trailer */
        [Description("TRLR")]
        TRAILER,

        /** Type */
        [Description("TYPE")]
        TYPE,

        /** Version */
        [Description("VERS")]
        VERSION,

        /** Web address (URL) */
        [Description("WWW")]
        WEB_ADDRESS,

        /** Wife */
        [Description("WIFE")]
        WIFE
    }
}
