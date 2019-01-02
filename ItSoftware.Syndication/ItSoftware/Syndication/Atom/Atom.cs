using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
	/// <summary>
	/// ATOM Syndication.
    /// atomFeed =
    /// element atom:feed {
    ///  atomCommonAttributes,
    ///  (atomAuthor*
    ///   & atomCategory*
    ///   & atomContributor*
    ///   & atomGenerator?
    ///   & atomIcon?
    ///   & atomId
    ///   & atomLink*
    ///   & atomLogo?
    ///   & atomRights?
    ///   & atomSubtitle?
    ///   & atomTitle
    ///   & atomUpdated
    ///   & extensionElement*),
    ///  atomEntry*
    /// }
	/// </summary>
	public class Atom : SyndicationBase
    {
        #region Internal Static Readonly
        internal static readonly string XMLELEMENT_FEED = "feed";
        internal static readonly string XMLELEMENT_ICON = "icon";
        internal static readonly string XMLELEMENT_ID = "id";
        internal static readonly string XMLELEMENT_LOGO = "logo";
        internal static readonly string XMLELEMENT_RIGHTS = "rights";
        internal static readonly string XMLELEMENT_SUBTITLE = "subtitle";
        internal static readonly string XMLELEMENT_TITLE = "title";
        internal static readonly string XMLELEMENT_UPDATED = "updated";
        #endregion

        #region Private Const Data
        //
        // ATOM Attributes.
        //	
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";        
        private const string ATOM_ATTRIBUTE_XMLNS_VALUE_1_0 = "http://www.w3.org/2005/Atom";
                             
        //
        // Misc.
        //
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_XML_VERSION_1_0 = "1.0";
        private const string DEFAULT_ENCODING = "utf-8";
        #endregion

        #region ATOM Internal Static ReadOnly Fields
        internal static readonly string ATOM_ERRORMESSAGE_INVALID_ATOM_FORMAT_1_0 = "Invalid or unrecognized ATOM format.";
        internal static readonly string ATOM_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL = "Required field '{0}' cannot be null.";
        internal static readonly string ATOM_ERRORMESSAGE_UNKNOWN_VALUE = "Unknown value";
        internal static readonly string ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID = "Validation failed. '{0}' element had invalid '{1}' element.";
        internal static readonly string ATOM_ERRORMESSAGE_SERIALIZATION_ILLEGAL_VALUE = "Serialization failed because of an illegal '{0}' value on '{1}' element.";
        internal static readonly string ATOM_ERRORMESSAGE_CANNOT_SERIALIZE_UNRECOGNIZED_VERSION = "Cannot serialize unrecognized ATOM version.";
        internal static readonly string ATOM_ERRORMESSAGE_CANNOT_VALIDATE_UNRECOGNIZED_VERSION = "Cannot validate unrecognized ATOM version.";
        internal static readonly string ATOM_ERRORMESSAGE_CANNOT_SAVE_UNRECOGNIZED_VERSION = "Cannot save feed to unrecognized ATOM version.";
        #endregion  
        
        #region Constructors
        /// <summary>
		/// Public constructor.
		/// </summary>
		public Atom() {			
			this.Modules = new SyndicationModuleCollection();
		}		
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
		public Atom( XmlDocument xdAtom ) {			
			if ( xdAtom == null ) {								
				throw new ArgumentNullException("xdAtom");
			}
			this.DeserializeFromXml( xdAtom );
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
        public Atom(string atom)
        {	
			if ( atom == null ) {								
				throw new ArgumentNullException("atom");
			}
			XmlDocument xdAtom = new XmlDocument();
			xdAtom.LoadXml(Syndication.NormalizeSyndication(atom));
			this.DeserializeFromXml( xdAtom );
		}				
        #endregion

        #region Public Properties
        /// <summary>
        /// RecognizeVersion backing field.
        /// </summary>
        private AtomVersion m_recognizedVersion = AtomVersion.NotSet;
        /// <summary>
        /// Recognized Atom version.
        /// </summary>
        public AtomVersion RecognizedVersion {
            get
            {
                return m_recognizedVersion;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string m_namespaceUri;
        /// <summary>
        /// 
        /// </summary>
        public string NamespaceUri
        {
            get
            {
                return m_namespaceUri;
            }
            set
            {
                m_namespaceUri = value;
            }
        }
        /// <summary>
        /// ValidateOnSave backing field.
        /// </summary>
        private bool m_validateOnSave = false;//true
        /// <summary>
        /// Gets or sets wheather or not to validate ATOM on save.
        /// </summary>
        private bool ValidateOnSave
        {
            get
            {
                return m_validateOnSave;
            }
            set
            {
                m_validateOnSave = value;
            }
        }
        /// <summary>
        /// ValidateContent backing field.
        /// </summary>
        private bool m_validateContent = false;//true
        /// <summary>
        /// Gets or sets wheather or not to validate content on validation.
        /// </summary>
        private bool ValidateContent
        {
            get
            {
                return m_validateContent;
            }
            set
            {
                m_validateContent = value;
            }
        }
        #endregion

        #region Deserialization Methods
        /// <summary>
        /// Deserializes the object from XML.
        /// </summary>
        /// <param name="xdRss"></param>
        private void DeserializeFromXml(XmlDocument xdAtom)
        {
            //
            // Set default values.
            //            
            this.m_recognizedVersion = AtomVersion.Unknown;            
            this.NamespaceUri = null;            

            //
            // Load document element.
            //
            XmlNode xnAtom = xdAtom.DocumentElement;

            //
            // Check that we have ATOM local name.
            //
            if (xnAtom.LocalName != XMLELEMENT_FEED)
            {
                throw new SyndicationFormatInvalidException(Atom.ATOM_ERRORMESSAGE_INVALID_ATOM_FORMAT_1_0);
            }

            //
            // Version 1.0
            //
            this.NamespaceUri = xnAtom.NamespaceURI; 
            if (xnAtom.NamespaceURI == ATOM_ATTRIBUTE_XMLNS_VALUE_1_0)
            {                
                this.m_recognizedVersion = AtomVersion.Atom_1_0;                
            }   
         
            //
            // Deserialize from XML.
            //
            DeserializeFromXml(xnAtom);
        }
        /// <summary>
        /// Deserializes the object from XML.
        /// </summary>
        /// <param name="xnRss"></param>
        private void DeserializeFromXml(XmlNode xnFeed)
        {
            //
            // Try-Catch block to make sure the version is set back to NotSet if the deserialization fails.
            //
            try
            {
                //
                // Set namespace manager.
                //                
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnFeed.OwnerDocument.NameTable);
                nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, this.NamespaceUri);

                //
                // Author.
                //
                XmlNodeList xnlAuthors = xnFeed.SelectNodes(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomAuthor.XML_TAG), nsmgr);
                if (xnlAuthors != null)
                {
                    this.Authors = new AtomAuthorCollection(xnlAuthors);
                }

                //
                // Category.
                //
                XmlNodeList xnlCategories = xnFeed.SelectNodes(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomCategory.XML_TAG), nsmgr);
                if (xnlCategories != null)
                {
                    this.Categories = new AtomCategoryCollection(xnlCategories);
                }

                //
                // Contributor.
                //								
                XmlNodeList xnlContributors = xnFeed.SelectNodes(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomContributor.XML_TAG), nsmgr);
                if (xnlContributors != null)
                {
                    this.Contributors = new AtomContributorCollection(xnlContributors);
                }

                //
                // Generator.
                //
                XmlNode xnGenerator = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomGenerator.XML_TAG), nsmgr);
                if (xnGenerator != null)
                {
                    this.Generator = new AtomGenerator(xnGenerator);
                }

                //
                // Icon.
                //
                XmlNode xnIcon = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_ICON), nsmgr);
                if (xnIcon != null)
                {
                    this.Icon = xnIcon.InnerText;
                }

                //
                // Id.
                //
                XmlNode xnId = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_ID), nsmgr);
                if (xnId != null)
                {
                    this.Id = xnId.InnerText;
                }

                //
                // Link.
                //
                XmlNodeList xnlLink = xnFeed.SelectNodes(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomLink.XML_TAG), nsmgr);
                if (xnlLink != null)
                {
                    this.Links = new AtomLinkCollection(xnlLink);
                }

                //
                // Logo.
                //
                XmlNode xnLogo = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_LOGO), nsmgr);
                if (xnLogo != null)
                {
                    this.Logo = xnLogo.InnerText;
                }

                //
                // Rights.
                //
                XmlNode xnRights = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_RIGHTS), nsmgr);
                if (xnRights != null)
                {
                    this.Rights = new AtomText(xnRights);
                }

                //
                // Subtitle.
                //
                XmlNode xnSubtitle = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_SUBTITLE), nsmgr);
                if (xnSubtitle != null)
                {
                    this.Subtitle = new AtomText(xnSubtitle);
                }

                //
                // Title.
                //
                XmlNode xnTitle = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_TITLE), nsmgr);
                if (xnTitle != null)
                {
                    this.Title = new AtomText(xnTitle);
                }

                //
                // Updated.
                //
                XmlNode xnUpdated = xnFeed.SelectSingleNode(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, XMLELEMENT_UPDATED), nsmgr);
                if (xnUpdated != null)
                {
                    this.Updated = new AtomDateTime(xnUpdated.InnerText);
                }

                //
                // Entries.
                //
                XmlNodeList xnlEntries = xnFeed.SelectNodes(string.Format("{0}:{1}", NAMESPACE_PREFIX_XMLNS, AtomEntry.XML_TAG), nsmgr);
                if (xnlEntries != null)
                {
                    this.Entries = new AtomEntryCollection(xnlEntries);
                }

                //
                // Deserialize Modules.
                //		
                List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
                //exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_FEED, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomAuthor.XML_TAG, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomCategory.XML_TAG, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomContributor.XML_TAG, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomGenerator.XML_TAG, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_ICON, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_ID, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomLink.XML_TAG, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_LOGO, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_RIGHTS, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_SUBTITLE, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_TITLE, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_UPDATED, this.NamespaceUri));
                exclusionElements.Add(new SyndicationModuleExclusionElement(AtomEntry.XML_TAG, this.NamespaceUri));

                this.Modules = new SyndicationModuleCollection(xnFeed, exclusionElements);
            }
            catch (OutOfMemoryException)
            {
                throw;
            }            
            catch (StackOverflowException)
            {
                throw;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception)
            {
                this.m_recognizedVersion = AtomVersion.NotSet;
                throw;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates that the object model has the elements it needs
        /// to produce an ATOM XML of the desired version.
        /// </summary>
        /// <param name="version"></param>
        public bool Validate(AtomVersion version, bool validateContent)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                //
                // Check for required elements.
                //				
                if (this.Id == null)
                {
                    string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL, "Id");
                    throw new SyndicationValidationException(msg);
                }
                if (this.Title == null)
                {
                    string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL, "Title");
                    throw new SyndicationValidationException(msg);
                }
                if (this.Updated == null)
                {
                    string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL, "Updated");
                    throw new SyndicationValidationException(msg);
                }                

                //
                // Ask each item to validate itself.
                //
                if (this.Authors != null)
                {
                    this.Authors.Validate(version, validateContent);
                }
                if (this.Categories != null)
                {                    
                    this.Categories.Validate(version, validateContent);
                }
                if (this.Contributors != null)
                {
                    this.Contributors.Validate(version, validateContent);
                }
                if (this.Entries != null)
                {
                    this.Entries.Validate(version, validateContent);
                }
                if (this.Generator != null)
                {
                    this.Generator.Validate(version, validateContent);
                }                
                //if (this.Icon != null)
                //{
                //}
                //if (this.Id != null)
                //{
                //}                
                if (this.Links != null)
                {
                    this.Links.Validate(version, validateContent);
                }
                if (this.Rights != null)
                {
                    this.Rights.Validate(version, validateContent);
                }
                if (this.Subtitle != null)
                {
                    this.Subtitle.Validate(version, validateContent);
                }
                if (this.Title != null)
                {
                    this.Title.Validate(version, validateContent);
                }                
                //if (this.Updated != null)
                //{
                //}                                
                return true;
            }            
            throw new SyndicationValidationException(Atom.ATOM_ERRORMESSAGE_CANNOT_VALIDATE_UNRECOGNIZED_VERSION);
        }
        /// <summary>
        /// Save the ATOM object to XML.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="encoding"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public XmlDocument Save(AtomVersion version)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                if (ValidateOnSave)
                {
                    Validate(version, this.ValidateContent);
                }
                return SerializeToXml_1_0(DEFAULT_ENCODING);
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_CANNOT_SAVE_UNRECOGNIZED_VERSION);
        }
        /// <summary>
        /// Save the ATOM object to XML.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="encoding"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public XmlDocument Save(AtomVersion version, string encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (version == AtomVersion.Atom_1_0)
            {
                if (ValidateOnSave)
                {
                    Validate(version, this.ValidateContent);
                }
                return SerializeToXml_1_0(encoding);
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_CANNOT_SAVE_UNRECOGNIZED_VERSION);
        }
        /// <summary>
        /// Saves the ATOM object according to the version and encoding arguments.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public void Save(System.IO.Stream stream, AtomVersion version, string encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (version == AtomVersion.Atom_1_0)
            {                
                this.Save(version, encoding).Save(stream);
                return;
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_CANNOT_SAVE_UNRECOGNIZED_VERSION);
        }
        /// <summary>
        /// Saves the ATOM object according to the version and encoding arguments.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        /// <param name="encoding"></param>
        /// <param name="customVersion"></param>
        public void Save(string filename, AtomVersion version, string encoding)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (version == AtomVersion.Atom_1_0)
            {
                this.Save(version, encoding).Save(filename);
                return;
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_CANNOT_SAVE_UNRECOGNIZED_VERSION);
        }
        #endregion

        #region Serialization Methods
        /// <summary>
        /// Serialize to ATOM 1.0 feed.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private XmlDocument SerializeToXml_1_0(string encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            XmlDocument xdAtom = new XmlDocument();

            XmlDeclaration xdDeclaration = xdAtom.CreateXmlDeclaration(ATOM_XML_VERSION_1_0, encoding, null);
            xdAtom.AppendChild(xdDeclaration);            

            //
            // Create the rdf element.
            //
            XmlElement xeFeed = xdAtom.CreateElement(XMLELEMENT_FEED);

            XmlAttribute xaXmlns = xdAtom.CreateAttribute(ATOM_ATTRIBUTE_XMLNS);
            xaXmlns.InnerText = (this.NamespaceUri != null) ? this.NamespaceUri : ATOM_ATTRIBUTE_XMLNS_VALUE_1_0;
            xeFeed.Attributes.Append(xaXmlns);

            xdAtom.AppendChild(xeFeed);

            //
            // Serialize authors.
            //
            if (this.Authors != null)
            {
                List<XmlNode> authors = this.Authors.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in authors)
                {
                    xeFeed.AppendChild(node);
                }
            }
            //
            // Serialize categories.
            //
            if (this.Categories != null)
            {
                List<XmlNode> categories = this.Categories.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in categories)
                {
                    xeFeed.AppendChild(node);
                }
            }
            //
            // Serialize contributors.
            //
            if (this.Contributors != null)
            {
                List<XmlNode> contributors = this.Contributors.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in contributors)
                {
                    xeFeed.AppendChild(node);
                }
            }            

            //
            // Serialize generator.
            //
            if (this.Generator != null)
            {
                xeFeed.AppendChild(this.Generator.SerializeToXml_1_0(xdAtom));
            }
            else
            {
                xeFeed.AppendChild(AtomGenerator.Syndication.SerializeToXml_1_0(xdAtom));
            }

            //
            // Serialize icon.
            //
            if (this.Icon != null)
            {
                XmlElement xeIcon = xdAtom.CreateElement(XMLELEMENT_ICON);
                xeIcon.InnerText = this.Icon;
                xeFeed.AppendChild(xeIcon);
            }

            //
            // Serialize id.
            //
            if (this.Id != null)
            {
                XmlElement xeId = xdAtom.CreateElement(XMLELEMENT_ID);
                xeId.InnerText = this.Id;
                xeFeed.AppendChild(xeId);
            }

            //
            // Serialize links.
            //
            if (this.Links != null)
            {
                List<XmlNode> nodes = this.Links.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes)
                {
                    xeFeed.AppendChild(node);
                }
            }
            
            //
            // Serialize logo.
            // 
            if (this.Logo != null)
            {
                XmlElement xeLogo = xdAtom.CreateElement(XMLELEMENT_LOGO);
                xeLogo.InnerText = this.Logo;
                xeFeed.AppendChild(xeLogo);
            }
            
            //
            // Serialize rights.
            //
            if (this.Rights != null)
            {
                XmlElement xeRights = xdAtom.CreateElement(XMLELEMENT_RIGHTS);
                this.Rights.SerializeToXml(AtomVersion.Atom_1_0, xeRights);
                xeFeed.AppendChild(xeRights);
            }
            
            //
            // Serialize subtitle.
            //
            if (this.Subtitle != null)
            {
                XmlElement xeSubtitle = xdAtom.CreateElement(XMLELEMENT_SUBTITLE);
                this.Subtitle.SerializeToXml(AtomVersion.Atom_1_0, xeSubtitle);
                xeFeed.AppendChild(xeSubtitle);
            }
            
            //
            // Serialize title.
            //
            if (this.Title != null)
            {
                XmlElement xeTitle = xdAtom.CreateElement(XMLELEMENT_TITLE);
                this.Title.SerializeToXml(AtomVersion.Atom_1_0, xeTitle);
                xeFeed.AppendChild(xeTitle);
            }
            
            //
            // Serialize updated.
            //
            if (this.Updated != null)
            {
                XmlElement xeUpdated = xdAtom.CreateElement(XMLELEMENT_UPDATED);
                xeUpdated.InnerText = this.Updated.ToString();
                xeFeed.AppendChild(xeUpdated);
            }
            
            //
            // Serialize entries.
            //
            if (this.Entries != null)
            {
                List<XmlNode> entries = this.Entries.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in entries)
                {
                    xeFeed.AppendChild(node);
                }
            }

            //
            // Return the created XML document.
            //
            return xdAtom;            
        }
        #endregion

        #region Required Fields
        /// <summary>
        /// Id backing field.
        /// </summary>
        private string m_id;                
        /// <summary>
        /// Identifies the feed using a universally unique and permanent URI. If you have a long-term, renewable lease on your Internet domain name, then you can feel free to use your website's address.
        /// </summary>
        public string Id
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }
        /// <summary>
        /// Title backing field.
        /// </summary>
        private AtomText m_title;
        /// <summary>
        /// Contains a human readable title for the feed. Often the same as the title of the associated website. This value should not be blank.
        /// </summary>
        public AtomText Title
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
        /// <summary>
        /// Updated backing field.
        /// </summary>
        private AtomDateTime m_updated;
        /// <summary>
        /// Indicates the last time the feed was modified in a significant way.
        /// </summary>
        public AtomDateTime Updated
        {
            get
            {
                return m_updated;
            }
            set 
            {
                m_updated = value;
            }
        }
        #endregion

        #region Reccommended Fields
        /// <summary>
        /// Authors backing field.
        /// </summary>
        private AtomAuthorCollection m_authors = new AtomAuthorCollection();
        /// <summary>
        /// Names one author of the feed. A feed may have multiple author elements. A feed must contain at least one author element unless all of the entry elements contain at least one author element. 
        /// </summary>
        public AtomAuthorCollection Authors
        {
            get
            {
                return m_authors;
            }
            set
            {
                m_authors = value;
            }
        }   
        /// <summary>
        /// Link backing field.
        /// </summary>
        private AtomLinkCollection m_links = new AtomLinkCollection();
        /// <summary>
        /// Identifies a related Web page. The type of relation is defined by the rel attribute. A feed is limited to one alternate per type and hreflang. A feed should contain a link back to the feed itself.
        /// </summary>
        public AtomLinkCollection Links
        {
            get
            {
                return m_links;
            }
            set
            {
                m_links = value;
            }
        }        
        #endregion

        #region Optional Fields
        /// <summary>
        /// Categories backing field.
        /// </summary>
        private AtomCategoryCollection m_categories = new AtomCategoryCollection();
        /// <summary>
        /// Specifies a category that the feed belongs to. A feed may have multiple category elements.
        /// </summary>
        public AtomCategoryCollection Categories
        {
            get
            {
                return m_categories;
            }
            set 
            {
                m_categories = value;
            }
        }
        /// <summary>
        /// Contributors backing field.
        /// </summary>
        private AtomContributorCollection m_contributors = new AtomContributorCollection();
        /// <summary>
        /// Names one contributor to the feed. An feed may have multiple contributor elements.
        /// </summary>
        public AtomContributorCollection Contributors
        {
            get
            {
                return m_contributors;
            }
            set
            {
                m_contributors = value;
            }
        }
        /// <summary>
        /// Generator backing field.
        /// </summary>
        private AtomGenerator m_generator;
        /// <summary>
        /// Identifies the software used to generate the feed, for debugging and other purposes. Both the uri and version attributes are optional. 
        /// </summary>
        public AtomGenerator Generator
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
        /// <summary>
        /// Icon backing field.
        /// </summary>
        private string m_icon;
        /// <summary>
        /// Identifies a small image which provides iconic visual identification for the feed. Icons should be square.
        /// </summary>
        public string Icon
        {
            get
            {
                return m_icon;
            }
            set
            {
                m_icon = value;
            }
        }
        /// <summary>
        /// Logo backing field.
        /// </summary>
        private string m_logo;
        /// <summary>
        /// Identifies a larger image which provides visual identification for the feed. Images should be twice as wide as they are tall. 
        /// </summary>
        public string Logo
        {
            get
            {
                return m_logo;
            }
            set 
            {
                m_logo = value;
            }
        }
        /// <summary>
        /// Rights backing field.
        /// </summary>
        private AtomText m_rights;
        /// <summary>
        /// Conveys information about rights, e.g. copyrights, held in and over the feed.
        /// </summary>
        public AtomText Rights
        {
            get
            {
                return m_rights;
            }
            set
            {
                m_rights = value;
            }
        }
        /// <summary>
        /// Subtitle backing field.
        /// </summary>
        private AtomText m_subtitle;
        /// <summary>
        /// Conveys a human-readable description or subtitle for a feed.
        /// </summary>
        public AtomText Subtitle
        {
            get
            {
                return m_subtitle;
            }
            set
            {
                m_subtitle = value;
            }
        }
        /// <summary>
        /// Entries backing field.
        /// </summary>
        private AtomEntryCollection m_entries = new AtomEntryCollection();
        /// <summary>
        /// Example of an entry would be a single post on a weblog
        /// </summary>
        public AtomEntryCollection Entries
        {
            get
            {
                return m_entries;
            }
            set
            {
                m_entries = value;
            }
        }
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
    }// class
}// namespace
