using System;
using System.Xml;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Syndication module element.
	/// </summary>
	public sealed class SyndicationModuleAttribute {								
		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="innerText"></param>
		public SyndicationModuleAttribute(string prefix, string namespaceURI, string name, string innerText) {			
			this.m_prefix = NormalizePrefix(prefix);
			this.m_namespaceURI = NormalizeNamespaceUri(namespaceURI);
			this.Name = name;
			this.InnerText = innerText;

			//
			// The following combination is not valid. You cannot
			// have a module with a prefix but not a namespace.
			//
			if ( !IsValidNamespaceURI() && IsValidPrefix() ) {
				string msg = string.Format("SyndicationModuleAttribute cannot have a prefix and not a namespace.");
				throw new ArgumentException(msg,"prefix,namespaceUri");
			}
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <remarks>
		/// If a node has more than or equal to 2 child nodes, then
		/// it contains xml and therefore the InnerXml should be set
		/// last. If the element contains 1 child node and that child
		/// node contains more than 0 child nodes than again it contains
		/// XML and should have it InnerXml set last. Otherwise set InnerText
		/// last.
		/// </remarks>
		/// <param name="xeElement"></param>
		internal SyndicationModuleAttribute(XmlAttribute xa) {			
			if ( xa == null ) {
				throw new ArgumentNullException("xa");
			}
			this.DeserializeFromXml(xa);			
		}	
		#endregion				

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xa"></param>
        private void DeserializeFromXml(XmlAttribute xa) {
			this.Name = xa.LocalName;							
			this.m_namespaceURI = xa.NamespaceURI;
			this.m_prefix = xa.Prefix;
			this.m_innerText = xa.InnerText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private string NormalizePrefix(string prefix)
        {
            if (prefix == string.Empty)
            {
                return null;
            }
            return prefix;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        private string NormalizeNamespaceUri(string namespaceUri)
        {
            if (namespaceUri == string.Empty)
            {
                return null;
            }
            return namespaceUri;
        }
        /// <summary>
        /// Returnes true if prefix is valid.
        /// </summary>
        /// <returns></returns>
        private bool IsValidPrefix()
        {
            if (this.Prefix == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Returnes true if namespace is valid.
        /// </summary>
        /// <returns></returns>
        private bool IsValidNamespaceURI()
        {
            if (this.NamespaceURI == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Serialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeElement"></param>
		internal void SerializeToXml(XmlElement xeElement) {
			XmlAttribute xaNewAttribute = xeElement.OwnerDocument.CreateAttribute(this.Prefix,this.Name,this.NamespaceURI);
			if ( this.InnerText != null ) {
				xaNewAttribute.InnerText = this.InnerText;
			}
			xeElement.Attributes.Append(xaNewAttribute);
		}
		#endregion
        	
		#region Public Properties	
		/// <summary>
		/// Prefix backing field.
		/// </summary>
		private string m_prefix;
		/// <summary>
		/// Get Prefix.
		/// </summary>
		public string Prefix {
			get {
				return m_prefix;
			}
		}
		/// <summary>
		/// NamespaceURI backing field.
		/// </summary>
		private string m_namespaceURI;
		/// <summary>
		/// Get NamespaceURI.
		/// </summary>
		public string NamespaceURI {
			get {
				return m_namespaceURI;
			}
		}
		/// <summary>
		/// Name backing field.
		/// </summary>
		private string m_name;
		/// <summary>
		/// Gets the element name.
		/// </summary>
		public string Name {
			get {
				return m_name;
			}			
			set {
				if ( value == null ) {
					throw new ArgumentNullException("name");
				}
				if ( value == string.Empty ) {
					throw new ArgumentException("Name must have a value.","name");
				}
				m_name = value;
			}
		}		
		/// <summary>
		/// InnerText backing field.
		/// </summary>
		private string m_innerText;
		/// <summary>
		/// Gets or sets the elements inner text.
		/// </summary>
		public string InnerText {
			get {
				return m_innerText;			
			}	
			set {
				m_innerText = value;
			}
		}
		#endregion
	}// class
}// namespace
