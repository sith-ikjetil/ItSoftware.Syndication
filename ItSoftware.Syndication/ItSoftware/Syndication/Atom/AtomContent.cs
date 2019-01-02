using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomInlineTextContent =
    /// element atom:content {
    ///   atomCommonAttributes,
    ///   attribute type { "text" | "html" }?,
    ///   (text)*
    /// }
    /// 
    /// atomInlineXHTMLContent =
    /// element atom:content {
    ///   atomCommonAttributes,
    ///   attribute type { "xhtml" },
    ///   xhtmlDiv
    /// }
    /// 
    /// atomInlineOtherContent =
    /// element atom:content {
    ///   atomCommonAttributes,
    ///   attribute type { atomMediaType }?,
    ///   (text|anyElement)*
    /// }
    /// 
    /// atomOutOfLineContent =
    /// element atom:content {
    ///   atomCommonAttributes,
    ///   attribute type { atomMediaType }?,
    ///   attribute src { atomUri },
    ///   empty
    /// }
    /// 
    /// atomContent = atomInlineTextContent
    /// | atomInlineXHTMLContent
    /// | atomInlineOtherContent
    /// | atomOutOfLineContent
    /// </summary>
    public class AtomContent : AtomElementBase
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "content";
        internal static readonly string XMLATTRIBUTE_TYPE = "type";
        internal static readonly string XMLATTRIBUTE_SRC = "src";
        #endregion

        #region Private Const Data
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        private const string REGEX_MEDIATYPE = ".+/.+";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomContent()
        {
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="src"></param>
        public AtomContent(string content, AtomTextType type, string src)
        {
            this.Content = content;
            this.Type = type;
            this.Src = src;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnContent"></param>
        internal AtomContent(XmlNode xnContent)
        {
            if (xnContent == null)
            {
                throw new ArgumentNullException("xnContent");
            }
            this.DeserializeFromXml(xnContent);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnContent"></param>
        private void DeserializeFromXml(XmlNode xnContent)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnContent.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnContent.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            this.Content = xnContent.InnerText;
            XmlAttribute xaType = xnContent.Attributes[XMLATTRIBUTE_TYPE];
            if (xaType != null)
            {
                switch (xaType.InnerText.ToLower())
                {
                    case "":
                    case "text":
                        this.Type = AtomTextType.Text;
                        break;
                    case "html":
                        this.Type = AtomTextType.Html;
                        break;
                    case "xhtml":
                        this.Type = AtomTextType.XHtml;
                        break;
                    default:
                        this.MediaType = xaType.InnerText;
                        this.Type = AtomTextType.Unknown;
                        break;
                };     
            }
            XmlNode xnSrc = xnContent.Attributes[XMLATTRIBUTE_SRC];
            if (xnSrc != null)
            {
                this.Src = xnSrc.InnerText;
            }

            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLATTRIBUTE_SRC, namespaceUri));

            this.Modules = new SyndicationModuleCollection(xnContent, exclusionElements);
        }
        #endregion

        #region Fields
        private string m_content;
        public string Content
        {
            get
            {
                return m_content;
            }
            set
            {
                m_content = value;
            }
        }
        private AtomTextType m_type = AtomTextType.NotSet;
        public AtomTextType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        private string m_mediaType;
        public string MediaType
        {
            get
            {
                return m_mediaType;
            }
            set
            {
                m_mediaType = value;
            }
        }
        private string m_src;
        public string Src 
        {
            get
            {
                return m_src;
            }
            set
            {
                m_src = value;
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

        #region Protected Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            XmlElement xeContent = xdAtom.CreateElement(XML_TAG);

            if (this.Content != null)
            {
                if (this.Type == AtomTextType.XHtml)
                {
                    xeContent.InnerXml = this.Content;
                }
                else
                {
                    xeContent.InnerText = this.Content;
                }
            }

            if (this.Type == AtomTextType.Unknown )
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_SERIALIZATION_ILLEGAL_VALUE, "AtomTextType", XML_TAG);
                throw new SyndicationSerializationException(msg);                
            }
            if ( this.Type != AtomTextType.NotSet ) {
                XmlAttribute xaType = xdAtom.CreateAttribute(XMLATTRIBUTE_TYPE);
                xaType.InnerText = this.Type.ToString().ToLower();
                xeContent.Attributes.Append(xaType);
            }

            if (this.Src != null)
            {
                XmlAttribute xaSrc = xdAtom.CreateAttribute(XMLATTRIBUTE_SRC);
                xaSrc.InnerText = this.Src;
                xeContent.Attributes.Append(xaSrc);
            }



            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeContent);
            }

            return xeContent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.Type == AtomTextType.Unknown)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLATTRIBUTE_TYPE);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion
    }// class
}// namespace
