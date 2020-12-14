using System;
using System.Xml;
using System.Collections.Generic;

namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfChannel class.
	/// </summary>
	public sealed class RdfChannel {

		#region Private Const Data
		private const string RDF_ATTRIBUTE_ABOUT = "about";
		private const string RDF_ELEMENT_CHANNEL = "channel";
		private const string RDF_ELEMENT_TITLE = "title";
		private const string RDF_ELEMENT_LINK = "link";
		private const string RDF_ELEMENT_IMAGE = "image";
		private const string RDF_ELEMENT_DESCRIPTION = "description";
		private const string RDF_ELEMENT_TEXTINPUT = "textinput";
		private const string RDF_ELEMENT_ITEMS = "items";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";					
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion		

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfChannel() {
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Deserialization Constructor.
		/// </summary>
		/// <param name="xnChannel"></param>
		internal RdfChannel(XmlNode xnChannel) {
			if ( xnChannel == null ) {
				throw new ArgumentNullException("xnChannel");
			}
			this.DeserializeFromXml(xnChannel);
		}
		#endregion

		#region About Helper Methods
		/// <summary>
		/// Returnes the about value.
		/// </summary>
		/// <returns></returns>
		private string GetAbout() {
			if ( this.About == null ) {
				return string.Empty;
			}
			return this.About;
		}
		/// <summary>
		/// Returnes all about's in channel.
		/// </summary>
		/// <returns></returns>
		internal RdfAboutCollection GetAbouts() {
			RdfAboutCollection rac = new RdfAboutCollection();
			rac.Add( this.GetAbout() );
			return rac;			
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
		internal XmlNode SerializeToXml(XmlDocument xdRdf, RdfVersion version,RdfChannelItems channelItems,string prefix,RdfChannelImage channelImage, RdfChannelTextInput channelTextInput) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return SerializeToXml_1_0(xdRdf,channelItems,prefix,channelImage,channelTextInput);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object to XML according to RSS 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_1_0(XmlDocument xdRdf,RdfChannelItems channelItems,string prefix,RdfChannelImage channelImage,RdfChannelTextInput channelTextInput) {			
			XmlAttribute xaAbout = xdRdf.CreateAttribute(prefix,RDF_ATTRIBUTE_ABOUT,xdRdf.DocumentElement.NamespaceURI);
			xaAbout.InnerText = this.GetAbout();
					
			XmlElement xeChannel = xdRdf.CreateElement(null,RDF_ELEMENT_CHANNEL,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeChannel.Attributes.Append(xaAbout);			

			if ( this.Title != null ) {
				xeChannel.AppendChild( SerializeTitleToXml(xdRdf) );
			}
			if ( this.Link != null ) {
				xeChannel.AppendChild( SerializeLinkToXml(xdRdf) );
			}			
			if ( this.Description != null ) {
				xeChannel.AppendChild( SerializeDescriptionToXml(xdRdf) );
			}
			if ( this.Image != null ) {
				xeChannel.AppendChild( this.Image.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}
			else if ( channelImage != null ) {				
				xeChannel.AppendChild( channelImage.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}
			if ( this.Items != null ) {
				xeChannel.AppendChild( this.Items.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );				
			}
			else if ( channelItems != null ) {
				xeChannel.AppendChild( channelItems.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );				
			}			
			if ( this.TextInput != null ) {
				xeChannel.AppendChild( this.TextInput.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}
			else if ( channelTextInput != null ) {
				xeChannel.AppendChild( channelTextInput.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeChannel);
			}
			return xeChannel;
		}
		/// <summary>
		/// Serializes Title to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeTitleToXml(XmlDocument xdRdf) {
			XmlElement xeTitle = xdRdf.CreateElement(null,RDF_ELEMENT_TITLE,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeTitle.InnerText = this.Title;
			return xeTitle;
		}		
		/// <summary>
		/// Serializes Link to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeLinkToXml(XmlDocument xdRdf) {
			XmlElement xeLink = xdRdf.CreateElement(null,RDF_ELEMENT_LINK,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeLink.InnerText = this.Link;
			return xeLink;
		}
		/// <summary>
		/// Serializes Description to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDescriptionToXml(XmlDocument xdRdf) {
			XmlElement xeDescription = xdRdf.CreateElement(null,RDF_ELEMENT_DESCRIPTION,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeDescription.InnerText = this.Description;
			return xeDescription;
		}				
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xnChannel"></param>
		private void DeserializeFromXml(XmlNode xnChannel) {						
			//
			// Set Namespace Manager.
			//
			string namespaceUri = xnChannel.OwnerDocument.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText;
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnChannel.OwnerDocument.NameTable);				
			nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);		

			//
			// About.
			//
			XmlAttribute xaAbout = xnChannel.Attributes[RDF_ATTRIBUTE_ABOUT,xnChannel.OwnerDocument.DocumentElement.NamespaceURI];
			if ( xaAbout != null ) {
				this.About = xaAbout.InnerText;
			}

			//
			// Title.
			//
			XmlNode xnTitle = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_TITLE,nsmgr);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;				
			}

			//
			// Link.
			//
			XmlNode xnLink = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_LINK,nsmgr);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;				
			}

			//
			// Description.
			//
			XmlNode xnDescription = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_DESCRIPTION,nsmgr);
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;				
			}

			//
			// Image.
			//
			XmlNode xnImage = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_IMAGE,nsmgr);
			if ( xnImage != null ) {
				this.Image = new RdfChannelImage(xnImage);				
			}

			//
			// Items.
			//
			XmlNode xnItems = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_ITEMS,nsmgr);
			if ( xnItems != null ) {
				this.Items = new RdfChannelItems(xnItems);
			}

			//
			// TextInput.
			//
			XmlNode xnTextInput = xnChannel.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_TEXTINPUT,nsmgr);
			if ( xnTextInput != null ) {
				this.TextInput = new RdfChannelTextInput(xnTextInput);				
			}

			//
			// Deserialize modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_TITLE,namespaceUri) );
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_LINK,namespaceUri) );
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_IMAGE,namespaceUri) );
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_DESCRIPTION,namespaceUri) );
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_TEXTINPUT,namespaceUri) );
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_ITEMS,namespaceUri) );

            this.Modules = new SyndicationModuleCollection(xnChannel, exclusionElements);					
		}
		#endregion

		#region Validation Methods
		/// <summary>
		/// Validates the class.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RdfVersion version, bool validateContent,RdfAboutCollection rac,RdfAboutCollection racItems,string textInputAbout,string imageAbout,string textinputAbout) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return Validate_1_0(validateContent,rac,racItems,textInputAbout,imageAbout,textinputAbout);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");		
		}	
		/// <summary>
		/// Validates class according to RDF 1.0.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		private bool Validate_1_0(bool validateContent,RdfAboutCollection rac,RdfAboutCollection racItems,string textInputAbout,string imageAbout,string textinputAbout) {
			if ( this.Title == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_CHANNEL,RDF_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			if ( this.Link == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_CHANNEL,RDF_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			if ( this.Description == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_CHANNEL,RDF_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}								
			if ( this.Items != null ) {
				this.Items.Validate(RdfVersion.RDF_1_0,validateContent,rac,racItems);					
			}
			if ( this.Image != null ) {
				this.Image.Validate(RdfVersion.RDF_1_0,validateContent,imageAbout);
			}
			if ( this.TextInput != null ) {
				this.TextInput.Validate(RdfVersion.RDF_1_0,validateContent,textinputAbout);
			}

			if ( validateContent ) {
				ValidateAbout(RdfVersion.RDF_1_0,rac);				
			}
			return true;
		}		
		/// <summary>
		/// Validate about attribute.
		/// </summary>
		/// <param name="rac"></param>
		private void ValidateAbout(RdfVersion version, RdfAboutCollection rac) {
			if ( version == RdfVersion.RDF_1_0 ) {
				string about = this.GetAbout();
				if ( about == null || about == string.Empty ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_NOT_UNIQUE_ABOUT_1_0,RDF_ELEMENT_CHANNEL);
					throw new SyndicationValidationException(msg);					
				}
				about = about.Trim();
				bool validUrl = false;
				foreach ( string prefix in Rdf.RDF_VALIDVALUES_URL_1_0 ) {
					if ( about.StartsWith(prefix) ) {
						validUrl = true;
						break;
					}
				}
				if ( !validUrl ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_URL_WRONG_TYPE_1_0,RDF_ELEMENT_CHANNEL+","+RDF_ATTRIBUTE_ABOUT,about);					
					throw new SyndicationValidationException(msg);					
				}
				if ( !rac.IsUnique(about) ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_NOT_UNIQUE_ABOUT_1_0,RDF_ELEMENT_CHANNEL);					
					throw new SyndicationValidationException(msg);					
				}
			}
		}		
		#endregion

		#region Public Properties
		/// <summary>
		/// About backing field.
		/// </summary>
		private string m_about;
		/// <summary>
		/// Get/Set About.
		/// </summary>
		public string About {
			get {
				return m_about;
			}
			set {
				m_about = value;
			}
		}
		/// <summary>
		/// Title backing field.
		/// </summary>
		private string m_title;
		/// <summary>
		/// Get/Set Title.
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
		/// Link backing field.
		/// </summary>
		private string m_link;
		/// <summary>
		/// Get/Set Link.
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
		/// Description backing field.
		/// </summary>
		private string m_description;
		/// <summary>
		/// Get/Set description.
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
		/// Image backing field.
		/// </summary>
		private RdfChannelImage m_image;
		/// <summary>
		/// Get/Set Image.
		/// </summary>
		public RdfChannelImage Image {
			get {
				return m_image;
			}
			set {
				m_image = value;
			}
		}		
		/// <summary>
		/// TextInput backing field.
		/// </summary>
		private RdfChannelTextInput m_textInput;
		/// <summary>
		/// Get/Set TextInput.
		/// </summary>
		public RdfChannelTextInput TextInput {
			get {
				return m_textInput;
			}
			set {
				m_textInput = value;
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
		/// <summary>
		/// Items backing field.
		/// </summary>
		private RdfChannelItems m_items;
		/// <summary>
		/// Get/Set Items.
		/// </summary>
		public RdfChannelItems Items {
			get {
				return m_items;
			}
			set {
				m_items = value;
			}
		}
		#endregion

	}// class
}// namespace
