using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using ItSoftware.Syndication;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomGenerator = element atom:generator {
    /// atomCommonAttributes,
    /// attribute uri { atomUri }?,
    /// attribute version { text }?,
    /// text
    /// }
    /// </summary>
    public class AtomGenerator : AtomElementBase
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "generator";
        internal static readonly string XMLATTRIBUTE_VERSION = "version";
        internal static readonly string XMLATTRIBUTE_URI = "uri";
        #endregion

        #region Private Const Fields
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomGenerator()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="version"></param>
        /// <param name="uri"></param>
        public AtomGenerator(string generator, string version, string uri)
        {
            this.Generator = generator;
            this.Version = version;
            this.Uri = uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnGenerator"></param>
        internal AtomGenerator(XmlNode xnGenerator)
        {
            if (xnGenerator == null)
            {
                throw new ArgumentNullException("xnGenerator");
            }
            this.DeserializeFromXml(xnGenerator);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnGenerator"></param>
        private void DeserializeFromXml(XmlNode xnGenerator)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnGenerator.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnGenerator.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            //this.Generator = new AtomText(xnGenerator);
            this.Generator = xnGenerator.InnerText;

            XmlAttribute xaVersion = xnGenerator.Attributes[XMLATTRIBUTE_VERSION];
            if (xaVersion != null)
            {
                this.Version = xaVersion.InnerText;
            }
            XmlAttribute xaUri = xnGenerator.Attributes[XMLATTRIBUTE_URI];
            if (xaUri != null)
            {
                this.Uri = xaUri.InnerText;
            }

            this.Modules = new SyndicationModuleCollection(xnGenerator, new List<SyndicationModuleExclusionElement>() );
        }
        #endregion

        #region Required Field
        private string m_generator;//AtomText
        public string Generator//AtomText
        {
            get
            {
                return m_generator;
            }
            set 
            {
                m_generator = value;
            }
        }
        #endregion

        #region Optional Fields
        private string m_version;
        public string Version
        {
            get
            {
                return m_version;
            }
            set 
            {
                m_version = value;
            }
        }
        private string m_uri;
        public string Uri
        {
            get
            {
                return m_uri;
            }
            set
            {
                m_uri = value;
            }
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// 
        /// </summary>
        public static AtomGenerator Syndication
        {
            get
            {
                AtomGenerator ag = new AtomGenerator();
                ag.Generator = "ItSoftware.Syndication";
                ag.Uri = "http://www.itsoftware.no/syndication/default.aspx";
                ag.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return ag;
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
            XmlElement xeGenerator = xdAtom.CreateElement(XML_TAG);

            if (this.Generator != null )
            {
                xeGenerator.InnerText = this.Generator;
            }
            if (this.Uri != null)
            {
                XmlAttribute xaUri = xdAtom.CreateAttribute(XMLATTRIBUTE_URI);
                xaUri.InnerText = this.Uri;
                xeGenerator.Attributes.Append(xaUri);
            }            
            if (this.Version != null)
            {
                XmlAttribute xaVersion = xdAtom.CreateAttribute(XMLATTRIBUTE_VERSION);
                xaVersion.InnerText = this.Version;
                xeGenerator.Attributes.Append(xaVersion);
            }

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeGenerator);
            }

            return xeGenerator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.Generator == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XML_TAG);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion
    }// class
}// namespace
