using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Specifies a GIF, JPEG or PNG image that can be displayed with the channel.
	/// </summary>
	public sealed class RssImage : RssElementBase {

		#region Private Const Data
		private const int RSS_MAXLENGTH_URL = 500;
		private const int RSS_MAXLENGTH_TITLE_0_91 = 100;
		private const int RSS_MAXLENGTH_LINK_0_91 = 500;
		private const int RSS_MAXVALUE_WIDTH_0_91 = 144;
		private const int RSS_MAXVALUE_WIDTH_0_92 = 144;
		private const int RSS_MAXVALUE_WIDTH_2_0 = 144;
		private const int RSS_MAXVALUE_HEIGHT_0_91 = 400;
		private const int RSS_MAXVALUE_HEIGHT_0_92 = 400;
		private const int RSS_MAXVALUE_HEIGHT_2_0 = 400;
		private const string RSS_ELEMENT_IMAGE = "image";
		private const string RSS_ELEMENT_URL = "url";
		private const string RSS_ELEMENT_LINK = "link";
		private const string RSS_ELEMENT_TITLE = "title";
		private const string RSS_ELEMENT_WIDTH = "width";
		private const string RSS_ELEMENT_HEIGHT = "height";
		private const string RSS_ELEMENT_DESCRIPTION = "description";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="link"></param>
		/// <param name="title"></param>
		public RssImage(string url, string link, string title) {			
			this.URL = url;
			this.Link = link;
			this.Title = title;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="link"></param>
		/// <param name="title"></param>
		/// <param name="width"></param>
		public RssImage(string url, string link, string title, string width) : this(url,link,title) {			
			this.Width = width;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="link"></param>
		/// <param name="title"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public RssImage(string url, string link, string title, string width, string height) : this(url,link,title,width) {			
			this.Height = height;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="link"></param>
		/// <param name="title"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="description"></param>
		public RssImage(string url, string link, string title, string width, string height, string description) : this(url,link,title,width,height) {			
			this.Description = description;			
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnImage"></param>
		internal RssImage(XmlNode xnImage) {
			this.DeserializeFromXml(xnImage);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnImage"></param>
		private void DeserializeFromXml(XmlNode xnImage) {
			//
			// Required Elements.
			//
			XmlNode xnUrl = xnImage.SelectSingleNode(RSS_ELEMENT_URL);
			if ( xnUrl != null ) {
				this.URL = xnUrl.InnerText;
			}
			XmlNode xnLink = xnImage.SelectSingleNode(RSS_ELEMENT_LINK);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}
			XmlNode xnTitle = xnImage.SelectSingleNode(RSS_ELEMENT_TITLE);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;
			}			
			//
			// Optional Elements.
			// 
			XmlNode xnWidth = xnImage.SelectSingleNode(RSS_ELEMENT_WIDTH);
			if ( xnWidth != null ) {
				this.Width = xnWidth.InnerText;
			}
			XmlNode xnHeight = xnImage.SelectSingleNode(RSS_ELEMENT_HEIGHT);
			if ( xnHeight != null ) {
				this.Height = xnHeight.InnerText;
			}
			XmlNode xnDescription = xnImage.SelectSingleNode(RSS_ELEMENT_DESCRIPTION);
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;
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
		/// <param name="elementName"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
			XmlElement xeImage = xdRss.CreateElement(RSS_ELEMENT_IMAGE);
			//
			// Required Elements
			//
			if ( this.URL != null ) {
				xeImage.AppendChild( SerializeUrlToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeImage.AppendChild( SerializeLinkToXml(xdRss) );
			}
			if ( this.Title != null ) {
				xeImage.AppendChild( SerializeTitleToXml(xdRss) );
			}			
			//
			// Optional Elements
			//
			if ( this.Width != null ) {
				xeImage.AppendChild( SerializeWidthToXml(xdRss) );
			}
			if ( this.Height != null ) {
				xeImage.AppendChild( SerializeHeightToXml(xdRss) );				
			}
			if ( this.Description != null ) {
				xeImage.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			return xeImage;
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="elementName"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeImage = xdRss.CreateElement(RSS_ELEMENT_IMAGE);
			//
			// Required Elements
			//
			if ( this.URL != null ) {
				xeImage.AppendChild( SerializeUrlToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeImage.AppendChild( SerializeLinkToXml(xdRss) );
			}
			if ( this.Title != null ) {
				xeImage.AppendChild( SerializeTitleToXml(xdRss) );
			}			
			//
			// Optional Elements
			//
			if ( this.Width != null ) {
				xeImage.AppendChild( SerializeWidthToXml(xdRss) );
			}
			if ( this.Height != null ) {
				xeImage.AppendChild( SerializeHeightToXml(xdRss) );				
			}
			if ( this.Description != null ) {
				xeImage.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			return xeImage;
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="elementName"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeImage = xdRss.CreateElement(RSS_ELEMENT_IMAGE);
			//
			// Required Elements
			//
			if ( this.URL != null ) {
				xeImage.AppendChild( SerializeUrlToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeImage.AppendChild( SerializeLinkToXml(xdRss) );
			}
			if ( this.Title != null ) {
				xeImage.AppendChild( SerializeTitleToXml(xdRss) );
			}			
			//
			// Optional Elements
			//
			if ( this.Width != null ) {
				xeImage.AppendChild( SerializeWidthToXml(xdRss) );
			}
			if ( this.Height != null ) {
				xeImage.AppendChild( SerializeHeightToXml(xdRss) );				
			}
			if ( this.Description != null ) {
				xeImage.AppendChild( SerializeDescriptionToXml(xdRss) );
			}
			return xeImage;
		}
		/// <summary>
		/// Serializes URL to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeUrlToXml(XmlDocument xdRss) {
			XmlElement xeURL = xdRss.CreateElement(RSS_ELEMENT_URL);
			xeURL.InnerText = this.URL;
			return xeURL;
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
		/// Serializes Width to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeWidthToXml(XmlDocument xdRss) {
			XmlElement xeWidth = xdRss.CreateElement(RSS_ELEMENT_WIDTH);
			xeWidth.InnerText = this.Width;
			return xeWidth;
		}
		/// <summary>
		/// Serializes Height to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeHeightToXml(XmlDocument xdRss) {
			XmlElement xeHeight = xdRss.CreateElement(RSS_ELEMENT_HEIGHT);
			xeHeight.InnerText = this.Height;
			return xeHeight;
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
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			//
			// Required Elements.
			//
			if ( this.URL == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_0_91);
				ValidateTitle(RssVersion.RSS_0_91);
				ValidateLink(RssVersion.RSS_0_91);
				ValidateWidth(RssVersion.RSS_0_91);
				ValidateHeight(RssVersion.RSS_0_91);
				ValidateDescription(RssVersion.RSS_0_91);				
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
			// Required Elements.
			//
			if ( this.URL == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_0_92);
				ValidateTitle(RssVersion.RSS_0_92);
				ValidateLink(RssVersion.RSS_0_92);
				ValidateWidth(RssVersion.RSS_0_92);
				ValidateHeight(RssVersion.RSS_0_92);
				ValidateDescription(RssVersion.RSS_0_92);				
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
			// Required Elements.
			//
			if ( this.URL == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateUrl(RssVersion.RSS_0_91);
				ValidateTitle(RssVersion.RSS_0_91);
				ValidateLink(RssVersion.RSS_0_91);
				ValidateWidth(RssVersion.RSS_0_91);
				ValidateHeight(RssVersion.RSS_0_91);
				ValidateDescription(RssVersion.RSS_0_91);				
			}			
		}
		/// <summary>
		/// Validates URL.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateUrl(RssVersion version) {
			if ( this.URL == null ) {
				return;
			}			

			if ( version == RssVersion.RSS_0_91 ) {
				string url = this.URL.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_91 ) {
					if ( url.StartsWith( validPrefix ) ) {
						if ( this.URL.Length > RSS_MAXLENGTH_URL ) {
                            string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
							throw new SyndicationValidationException(msg);
						}
						else {
                            foreach (string validExtension in Rss.RSS_VALIDVALUES_IMAGE_URL_0_91)
                            {
								if ( url.IndexOf(validExtension) != -1 ) {
									return;
								}
							}
                            string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
							throw new SyndicationValidationException(msg);
						}						
					}
				}
                string msg2 = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg2);				
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				string url = this.URL.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92)
                {
					if ( url.StartsWith( validPrefix ) ) {												
						foreach ( string validExtension in Rss.RSS_VALIDVALUES_IMAGE_URL_0_92 ) {
							if ( url.IndexOf(validExtension) != -1 ) {
								return;
							}
						}
                        string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
						throw new SyndicationValidationException(msg);					
					}
				}
                string msg2 = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg2);				
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string url = this.URL.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1)
                {
					if ( url.StartsWith( validPrefix ) ) {
                        foreach (string validExtension in Rss.RSS_VALIDVALUES_IMAGE_URL_2_0_1)
                        {
							if ( url.IndexOf(validExtension) != -1 ) {
								return;
							}
						}
                        string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_IMAGE, RSS_ELEMENT_URL);
						throw new SyndicationValidationException(msg);					
					}
				}
                string msg2 = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg2);				
			}
		}
		/// <summary>
		/// Validates Title.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateTitle(RssVersion version){
			if ( this.Title == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Title.Length > RSS_MAXLENGTH_TITLE_0_91 ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_TITLE);
					throw new SyndicationValidationException(msg);				
				}
			}
		}
		/// <summary>
		/// Validate Link.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLink(RssVersion version){
			if ( this.Link == null ) {
				return;
			}			
			if ( version == RssVersion.RSS_0_91 ) {				
				if ( this.Link.Length > RSS_MAXLENGTH_LINK_0_91 ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91,RSS_ELEMENT_LINK,this.Link);
					throw new SyndicationValidationException(msg);				
				}
				string link = this.Link.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_91 ) {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
				string msg2 = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91,RSS_ELEMENT_LINK,this.Link);
				throw new SyndicationValidationException(msg2);				
			}// if
			else if ( version == RssVersion.RSS_0_92 ) {								
				string link = this.Link.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92 ) {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92,RSS_ELEMENT_LINK,this.Link);
				throw new SyndicationValidationException(msg);				
			}// if
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string link = this.Link.ToLower().Trim();
				foreach ( string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1 ) {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1,RSS_ELEMENT_LINK,this.Link);
				throw new SyndicationValidationException(msg);				
			}
		}
		/// <summary>
		/// Validate width.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateWidth(RssVersion version){
			if ( this.Width == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				try {
					int width = Convert.ToInt32(this.Width);
					if ( width < 0 || width > RSS_MAXVALUE_WIDTH_0_91 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				try {
					int width = Convert.ToInt32(this.Width);
					if ( width < 0 || width > RSS_MAXVALUE_WIDTH_0_92 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				try {
					int width = Convert.ToInt32(this.Width);
					if ( width < 0 || width > RSS_MAXVALUE_WIDTH_2_0 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
		}
		private void ValidateHeight(RssVersion version){
			if ( this.Height == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				try {
					int height = Convert.ToInt32(this.Height);
					if ( height < 0 || height > RSS_MAXVALUE_HEIGHT_0_91 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				try {
					int height = Convert.ToInt32(this.Height);
					if ( height < 0 || height > RSS_MAXVALUE_HEIGHT_0_92 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				try {
					int height = Convert.ToInt32(this.Height);
					if ( height < 0 || height > RSS_MAXVALUE_HEIGHT_2_0 ) {
						string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
						throw new SyndicationValidationException(msg);				
					}
				}
				catch ( FormatException ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_IMAGE,RSS_ELEMENT_WIDTH);
					throw new SyndicationValidationException(msg);				
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDescription(RssVersion version){
			if ( this.Description == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// URL backing field.
		/// </summary>
		private string m_url;
		/// <summary>
		/// URL of a GIF, JPEG or PNG image that represents the channel. 
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
		/// Title backing field.
		/// </summary>
		private string m_title;
		/// <summary>
		/// Describes the image, it's used in the ALT attribute of the 
		/// HTML &lt;img&gt; tag when the channel is rendered in HTML.
		/// </summary>
		public string Title {
			get {
				return m_title;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_TITLE,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_title = value;
			}
		}
		/// <summary>
		/// Link backing field.
		/// </summary>
		private string m_link;
		/// <summary>
		/// The URL of the site, when the channel is rendered, 
		/// the image is a link to the site. (Note, in practice 
		/// the image <title> and <link> should have the same value 
		/// as the channel's <title> and <link>. 
		/// </summary>
		public string Link {
			get {
				return m_link;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException(RSS_ELEMENT_LINK,Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_link = value;
			}
		}
		#endregion
		
		#region Optional Elements
		/// <summary>
		/// Width backing field.
		/// </summary>
		private string m_width;
		/// <summary>
		/// Indicating the width of the image in pixels.
		/// Maximum value for width is 144, default value is 88. 
		/// </summary>
		public string Width {
			get {
				return m_width;
			}
			set {
				m_width = value;
			}
		}
		/// <summary>
		/// Height backing field.
		/// </summary>
		private string m_height;
		/// <summary>
		/// Indicating the height of the image in pixels.
		/// Maximum value for height is 400, default value is 31.
		/// </summary>
		public string Height {
			get {
				return m_height;
			}
			set {
				m_height = value;
			}
		}
		/// <summary>
		/// Description backing field.
		/// </summary>
		private string m_description;
		/// <summary>
		/// Contains text that is included in the TITLE attribute of the link formed 
		/// around the image in the HTML rendering.
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

	}// class
}// namespace
