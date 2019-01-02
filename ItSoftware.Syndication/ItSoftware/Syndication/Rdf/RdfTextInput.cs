using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfTextInput class.
	/// </summary>
	public sealed class RdfTextInput {
		
		#region Private Const Data
		private const string RDF_ELEMENT_TEXTINPUT = "textinput";
		private const string RDF_ELEMENT_TITLE = "title";
		private const string RDF_ELEMENT_DESCRIPTION = "description";
		private const string RDF_ELEMENT_NAME = "name";
		private const string RDF_ELEMENT_LINK = "link";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";		
		private const string RDF_ATTRIBUTE_ABOUT = "about";
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfTextInput() {	
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="name"></param>
		/// <param name="link"></param>
		public RdfTextInput(string title, string description, string name, string link) : this() {
			this.Title = title;
			this.Description = description;
			this.Name = name;
			this.Link = link;			
		}
		/// <summary>
		/// Internal Deserialization Constructor.
		/// </summary>
		/// <param name="xnTextInput"></param>
		internal RdfTextInput(XmlNode xnTextInput) {
			if ( xnTextInput == null ) {
				throw new ArgumentNullException("xnTextInput");
			}
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
			// Set Namespace Manager.
			//
			string namespaceUri = xnTextInput.OwnerDocument.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText;
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnTextInput.OwnerDocument.NameTable);				
			nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);		

			//
			// About.
			//
			XmlAttribute xaAbout = xnTextInput.Attributes[RDF_ATTRIBUTE_ABOUT,xnTextInput.OwnerDocument.DocumentElement.NamespaceURI];
			if ( xaAbout != null ) {
				this.About = xaAbout.InnerText;
			}

			//
			// Title.
			//
			XmlNode xnTitle = xnTextInput.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_TITLE,nsmgr);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;
			}

			//
			// Description.
			//
			XmlNode xnDescription = xnTextInput.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_DESCRIPTION,nsmgr);
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;
			}

			//
			// Name.
			//
			XmlNode xnName = xnTextInput.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_NAME,nsmgr);
			if ( xnName != null ) {
				this.Name = xnName.InnerText;
			}

			//
			// Link.
			//
			XmlNode xnLink = xnTextInput.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_LINK,nsmgr);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}		
			
			//
			// Deserialize Modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_TITLE,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_DESCRIPTION,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_NAME,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_LINK,namespaceUri) );			
			
            this.Modules = new SyndicationModuleCollection(xnTextInput, exclusionElements);
		}
		#endregion

		#region About Helper Methods
		/// <summary>
		/// Returnes the about value.
		/// </summary>
		/// <returns></returns>
		internal string GetAbout() {
			if ( this.About != null ) {
				return this.About;
			}
			else if ( this.Link != null ) {
				return this.Link;
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
		internal RdfChannelTextInput GetChannelTextInput() {
			return new RdfChannelTextInput(this.GetAbout());
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRdf,RdfVersion version,string prefix) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return SerializeToXml_1_0(xdRdf,prefix);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object according to RSS version 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_1_0(XmlDocument xdRdf,string prefix) {
			XmlAttribute xaAbout = xdRdf.CreateAttribute(prefix,RDF_ATTRIBUTE_ABOUT,xdRdf.DocumentElement.NamespaceURI);
			xaAbout.InnerText = GetAbout();
					
			XmlElement xeTextInput = xdRdf.CreateElement(null,RDF_ELEMENT_TEXTINPUT,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);			
			xeTextInput.Attributes.Append(xaAbout);
			//
			// Required Elements
			//
			if ( this.Title != null ) {
				xeTextInput.AppendChild( SerializeTitleToXml(xdRdf) );
			}
			if ( this.Description != null ) {
				xeTextInput.AppendChild( SerializeDescriptionToXml(xdRdf) );				
			}
			if ( this.Name != null ) {
				xeTextInput.AppendChild( SerializeNameToXml(xdRdf) );
			}
			if ( this.Link != null ) {
				xeTextInput.AppendChild( SerializeLinkToXml(xdRdf) );				
			}
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeTextInput);
			}
			return xeTextInput;
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
		/// Serializes Description To XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDescriptionToXml(XmlDocument xdRdf) {
			XmlElement xeDescription = xdRdf.CreateElement(null,RDF_ELEMENT_DESCRIPTION,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeDescription.InnerText = this.Description;
			return xeDescription;
		}
		/// <summary>
		/// Serializes Name to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeNameToXml(XmlDocument xdRdf) {
			XmlElement xeName = xdRdf.CreateElement(null,RDF_ELEMENT_NAME,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeName.InnerText = this.Name;
			return xeName;
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
		/// Description backing field.
		/// </summary>
		private string m_description;
		/// <summary>
		/// Get/Set Description.
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
		/// Get/Set Name.
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
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_TEXTINPUT,RDF_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			if ( this.Description == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_TEXTINPUT,RDF_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}
			if ( this.Name == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_TEXTINPUT,RDF_ELEMENT_NAME);
				throw new SyndicationValidationException(msg);
			}			
			if ( this.Link == null ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID,RDF_ELEMENT_TEXTINPUT,RDF_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}

			if ( validateContent ) {
				ValidateLink(RdfVersion.RDF_1_0);
				ValidateAbout(RdfVersion.RDF_1_0,rac);
			}
			
			return true;
		}
		/// <summary>
		/// Validates about attribute.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="rac"></param>
		private void ValidateAbout(RdfVersion version, RdfAboutCollection rac) {
			if ( version == RdfVersion.RDF_1_0 ) {
				if ( !rac.IsUnique(this.GetAbout()) ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_NOT_UNIQUE_ABOUT_1_0,RDF_ELEMENT_TEXTINPUT);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// 
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
