using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomSource =
    /// element atom:source {
    ///   atomCommonAttributes,
    ///   (atomAuthor*
    ///    & atomCategory*
    ///    & atomContributor*
    ///    & atomGenerator?
    ///    & atomIcon?
    ///    & atomId?
    ///    & atomLink*
    ///    & atomLogo?
    ///    & atomRights?
    ///    & atomSubtitle?
    ///    & atomTitle?
    ///    & atomUpdated?
    ///    & extensionElement*)
    /// }
    /// </summary>
    public class AtomSource : AtomElementBase
    {
        //
        // ATOM Elements.
        //		
        public static readonly string XML_TAG = "source";
        public static readonly string XMLTAG_ICON = "icon";
        public static readonly string XMLTAG_ID = "id";
        public static readonly string XMLTAG_LOGO = "logo";
        public static readonly string XMLTAG_RIGHTS = "rights";
        public static readonly string XMLTAG_SUBTITLE = "subtitle";
        public static readonly string XMLTAG_TITLE = "title";
        public static readonly string XMLTAG_UPDATED = "updated";

        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        public AtomSource()
        {
        }

        public AtomSource(XmlNode xnSource)
        {
            if (xnSource == null)
            {
                throw new ArgumentNullException("xnSource");
            }
            this.DeserializeFromXml(xnSource);
        }

        private void DeserializeFromXml(XmlNode xnSource)
        {

            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnSource.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnSource.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            //
            // Author.
            //
            XmlNodeList xnlAuthors = xnSource.SelectNodes(NAMESPACE_PREFIX_XMLNS + ":" + AtomAuthor.XML_TAG,nsmgr);
            if (xnlAuthors != null)
            {
                this.Authors = new AtomAuthorCollection(xnlAuthors);
            }

            //
            // Category.
            //
            XmlNodeList xnlCategories = xnSource.SelectNodes(NAMESPACE_PREFIX_XMLNS + ":" + AtomCategory.XML_TAG,nsmgr);
            if (xnlCategories != null)
            {
                this.Categories = new AtomCategoryCollection(xnlCategories);
            }

            //
            // Contributor.
            //								
            XmlNodeList xnlContributors = xnSource.SelectNodes(NAMESPACE_PREFIX_XMLNS + ":" + AtomContributor.XML_TAG,nsmgr);
            if (xnlContributors != null)
            {
                this.Contributors = new AtomContributorCollection(xnlContributors);
            }

            //
            // Generator.
            //
            XmlNode xnGenerator = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + AtomGenerator.XML_TAG,nsmgr);
            if (xnGenerator != null)
            {
                this.Generator = new AtomGenerator(xnGenerator);
            }

            //
            // Icon.
            //
            XmlNode xnIcon = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_ICON, nsmgr);
            if (xnIcon != null)
            {
                this.Icon = xnIcon.InnerText;
            }

            //
            // Id.
            //
            XmlNode xnId = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_ID, nsmgr);
            if (xnId != null)
            {
                this.Id = xnId.InnerText;
            }

            //
            // Link.
            //
            XmlNodeList xnlLinks = xnSource.SelectNodes(NAMESPACE_PREFIX_XMLNS + ":" + AtomLink.XML_TAG,nsmgr);
            if (xnlLinks != null)
            {
                this.Links = new AtomLinkCollection(xnlLinks);
            }

            //
            // Logo.
            //
            XmlNode xnLogo = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_LOGO, nsmgr);
            if (xnLogo != null)
            {
                this.Logo = xnLogo.InnerText;
            }

            //
            // Rights.
            //
            XmlNode xnRights = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_RIGHTS, nsmgr);
            if (xnRights != null)
            {
                this.Rights = new AtomText(xnRights);
            }

            //
            // Subtitle.
            //
            XmlNode xnSubtitle = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_SUBTITLE, nsmgr);
            if (xnSubtitle != null)
            {
                this.Subtitle = new AtomText(xnSubtitle);
            }

            //
            // Title.
            //
            XmlNode xnTitle = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_TITLE, nsmgr);
            if (xnTitle != null)
            {
                this.Title = new AtomText(xnTitle);
            }

            //
            // Updated.
            //
            XmlNode xnUpdated = xnSource.SelectSingleNode(NAMESPACE_PREFIX_XMLNS + ":" + XMLTAG_UPDATED, nsmgr);
            if (xnUpdated != null)
            {
                this.Updated = new AtomDateTime(xnUpdated.InnerText);
            }

            //
            // Deserialize From Modules.
            //		            
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomAuthor.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomCategory.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomContributor.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomGenerator.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_ICON, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_ID, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomLink.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_LOGO, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_RIGHTS, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_SUBTITLE, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_TITLE, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLTAG_UPDATED, namespaceUri));

            this.Modules = new SyndicationModuleCollection(xnSource, exclusionElements);
        }


        #region Required Fields
        private string m_id;
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
        private AtomText m_title;
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
        private AtomDateTime m_updated;
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
        private AtomAuthorCollection m_authors = new AtomAuthorCollection();
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
        private AtomLinkCollection m_links = new AtomLinkCollection();
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
        private AtomCategoryCollection m_categories = new AtomCategoryCollection();
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
        private AtomContributorCollection m_contributors = new AtomContributorCollection();
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
        private AtomGenerator m_generator;
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
        private AtomText m_rights;
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
        private AtomText m_subtitle;
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
        #endregion


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

        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            XmlElement xeSource = xdAtom.CreateElement(XML_TAG);

            if (this.Authors != null)
            {
                List<XmlNode> nodes = this.Authors.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes)
                {
                    xeSource.AppendChild(node);
                }
            }
            if (this.Categories != null)
            {
                List<XmlNode> nodes = this.Categories.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes) {
                    xeSource.AppendChild(node);
                }
            }
            if (this.Contributors != null)
            {
                List<XmlNode> nodes = this.Contributors.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes)
                {
                    xeSource.AppendChild(node);
                }
            }
            if (this.Generator != null)
            {
                xeSource.AppendChild(this.Generator.SerializeToXml_1_0(xdAtom));
            }
            if (this.Icon != null)
            {
                XmlElement xeIcon = xdAtom.CreateElement(XMLTAG_ICON);
                xeIcon.InnerText = this.Icon;
                xeSource.AppendChild(xeIcon);
            }
            if (this.Id != null)
            {
                XmlElement xeId = xdAtom.CreateElement(XMLTAG_ID);
                xeId.InnerText = this.Id;
                xeSource.AppendChild(xeId);
            }
            if (this.Links != null)
            {
                List<XmlNode> nodes = this.Links.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes)
                {
                    xeSource.AppendChild(node);
                }
            }
            if (this.Logo != null) {
                XmlElement xeLogo = xdAtom.CreateElement(XMLTAG_LOGO);
                xeLogo.InnerText = this.Logo;
                xeSource.AppendChild(xeLogo);
            }
            if (this.Rights != null)
            {
                xeSource.AppendChild(this.Rights.SerializeToXml_1_0(xdAtom));
            }
            if (this.Subtitle != null)
            {
                xeSource.AppendChild(this.Subtitle.SerializeToXml_1_0(xdAtom));
            }
            if (this.Title != null)
            {
                XmlElement xeTitle = xdAtom.CreateElement(XMLTAG_TITLE);
                this.Title.SerializeToXml(AtomVersion.Atom_1_0, xeTitle);
                xeSource.AppendChild(xeTitle);
            }
            if (this.Updated != null)
            {
                XmlElement xeUpdated = xdAtom.CreateElement(XMLTAG_UPDATED);
                xeUpdated.InnerText = this.Updated.ToString();
                xeSource.AppendChild(xeUpdated);
            }

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeSource);
            }

            return xeSource;
        }

        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            // Nothing to validate.
        }
    }// class
}// namespace
