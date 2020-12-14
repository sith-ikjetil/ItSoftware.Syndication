using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Specify one or more categories that the channel belongs to.
	/// Follows the same rules as the <item>-level category element.
	/// </summary>
    public sealed class RssCategory : RssElementBase {
		#region Private Const Data
		private const string RSS_ELEMENT_CATEGORY = "category";
		private const string RSS_ELEMENT_DOMAIN = "domain";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="category"></param>
		public RssCategory(string category) {
			this.Category = category;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="category"></param>
		/// <param name="domain"></param>
		public RssCategory(string category,string domain) {			
			this.Category = category;
			this.Domain = domain;
		}
		/// <summary>
		/// Internal deseralization constructor.
		/// </summary>
		/// <param name="xnCategory"></param>
		internal RssCategory(XmlNode xnCategory) {
			this.DeserializeFromXml(xnCategory);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from xml.
		/// </summary>
		/// <param name="xnCategory"></param>
		private void DeserializeFromXml(XmlNode xnCategory) {
			//
			// Required Elements
			//
			this.Category = xnCategory.InnerText;			

			//
			// Optional Elements
			//
			XmlAttribute xaDomain = xnCategory.Attributes[RSS_ELEMENT_DOMAIN];
			if ( xaDomain != null ) {
				this.Domain = xaDomain.InnerText;
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="elementName"></param>
		/// <param name="rssVersion"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRss, RssVersion version) {
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
		///  Serializes object according to RSS version 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_CATEGORY);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		///  Serializes object according to RSS version 0.92.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeCategory = xdRss.CreateElement(RSS_ELEMENT_CATEGORY);
			if ( this.Category != null ) {
				xeCategory.InnerText = this.Category;
			}

			if ( this.Domain != null ) {
				XmlAttribute xaDomain = xdRss.CreateAttribute(RSS_ELEMENT_DOMAIN);
				xaDomain.InnerText = this.Domain;	
				xeCategory.Attributes.Append(xaDomain);
			}
			
			return xeCategory;
		}
		/// <summary>
		/// Serializes object according to RSS version 2.0.1.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeCategory = xdRss.CreateElement(RSS_ELEMENT_CATEGORY);
			if ( this.Category != null ) {
				xeCategory.InnerText = this.Category;
			}

			if ( this.Domain != null ) {
				XmlAttribute xaDomain = xdRss.CreateAttribute(RSS_ELEMENT_DOMAIN);
				xaDomain.InnerText = this.Domain;	
				xeCategory.Attributes.Append(xaDomain);
			}
			
			return xeCategory;
		}
		#endregion

		#region Validation Methods
		/*/// <summary>
		/// Validates against given RSS spesification.
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
            throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE, "version");		
		} 
         * */
		/// <summary>
		/// Validates against version 0.91 of the RSS spesification.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		protected override void Validate_0_91(bool validateContent) {
            string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED, RSS_ELEMENT_CATEGORY);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// Validates against version 0.92 of the RSS spesification.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		protected override void Validate_0_92(bool validateContent) {
			//
			// Required Elements.
			//
			if ( this.Category == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CATEGORY, RSS_ELEMENT_CATEGORY);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate content
			//
			if ( validateContent ) {
				ValidateCategory(RssVersion.RSS_0_91);
				ValidateDomain(RssVersion.RSS_0_91);
			}			
		}
		/// <summary>
		/// Validates against version 2.0 of the RSS spesification.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		protected override void Validate_2_0_1(bool validateContent) {
			//
			// Required Elements.
			//
			if ( this.Category == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_CATEGORY,RSS_ELEMENT_CATEGORY);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate content
			//
			if ( validateContent ) {
				ValidateCategory(RssVersion.RSS_0_91);
				ValidateDomain(RssVersion.RSS_0_91);
			}			
		}
		/// <summary>
		/// Validates category.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateCategory(RssVersion version) {
			if ( this.Category == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// Validates domain.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDomain(RssVersion version) {
			if ( this.Domain == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Category backing field.
		/// </summary>
		private string m_category;
		/// <summary>
		/// Includes the item in one or more categories.
		/// </summary>
		public string Category {
			get {
				return m_category;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_CATEGORY,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_category = value;
			}
		}		
		#endregion

		#region Optional Elements
		/// <summary>
		/// Domain backing field.
		/// </summary>
		private string m_domain;
		/// <summary>
		/// The value of the element is a forward-slash-separated string that 
		/// identifies a hierarchic location in the indicated taxonomy. Processors 
		/// may establish conventions for the interpretation of categories.
		/// </summary>
		public string Domain {
			get {
				return m_domain;
			}
			set {		
				m_domain = value;
			}
		}
		#endregion
	}// class
}// namespace
