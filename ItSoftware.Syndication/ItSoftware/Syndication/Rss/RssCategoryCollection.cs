using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Collection of RssCategory objects.
	/// </summary>
	public sealed class RssCategoryCollection : List<RssCategory> {
		#region Private Const Data
		private const string RSS_ELEMENT_CATEGORY = "category";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RssCategoryCollection() {						
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnlCategories"></param>
		internal RssCategoryCollection(XmlNodeList xnlCategories) {			
			this.DeserializeFromXml(xnlCategories);
		}
		#endregion

		#region Deserialization Methods
		private void DeserializeFromXml(XmlNodeList xnlCategories) {
			foreach ( XmlNode xnCategory in xnlCategories ) {
				this.Add( new RssCategory(xnCategory) );
			}
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
		internal void SerializeToXml(XmlDocument xdRss, RssVersion version, XmlElement xeChannelOrItem) {
			if ( version == RssVersion.RSS_0_91 ) {
				SerializeToXml_0_91(xdRss, xeChannelOrItem);
				return;
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				SerializeToXml_0_92(xdRss, xeChannelOrItem);
				return;
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				SerializeToXml_2_0_1(xdRss, xeChannelOrItem);
				return;
			}
			throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}
		/// <summary>
		/// Serializes the collection to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_0_91(XmlDocument xdRss,XmlElement xeChannelOrItem) {
			foreach ( RssCategory category in this ) {
				xeChannelOrItem.AppendChild(category.SerializeToXml(xdRss,RssVersion.RSS_0_91));
			}
		}
		/// <summary>
		/// Serializes the collection to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_0_92(XmlDocument xdRss,XmlElement xeChannelOrItem) {
			foreach ( RssCategory category in this ) {
				xeChannelOrItem.AppendChild(category.SerializeToXml(xdRss,RssVersion.RSS_0_92));
			}
		}
		/// <summary>
		/// Serializes the collection to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="xeChannel"></param>
		private void SerializeToXml_2_0_1(XmlDocument xdRss,XmlElement xeChannelOrItem) {
			foreach ( RssCategory category in this ) {
				xeChannelOrItem.AppendChild(category.SerializeToXml(xdRss,RssVersion.RSS_2_0_1));
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
			foreach ( RssCategory category in this ) {
				category.Validate(version,validateContent);
			}			
			return true;
		}
		#endregion	
	}// class
}// namespace
