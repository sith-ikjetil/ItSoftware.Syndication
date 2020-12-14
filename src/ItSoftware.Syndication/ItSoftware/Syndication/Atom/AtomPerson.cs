using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// satomPersonConstruct =
    /// atomCommonAttributes,
    /// (element atom:name { text }
    /// & element atom:uri { atomUri }?
    /// & element atom:email { atomEmailAddress }?
    /// & extensionElement*)
    /// </summary>
    public abstract class AtomPerson : AtomElementBase
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XMLTAG_NAME = "name";
        internal static readonly string XMLTAG_URI = "uri";
        internal static readonly string XMLTAG_EMAIL = "email";
        #endregion

        #region Private Const Data
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomPerson()
        {
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="email"></param>
        public AtomPerson(string name, string uri, string email)
        {
            this.Name = name;
            this.Uri = uri;
            this.Email = email;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnPerson"></param>
        internal AtomPerson(XmlNode xnPerson)
        {
            if (xnPerson == null)
            {
                throw new ArgumentNullException("xnPerson");
            }
            this.DeserializeFromXml(xnPerson);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnPerson"></param>
        private void DeserializeFromXml(XmlNode xnPerson)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnPerson.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnPerson.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            XmlNode xnName = xnPerson.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLTAG_NAME), nsmgr); // required
            if (xnName != null)
            {
                this.Name = xnName.InnerText;
            }
            XmlNode xnUri = xnPerson.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLTAG_URI), nsmgr);
            if (xnUri != null)
            {
                this.Uri = xnUri.InnerText;
            }
            XmlNode xnEmail = xnPerson.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLTAG_EMAIL), nsmgr);
            if (xnEmail != null)
            {
                this.Email = xnEmail.InnerText;
            }
          
            //
            // Deserialize modules.
            //		            
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add( new SyndicationModuleExclusionElement(XMLTAG_NAME, namespaceUri) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(XMLTAG_URI, namespaceUri));
            exclusionElements.Add( new SyndicationModuleExclusionElement(XMLTAG_EMAIL, namespaceUri));
            this.ExclusionElements = exclusionElements;

            this.Modules = new SyndicationModuleCollection(xnPerson, this.ExclusionElements);
        }
        #endregion

        #region Internal Properties
        private List<SyndicationModuleExclusionElement> m_exclusionElements;
        internal List<SyndicationModuleExclusionElement> ExclusionElements
        {
            get
            {
                return m_exclusionElements;
            }
            private set
            {
                m_exclusionElements = value;
            }
        }
        #endregion

        #region Required Fields
        private string m_name;
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        #endregion

        #region Optional Fields
        /// <summary>
        /// 
        /// </summary>
        private string m_uri;
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        private string m_email;
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get
            {
                return m_email;
            }
            set
            {
                m_email = value;
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

        #region Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="xe"></param>
        internal void SerializeToXml(AtomVersion version, XmlElement xe)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                if (this.Name != null)
                {
                    XmlElement xeName = xe.OwnerDocument.CreateElement(XMLTAG_NAME);
                    xeName.InnerText = this.Name;
                    xe.AppendChild(xeName);
                }
                if (this.Uri != null)
                {
                    XmlElement xeUri = xe.OwnerDocument.CreateElement(XMLTAG_URI);
                    xeUri.InnerText = this.Uri;
                    xe.AppendChild(xeUri);
                }
                if (this.Email != null)
                {
                    XmlElement xeEmail = xe.OwnerDocument.CreateElement(XMLTAG_EMAIL);
                    xeEmail.InnerText = this.Email;
                    xe.AppendChild(xeEmail);
                }

                //
                // Always serialize modules last.
                //
                if (this.Modules != null)
                {
                    this.Modules.SerializeToXml(xe);
                }
            }
        }
        #endregion
        
        #region Protected Internal Methods
        /// <summary>
        /// Validate element.
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {            
        }
        #endregion
    }// class
}// namespace
