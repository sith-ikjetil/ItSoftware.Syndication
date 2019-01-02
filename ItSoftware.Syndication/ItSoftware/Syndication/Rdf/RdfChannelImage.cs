using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// Summary description for RdfChannelImage.
	/// </summary>
	public sealed class RdfChannelImage {

		#region Private Const Data
		private const string RDF_ELEMENT_IMAGE = "image";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		private const string RDF_ATTRIBUTE_RESOURCE_PREFIX = "rdf";
		private const string RDF_ATTRIBUTE_RESOURCE = "resource";				
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion

		#region Constructors
		/// <summary>
		/// Public Constructor
		/// </summary>
		public RdfChannelImage() {			
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="url"></param>
		public RdfChannelImage(string resource) {
			this.Resource = resource;
		}
		/// <summary>
		/// Deserialization Constructor.
		/// </summary>
		/// <param name="image"></param>
		internal RdfChannelImage(XmlNode xnImage) {
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
			// Resource.
			//			
			XmlAttribute xaResource = xnImage.Attributes[RDF_ATTRIBUTE_RESOURCE,xnImage.OwnerDocument.DocumentElement.NamespaceURI];
			if ( xaResource != null ) {
				this.Resource = xaResource.InnerText;					
			}				
			
			//
			// Deserialize Modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_IMAGE,namespaceUri) );			
						
            this.Modules = new SyndicationModuleCollection(xnImage, exclusionElements);
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
		internal XmlNode SerializeToXml(XmlDocument xdRdf, RdfVersion version, string prefix) {
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
			XmlAttribute xaResource = xdRdf.CreateAttribute(prefix,RDF_ATTRIBUTE_RESOURCE,xdRdf.DocumentElement.NamespaceURI);
			if ( this.Resource != null ) {
				xaResource.InnerText = this.Resource;
			}
					
			XmlElement xeImage = xdRdf.CreateElement(null,RDF_ELEMENT_IMAGE,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);
			xeImage.Attributes.Append(xaResource);			
			
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeImage);
			}
			return xeImage;
		}
		#endregion

		#region Validation Methods
		/// <summary>
		/// Validates the item object.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RdfVersion version, bool validateContent, string imageAbout) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return Validate_1_0(validateContent,imageAbout);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");		
		}	
		/// <summary>
		/// Validate class against RDF version 1.0.
		/// </summary>
		/// <param name="validateContent"></param>
		/// <param name="imageAbout"></param>
		/// <returns></returns>
		private bool Validate_1_0(bool validateContent,string imageAbout) {
			if ( validateContent) {
				if ( this.Resource != imageAbout ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_IMAGE_URLS_NOT_EQUAL);
					throw new SyndicationValidationException(msg);
				}
			}
			return true;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// URL backing field.
		/// </summary>
		private string m_resource;
		/// <summary>
		/// Get/Set URL.
		/// </summary>
		public string Resource {
			get {
				return m_resource;
			}
			set {
				m_resource = value;
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

	}// class
}// namespace
