using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Summary description for TextInput.
	/// </summary>
	public sealed class RssTextInput : RssElementBase {		

		#region Private Const Data
		private const int RSS_MAXLENGTH_TITLE_0_91 = 100;
		private const int RSS_MAXLENGTH_DESCRIPTION_0_91 = 500;
		private const int RSS_MAXLENGTH_NAME_0_91 = 20;
		private const int RSS_MAXLENGTH_LINK_0_91 = 500;
		private const string RSS_ELEMENT_TEXTINPUT = "textInput";
		private const string RSS_ELEMENT_TITLE = "title";
		private const string RSS_ELEMENT_DESCRIPTION = "description";
		private const string RSS_ELEMENT_NAME = "name";
		private const string RSS_ELEMENT_LINK = "link";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="name"></param>
		/// <param name="link"></param>
		public RssTextInput(string title, string description, string name, string link) {
			this.Title = title;
			this.Description = description;
			this.Name = name;
			this.Link = link;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnTextInput"></param>
		internal RssTextInput(XmlNode xnTextInput) {
			this.DeserializeFromXml(xnTextInput);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnTextInput"></param>
		private void DeserializeFromXml(XmlNode xnTextInput) {
			//
			// Required Elements
			//
			XmlNode xnTitle = xnTextInput.Attributes[RSS_ELEMENT_TITLE];
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;
			}
			XmlNode xnDescription = xnTextInput.Attributes[RSS_ELEMENT_DESCRIPTION];
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;
			}
			XmlNode xnName = xnTextInput.Attributes[RSS_ELEMENT_NAME];
			if ( xnName != null ) {
				this.Name = xnName.InnerText;
			}
			XmlNode xnLink = xnTextInput.Attributes[RSS_ELEMENT_LINK];
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}			
		}
		#endregion

		#region Serialization Mehtods
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
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_TEXTINPUT);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object according to RSS version 0.92.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION,RSS_ELEMENT_TEXTINPUT);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object according to RSS version 2.0.1.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeTextInput = xdRss.CreateElement(RSS_ELEMENT_TEXTINPUT);			
			//
			// Required Elements
			//
			if ( this.Title != null ) {
				xeTextInput.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Description != null ) {
				xeTextInput.AppendChild( SerializeDescriptionToXml(xdRss) );				
			}
			if ( this.Name != null ) {
				xeTextInput.AppendChild( SerializeNameToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeTextInput.AppendChild( SerializeLinkToXml(xdRss) );				
			}
			return xeTextInput;
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
		/// Serializes Description To XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDescriptionToXml(XmlDocument xdRss) {
			XmlElement xeDescription = xdRss.CreateElement(RSS_ELEMENT_DESCRIPTION);
			xeDescription.InnerText = this.Description;
			return xeDescription;
		}
		/// <summary>
		/// Serializes Name to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeNameToXml(XmlDocument xdRss) {
			XmlElement xeName = xdRss.CreateElement(RSS_ELEMENT_NAME);
			xeName.InnerText = this.Name;
			return xeName;
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
		/// Validates according to RSS 0.91.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_91(bool validateContent)
        {
			//
			// Required Elements.
			//
			if ( this.Title == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Name == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_NAME);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_0_91);
				ValidateDescription(RssVersion.RSS_0_91);
				ValidateName(RssVersion.RSS_0_91);
				ValidateLink(RssVersion.RSS_0_91);
			}			
		}
		/// <summary>
		/// Validates according to RSS 0.92.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_0_92(bool validateContent)
        {
			//
			// Required Elements.
			//
			if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TEXTINPUT, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TEXTINPUT, RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Name == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TEXTINPUT, RSS_ELEMENT_NAME);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TEXTINPUT, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_0_92);
				ValidateDescription(RssVersion.RSS_0_92);
				ValidateName(RssVersion.RSS_0_92);
				ValidateLink(RssVersion.RSS_0_92);
			}			
		}
		/// <summary>
		/// Validates according to RSS 2.0.1.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
        protected override void Validate_2_0_1(bool validateContent)
        {
			//
			// Required Elements.
			//
			if ( this.Title == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Name == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_NAME);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
				string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}			
			//
			// Validate content.
			//
			if ( validateContent ) {
				ValidateTitle(RssVersion.RSS_2_0_1);
				ValidateDescription(RssVersion.RSS_2_0_1);
				ValidateName(RssVersion.RSS_2_0_1);
				ValidateLink(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// Validate Title.
		/// </summary>
		/// <param name="?"></param>
		private void ValidateTitle(RssVersion version) {
			if ( this.Title == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Title.Length > RSS_MAXLENGTH_TITLE_0_91 ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_TITLE);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates Description.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDescription(RssVersion version){
			if ( this.Description == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Description.Length > RSS_MAXLENGTH_DESCRIPTION_0_91 ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_TEXTINPUT,RSS_ELEMENT_DESCRIPTION);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates Name.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateName(RssVersion version){
			if ( this.Name == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Name.Length > RSS_MAXLENGTH_NAME_0_91 ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_TEXTINPUT, RSS_ELEMENT_NAME);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates Link.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLink(RssVersion version){
			if ( this.Link == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {				
				if ( this.Title.Length > RSS_MAXLENGTH_LINK_0_91 ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91, RSS_ELEMENT_LINK, this.Link);
					throw new SyndicationValidationException(msg);				
				}
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_91)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg2 = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg2);				
			}// if
			else if ( version == RssVersion.RSS_0_92 ) {								
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg);				
			}// if
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg);				
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Title backing field.
		/// </summary>
		private string m_title;
		/// <summary>
		/// The label of the Submit button in the text input area. 
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
		/// Explains the text input area. 
		/// </summary>
		public string Description {
			get {
				return m_description;
			}
			set {
				m_description = value;
			}
		}
		/// <summary>
		/// Name backing field.
		/// </summary>
		private string m_name;
		/// <summary>
		/// The name of the text object in the text input area. 
		/// </summary>
		public string Name {
			get {
				return m_name;
			}
			set {
				m_name = value;
			}
		}
		/// <summary>
		/// Link backing field.
		/// </summary>
		private string m_link;
		/// <summary>
		/// The URL of the CGI 
		/// </summary>
		public string Link {
			get {
				return m_link;
			}
			set {
				m_link = value;
			}
		}
		#endregion

	}// class
}// namespace
