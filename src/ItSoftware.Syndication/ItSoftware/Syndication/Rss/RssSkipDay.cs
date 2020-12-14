using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// A hint for aggregators telling them which days they can skip.
	/// </summary>
	public sealed class RssSkipDay : RssElementBase {	

		#region Private Const Data
		private const string RSS_ELEMENT_DAY = "day";
		private const string RSS_ELEMENT_SKIPDAYS = "skipDays";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="day"></param>
		public RssSkipDay(string day) {
			this.Day = day;							
		}		
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnSkipDay"></param>
		internal RssSkipDay(XmlNode xnSkipDay) {
			this.DeserializeFromXml(xnSkipDay);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnSkipDay"></param>
		private void DeserializeFromXml(XmlNode xnSkipDay) {
			this.Day = xnSkipDay.InnerText;
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
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			XmlElement xeSkipDay = xdRss.CreateElement(RSS_ELEMENT_DAY);
			if ( this.Day != null ) {
				xeSkipDay.InnerText = this.Day;
			}
			return xeSkipDay;
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeSkipDay = xdRss.CreateElement(RSS_ELEMENT_DAY);
			if ( this.Day != null ) {
				xeSkipDay.InnerText = this.Day;
			}
			return xeSkipDay;
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeSkipDay = xdRss.CreateElement(RSS_ELEMENT_DAY);
			if ( this.Day != null ) {
				xeSkipDay.InnerText = this.Day;
			}
			return xeSkipDay;
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
			if ( this.Day  == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPDAYS, RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);				
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateDay(RssVersion.RSS_0_91);
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
			if ( this.Day == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_SKIPDAYS,RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);						
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateDay(RssVersion.RSS_0_92);
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
			if ( this.Day == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPDAYS, RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);						
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateDay(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDay(RssVersion version) {
			if ( this.Day == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				string day = this.Day.Trim().ToLower();
                foreach (string validDay in Rss.RSS_VALIDVALUES_SKIPDAYS_DAYS_0_91)
                {
					if ( day == validDay ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPDAYS, RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);						
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				string day = this.Day.Trim().ToLower();
                foreach (string validDay in Rss.RSS_VALIDVALUES_SKIPDAYS_DAYS_0_92)
                {
					if ( day == validDay ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPDAYS, RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);						
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string day = this.Day.Trim().ToLower();
                foreach (string validDay in Rss.RSS_VALIDVALUES_SKIPDAYS_DAYS_2_0_1)
                {
					if ( day == validDay ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_SKIPDAYS, RSS_ELEMENT_DAY);
				throw new SyndicationValidationException(msg);						
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Day backing field.
		/// </summary>
		private string m_day;
		/// <summary>
		/// Elements whose value is Monday, Tuesday, Wednesday, Thursday, 
		/// Friday, Saturday or Sunday. Aggregators may not read the channel 
		/// during days listed in the skipDays element.
		/// </summary>
		public string Day {
			get {
				return m_day;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_DAY,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_day = value;
			}
		}
		#endregion		

	}// class
}// namespace
