using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// Summary description for RdfChannelItems.
	/// </summary>
	public sealed class RdfChannelItems {

		#region Private Const Data
		private const string RDF_ELEMENT_ITEMS = "items";		
		private const string RDF_ELEMENT_SEQ = "Seq";
		private const string RDF_ELEMENT_LI = "li";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfChannelItems() {			
			this.Modules = new SyndicationModuleCollection();
			this.Sequence = new RdfSequence();
		}
		/// <summary>
		/// Internal Deserialization Constructor.
		/// </summary>
		/// <param name="xnItems"></param>
		internal RdfChannelItems(XmlNode xnItems) {
			if ( xnItems == null ) {
				throw new ArgumentNullException("xnItems");
			}
			this.DeserializeFromXml(xnItems);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnItems"></param>
		private void DeserializeFromXml(XmlNode xnItems) {
			//
			// Set Namespace Manager.
			//
			string namespaceUri = xnItems.OwnerDocument.DocumentElement.NamespaceURI;
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnItems.OwnerDocument.NameTable);				
			nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);		

			XmlNode xnSequence = xnItems.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_SEQ,nsmgr);
			if ( xnSequence != null ) {
				this.Sequence = new RdfSequence(xnSequence);
			}

			//
			// Deserialize modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_SEQ,namespaceUri) );						
						
			this.Modules = new SyndicationModuleCollection(xnItems,exclusionElements);
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRdf"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRdf, RdfVersion version,string prefix) {
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
			XmlElement xeItem = xdRdf.CreateElement(null,RDF_ELEMENT_ITEMS,xdRdf.DocumentElement.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText);

			if ( this.Sequence != null ) {
				xeItem.AppendChild( this.Sequence.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}

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
		/// Sequence backing field.
		/// </summary>
		private RdfSequence m_rdfSequence;
		/// <summary>
		/// Get/Set Sequence.
		/// </summary>
		public RdfSequence Sequence {
			get {
				return m_rdfSequence;
			}
			set {
				m_rdfSequence = value;
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
			if ( this.Sequence != null ) {
				this.Sequence.Validate(RdfVersion.RDF_1_0,validateContent,rac,racItems);
			}
			return true;
		}
		#endregion
	}// class
}// namespace
