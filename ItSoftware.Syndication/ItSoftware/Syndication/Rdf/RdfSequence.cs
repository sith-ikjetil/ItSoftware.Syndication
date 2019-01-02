using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfSequence class.
	/// </summary>
	public sealed class RdfSequence : IEnumerable {

		#region Private Data
		private ArrayList m_items = new ArrayList();
		#endregion

		#region Private Const Data		
		private const string RDF_ELEMENT_SEQ = "Seq";
		private const string RDF_ELEMENT_LI = "li";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		#endregion

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfSequence() {		
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Internal Deserialization Constructor.
		/// </summary>
		/// <param name="xnSequence"></param>
		internal RdfSequence(XmlNode xnSequence) {
			if ( xnSequence == null ) {
				throw new ArgumentNullException("xnSequence");
			}
			this.DeserializeFromXml(xnSequence);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnSequence"></param>
		private void DeserializeFromXml(XmlNode xnSequence) {
			//
			// Set Namespace Manager.
			//
			string namespaceUri = xnSequence.OwnerDocument.DocumentElement.NamespaceURI;
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnSequence.OwnerDocument.NameTable);				
			nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);		

			//
			// Items.
			//
			XmlNodeList xnlItems = xnSequence.SelectNodes(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_LI,nsmgr);
			if ( xnlItems != null ) {
				foreach ( XmlNode node in xnlItems ) {
					this.Add( new RdfSequenceItem(node) );				
				}
			}

			//
			// Deserialize modules.
			//		
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_SEQ,namespaceUri) );			
			exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_LI,namespaceUri) );			
						
            this.Modules = new SyndicationModuleCollection(xnSequence, exclusionElements);
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
			XmlElement xeSeq = xdRdf.CreateElement(prefix,RDF_ELEMENT_SEQ,xdRdf.DocumentElement.NamespaceURI);
			foreach ( RdfSequenceItem item in this ) {				
				xeSeq.AppendChild( item.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}		
			return xeSeq;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// IEnumerable.GetEnumerator
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() {
			return m_items.GetEnumerator(); 
		}
		/// <summary>
		/// Add an item to the collection.
		/// </summary>
		/// <param name="item"></param>
		public void Add( RdfSequenceItem item ) {
			m_items.Add( item );
		}
		/// <summary>
		/// Clear the collection of items.
		/// </summary>
		public void Clear() {
			m_items.Clear();
		}
		#endregion

		#region Public Properties and Indexers
		/// <summary>
		/// Get the number of items in the collection.
		/// </summary>
		public int Count {
			get {
				return m_items.Count;
			}
		}
		/// <summary>
		/// Indexer.
		/// </summary>
		public RdfSequenceItem this[int index] {
			get {
				return m_items[index] as RdfSequenceItem;
			}
			set {
				m_items[index] = value;
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
			if ( this.Count != racItems.Count ) {
				string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_SEQUENCE_INVALID_NUMBER_OF_ITEMS);
				throw new SyndicationValidationException(msg);
			}
			foreach ( RdfSequenceItem item in this ) {
				item.Validate(RdfVersion.RDF_1_0,validateContent,rac,racItems);
			}
			return true;
		}
		#endregion
	}// class
}// namespace
