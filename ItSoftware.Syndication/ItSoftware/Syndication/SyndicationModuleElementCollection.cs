using System;
using System.Xml;
using System.Collections;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Summary description for SyndicationModuleAttributeCollection.
	/// </summary>	
	public sealed class SyndicationModuleElementCollection : IEnumerable {
		private ArrayList m_items = new ArrayList();


		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public SyndicationModuleElementCollection() {
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="attributes"></param>
		public SyndicationModuleElementCollection(SyndicationModuleElement[] elements) {
			if ( elements == null ) {
				throw new ArgumentNullException("elements");
			}
			foreach ( SyndicationModuleElement element in elements ) {
				this.Add( element );
			}
		}
		/// <summary>
		/// Internal Constructor.
		/// </summary>
		/// <param name="xnElement"></param>
		internal SyndicationModuleElementCollection(XmlNode xnElement) {
		}
		#endregion

        #region Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeElement"></param>
        /// <param name="prefix"></param>
        /// <param name="namespaceURI"></param>
        internal void SerializeToXml(XmlElement xeElement,string prefix, string namespaceURI) {
			foreach ( SyndicationModuleElement element in this ) {
				element.SerializeToXml(xeElement,prefix,namespaceURI);				
			}
        }
        #endregion

        #region IEnumerable Members
        public IEnumerator GetEnumerator() {
			return m_items.GetEnumerator();
		}
		#endregion

		#region Public Properties and Indexers
		public int Count {
			get {
				return m_items.Count;
			}
		}
		public SyndicationModuleElement this[int index] {
			get {
				return m_items[index] as SyndicationModuleElement;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Removes an element from the collection.
		/// </summary>
		/// <param name="elementName"></param>
		public void Remove( string elementName ) {
			if ( elementName == null ) {
				throw new ArgumentNullException("name");
			}
			if ( elementName.Length == 0 ) {
				throw new ArgumentException("Length cannot be 0.","name");
			}
			for ( int i = 0; i < this.Count; i++ ) {
				if ( this[i].Name == elementName ) {
					m_items.RemoveAt(i);
					break;
				}
			}			
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
		public void Add(SyndicationModuleElement element) {
			if ( element == null ) {
				throw new ArgumentNullException("element");
			}
			m_items.Add( element );
		}
        /// <summary>
        /// 
        /// </summary>
		public void Clear() {
			m_items.Clear();
		}
		/// <summary>
		/// Get an element from the collection.
		/// </summary>
		public SyndicationModuleElement this[string elementName] {
			get {
				foreach ( SyndicationModuleElement element in this ) {
					if ( element.Name == elementName ) {
						return element;
					}
				}
				return null;
			}
		}
		#endregion
	}// class
}// namespace
