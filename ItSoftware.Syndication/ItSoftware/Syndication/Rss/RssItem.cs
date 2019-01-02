using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// All elements of an item are optional, however at least one 
	/// of title or description must be present.
	/// </summary>
	public sealed class RssItem : RssElementBase {

		#region Private Const Data
		private const int RSS_MAXLENGTH_TITLE = 100;
		private const int RSS_MAXLENGTH_LINK = 500;
		private const int RSS_MAXLENGTH_DESCRIPTION = 500;
		private const string RSS_ELEMENT_ITEM = "item";
		private const string RSS_ELEMENT_TITLE = "title";
		private const string RSS_ELEMENT_DESCRIPTION = "description";
		private const string RSS_ELEMENT_LINK = "link";
		private const string RSS_ELEMENT_AUTHOR = "author";
		private const string RSS_ELEMENT_CATEGORY = "category";
		private const string RSS_ELEMENT_COMMENTS = "comments";
		private const string RSS_ELEMENT_ENCLOSURE = "enclosure";
		private const string RSS_ELEMENT_GUID = "guid";
		private const string RSS_ELEMENT_PUBDATE = "pubDate";
		private const string RSS_ELEMENT_SOURCE = "source";
		#endregion		

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RssItem() {
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		public RssItem(string title, string description) {
			this.Title = title;
			this.Description = description;
            this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="link"></param>
		public RssItem(string title, string description, string link) : this(title,description) {			
			this.Link = link;            
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnItem"></param>
		internal RssItem(XmlNode xnItem) {
            this.Modules = new SyndicationModuleCollection();
			this.DeserializeFromXml(xnItem);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnItem"></param>
		private void DeserializeFromXml(XmlNode xnItem) {
			//
			// Required Elements
			//
			XmlNode xnTitle = xnItem.SelectSingleNode(RSS_ELEMENT_TITLE);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;
			}
			XmlNode xnDescription = xnItem.SelectSingleNode(RSS_ELEMENT_DESCRIPTION);
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;
			}			
			/*catch ( NullReferenceException ) {
				string msg = string.Format(RSS.RSS_ERRORMESSAGE_ELEMENT_INVALID,RSS_ELEMENT_ITEM,xnItem.OuterXml);
				throw new SyndicationFormatInvalidException(msg);
			}
			*/
			//
			// Optional Elements
			//
			XmlNode xnLink = xnItem.SelectSingleNode(RSS_ELEMENT_LINK);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}
			XmlNode xnAuthor = xnItem.SelectSingleNode(RSS_ELEMENT_AUTHOR);
			if ( xnAuthor != null ) {
				this.Author = xnAuthor.InnerText;
			}
			XmlNodeList xnlCategories = xnItem.SelectNodes(RSS_ELEMENT_CATEGORY);
			if ( xnlCategories != null ) {
				this.Category = new RssCategoryCollection(xnlCategories);
			}
			XmlNode xnComments = xnItem.SelectSingleNode(RSS_ELEMENT_COMMENTS);
			if ( xnComments != null ) {
				this.Comments = xnComments.InnerXml;
			}
			XmlNode xnEnclosure = xnItem.SelectSingleNode(RSS_ELEMENT_ENCLOSURE);
			if ( xnEnclosure != null ) {
				this.Enclosure = new RssEnclosure(xnEnclosure);
			}
			XmlNode xnGuid = xnItem.SelectSingleNode(RSS_ELEMENT_GUID);
			if ( xnGuid != null ) {
				this.GUID = new RssGuid(xnGuid);
			}
			XmlNode xnPubDate = xnItem.SelectSingleNode(RSS_ELEMENT_PUBDATE);
			if ( xnPubDate != null ) {
				this.PubDate = xnPubDate.InnerText;
			}
			XmlNode xnSource = xnItem.SelectSingleNode(RSS_ELEMENT_SOURCE);
			if ( xnSource != null ) {
				this.Source = new RssSource(xnSource);
			}

			//
			// Deserialize modules.
			//            
		    List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_TITLE,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_DESCRIPTION,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_LINK,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_AUTHOR,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_CATEGORY,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_COMMENTS,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_ENCLOSURE,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_GUID,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_PUBDATE,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_SOURCE,string.Empty) );

            this.Modules = new SyndicationModuleCollection(xnItem, exclusionElements);
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRss,RssVersion version) {
			if ( version == RssVersion.RSS_0_91 ) {
				return SerializeToXml_0_91(xdRss);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				return SerializeToXml_0_92(xdRss);
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				return SerializeToXml_2_0_1(xdRss);
			}
			throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object to XML according to RSS 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			XmlElement xeItem = xdRss.CreateElement(RSS_ELEMENT_ITEM);

			//
			// Required Elements.
			//
			if ( this.Title != null ) {
				xeItem.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeItem.AppendChild( SerializeLinkToXml(xdRss) );
			}
			//
			// Optional Elements
			//
			if ( this.Description != null ) {
				xeItem.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeItem);
			}

			return xeItem;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeItem = xdRss.CreateElement(RSS_ELEMENT_ITEM);
			//
			// Optional Elements.
			//
			if ( this.Title != null ) {
				xeItem.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeItem.AppendChild( SerializeLinkToXml(xdRss) );
			}			
			if ( this.Description != null ) {
				xeItem.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			if ( this.Source != null ) {
				xeItem.AppendChild( SerializeSourceToXml(xdRss,RssVersion.RSS_0_92) );
			}
			if ( this.Enclosure != null ) {
				xeItem.AppendChild( SerializeEnclosureToXml(xdRss,RssVersion.RSS_0_92) );
			}
			if ( this.Category != null ) {
				SerializeCategoryToXml(xdRss,xeItem,RssVersion.RSS_0_92);				
			}
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeItem);
			}
			return xeItem;
		}
		/// <summary>
		/// Serializes the object to XML according to RSS 2.0.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeItem = xdRss.CreateElement(RSS_ELEMENT_ITEM);
			//
			// Required Elements. One of them is required.
			//
			if ( this.Title != null ) {
				xeItem.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Description != null ) {
				xeItem.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			//
			// Optional Elements.
			//
			if ( this.Link != null ) {
				xeItem.AppendChild( SerializeLinkToXml(xdRss) );
			}
			if ( this.Author != null ) {
				xeItem.AppendChild( SerializeAuthorToXml(xdRss) );
			}
			if ( this.Category != null ) {
				SerializeCategoryToXml(xdRss,xeItem,RssVersion.RSS_2_0_1);				
			}
			if ( this.Comments != null ) {
				xeItem.AppendChild( SerializeCommentsToXml(xdRss) );
			}
			if ( this.Enclosure != null ) {
				xeItem.AppendChild( SerializeEnclosureToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			if ( this.GUID != null ) {
				xeItem.AppendChild( SerializeGuidToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			if ( this.PubDate != null ) {
				xeItem.AppendChild( SerializePubDateToXml(xdRss) );
			}
			if ( this.Source != null ) {
				xeItem.AppendChild( SerializeSourceToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeItem);
			}
			return xeItem;
		}
		/// <summary>
		/// Serializes source to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeSourceToXml(XmlDocument xdRss,RssVersion version) {
			return this.Source.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes PubDate to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializePubDateToXml(XmlDocument xdRss) {
			XmlElement xePubDate = xdRss.CreateElement(RSS_ELEMENT_PUBDATE);
			xePubDate.InnerText = this.PubDate;
			return xePubDate;
		}
		/// <summary>
		/// Serializes Guid to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeGuidToXml(XmlDocument xdRss,RssVersion version) {
			return this.GUID.SerializeToXml(xdRss,version);
		}	
		/// <summary>
		/// Serializes Enclosure to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeEnclosureToXml(XmlDocument xdRss,RssVersion version) {
			return this.Enclosure.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes Comments to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeCommentsToXml(XmlDocument xdRss) {
			XmlElement xeComments = xdRss.CreateElement(RSS_ELEMENT_COMMENTS);
			xeComments.InnerText = this.Comments;
			return xeComments;
		}
		/// <summary>
		/// Serializes category to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="xeItem"></param>
		private void SerializeCategoryToXml(XmlDocument xdRss,XmlElement xeItem,RssVersion version) {
			this.Category.SerializeToXml(xdRss,version,xeItem);			
		}
		/// <summary>
		/// Serializes Author to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeAuthorToXml(XmlDocument xdRss) {
			XmlElement xeAuthor = xdRss.CreateElement(RSS_ELEMENT_AUTHOR);
			xeAuthor.InnerText = this.Author;
			return xeAuthor;
		}
		/// <summary>
		/// Serializes Link to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeLinkToXml(XmlDocument xdRss) {
			XmlElement xeLink = xdRss.CreateElement(RSS_ELEMENT_LINK);
			xeLink.InnerText = this.Link;
			return xeLink;
		}
		/// <summary>
		/// Serializes Description to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDescriptionToXml(XmlDocument xdRss) {
			XmlElement xeDescription = xdRss.CreateElement(RSS_ELEMENT_DESCRIPTION);
			xeDescription.InnerText = this.Description;
			return xeDescription;
		}
		/// <summary>
		/// Serializes Title to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeTitleToXml(XmlDocument xdRss) {
			XmlElement xeTitle = xdRss.CreateElement(RSS_ELEMENT_TITLE);
			xeTitle.InnerText = this.Title;
			return xeTitle;
		}		
		#endregion

		#region Validation Methods
		/*/// <summary>
		/// Validates the item object.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RssVersion version, bool validateContent) {
			if ( version == RssVersion.RSS_0_91 ) {
				return Validate_0_91(validateContent);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				return Validate_0_92(validateContent);
			}
			else if  ( version == RssVersion.RSS_2_0_1 ) {
				return Validate_2_0_1(validateContent);
			}
			throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE,"version");		
		}*/	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			//
			// Required Elements
			//
			if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TITLE, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);				
			}
			if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_LINK, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);				
			}

			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_0_91);
				ValidateLink(RssVersion.RSS_0_91);
				ValidateDescription(RssVersion.RSS_0_91);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			//
			// No Required Elements.
			//

			//
			// Validate objects.
			//
			if ( this.Source != null ) {
				this.Source.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.Enclosure != null ) {
				this.Enclosure.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.Category != null ) {
				this.Category.Validate(RssVersion.RSS_0_92,validateContent);
			}

			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_0_92);
				ValidateLink(RssVersion.RSS_0_92);
				ValidateDescription(RssVersion.RSS_0_92);								
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_2_0_1(bool validateContent)
        {
			//
			// Validate required elements
			//
			if ( this.Title == null && this.Description == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_ITEM_TITLE_DESCRIPTION_REQUIRED_FIELD_NULL);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate objects.
			//
			if ( this.Source != null ) {
				this.Source.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Enclosure != null ) {
				this.Enclosure.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Category != null ) {
				this.Category.Validate(RssVersion.RSS_2_0_1,validateContent);
			}	
			if ( this.GUID != null ) {
				this.GUID.Validate(RssVersion.RSS_2_0_1,validateContent);
			}

			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_2_0_1);
				ValidateLink(RssVersion.RSS_2_0_1);
				ValidateDescription(RssVersion.RSS_2_0_1);								
				ValidateAuthor(RssVersion.RSS_2_0_1);
				ValidateComments(RssVersion.RSS_2_0_1);
				ValidatePubDate(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// Validates PubDate
		/// </summary>
		/// <param name="version"></param>
		private void ValidatePubDate(RssVersion version) {
			if ( this.PubDate == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 || 
				version == RssVersion.RSS_0_92 ||
				version == RssVersion.RSS_2_0_1 ) {
				try {
					RssDateTime rdt = new RssDateTime(this.PubDate);
				}
				catch ( Exception x ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ITEM, RSS_ELEMENT_PUBDATE);
					throw new SyndicationValidationException(msg,x);
				}
			}
		}
		/// <summary>
		/// Validates Comments element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateComments(RssVersion version) {
			if ( this.Comments == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// Validates Author element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateAuthor(RssVersion version) {
			if ( this.Author == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateTitle(RssVersion version) {
			if ( this.Title == null ) {
				return;
			}

			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Title.Length > RSS_MAXLENGTH_TITLE ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_TITLE,RSS_MAXLENGTH_TITLE.ToString(),this.Title.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLink(RssVersion version) {
			if ( this.Link == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Link.Length > RSS_MAXLENGTH_LINK ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH, RSS_ELEMENT_LINK, RSS_MAXLENGTH_LINK.ToString(), this.Link.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDescription(RssVersion version) {
			if ( this.Description == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Description.Length > RSS_MAXLENGTH_DESCRIPTION ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_DESCRIPTION,RSS_MAXLENGTH_DESCRIPTION.ToString(),this.Description.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Title backing field.
		/// </summary>
		private string m_title;
		/// <summary>
		/// The title of the item.
		/// </summary>
		public string Title {
			get {
				return m_title;
			}
			set {				
				m_title = value;
			}
		}
		/// <summary>
		/// Description backing field.
		/// </summary>
		private string m_description;
		/// <summary>
		/// The item synopsis.
		/// </summary>
		public string Description {
			get {
				return m_description;
			}
			set {				
				m_description = value;
			}
		}
		#endregion

		#region Optional Elements
		/// <summary>
		/// Link backing field.
		/// </summary>
		private string m_link;
		/// <summary>
		/// The URL of the item.
		/// </summary>
		public string Link {
			get {
				return m_link;
			}
			set {
				m_link = value;
			}
		}
		/// <summary>
		/// Author backing field.
		/// </summary>
		private string m_author;
		/// <summary>
		/// It's the email address of the author of the item. For newspapers and 
		/// magazines syndicating via RSS, the author is the person who wrote the 
		/// article that the <item> describes. 
		/// </summary>
		public string Author {
			get {
				return m_author;
			}
			set {
				m_author = value;
			}
		}
		/// <summary>
		/// Category backing field.
		/// </summary>
		private RssCategoryCollection m_category;
		/// <summary>
		/// Includes the item in one or more categories.
		/// </summary>
		public RssCategoryCollection Category {
			get {
				return m_category;
			}
			set {
				m_category = value;
			}
		}
		/// <summary>
		/// Comments backing field.
		/// </summary>
		private string m_comments;
		/// <summary>
		/// URL of a page for comments relating to the item.
		/// </summary>
		public string Comments {
			get {
				return m_comments;
			}
			set {
				m_comments = value;
			}
		}
		/// <summary>
		/// Enclosure backing field.
		/// </summary>
		private RssEnclosure m_enclosure;
		/// <summary>
		/// Describes a media object that is attached to the item.
		/// </summary>
		public RssEnclosure Enclosure {
			get {
				return m_enclosure;
			}
			set {
				m_enclosure = value;
			}
		}
		/// <summary>
		/// GUID backing field.
		/// </summary>
		private RssGuid m_guid;
		/// <summary>
		/// A string that uniquely identifies the item. 
		/// </summary>
		public RssGuid GUID {
			get {
				return m_guid;
			}
			set {
				m_guid = value;
			}
		}
		/// <summary>
		/// PubDate backing field.
		/// </summary>
		private string m_pubDate;
		/// <summary>
		/// Indicates when the item was published.
		/// </summary>
		public string PubDate {
			get {
				return m_pubDate;
			}
			set {
				m_pubDate = value;
			}
		}
		/// <summary>
		/// Source backing field.
		/// </summary>
		private RssSource m_source;
		/// <summary>
		/// The RSS channel that the item came from.
		/// </summary>
		public RssSource Source {
			get {
				return m_source;
			}
			set {
				m_source = value;
			}
		}
		/// <summary>
		/// Modules backing field.
		/// </summary>
		private SyndicationModuleCollection m_modules;
		/// <summary>
		/// Gets or sets the syndication modules.
		/// </summary>
		public SyndicationModuleCollection Modules {
			get {
				return m_modules;
			}
			set {
				m_modules = value;
			}
		}
		#endregion

        #region Internal Static Properties
        /// <summary>
        /// Creates an RssItem 
        /// </summary>
        internal static RssItem InvalidLicence
        {
            get
            {
                return new RssItem("ItSoftware.Syndication", "The syndication library from ItSoftware makes it easy to create and consume syndication content. This item is visible because the library does not have a proper licence.", "http://www.itsoftware.no/syndication/default.aspx");
            }
        }        
        #endregion

    }// class
}// namespace
