using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Summary description for SyndicationModuleAttributeCollection.
	/// </summary>	
	public sealed class SyndicationModuleAttributeCollection : List<SyndicationModuleAttribute>  {
		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public SyndicationModuleAttributeCollection() {
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="attributes"></param>
		public SyndicationModuleAttributeCollection(SyndicationModuleAttribute[] attributes) {
			if ( attributes == null ) {
				throw new ArgumentNullException("attributes");
			}
			foreach ( SyndicationModuleAttribute attribute in attributes ) {
				this.Add( attribute );
			}
		}
		/// <summary>
		/// Internal Constructor.
		/// </summary>
		/// <param name="xnElement"></param>
		internal SyndicationModuleAttributeCollection(XmlNode xnElement) {
			if ( xnElement == null ) {
				throw new ArgumentNullException("xnElement");
			}
			this.DeserializeFromXml(xnElement);
		}
		#endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnElement"></param>
        private void DeserializeFromXml(XmlNode xnElement) {
			foreach ( XmlAttribute xa in xnElement.Attributes ) {
				this.Add( new SyndicationModuleAttribute(xa) );
			}
        }
        #endregion

        #region Serialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeElement"></param>
        internal void SerializeToXml(XmlElement xeElement) {
			foreach ( SyndicationModuleAttribute attribute in this ) {
				attribute.SerializeToXml(xeElement);
			}
		}
		#endregion		
	}// class
}// namespace
