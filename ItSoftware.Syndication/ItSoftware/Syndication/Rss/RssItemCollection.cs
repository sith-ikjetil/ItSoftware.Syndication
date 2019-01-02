using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// RssItem collection.
	/// </summary>
	public sealed class RssItemCollection : List<RssItem> {		

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RssItemCollection() {			
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnlItems"></param>
		internal RssItemCollection(XmlNodeList xnlItems) {
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
				this.Add( new RssItem(node) );
			}            
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		internal void SerializeToXml(XmlDocument xdRss,RssVersion version,XmlElement xeChannel) {
			if ( version == RssVersion.RSS_0_91 ) {
				SerializeToXml_0_91(xdRss,xeChannel);
				return;
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				SerializeToXml_0_92(xdRss,xeChannel);
				return;
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				SerializeToXml_2_0_1(xdRss,xeChannel);
				return;
			}
            throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE, "version");
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_0_91(XmlDocument xdRss, XmlElement xeChannel) {            
            for (int i = 0; i < Rss.RSS_MAXVALUE_ITEM_MAX_NUMBER_OF_0_91; i++)
            {
				xeChannel.AppendChild( this[i].SerializeToXml(xdRss,RssVersion.RSS_0_91) );
			}
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_0_92(XmlDocument xdRss, XmlElement xeChannel) {            
			foreach ( RssItem item in this) { 
				xeChannel.AppendChild( item.SerializeToXml(xdRss,RssVersion.RSS_0_92) );
			}
		}
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_2_0_1(XmlDocument xdRss, XmlElement xeChannel) {           
			foreach ( RssItem item in this) { 
				xeChannel.AppendChild( item.SerializeToXml(xdRss,RssVersion.RSS_2_0_1) );
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
		internal bool Validate(RssVersion version, bool validateContent) {
			foreach ( RssItem item in this ) {
				item.Validate(version,validateContent);
			}			
			return true;
		}
		#endregion

	}// class
}// namespace
