using System;
using System.Xml;
using System.Collections.Generic;

namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// RssSkipDay collection.
	/// </summary>
	public sealed class RssSkipDayCollection : List<RssSkipDay> {

		#region Private Const Data
		private const string RSS_ELEMENT_DAY = "day";
		private const string RSS_ELEMENT_SKIPDAY = "skipDays";		
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor
		/// </summary>
		public RssSkipDayCollection() {		
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnSkipHours"></param>
		internal RssSkipDayCollection(XmlNode xnSkipHours) {		
			this.DeserializeFromXml(xnSkipHours);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the collection from XML.
		/// </summary>
		/// <param name="xnSkipDays"></param>
		private void DeserializeFromXml(XmlNode xnSkipDays) {
			XmlNodeList xnlSkipDays = xnSkipDays.SelectNodes(RSS_ELEMENT_DAY);
			foreach ( XmlNode node in xnlSkipDays ) {
				this.Add( new RssSkipDay(node) );
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the collection to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRss,RssVersion version) {
			XmlElement xeSkipDays = xdRss.CreateElement(RSS_ELEMENT_SKIPDAY);
			foreach ( RssSkipDay skipDay in this ) {
				xeSkipDays.AppendChild(skipDay.SerializeToXml(xdRss,version));
			}
			return xeSkipDays;
		}
		#endregion

		#region Validation Methods
		internal bool Validate(RssVersion version, bool validateContent) {
			foreach ( RssSkipDay skipDay in this ) {
				skipDay.Validate(version,validateContent);
			}			
			return true;
		}
		#endregion

				
	}// class
}// namespace
