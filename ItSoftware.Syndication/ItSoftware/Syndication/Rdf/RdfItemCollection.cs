using System;
using System.Xml;
using System.Collections.Generic;

namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfItemCollection class.
	/// </summary>
	public sealed class RdfItemCollection : List<RdfItem> {
		
		#region Private Const Data
		private const string RDF_ELEMENT_ITEMS = "items";
		private const string RDF_ELEMENT_SEQ_PREFIX = "rdf";
		private const string RDF_ELEMENT_SEQ = "Seq";
		private const string RDF_ELEMENT_LI_PREFIX = "rdf";
		private const string RDF_ELEMENT_LI = "li";
		private const string RDF_ATTRIBUTE_RESOURCE = "resource";
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RdfItemCollection() {			
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnlItems"></param>
		internal RdfItemCollection(XmlNodeList xnlItems) {
			if ( xnlItems == null ) {
				throw new ArgumentNullException("xnlItems");
			}
			this.DeserializeFromXml(xnlItems);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the collection from XML.
		/// </summary>
		/// <param name="xnlItems"></param>
		private void DeserializeFromXml(XmlNodeList xnlItems) {            
			foreach ( XmlNode node in xnlItems ) {
				this.Add( new RdfItem(node) );
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		internal void SerializeToXml(XmlDocument xdRdf,RdfVersion version,XmlElement xeRdf,string prefix) {            
			if ( version == RdfVersion.RDF_1_0 ) {
				SerializeToXml_1_0(xdRdf,xeRdf,prefix);
				return;
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the object according to RDF 1.0.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_1_0(XmlDocument xdRdf, XmlElement xeRdf,string prefix) {
            
			for ( int i = 0; i < this.Count;i++) {
				xeRdf.AppendChild( this[i].SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix) );
			}
		}		
		#endregion

		#region Validation Methods
		/// <summary>
		/// Validates the collection.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RdfVersion version, bool validateContent,RdfAboutCollection rac) {
			if ( version == RdfVersion.RDF_1_0 ) {
				if ( this.Count < 1 ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_ILLEGAL_NUMBER_OF_ITEMS_1_0);
					throw new SyndicationValidationException(msg);
				}	
				foreach ( RdfItem item in this ) {
					item.Validate(version,validateContent,rac);
				}			
			}
			return true;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returnes a RdfChannelItems of the items in the collection.
		/// </summary>
		/// <param name="xdRdf"></param>
		/// <returns></returns>
		internal RdfChannelItems GetChannelItems() {
			RdfChannelItems items = new RdfChannelItems();					
			foreach ( RdfItem item in this ) {
				items.Sequence.Add(new RdfSequenceItem(item.Link) );				
			}			
			return items;
		}		
		#endregion				

		#region About Methods
		/// <summary>
		/// Returnes all about's in channel.
		/// </summary>
		/// <returns></returns>
		internal RdfAboutCollection GetAbouts() {
			RdfAboutCollection rac = new RdfAboutCollection();
			foreach ( RdfItem item in this ) {
				rac.Add( item.GetAbouts() );
			}
			return rac;			
		}
		#endregion

	}// class
}// namespace
