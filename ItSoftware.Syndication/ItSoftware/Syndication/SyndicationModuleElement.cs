using System;
using System.Xml;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Syndication module element.
	/// </summary>
	public sealed class SyndicationModuleElement {

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="innerText"></param>
		public SyndicationModuleElement(string name, string @value, SyndicationModuleElementValueType valueType) {			
			this.Name = name;			
			if ( valueType == SyndicationModuleElementValueType.InnerText ) {
				this.InnerText = @value;
			}
			else if ( valueType == SyndicationModuleElementValueType.InnerXml ) {
				this.InnerXml = @value;
			}
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="innerText"></param>
		public SyndicationModuleElement(string name, string @value, SyndicationModuleElementValueType valueType, SyndicationModuleAttributeCollection attributes) {
			this.Name = name;						
			this.Attributes = attributes;
			if ( valueType == SyndicationModuleElementValueType.InnerText ) {
				this.InnerText = @value;
			}
			else if ( valueType == SyndicationModuleElementValueType.InnerXml ) {
				this.InnerXml = @value;
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
		internal SyndicationModuleElement(XmlNode xeElement) {
			if ( xeElement == null ) {
				throw new ArgumentNullException("xeElement");
			}
			this.DeserializeFromXml(xeElement);
		}	
		#endregion	
		
        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeElement"></param>
        private void DeserializeFromXml(XmlNode xeElement) {
			this.Name = xeElement.LocalName;				
			if ( xeElement.ChildNodes.Count >= 2 ) {											
				this.InnerText = xeElement.InnerText;
				this.InnerXml = xeElement.InnerXml;
			}
			else if ( xeElement.ChildNodes.Count == 1 ) {
				if ( xeElement.ChildNodes[0].ChildNodes.Count > 0 ) {
					this.InnerText = xeElement.InnerText;
					this.InnerXml = xeElement.InnerXml;
				}
				else {
					this.InnerXml = xeElement.InnerXml;
					this.InnerText = xeElement.InnerText;				
				}
			}
			else {
				this.InnerXml = xeElement.InnerXml;
				this.InnerText = xeElement.InnerText;				
			}
			this.Attributes = new SyndicationModuleAttributeCollection(xeElement);
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
			try {				
				XmlElement xeNewElement = xeElement.OwnerDocument.CreateElement(prefix,this.Name,namespaceURI);
				if ( this.ValueType == SyndicationModuleElementValueType.InnerText ) {
					if ( this.InnerText != null ) {
						xeNewElement.InnerText = this.InnerText;
					}
				}
				else if ( this.ValueType == SyndicationModuleElementValueType.InnerXml ) {
					if ( this.InnerXml != null ) {
						xeNewElement.InnerXml = this.InnerXml;
					}
				}	
				this.Attributes.SerializeToXml(xeNewElement);
				xeElement.AppendChild(xeNewElement);
			}
			catch ( Exception x ) {
				string msg = String.Format("Failed to serialize module element named {0}.\r\nElement data:\r\nPrefix:{1}\r\nNamespace:{2}.",this.Name,((prefix==null)?"":prefix),((namespaceURI==null)?"":namespaceURI));
				throw new SyndicationSerializationException(msg,x);
			}
        }
        #endregion

        #region Public Properties
        /// <summary>
		/// Attributes backing field.
		/// </summary>
		private SyndicationModuleAttributeCollection m_attributes = new SyndicationModuleAttributeCollection();
		/// <summary>
		/// Get Attributes.
		/// </summary>
		public SyndicationModuleAttributeCollection Attributes {
			get {
				return m_attributes;
			}
			set {
				if ( value == null ) {
					throw new ArgumentNullException("attributes");
				}
				m_attributes = value;
			}
		}
		/// <summary>
		/// ValueType backing field.
		/// </summary>
		private SyndicationModuleElementValueType m_valueType = SyndicationModuleElementValueType.InnerText;
		/// <summary>
		/// Gets the value type.
		/// </summary>
		public SyndicationModuleElementValueType ValueType {
			get {
				return m_valueType;
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
					throw new ArgumentException("Value cannot be empty.","name");
				}
				m_name = value;
			}
		}		
		/// <summary>
		/// InnerXml backing field.
		/// </summary>
		private string m_innerXml;
		/// <summary>
		/// Gets or sets the elements inner xml.
		/// </summary>
		public string InnerXml {
			get {
				return m_innerXml;
			}
			set {
				this.m_valueType = SyndicationModuleElementValueType.InnerXml;
				m_innerXml = value;
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
				this.m_valueType = SyndicationModuleElementValueType.InnerText;
				m_innerText = value;
			}
		}
		#endregion
	}// class
}// namespace
