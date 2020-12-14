using System;
using System.Xml;
using System.Text;
using ItSoftware.Syndication;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Represents an RSS feed.
	/// </summary>
	public sealed class Rss : SyndicationBase {		

		#region Private Const Data
		private const string RSS_ROOT_ELEMENT = "/rss";
		private const string RSS_ELEMENT_RSS = "rss";
		private const string RSS_VERSION_0_91 = "0.91";
		private const string RSS_VERSION_0_92 = "0.92";
		private const string RSS_VERSION_2_0_1 = "2.0";
		private const string RSS_ATTRIBUTE_VERSION = "version";
		private const string RSS_ELEMENT_CHANNEL = "channel";
		private const string RSS_XML_VERSION = "1.0";
		#endregion

        #region RSS Internal Static ReadOnly Variables
        //
        // Valid Values
        //
        internal static readonly string[] RSS_VALIDVALUES_LINK_URL_0_91 = { "http://", "ftp://" };
        internal static readonly string[] RSS_VALIDVALUES_LINK_URL_0_92 = { "http://", "ftp://" };
        internal static readonly string[] RSS_VALIDVALUES_LINK_URL_2_0_1 = { "http://", "https://", "news://", "mailto:", "ftp://" };
        internal static readonly string[] RSS_VALIDVALUES_CLOUD_PROTOCOL_0_92 = { "xml-rpc", "soap", "http-post" };
        internal static readonly string[] RSS_VALIDVALUES_CLOUD_PROTOCOL_2_0_1 = { "xml-rpc", "soap", "http-post" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPDAYS_DAYS_0_91 = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPDAYS_DAYS_0_92 = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPDAYS_DAYS_2_0_1 = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPHOURS_HOUR_0_91 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPHOURS_HOUR_0_92 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
        internal static readonly string[] RSS_VALIDVALUES_SKIPHOURS_HOUR_2_0_1 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
        internal static readonly string[] RSS_VALIDVALUES_IMAGE_URL_0_91 = { ".png", ".gif", ".jpeg", ".jpg" };
        internal static readonly string[] RSS_VALIDVALUES_IMAGE_URL_0_92 = { ".png", ".gif", ".jpeg", ".jpg" };
        internal static readonly string[] RSS_VALIDVALUES_IMAGE_URL_2_0_1 = { ".png", ".gif", ".jpeg", ".jpg" };

        //
        // Max Values
        //
        internal static readonly int RSS_MAXVALUE_ITEM_MAX_NUMBER_OF_0_91 = 15;

        //
        // Validation Error Messages
        //
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH = "{0} element can have a maxium length of {1} characters. Length was: {2} characters.";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91 = "{0} element has invalid value.\r\nShould start with 'http://' or 'ftp://'.\r\nValue was: '{1}'";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92 = "{0} element has invalid value.\r\nShould start with 'http://' or 'ftp://'.\r\nValue was: '{1}'";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1 = "{0} element has invalid value.\r\nShould start with 'http://', 'https://', 'news://', 'mailto:' or 'ftp://'.\r\nValue was: '{1}'";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID_NUMBER_VALUE = "{0} element has invalid number value. Value was: '{1}'";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED = "Cannot validate {0} element because it is not supported by given RSS version.";
        internal static readonly string RSS_ERRORMESSAGE_VALIDATION_FAILED = "Validation failed. '{0}' element had invalid '{1}' element.";
        internal static readonly string RSS_ERRORMESSAGE_CANNOT_VALIDATE_UNRECOGNIZED_VERSION = "Cannot validate unrecognized RSS version.";

        //
        // Serialization Error Messages
        //
        internal static readonly string RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION = "{0} element is not part of specified RSS version.";

        //
        // Misc. Messages
        //
        internal static readonly string RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL = "Required fields cannot be null.";
        internal static readonly string RSS_ERRORMESSAGE_ITEM_TITLE_DESCRIPTION_REQUIRED_FIELD_NULL = "Both Title and Description can't be null";
        internal static readonly string RSS_ERRORMESSAGE_ELEMENT_INVALID = "{0} element invalid. Value was: '{1}'";
        internal static readonly string RSS_ERRORMESSAGE_INVALID_VERSION_ATTRIBUTE_NOT_SET = "Version attribute not set.";
        internal static readonly string RSS_ERRORMESSAGE_INVALID_VERSION_ATTRIBUTE = "Version attribute invalid or not supported. Value was: '{0}'. Expected: '0.91', '0.92' or '2.0'.";
        internal static readonly string RSS_ERRORMESSAGE_INVALID_RSS_FORMAT_MISSING_CHANNEL_ELEMENT = "Invalid RSS format. Channel element missing.";
        internal static readonly string RSS_ERRORMESSAGE_INVALID_RSS_FORMAT = "Invalid RSS format.";
        internal static readonly string RSS_ERRORMESSAGE_UNKNOWN_VALUE = "Unknown value";
        #endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public Rss() {
			this.Channel = new RssChannel();
		}		
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rssChannel"></param>
		public Rss(RssChannel rssChannel) {
			this.Channel = rssChannel;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
		public Rss( XmlDocument rss ) {			
			this.DeserializeFromXml( rss );
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
		public Rss( string rss ) {
            if (rss == null)
            {
                throw new ArgumentNullException("rss");
            }
			XmlDocument xdRss = new XmlDocument();
			xdRss.LoadXml(Syndication.NormalizeSyndication(rss));
			this.DeserializeFromXml( xdRss );
		}				
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xdRss"></param>
		private void DeserializeFromXml(XmlDocument xdRss) {			
			XmlNode xnRss = xdRss.SelectSingleNode(RSS_ROOT_ELEMENT);
			if ( xnRss == null ) {								
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_INVALID_RSS_FORMAT);
				throw new SyndicationFormatInvalidException(msg);
			}
			else {
				DeserializeFromXml(xnRss);
			}
		}		
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnRss"></param>
		private void DeserializeFromXml(XmlNode xnRss) {
			XmlAttribute xaVersion = xnRss.Attributes[RSS_ATTRIBUTE_VERSION];			
			if ( xaVersion == null || xaVersion.InnerText == null || xaVersion.InnerText.Length == 0 ) {
                throw new SyndicationFormatInvalidException(Rss.RSS_ERRORMESSAGE_INVALID_VERSION_ATTRIBUTE_NOT_SET);
			}		
			SetVersion(xaVersion.InnerText);

			//Try-Catch block to make sure the version is set back to NotSet if the deserialization fails.
			try {
				//
				// Channel
				//
				XmlNode xnChannel = xnRss.SelectSingleNode(RSS_ELEMENT_CHANNEL);
				if ( xnChannel == null ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_INVALID_RSS_FORMAT_MISSING_CHANNEL_ELEMENT);
					throw new SyndicationFormatInvalidException(msg);
				}
				this.Channel = new RssChannel(xnChannel);					
			}
			catch ( Exception ) {
				this.m_version = null;
				this.m_recognizedVersion = RssVersion.NotSet;
				throw;
			}
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Sets the version of the deserializing RSS XML.
		/// </summary>
		/// <param name="version"></param>
		private void SetVersion(string version) {						
			this.m_version = version;

			if ( version == RSS_VERSION_0_91 ) {
				this.m_recognizedVersion = RssVersion.RSS_0_91;
			}
			else if ( version == RSS_VERSION_0_92 ) {
				this.m_recognizedVersion = RssVersion.RSS_0_92;
			}
			else if ( version == RSS_VERSION_2_0_1 ) {
				this.m_recognizedVersion = RssVersion.RSS_2_0_1;
			}
			else {
				this.m_recognizedVersion = RssVersion.Unknown;
			}			
		}	
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Saves the RSS feed as RSS 0.91.
		/// </summary>
		/// <returns></returns>
		private XmlDocument SerializeToXml_0_91(string encoding,string version) {			
			XmlDocument xdRss = new XmlDocument();

			XmlDeclaration xdDeclaration = xdRss.CreateXmlDeclaration(RSS_XML_VERSION,encoding,null);
			xdRss.AppendChild(xdDeclaration);

			//
			// Create the rss element.
			//
			XmlElement xeRss = xdRss.CreateElement(RSS_ELEMENT_RSS);
			xdRss.AppendChild(xeRss);

			//
			// Create and add the version attribute.
			//
			XmlAttribute xaRssVersion = xdRss.CreateAttribute(RSS_ATTRIBUTE_VERSION);
			xaRssVersion.InnerText = version;//RSS_VERSION_0_91;
			xeRss.Attributes.Append(xaRssVersion);
						
			//
			// Append the channel element.
			//
			xeRss.AppendChild(this.Channel.SerializeToXml(xdRss,RssVersion.RSS_0_91));

			//
			// Return the created XML document.
			//
			return xdRss;
		}
		/// <summary>
		/// Saves the RSS feed as RSS 0.92.
		/// </summary>
		/// <returns></returns>
		private XmlDocument SerializeToXml_0_92(string encoding,string version) {			
			XmlDocument xdRss = new XmlDocument();

			XmlDeclaration xdDeclaration = xdRss.CreateXmlDeclaration(RSS_XML_VERSION,encoding,null);
			xdRss.AppendChild(xdDeclaration);

			//
			// Create the rss element.
			//
			XmlElement xeRss = xdRss.CreateElement(RSS_ELEMENT_RSS);
			xdRss.AppendChild(xeRss);

			//
			// Create and add the version attribute.
			//
			XmlAttribute xaRssVersion = xdRss.CreateAttribute(RSS_ATTRIBUTE_VERSION);
			xaRssVersion.InnerText = version;//RSS_VERSION_0_92;
			xeRss.Attributes.Append(xaRssVersion);
						
			//
			// Append the channel element.
			//
			xeRss.AppendChild(this.Channel.SerializeToXml(xdRss,RssVersion.RSS_0_92));

			//
			// Return the created XML document.
			//
			return xdRss;
		}
		/// <summary>
		/// Saves the RSS feed as RSS 2.0.
		/// </summary>
		/// <param name="encoding"></param>
		/// <returns></returns>
		private XmlDocument SerializeToXml_2_0_1(string encoding,string version) {
			XmlDocument xdRss = new XmlDocument();

			XmlDeclaration xdDeclaration = xdRss.CreateXmlDeclaration(RSS_XML_VERSION,encoding,null);
			xdRss.AppendChild(xdDeclaration);

			//
			// Create the rss element.
			//
			XmlElement xeRss = xdRss.CreateElement(RSS_ELEMENT_RSS);
			xdRss.AppendChild(xeRss);

			//
			// Create and add the version attribute.
			//
			XmlAttribute xaRssVersion = xdRss.CreateAttribute(RSS_ATTRIBUTE_VERSION);
			xaRssVersion.InnerText = version;//RSS_VERSION_2_0_1;
			xeRss.Attributes.Append(xaRssVersion);
						
			//
			// Append the channel element.
			//
			xeRss.AppendChild(this.Channel.SerializeToXml(xdRss,RssVersion.RSS_2_0_1));

			//
			// Return the created XML document.
			//
			return xdRss;
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Validates that the object model has the elements it needs
		/// to produce an RSS XML of the desired version.
		/// </summary>
		/// <param name="version"></param>
		public void Validate(RssVersion version, bool validateContent) {			
			this.Channel.Validate(version,validateContent);
		}
		/// <summary>
		/// Save the RSS object with specified version number to XML.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public XmlDocument Save(RssVersion version, string encoding, string customVersion) {
			if ( customVersion == null ) {
				throw new ArgumentNullException("customVersion");
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return SerializeToXml_0_91(encoding,customVersion);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return SerializeToXml_0_92(encoding,customVersion);
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return SerializeToXml_2_0_1(encoding,customVersion);
			}
			throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE,RSS_ATTRIBUTE_VERSION);
		}
		/// <summary>
		/// Saves the RSS object accoring to the version and encoding arguments.		
		/// </summary>
		/// <param name="rssVersion"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public XmlDocument Save(RssVersion version, string encoding) {
			if ( version == RssVersion.RSS_0_91 ) {
				return this.Save(version,encoding,RSS_VERSION_0_91);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return this.Save(version,encoding,RSS_VERSION_0_92);
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return this.Save(version,encoding,RSS_VERSION_2_0_1);
			}
			throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE,RSS_ATTRIBUTE_VERSION);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <param name="customVersion"></param>
		public void Save(System.IO.Stream stream, RssVersion version, string encoding, string customVersion) {
			if ( customVersion == null ) {
				throw new ArgumentNullException("customVersion");
			}
			if ( stream == null ) {
				throw new ArgumentNullException("stream");
			}
			this.Save(version,encoding,customVersion).Save(stream);
		}
		/// <summary>
		/// Saves the RSS object according to the version and encoding arguments.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public void Save(System.IO.Stream stream, RssVersion version, string encoding) {
			if ( stream == null ) {
				throw new ArgumentNullException("stream");
			}
			this.Save(version,encoding).Save(stream);
		}
		/// <summary>
		/// Saves the RSS object according to the version and encoding arguments.
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <param name="customVersion"></param>
		public void Save(string filename, RssVersion version, string encoding,string customVersion) {
			if ( customVersion == null ) {
				throw new ArgumentNullException("customVersion");
			}
			if ( filename == null ) {
				throw new ArgumentNullException("filename");
			}
			this.Save(version,encoding,customVersion).Save(filename);
		}
		/// <summary>
		/// Saves the RSS object according to the version and encoding arguments.
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		public void Save(string filename, RssVersion version, string encoding) {
			if ( filename == null ) {
				throw new ArgumentNullException("filename");
			}
			this.Save(version,encoding).Save(filename);
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Version backing field
		/// </summary>
		private string m_version; 
		/// <summary>
		/// Gets the RSS version of the underlying document.
		/// </summary>
		public string Version {
			get {
				return m_version;
			}
		}
		/// <summary>
		/// RecognizedVersion backing field.
		/// </summary>
		private RssVersion m_recognizedVersion = RssVersion.NotSet;
		/// <summary>
		/// Gets the RecognizedVersion. The version known og unknown.
		/// </summary>
		public RssVersion RecognizedVersion {
			get {
				return m_recognizedVersion;
			}
		}
		/// <summary>
		/// ValidateOnSave backing field.
		/// </summary>
		private bool m_validateOnSave = false;//true
		/// <summary>
		/// Gets or sets wheather or not to validate RSS on save.
		/// </summary>
		private bool ValidateOnSave {
			get {
				return m_validateOnSave;
			}
			set {
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
		private bool ValidateContent {
			get {
				return m_validateContent;
			}
			set {
				m_validateContent = value;
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Channel backing field.
		/// </summary>
		private RssChannel m_rssChannel;
		/// <summary>
		/// Contains information about the channel (metadata) and its contents.
		/// </summary>
		public RssChannel Channel {
			get {
				return m_rssChannel;
			}
			set {
				m_rssChannel = value;
			}
		}		
		#endregion

	}// class RSS
}// namespace
