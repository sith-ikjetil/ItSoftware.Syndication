using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Describes a media object that is attached to the item.
	/// </summary>
	public sealed class RssEnclosure : RssElementBase {

		#region Private Const Data
		private const string RSS_ELEMENT_ENCLOSURE = "enclosure";
		private const string RSS_ELEMENT_URL = "url";
		private const string RSS_ELEMENT_LENGTH = "length";
		private const string RSS_ELEMENT_TYPE = "type";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="length"></param>
		/// <param name="type"></param>
		public RssEnclosure(string url, string length, string type) {
			this.URL = url;
			this.Length = length;
			this.Type = type;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnEnclosure"></param>
		internal RssEnclosure(XmlNode xnEnclosure) {
			this.DeserializeFromXml(xnEnclosure);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from xml.
		/// </summary>
		/// <param name="xnEnclosure"></param>
		private void DeserializeFromXml(XmlNode xnEnclosure) {
			XmlNode xnUrl = xnEnclosure.Attributes[RSS_ELEMENT_URL];
			if ( xnUrl != null ) {
				this.URL = xnUrl.InnerText;
			}
			XmlNode xnLength = xnEnclosure.Attributes[RSS_ELEMENT_LENGTH];
			if ( xnLength != null ) {
				this.Length = xnLength.InnerText;
			}
			XmlNode xnType = xnEnclosure.Attributes[RSS_ELEMENT_TYPE];
			if ( xnType != null ) {
				this.Type = xnEnclosure.Attributes[RSS_ELEMENT_TYPE].InnerText;
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
		/// Serializes object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_ENCLOSURE);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeEnclosure = xdRss.CreateElement(RSS_ELEMENT_ENCLOSURE);
			
			//
			// Required Elements.
			//
			if ( this.URL != null ) {
				xeEnclosure.Attributes.Append( SerializeUrlToXml(xdRss) );
			}
			if ( this.Length != null ) {
				xeEnclosure.Attributes.Append( SerializeLengthToXml(xdRss) );
			}			
			if ( this.Type != null ) {
				xeEnclosure.Attributes.Append( SerializeTypeToXml(xdRss) );
			}			

			return xeEnclosure;
		}
		/// <summary>
		/// Serializes object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeEnclosure = xdRss.CreateElement(RSS_ELEMENT_ENCLOSURE);
			
			//
			// Required Elements.
			//
			if ( this.URL != null ) {
				xeEnclosure.Attributes.Append( SerializeUrlToXml(xdRss) );
			}
			if ( this.Length != null ) {
				xeEnclosure.Attributes.Append( SerializeLengthToXml(xdRss) );
			}			
			if ( this.Type != null ) {
				xeEnclosure.Attributes.Append( SerializeTypeToXml(xdRss) );
			}			

			return xeEnclosure;
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
		/// <summary>
		/// Serializes Length to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeLengthToXml(XmlDocument xdRss) {
			XmlAttribute xaLength = xdRss.CreateAttribute(RSS_ELEMENT_LENGTH);
			xaLength.InnerText = this.Length;
			return xaLength;
		}
		/// <summary>
		/// Serializes Type to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		private XmlAttribute SerializeTypeToXml(XmlDocument xdRss) {
			XmlAttribute xaType = xdRss.CreateAttribute(RSS_ELEMENT_TYPE);
			xaType.InnerText = this.Type;
			return xaType;
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
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED,RSS_ELEMENT_ENCLOSURE);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			//
			// Required Elements.
			//			
			if ( this.URL == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);	
			}
			if ( this.Type == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_TYPE);
				throw new SyndicationValidationException(msg);					
			}
			if ( this.Length == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_LENGTH);
				throw new SyndicationValidationException(msg);	
			}

			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_0_92);
				ValidateType(RssVersion.RSS_0_92);
				ValidateLength(RssVersion.RSS_0_92);								
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
			// Required Elements.
			//			
			if ( this.URL == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);	
			}
			if ( this.Type == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_TYPE);
				throw new SyndicationValidationException(msg);					
			}
			if ( this.Length == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_LENGTH);
				throw new SyndicationValidationException(msg);	
			}

			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_2_0_1);
				ValidateType(RssVersion.RSS_2_0_1);
				ValidateLength(RssVersion.RSS_2_0_1);								
			}			
		}
		/// <summary>
		/// Validate Url.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateUrl(RssVersion version) {
			if ( this.URL == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_92 ) {
				string url = this.URL.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92)
                {
					if ( url.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92, RSS_ELEMENT_URL, this.URL);
				throw new SyndicationValidationException(msg);
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string url = this.URL.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1)
                {
					if ( url.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1, RSS_ELEMENT_URL, this.URL);
				throw new SyndicationValidationException(msg);
			}
		}
		/// <summary>
		/// Validate type.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateType(RssVersion version) {
			if ( this.Type == null ) {
				return;
			}
			//
			// All types are valid.
			//
		}
		/// <summary>
		/// Validate length.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLength(RssVersion version) {
			if ( this.Length == null ) {
				return;
			}
			//
			// All version validate equal.
			//
			try {
				long length = Convert.ToInt64(this.Length);
			}
			catch ( FormatException ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_ENCLOSURE, RSS_ELEMENT_LENGTH);
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
		/// URL to resource. Must be http://.
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
		/// <summary>
		/// Length backing field.
		/// </summary>
		private string m_length;
		/// <summary>
		/// Length in bytes.
		/// </summary>
		public string Length {
			get {
				return m_length;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_LENGTH,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_length = value;
			}
		}
		/// <summary>
		/// Type backing field.
		/// </summary>
		private string m_type;
		/// <summary>
		/// MIME type.
		/// </summary>
		public string Type {
			get {
				return m_type;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_TYPE, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_type = value;
			}
		}
		#endregion

	}// class
}// namespace
