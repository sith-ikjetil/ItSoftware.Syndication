using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// The purpose of this element is to propagate credit for links,
	/// to publicize the sources of news items. It can be used in the 
	/// Post command of an aggregator. It should be generated automatically 
	/// when forwarding an item from an aggregator to a weblog authoring tool.
	/// </summary>
	public sealed class RssSource : RssElementBase {

		#region Private Const Data
		private const string RSS_ELEMENT_URL = "url";
		private const string RSS_ELEMENT_SOURCE = "source";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructors.
		/// </summary>
		/// <param name="url"></param>
		public RssSource(string url) {			
			this.URL = url;
		}
		/// <summary>
		/// Public constructors.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="source"></param>
		public RssSource(string url,string source) {			
			this.URL = url;
			this.Source = source;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnSource"></param>
		internal RssSource(XmlNode xnSource) {
			this.DeserializeFromXml(xnSource);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnSource"></param>
		private void DeserializeFromXml(XmlNode xnSource) {
			//
			// Required Elements.
			//
			XmlNode xnUrl = xnSource.Attributes[RSS_ELEMENT_URL];
			if ( xnUrl != null ) {
				this.URL = xnUrl.InnerText;
			}			
			//
			// Optional Elements
			//
			this.Source = xnSource.InnerText;
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
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
		/// Serializes the object according to RSS version 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_SOURCE);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object according to RSS version 0.92.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeSource = xdRss.CreateElement(RSS_ELEMENT_SOURCE);			
			//
			// Required Elements
			//
			if ( this.URL != null ) {
				xeSource.Attributes.Append( SerializeUrlToXml(xdRss) );
			}
			//
			// Optional Elements
			//
			if ( this.Source != null ) {
				xeSource.InnerText = this.Source;
			}
			return xeSource;
		}
		/// <summary>
		/// Serializes the object according to RSS version 2.0.1.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeSource = xdRss.CreateElement(RSS_ELEMENT_SOURCE);			
			//
			// Required Elements
			//
			if ( this.URL != null ) {
				xeSource.Attributes.Append( SerializeUrlToXml(xdRss) );
			}
			//
			// Optional Elements
			//
			if ( this.Source != null ) {
				xeSource.InnerText = this.Source;
			}
			return xeSource;
		}
		/// <summary>
		/// Serializes URL to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeUrlToXml(XmlDocument xdRss) {
			XmlAttribute xaUrl = xdRss.CreateAttribute(RSS_ELEMENT_URL);
			xaUrl.InnerText = this.URL;
			return xaUrl;
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
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED,RSS_ELEMENT_SOURCE);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			if ( this.URL == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_SOURCE,RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);	
			}
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_0_92);
				ValidateSource(RssVersion.RSS_0_92);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_2_0_1(bool validateContent)
        {
			if ( this.URL == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_SOURCE,RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);	
			}
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_2_0_1);
				ValidateSource(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateSource(RssVersion version) {
			if ( this.Source == null ) {
				return;
			}
			//
			// All Values Ok.
			//
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateUrl(RssVersion version) {
			if ( this.URL == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_92 )  {
				string url = this.URL.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92 ) {
					if ( url.StartsWith( validPrefix ) ) {
						return;
					}
				}
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92,RSS_ELEMENT_URL,this.URL);
				throw new SyndicationValidationException(msg);				
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string url = this.URL.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1 ) {
					if ( url.StartsWith( validPrefix ) ) {
						return;
					}
				}
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1,RSS_ELEMENT_URL,this.URL);
				throw new SyndicationValidationException(msg);				
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// URL backing field.
		/// </summary>
		private string m_url;
		/// <summary>
		/// Links to the XMLization of the source.
		/// </summary>
		public string URL {
			get {
				return m_url;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_URL,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_url = value;
			}
		}
		#endregion

		#region Optional Elements
		/// <summary>
		/// Source backing field.
		/// </summary>
		private string m_source;
		/// <summary>
		/// Its value is the name of the RSS channel that the item came 
		/// from, derived from its <title>. 
		/// </summary>
		public string Source {
			get {
				return m_source;
			}
			set {
				m_source = value;
			}
		}
		#endregion

	}// class
}// namespace
