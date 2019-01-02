using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomCategory =
    /// element atom:category {
    ///   atomCommonAttributes,
    ///   attribute term { text },
    ///   attribute scheme { atomUri }?,
    ///   attribute label { text }?,
    ///   undefinedContent
    ///  }
    /// </summary>
    public class AtomCategory : AtomElementBase
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "category";
        internal static readonly string XMLATTRIBUTE_LABEL = "label";
        internal static readonly string XMLATTRIBUTE_SCHEME = "scheme";
        internal static readonly string XMLATTRIBUTE_TERM = "term";
        #endregion

        #region Private Const Data
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomCategory()
        {
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        public AtomCategory(string term)
        {
            this.Term = term;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        public AtomCategory(string term, string scheme, string label)
        {
            this.Term = term;
            this.Scheme = scheme;
            this.Label = label;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnCategory"></param>
        internal AtomCategory(XmlNode xnCategory)
        {
            if (xnCategory == null)
            {
                throw new ArgumentNullException("xnCategory");
            }
            this.DeserializeFromXml(xnCategory);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnCategory"></param>
        private void DeserializeFromXml(XmlNode xnCategory)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnCategory.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnCategory.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            XmlAttribute xaTerm = xnCategory.Attributes[XMLATTRIBUTE_TERM];
            if (xaTerm != null)
            {
                this.Term = xaTerm.InnerText;
            }
            XmlAttribute xaScheme = xnCategory.Attributes[XMLATTRIBUTE_SCHEME];
            if (xaScheme != null)
            {
                this.Scheme = xaScheme.InnerText;
            }
            XmlAttribute xaLabel = xnCategory.Attributes[XMLATTRIBUTE_LABEL];
            if (xaLabel != null)
            {
                this.Label = xaLabel.InnerText;
            }
            
            this.Modules = new SyndicationModuleCollection(xnCategory, new List<SyndicationModuleExclusionElement>());
        }
        #endregion

        #region Required Fields
        private string m_term;
        public string Term
        {
            get
            {
                return m_term;
            }
            set
            {
                m_term = value;
            }
        }
        #endregion

        #region Optional Fields
        private string m_scheme;
        public string Scheme {
            get
            {
                return m_scheme;
            }
            set 
            {
                m_scheme = value;
            }
        }
        private string m_label;
        public string Label
        {
            get
            {
                return m_label;
            }
            set
            {
                m_label = value;
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
            XmlElement xeCategory = xdAtom.CreateElement(XML_TAG);

            if (this.Label != null)
            {
                XmlAttribute xaLabel = xdAtom.CreateAttribute(XMLATTRIBUTE_LABEL);
                xaLabel.InnerText = this.Label;
                xeCategory.Attributes.Append(xaLabel);
            }
            
            if (this.Scheme != null)
            {
                XmlAttribute xaScheme = xdAtom.CreateAttribute(XMLATTRIBUTE_SCHEME);
                xaScheme.InnerText = this.Scheme;
                xeCategory.Attributes.Append(xaScheme);
            }

            if (this.Term != null)
            {
                XmlAttribute xaTerm = xdAtom.CreateAttribute(XMLATTRIBUTE_TERM);
                xaTerm.InnerText = this.Term;
                xeCategory.Attributes.Append(xaTerm);
            }

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeCategory);
            }

            return xeCategory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.Term == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLATTRIBUTE_TERM);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion
    }// class
}// namespace
