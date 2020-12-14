using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Guid stands for globally unique identifier. 
	/// It's a string that uniquely identifies the item. When present,
	/// an aggregator may choose to use this string to determine if an 
	/// item is new.
	/// </summary>
	public sealed class RssGuid : RssElementBase {

		#region Private Const Data
		private const string RSS_ELEMENT_GUID = "guid";
		private const string RSS_ELEMENT_ISPERMALINK = "isPermaLink";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="guid"></param>
		public RssGuid(string guid) {
			this.GUID = guid;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="guid"></param>
		/// <param name="isPermaLink"></param>
		public RssGuid(string guid,string isPermaLink) {
			this.GUID = guid;
			this.IsPermaLink = isPermaLink;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnGuid"></param>
		internal RssGuid(XmlNode xnGuid) {
			this.DeserializeFromXml(xnGuid);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from xml.
		/// </summary>
		/// <param name="xnGuid"></param>
		private void DeserializeFromXml(XmlNode xnGuid) {
			//
			// Required Element.
			//
			this.GUID = xnGuid.InnerText;
			//
			// Optional Element.
			//
			XmlAttribute xaIsPermaLink = xnGuid.Attributes[RSS_ELEMENT_ISPERMALINK];
			if ( xaIsPermaLink != null ) {
				this.IsPermaLink = xaIsPermaLink.InnerText;
			}
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
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_GUID);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_GUID);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeGuid = xdRss.CreateElement(RSS_ELEMENT_GUID);
			if ( this.GUID != null ) {
				xeGuid.InnerText = this.GUID;
			}

			if ( this.IsPermaLink != null ) {
				xeGuid.Attributes.Append( SerializeIsPermaLinkToXml(xdRss) );				
			}

			return xeGuid;
		}
		/// <summary>
		/// Serialize IsPermaLink to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeIsPermaLinkToXml(XmlDocument xdRss) {
			XmlAttribute xaIsPermaLink = xdRss.CreateAttribute(RSS_ELEMENT_ISPERMALINK);
			xaIsPermaLink.InnerText = this.IsPermaLink;
			return xaIsPermaLink;
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
		}
         * */
		/// <summary>
		/// Validates the object.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED,RSS_ELEMENT_GUID);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// Validates the object.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED,RSS_ELEMENT_GUID);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// Validates the object.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_2_0_1(bool validateContent)
        {
			//
			// Required Elements.
			//						
			if ( this.GUID == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_GUID,RSS_ELEMENT_GUID);
				throw new SyndicationValidationException(msg);	
			}
			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateGuid(RssVersion.RSS_2_0_1);
				ValidateIsPermaLink(RssVersion.RSS_2_0_1);				
			}
		}
		/// <summary>
		/// Validate Url.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateGuid(RssVersion version) {
			if ( this.GUID == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// Validate type.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateIsPermaLink(RssVersion version) {
			if ( this.IsPermaLink == null ) {
				return;
			}
			try {
				bool isPermaLink = Convert.ToBoolean(this.IsPermaLink);
			}
			catch ( FormatException ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_GUID,RSS_ELEMENT_ISPERMALINK);
				throw new SyndicationValidationException(msg);	
			}
		}		
		#endregion

		#region Required Elements
		/// <summary>
		/// Guid backing field.
		/// </summary>
		private string m_guid;
		/// <summary>
		/// A string that uniquely identifies the item.
		/// </summary>
		public string GUID {
			get {
				return m_guid;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_GUID,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_guid = value;
			}
		}
		#endregion

		#region Optional Elements
		/// <summary>
		/// IsPermaLink backing field.
		/// </summary>
		private string m_isPermaLink;
		/// <summary>
		/// If the guid element has an attribute named "isPermaLink" with 
		/// a value of true, the reader may assume that it is a permalink 
		/// to the item, that is, a url that can be opened in a Web browser, 
		/// that points to the full item described by the <item> element.
		/// </summary>
		public string IsPermaLink {
			get {
				return m_isPermaLink;
			}
			set {
				m_isPermaLink = value;
			}
		}
		#endregion

	}// class
}// namespace
