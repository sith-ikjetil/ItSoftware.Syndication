using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ItSoftware.Syndication;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomLink =
    /// element atom:link {
    ///   atomCommonAttributes,
    ///   attribute href { atomUri },
    ///   attribute rel { atomNCName | atomUri }?,
    ///   attribute type { atomMediaType }?,
    ///   attribute hreflang { atomLanguageTag }?,
    ///   attribute title { text }?,
    ///   attribute length { text }?,
    ///   undefinedContent
    /// }
    /// </summary>
    public class AtomLink : AtomElementBase
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "link";
        internal static readonly string XMLATTRIBUTE_HREF = "href";
        internal static readonly string XMLATTRIBUTE_HREFLANG = "hreflang";
        internal static readonly string XMLATTRIBUTE_LENGTH = "length";
        internal static readonly string XMLATTRIBUTE_REL = "rel";
        internal static readonly string XMLATTRIBUTE_TITLE = "title";
        internal static readonly string XMLATTRIBUTE_TYPE = "type";
        #endregion

        #region Private Const Fields
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomLink()
        {
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="href"></param>
        /// <param name="rel"></param>
        /// <param name="type"></param>
        /// <param name="hreflang"></param>
        /// <param name="title"></param>
        /// <param name="length"></param>
        public AtomLink(string href, LinkRel rel, string type, string hreflang, string title, string length)
        {
            this.HRef = href;
            this.Rel = rel;
            this.Type = type;
            this.HRefLang = hreflang;
            this.Title = title;
            this.Length = length;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnLink"></param>
        internal AtomLink(XmlNode xnLink)
        {
            if (xnLink == null)
            {
                throw new ArgumentNullException("xnLink");
            }
            this.DeserializeFromXml(xnLink);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnLink"></param>
        private void DeserializeFromXml(XmlNode xnLink)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnLink.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnLink.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            XmlAttribute xaHRef = xnLink.Attributes[XMLATTRIBUTE_HREF];
            if (xaHRef != null)
            {
                this.HRef = xaHRef.InnerText; 
            }
            XmlAttribute xaRel = xnLink.Attributes[XMLATTRIBUTE_REL];
            if (xaRel != null)
            {
                switch (xaRel.InnerText.ToLower())
                {
                    case "alternate":
                        this.Rel = LinkRel.Alternate;
                        break;
                    case"enclosure":
                        this.Rel = LinkRel.Enclosure;
                        break;
                    case "related":
                        this.Rel = LinkRel.Related;
                        break;
                    case "self":
                        this.Rel = LinkRel.Self;
                        break;
                    case "via":
                        this.Rel = LinkRel.Via;
                        break;
                    default:
                        this.Rel = LinkRel.Unknown;
                        break;
                };
            }
            XmlAttribute xaType = xnLink.Attributes[XMLATTRIBUTE_TYPE];
            if (xaType != null)
            {
                this.Type = xaType.InnerText;
            }
            XmlAttribute xaHRefLang = xnLink.Attributes[XMLATTRIBUTE_HREFLANG];
            if (xaHRefLang != null)
            {
                this.HRefLang = xaHRefLang.InnerText;
            }
            XmlAttribute xaTitle = xnLink.Attributes[XMLATTRIBUTE_TITLE];
            if (xaTitle != null)
            {
                this.Title = xaTitle.InnerText;
            }
            XmlAttribute xaLength = xnLink.Attributes[XMLATTRIBUTE_LENGTH];
            if (xaLength != null)
            {
                this.Length = xaLength.InnerText;
            }
            
            this.Modules = new SyndicationModuleCollection(xnLink, new List<SyndicationModuleExclusionElement>());
        }
        #endregion

        #region LinkRel
        /// <summary>
        /// 
        /// </summary>
        public enum LinkRel
        {
            Unknown,
            NotSet,
            Alternate, //: an alternate representation of the entry or feed, for example a permalink to the html version of the entry, or the front page of the weblog. 
            Enclosure, //: a related resource which is potentially large in size and might require special handling, for example an audio or video recording. 
            Related,// an document related to the entry or feed. 
            Self,//: the feed itself. 
            Via//: the source 
        }
        #endregion

        #region Required Fields
        private string m_hRef;
        public string HRef
        {
            get
            {
                return m_hRef;
            }
            set
            {
                m_hRef = value;
            }
        }
        #endregion

        #region Optional Fields
        private LinkRel m_rel = LinkRel.NotSet;
        public LinkRel Rel
        {
            get
            {
                return m_rel;
            }
            set
            {
                m_rel = value;
            }
        }
        private string m_type;
        public string Type {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }        
        private string m_hReflang;
        public string HRefLang {
            get
            {
                return m_hReflang;
            }
            set
            {
                m_hReflang = value;
            }
        }
        private string m_title;
        public string Title
        {
            get
            {
                return m_title;
            }
            set
            {
                m_title = value;
            }
        }
        private string m_length;
        public string Length
        {
            get
            {
                return m_length;
            }
            set 
            {
                m_length = value;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Modules backing field.
        /// </summary>
        private SyndicationModuleCollection m_modules;
        /// <summary>
        /// Gets or sets the syndication modules.
        /// </summary>
        public SyndicationModuleCollection Modules
        {
            get
            {
                return m_modules;
            }
            set
            {
                m_modules = value;
            }
        }
        #endregion

        #region Protected Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            if (this.Rel == LinkRel.Unknown)
            {
                throw new SyndicationSerializationException("LinkRel cannot be Unknown.");
            }

            XmlElement xeLink = xdAtom.CreateElement(XML_TAG);

            if (this.HRef != null)
            {
                XmlAttribute xaHref = xdAtom.CreateAttribute(XMLATTRIBUTE_HREF);
                xaHref.InnerText = this.HRef;
                xeLink.Attributes.Append(xaHref);
            }
            if (this.HRefLang != null)
            {
                XmlAttribute xaHrefLang = xdAtom.CreateAttribute(XMLATTRIBUTE_HREFLANG);
                xaHrefLang.InnerText = this.HRefLang;
                xeLink.Attributes.Append(xaHrefLang);
            }
            if (this.Length != null)
            {
                XmlAttribute xaLength = xdAtom.CreateAttribute(XMLATTRIBUTE_LENGTH);
                xaLength.InnerText = this.Length;
                xeLink.Attributes.Append(xaLength);
            }
            if (this.Rel != LinkRel.NotSet)
            {
                XmlAttribute xaRel = xdAtom.CreateAttribute(XMLATTRIBUTE_REL);
                xaRel.InnerText = this.Rel.ToString().ToLower();
                xeLink.Attributes.Append(xaRel);
            }
            if (this.Title != null)
            {
                XmlAttribute xaTitle = xdAtom.CreateAttribute(XMLATTRIBUTE_TITLE);
                xaTitle.InnerText = this.Title;
                xeLink.Attributes.Append(xaTitle);
            }
            if (this.Type != null)
            {
                XmlAttribute xaType = xdAtom.CreateAttribute(XMLATTRIBUTE_TYPE);
                xaType.InnerText = this.Type;
                xeLink.Attributes.Append(xaType);
            }

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeLink);
            }

            return xeLink;
        }      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.HRef == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLATTRIBUTE_HREF);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion
    }// class
}// namespace
