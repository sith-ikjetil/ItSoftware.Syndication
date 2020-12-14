using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfSequenceItem class.
	/// </summary>
	public sealed class RdfSequenceItem {

		#region Private Const Data
		private const string RDF_ELEMENT_SEQUENCE_ITEM = "li";
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		private const string RDF_ATTRIBUTE_RESOURCE = "resource";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		#endregion

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfSequenceItem() {	
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="resource"></param>
		public RdfSequenceItem(string resource) : this() {
			this.Resource = resource;
		}
		/// <summary>
		/// Internal Deserialization Constructor.
		/// </summary>
		/// <param name="xnItem"></param>
		internal RdfSequenceItem(XmlNode xnItem) {
			if ( xnItem == null ) {
				throw new ArgumentNullException("xnItem");
			}
			this.DeserializeFromXml(xnItem);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnItem"></param>
		private void DeserializeFromXml(XmlNode xnItem) {
			//
			// Set Namespace Manager.
			//
			string namespaceUri = xnItem.OwnerDocument.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText;

			//
			// Resource.
			//
			XmlAttribute xaResource = xnItem.Attributes[RDF_ATTRIBUTE_RESOURCE];
			if ( xaResource != null ) {
				this.Resource = xaResource.InnerText;
			}

			//
			// Deserialize Modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_SEQUENCE_ITEM,namespaceUri) );			
						
            this.Modules = new SyndicationModuleCollection(xnItem, exclusionElements);
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRdf"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRdf,RdfVersion version,string prefix) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return SerializeToXml_1_0(xdRdf,prefix);
			}
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object according to RDF 1.0.
		/// </summary>
		/// <param name="xdRdf"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_1_0(XmlDocument xdRdf,string prefix) {			
			XmlAttribute xaResource = xdRdf.CreateAttribute(RDF_ATTRIBUTE_RESOURCE);
			if ( this.Resource != null ) {
				xaResource.InnerText = this.Resource;
			}			
					
			XmlElement xeItem = xdRdf.CreateElement(prefix,RDF_ELEMENT_SEQUENCE_ITEM,xdRdf.DocumentElement.NamespaceURI);
			xeItem.Attributes.Append(xaResource);			
			//
			// Always serialize modules last.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeItem);
			}
			return xeItem;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Resource backing field.
		/// </summary>
		private string m_resource;
		/// <summary>
		/// Get/Set Resource.
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

		#region Validation Methods
		/// <summary>
		/// Validates the item object.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RdfVersion version, bool validateContent,RdfAboutCollection rac,RdfAboutCollection racItems) {
			if ( version == RdfVersion.RDF_1_0 ) {
				return Validate_1_0(validateContent,rac,racItems);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");		
		}	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		private bool Validate_1_0(bool validateContent,RdfAboutCollection rac,RdfAboutCollection racItems) {			
			if  ( racItems.HitRate(this.Resource) != 1 ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_SEQUENCE_INVALID_RESOURCE);
				throw new SyndicationValidationException(msg);
			}
			return true;
		}
		#endregion

	}// class
}// namespace
