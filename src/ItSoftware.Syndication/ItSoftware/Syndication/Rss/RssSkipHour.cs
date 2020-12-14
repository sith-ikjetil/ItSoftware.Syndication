using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Elements whose value is a number between 0 and 23, representing 
	/// a time in GMT, when aggregators, if they support the feature, 
	/// may not read the channel on hours listed in the skipHours element.
	/// </summary>
	public sealed class RssSkipHour : RssElementBase {	

		#region Private Const Data
		private const string RSS_ELEMENT_HOUR = "hour";
		private const string RSS_ELEMENT_SKIPHOURS = "skipHours";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="hour"></param>
		public RssSkipHour(string hour) {
			this.Hour = hour;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnSkipHour"></param>
		internal RssSkipHour(XmlNode xnSkipHour) {
			this.DeserializeFromXml(xnSkipHour);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnSkipHour"></param>
		private void DeserializeFromXml(XmlNode xnSkipHour) {
			this.Hour = xnSkipHour.InnerText;
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
		/// Serializes the object to XML according to RSS version 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			XmlElement xeSkipHour = xdRss.CreateElement(RSS_ELEMENT_HOUR);
			if ( this.Hour != null ) {
				xeSkipHour.InnerText = this.Hour;
			}
			return xeSkipHour;
		}
		/// <summary>
		/// Serializes the object to XML according to RSS version 0.92.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeSkipHour = xdRss.CreateElement(RSS_ELEMENT_HOUR);
			if ( this.Hour != null ) {
				xeSkipHour.InnerText = this.Hour;
			}
			return xeSkipHour;
		}
		/// <summary>
		/// Serializes the object to XML according to RSS version 2.0.1.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeSkipHour = xdRss.CreateElement(RSS_ELEMENT_HOUR);
			if ( this.Hour != null ) {
				xeSkipHour.InnerText = this.Hour;
			}
			return xeSkipHour;
		}
		#endregion

		#region Validation Methods
		/*/// <summary>
		/// 
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
		}*/
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			//
			// Validate required elements.
			//
			if ( this.Hour == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_HOUR, RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateHour(RssVersion.RSS_0_91);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			//
			// Validate required elements.
			//
			if ( this.Hour == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_HOUR, RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateHour(RssVersion.RSS_0_92);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_2_0_1(bool validateContent)
        {
			//
			// Validate required elements.
			//
			if ( this.Hour == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_HOUR, RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateHour(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateHour(RssVersion version) {
			if ( this.Hour == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
                foreach (string validHour in Rss.RSS_VALIDVALUES_SKIPHOURS_HOUR_0_91)
                {
					if ( this.Hour == validHour ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPHOURS, RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}
			else if ( version == RssVersion.RSS_0_92 ) {
                foreach (string validHour in Rss.RSS_VALIDVALUES_SKIPHOURS_HOUR_0_91)
                {
					if ( this.Hour == validHour ) {
						return;
					}
				}
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_SKIPHOURS,RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
                foreach (string validHour in Rss.RSS_VALIDVALUES_SKIPHOURS_HOUR_2_0_1)
                {
					if ( this.Hour == validHour ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPHOURS, RSS_ELEMENT_HOUR);
				throw new SyndicationValidationException(msg);				
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Hour backing field.
		/// </summary>
		private string m_hour;
		/// <summary>
		/// Elements whose value is a number between 0 and 23, representing 
		/// a time in GMT, when aggregators, if they support the feature, 
		/// may not read the channel on hours listed in the skipHours element.
		/// </summary>
		public string Hour {
			get {
				return m_hour;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_HOUR, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_hour = value;
			}
		}
		#endregion
		
	}// class
}// namespace
