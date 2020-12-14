using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfImage class.
	/// </summary>
	public sealed class RdfImage {

		#region Private Const Data		
		private const string RDF_ATTRIBUTE_ABOUT = "about";
		private const string RDF_ELEMENT_IMAGE = "image";
		private const string RDF_ELEMENT_TITLE = "title";
		private const string RDF_ELEMENT_LINK = "link";
		private const string RDF_ELEMENT_URL = "url";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion
		
		#region Public Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfImage() {
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="about"></param>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="url"></param>
		public RdfImage(string title, string link, string url) : this() {
			this.Title = title;
			this.Link = link;
			this.URL = url;
		}
		/// <summary>
		/// Internal Deserialization Constructor.
		/// </summary>
		/// <param name="xnImage"></param>
		internal RdfImage(XmlNode xnImage) {
			if ( xnImage == null ) {
				throw new ArgumentNullException("xnImage");
			}
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
			// Set Namespace Manager.
			//
			string namespaceUri = xnImage.OwnerDocument.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText;
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnImage.OwnerDocument.NameTable);				
			nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);		

			//
			// About.
			//
			XmlAttribute xaAbout = xnImage.Attributes[RDF_ATTRIBUTE_ABOUT,xnImage.OwnerDocument.DocumentElement.NamespaceURI];
			if ( xaAbout != null ) {
				this.m_about = xaAbout.InnerText;
			}

			//
			// Title.
			//
			XmlNode xnTitle = xnImage.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_TITLE,nsmgr);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;
			}

			//
			// Link.
			//
			XmlNode xnLink = xnImage.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_LINK,nsmgr);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}

			//
			// URL.
			//
			XmlNode xnUrl = xnImage.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_URL,nsmgr);
			if ( xnUrl != null ) {
				this.URL = xnUrl.InnerText;
			}

			//
			// Deserialize modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_TITLE,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_LINK,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_URL,namespaceUri) );

            this.Modules = new SyndicationModuleCollection(xnImage, exclusionElements);

		}
		#endregion

		#region About Helper Methods
		/// <summary>
		/// Returnes the about value.
		/// </summary>
		/// <returns></returns>
		internal string GetAbout() {
			if ( this.URL != null ) {
				return this.URL;
			}
			return string.Empty;
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

		#region Internal Helper Methods
		/// <summary>
		/// Get a RdfChannelImage of this RdfImage.
		/// </summary>
		/// <returns></returns>
		internal RdfChannelImage GetChannelImage() {
			return new RdfChannelImage(this.GetAbout());
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRdf,RdfVersion version,string prefix) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return SerializeToXml_1_0(xdRdf,prefix);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object to XML according to RSS 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_1_0(XmlDocument xdRdf,string prefix) {
			XmlAttribute xaAbout = xdRdf.CreateAttribute(prefix,RDF_ATTRIBUTE_ABOUT,xdRdf.DocumentElement.NamespaceURI);
			xaAbout.InnerText = GetAbout();
					
			XmlElement xeImage = xdRdf.CreateElement(null,RDF_ELEMENT_IMAGE,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeImage.Attributes.Append(xaAbout);			

			if ( this.Title != null ) {
				xeImage.AppendChild( SerializeTitleToXml(xdRdf) );
			}
			if ( this.Link != null ) {
				xeImage.AppendChild( SerializeLinkToXml(xdRdf) );
			}			
			if ( this.URL != null ) {
				xeImage.AppendChild( SerializeURLToXml(xdRdf) );
			}	
		
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeImage);
			}

			return xeImage;
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
		private XmlNode SerializeURLToXml(XmlDocument xdRdf) {
			XmlElement xeURL = xdRdf.CreateElement(null,RDF_ELEMENT_URL,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeURL.InnerText = this.URL;
			return xeURL;
		}		
		#endregion

		#region Public Properties
		/// <summary>
		/// About backing field.
		/// </summary>
		private string m_about;
		/// <summary>
		/// Get About.
		/// </summary>
		public string About {
			get {
				return m_about;
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
		/// Url backing field.
		/// </summary>
		private string m_url;
		/// <summary>
		/// Get/Set Url.
		/// </summary>
		public string URL {
			get {
				return m_url;
			}
			set {
				m_url = value;
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
		#endregion

		#region Validation Methods
		/// <summary>
		/// Validates the item object.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RdfVersion version, bool validateContent,RdfAboutCollection rac) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return Validate_1_0(validateContent,rac);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");		
		}	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		private bool Validate_1_0(bool validateContent,RdfAboutCollection rac) {
			if ( this.Title == null ) {										   
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_IMAGE,RDF_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}

			if ( this.URL == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_IMAGE,RDF_ELEMENT_URL);
				throw new SyndicationValidationException(msg);
			}

			if ( this.Link == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_IMAGE,RDF_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}

			if ( validateContent ) {
				ValidateUrl(RdfVersion.RDF_1_0);// Implicitly validates about.
				ValidateLink(RdfVersion.RDF_1_0);
			}
			return true;
		}
		/// <summary>
		/// Validates URL.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateUrl(RdfVersion version) {
			if ( version == RdfVersion.RDF_1_0 ) {
				if ( this.URL == null ) {
					return;
				}
				string url = this.URL.ToLower().Trim();
				foreach ( string validPrefix in Rdf.RDF_VALIDVALUES_URL_1_0 ) {
					if ( url.StartsWith( validPrefix ) ) {
						return;					
					}
				}
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_URL_WRONG_TYPE_1_0,RDF_ELEMENT_URL,this.URL);
				throw new SyndicationValidationException(msg);			
			}
		}
		/// <summary>
		/// Validates Link.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLink(RdfVersion version) {
			if ( version == RdfVersion.RDF_1_0 ) {
				if ( this.Link == null ) {
					return;
				}
				string link = this.Link.ToLower().Trim();
				foreach ( string validPrefix in Rdf.RDF_VALIDVALUES_LINK_1_0 ) {
					if ( link.StartsWith( validPrefix ) ) {
						return;					
					}
				}
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_LINK_WRONG_TYPE_1_0,RDF_ELEMENT_LINK,this.Link);
				throw new SyndicationValidationException(msg);			
			}
		}
		#endregion

	}// class
}// namespace
