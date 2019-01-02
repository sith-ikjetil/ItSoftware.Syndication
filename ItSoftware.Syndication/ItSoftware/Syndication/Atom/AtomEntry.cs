using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomEntry =
    /// element atom:entry {
    ///   atomCommonAttributes,
    ///   (atomAuthor*
    ///    & atomCategory*
    ///    & atomContent?
    ///    & atomContributor*
    ///    & atomId
    ///    & atomLink*
    ///    & atomPublished?
    ///    & atomRights?
    ///    & atomSource?
    ///    & atomSummary?
    ///    & atomTitle
    ///    & atomUpdated
    ///    & extensionElement*)
    /// }
    /// </summary>
    public class AtomEntry : AtomElementBase
    {
        #region Internal Static Readonly Fields
        internal static readonly string XML_TAG = "entry";
        internal static readonly string XMLELEMENT_ID = "id";
        internal static readonly string XMLELEMENT_TITLE = "title";
        internal static readonly string XMLELEMENT_UPDATED = "updated";
        internal static readonly string XMLELEMENT_SUMMARY = "summary";
        internal static readonly string XMLELEMENT_PUBLISHED = "published";
        internal static readonly string XMLELEMENT_RIGHTS = "rights";
        internal static readonly string XMLELEMENT_SOURCE = "source";
        #endregion

        #region Private Const Fields
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomEntry()
        {
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="updated"></param>
        /// <param name="summary"></param>
        public AtomEntry(string id, AtomText title, AtomDateTime updated, AtomContent content, AtomText summary)
        {
            this.Id = id;
            this.Title = title;
            this.Updated = updated;
            this.Content = content;
            this.Summary = summary;
            this.Modules = new SyndicationModuleCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnEntry"></param>
        internal AtomEntry(XmlNode xnEntry)
        {
            if (xnEntry == null)
            {
                throw new ArgumentNullException("xnEntry");
            }
            this.DeserializeFromXml(xnEntry);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnEntry"></param>
        private void DeserializeFromXml(XmlNode xnEntry)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnEntry.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnEntry.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            XmlNode xnId = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS,XMLELEMENT_ID), nsmgr);
            if (xnId != null)
            {
                this.Id = xnId.InnerText;
            }
            XmlNode xnTitle = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_TITLE), nsmgr);
            if (xnTitle != null)
            {
                this.Title = new AtomText(xnTitle);
            }
            XmlNode xnUpdated = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_UPDATED), nsmgr);
            if (xnUpdated != null)
            {
                this.Updated = new AtomDateTime(xnUpdated.InnerText);
            }
            XmlNodeList xnlAuthors = xnEntry.SelectNodes(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, AtomAuthor.XML_TAG), nsmgr);
            if (xnlAuthors != null)
            {
                this.Authors = new AtomAuthorCollection(xnlAuthors);
            }
            XmlNode xnContent = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, AtomContent.XML_TAG), nsmgr);
            if (xnContent != null)
            {
                this.Content = new AtomContent(xnContent);
            }
            XmlNodeList xnlLinks = xnEntry.SelectNodes(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, AtomLink.XML_TAG), nsmgr);
            if (xnlLinks != null)
            {
                this.Links = new AtomLinkCollection(xnlLinks);
            }
            XmlNode xnSummary = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_SUMMARY), nsmgr);
            if (xnSummary != null)
            {
                this.Summary = new AtomText(xnSummary);
            }
            XmlNodeList xnlCategories = xnEntry.SelectNodes(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, AtomCategory.XML_TAG), nsmgr);
            if (xnlCategories != null)
            {
                this.Categories = new AtomCategoryCollection(xnlCategories);
            }
            XmlNodeList xnlContributors = xnEntry.SelectNodes(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, AtomContributor.XML_TAG), nsmgr);
            if (xnlContributors != null)
            {
                this.Contributors = new AtomContributorCollection(xnlContributors);
            }
            XmlNode xnPublished = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_PUBLISHED), nsmgr);
            if (xnPublished != null)
            {
                this.Published = new AtomDateTime(xnPublished.InnerText);
            }
            XmlNode xnRights = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_RIGHTS), nsmgr);
            if (xnRights != null)
            {
                this.Rights = new AtomText(xnRights);
            }
            XmlNode xnSource = xnEntry.SelectSingleNode(string.Format("{0}:{1}",NAMESPACE_PREFIX_XMLNS, XMLELEMENT_SOURCE), nsmgr);
            if (xnSource != null)
            {
                this.Source = new AtomSource(xnSource);
            }

            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_ID, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_TITLE, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_UPDATED, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomAuthor.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomContent.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomLink.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_SUMMARY, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomCategory.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(AtomContributor.XML_TAG, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_PUBLISHED, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_RIGHTS, namespaceUri));
            exclusionElements.Add(new SyndicationModuleExclusionElement(XMLELEMENT_SOURCE, namespaceUri));
            
            this.Modules = new SyndicationModuleCollection(xnEntry, exclusionElements);
        }
        #endregion

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
        private AtomContent m_content;
        public AtomContent Content
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
        private AtomText m_summary;
        public AtomText Summary
        {
            get
            {
                return m_summary;
            }
            set 
            {
                m_summary = value;
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
        private AtomDateTime m_published;
        public AtomDateTime Published
        {
            get 
            {
                return m_published;
            }
            set
            {
                m_published = value;
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
        private AtomSource m_source;
        public AtomSource Source
        {
            get
            {
                return m_source;
            }
            set 
            {
                m_source = value;
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

        #region Protected Overriden Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            XmlElement xeEntry = xdAtom.CreateElement("entry");
            
            if (this.Id != null)
            {
                XmlElement xeId = xdAtom.CreateElement("id");
                xeId.InnerText = this.Id;
                xeEntry.AppendChild(xeId);
            }
            if (this.Title != null)
            {
                XmlElement xeTitle = xdAtom.CreateElement("title");
                this.Title.SerializeToXml(AtomVersion.Atom_1_0,xeTitle);                
                xeEntry.AppendChild(xeTitle);
            }
            if (this.Updated != null)
            {
                XmlElement xeUpdated = xdAtom.CreateElement("updated");
                xeUpdated.InnerText = this.Updated.ToString();
                xeEntry.AppendChild(xeUpdated);
            }
            if (this.Authors != null)
            {
                List<XmlNode> nodes = this.Authors.SerializeToXml(AtomVersion.Atom_1_0, xdAtom);
                foreach (XmlNode node in nodes)
                {
                    xeEntry.AppendChild(node);
                }
            }
            if (this.Content != null)
            {
                xeEntry.AppendChild(this.Content.SerializeToXml_1_0(xdAtom));
            }
            if ( this.Links != null ) {
                List<XmlNode> nodes = this.Links.SerializeToXml(AtomVersion.Atom_1_0,xdAtom);
                foreach ( XmlNode node in nodes ) {
                    xeEntry.AppendChild(node);
                }
            }
            if ( this.Summary != null ) {
                XmlElement xeSummary = xdAtom.CreateElement("summary");
                this.Summary.SerializeToXml(AtomVersion.Atom_1_0,xeSummary);
                xeEntry.AppendChild(xeSummary);
            }
            if ( this.Categories != null ) {
                List<XmlNode> nodes = this.Categories.SerializeToXml(AtomVersion.Atom_1_0,xdAtom);
                foreach ( XmlNode node in nodes ) {
                    xeEntry.AppendChild(node);
                }
            }
            if ( this.Contributors != null ) {
                List<XmlNode> nodes = this.Contributors.SerializeToXml(AtomVersion.Atom_1_0,xdAtom);
                foreach ( XmlNode node in nodes ) {
                    xeEntry.AppendChild(node);
                }
            }
            if ( this.Published != null ) {
                XmlElement xePublished = xdAtom.CreateElement("published");
                xePublished.InnerText = this.Published.ToString();
                xeEntry.AppendChild(xePublished);
            }
            if ( this.Rights != null ) {
                xeEntry.AppendChild(this.Rights.SerializeToXml_1_0(xdAtom));
            }
            if ( this.Source != null ) {
                xeEntry.AppendChild(this.Source.SerializeToXml_1_0(xdAtom));
            }

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeEntry);
            }

            return xeEntry;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param> 
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.Id == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLELEMENT_ID);
                throw new SyndicationValidationException(msg);
            }
            else if (this.Title == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLELEMENT_TITLE);
                throw new SyndicationValidationException(msg);
            }
            else if (this.Updated == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, XMLELEMENT_UPDATED);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion

        #region Internal Static Properties
        /// <summary>
        /// 
        /// </summary>
        internal static AtomEntry InvalidLicence
        {
            get
            {
                return new AtomEntry(Guid.NewGuid().ToString(), new AtomText("ItSoftware.Syndication", AtomTextType.Text), new AtomDateTime(DateTime.Now, new TimeSpan(1, 0, 0)), new AtomContent("The syndication library from ItSoftware makes it easy to create and consume syndication content. This item is visible because the library does not have a proper licence.", AtomTextType.Text, null), null);
            }
        }
        #endregion

    }// class
}// namespace
