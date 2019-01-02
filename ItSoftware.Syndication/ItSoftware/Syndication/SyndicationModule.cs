using System;
using System.Xml;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Syndication module.
	/// </summary>
	public sealed class SyndicationModule {				
		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="namespaceUri"></param>
		/// <param name="namespacePosition"></param>
		public SyndicationModule(string prefix, string namespaceUri, SyndicationModuleNamespacePosition namespacePosition) {			
			this.m_prefix = NormalizePrefix(prefix);
			this.m_namespaceUri = NormalizeNamespaceUri(namespaceUri);
			this.m_namespacePosition = namespacePosition;
			
			//
			// The following combination is not valid. You cannot
			// set a namespace at document-element level and not
			// have a prefix.
			//
			if ( namespacePosition == SyndicationModuleNamespacePosition.DocumentElement && !this.IsValidPrefix() ) {				 
				string msg = string.Format("SyndicationModule cannot have a namespace positioned at DocumentElement without prefix value.");
				throw new ArgumentException(msg,"prefix,namespacePosition");
			}
			//
			// The following combination is not valid. You cannot
			// have a module with a prefix but not a namespace.
			//
			if ( !IsValidNamespaceURI() && IsValidPrefix() ) {
				string msg = string.Format("SyndicationModule cannot have a prefix and not a namespace.");
				throw new ArgumentException(msg,"prefix,namespaceUri");
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the module elements to XML.
		/// </summary>
		/// <param name="xeElement"></param>
		internal void SerializeToXml(XmlElement xeElement) {			
			if ( this.NamespacePosition == SyndicationModuleNamespacePosition.DocumentElement ){
				string prefix = "xmlns:";
				if ( IsValidPrefix() && IsValidNamespaceURI() ) {					
					prefix += this.Prefix;
					try {
						xeElement.OwnerDocument.DocumentElement.SetAttribute(prefix,this.NamespaceURI);
					}
					catch ( Exception x ) {
						string msg = String.Format("Failed to serialize module namespace on document element.\r\n Module data:\r\nPrefix:{0}\r\nNamespace:{1}",this.Prefix,this.NamespaceURI); 
						throw new SyndicationSerializationException(msg,x);
					}
				}				
			}
			this.Elements.SerializeToXml(xeElement,this.Prefix,this.NamespaceURI);			
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		private string NormalizePrefix(string prefix) {
			if ( prefix == string.Empty ) {
				return null;
			}
			return prefix;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="namespaceUri"></param>
		/// <returns></returns>
		private string NormalizeNamespaceUri(string namespaceUri) {
			if ( namespaceUri == string.Empty ) {
				return null;
			}
			return namespaceUri;
		}
		/// <summary>
		/// Returnes true if prefix is valid.
		/// </summary>
		/// <returns></returns>
		private bool IsValidPrefix() {
			if ( this.Prefix == null ) {
				return false;
			}
			return true;
		}
		/// <summary>
		/// Returnes true if namespace is valid.
		/// </summary>
		/// <returns></returns>
		private bool IsValidNamespaceURI() {
			if ( this.NamespaceURI == null ) {
				return false;
			}
			return true;
		}
		#endregion		
	
		#region Public Properties
		/// <summary>
		/// Prefix backing field.
		/// </summary>
		private string m_prefix;
		/// <summary>
		/// Gets or sets the namespace prefix.
		/// </summary>
		public string Prefix {
			get {
				return m_prefix;
			}				
		}
		/// <summary>
		/// 
		/// </summary>
		private string m_namespaceUri;
		/// <summary>
		/// Gets or sets the namespace URL.
		/// </summary>
		public string NamespaceURI {
			get {
				return m_namespaceUri;
			}			
		}
		/// <summary>
		/// NamespacePosition backing field.
		/// </summary>
		private SyndicationModuleNamespacePosition m_namespacePosition;
		/// <summary>
		/// Gets or sets the namespace position.
		/// </summary>
		public SyndicationModuleNamespacePosition NamespacePosition {
			get {
				return m_namespacePosition;
			}			
		}		
		/// <summary>
		/// Elements backing field.
		/// </summary>
		private SyndicationModuleElementCollection m_elements = new SyndicationModuleElementCollection();
		/// <summary>
		/// Get/Set Elements.
		/// </summary>
		public SyndicationModuleElementCollection Elements {
			get {
				return m_elements;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException("Elements");
				}
				m_elements = value;
			}
		}
		#endregion

        /*#region Public Static Methods
        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="module"></param>
        public static SyndicationModule CreateModule(string prefix, string namespaceUri, SyndicationModuleNamespacePosition namespacePosition)
        {
            return new SyndicationModule(prefix, namespaceUri, namespacePosition);            
        }
        #endregion
         * */
    }// class
}// namspace
