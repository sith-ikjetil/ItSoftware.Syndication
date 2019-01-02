using System;
using System.Xml;
using System.Collections.Generic;

namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// RssSkipHour collection.
	/// </summary>
	public sealed class RssSkipHourCollection : List<RssSkipHour> {

		#region Private Const Data
		private const string RSS_ELEMENT_HOUR = "hour";
		private const string RSS_ELEMENT_SKIPHOURS = "skipHours";		
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RssSkipHourCollection() {		
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnSkipHours"></param>
		internal RssSkipHourCollection(XmlNode xnSkipHours) {		
			this.DeserializeFromXml(xnSkipHours);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnSkipHours"></param>
		private void DeserializeFromXml(XmlNode xnSkipHours) {
			XmlNodeList xnlSkipHours = xnSkipHours.SelectNodes(RSS_ELEMENT_HOUR);
			foreach ( XmlNode node in xnlSkipHours ) {
				this.Add( new RssSkipHour(node) );
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
			XmlElement xeSkipHours = xdRss.CreateElement(RSS_ELEMENT_SKIPHOURS);
			foreach ( RssSkipHour skipHour in this ) {
				xeSkipHours.AppendChild(skipHour.SerializeToXml(xdRss,version));
			}
			return xeSkipHours;
		}
		#endregion

		#region Validation Methods
		internal bool Validate(RssVersion version, bool validateContent) {

			foreach ( RssSkipHour skipHour in this ) {
				skipHour.Validate(version,validateContent);
			}			

			return true;
		}
		#endregion

	}// class
}// namespace
