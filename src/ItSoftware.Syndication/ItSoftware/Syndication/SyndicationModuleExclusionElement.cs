using System;

namespace ItSoftware.Syndication {
	/// <summary>
	/// Exclusion element.
	/// </summary>
	internal sealed class SyndicationModuleExclusionElement {

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="namespaceUri"></param>
		public SyndicationModuleExclusionElement(string name, string namespaceUri) {
			this.m_name = NormalizeName(name);
			this.m_namespaceUri = NormalizeNamespaceUri(namespaceUri);						
		}
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="namespaceUri"></param>
		public SyndicationModuleExclusionElement(string name, string namespaceUri,SyndicationModuleExclusionAttribute[] attributes) {
			this.m_name = NormalizeName(name);
			this.m_namespaceUri = NormalizeNamespaceUri(namespaceUri);			
			if ( attributes == null ) {
				this.m_attributes = new SyndicationModuleExclusionAttribute[0];
			}
			else {
				this.m_attributes = attributes;
			}
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Normalizes the namespace uri.
		/// </summary>
		/// <param name="namespaceUri"></param>
		/// <returns></returns>
		private string NormalizeNamespaceUri(string namespaceUri) {
			if ( namespaceUri == null ) {
				return string.Empty;
			}
			return namespaceUri;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private string NormalizeName(string name) {
			if ( name == null ) {
				return string.Empty;
			}
			return name;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Name backing field.
		/// </summary>
		private string m_name;
		/// <summary>
		/// Gets or sets the element name.
		/// </summary>
		public string Name {
			get {
				return m_name;
			}			
		}
		/// <summary>
		/// NamespaceURI backing field.
		/// </summary>
		private string m_namespaceUri;
		/// <summary>
		/// Gets the namespace uri.
		/// </summary>
		public string NamespaceURI {
			get {
				return m_namespaceUri;
			}			
		}		
		/// <summary>
		/// Attributes backing field.
		/// </summary>
		private SyndicationModuleExclusionAttribute[] m_attributes = new SyndicationModuleExclusionAttribute[0];
		/// <summary>
		/// Get Attributes.
		/// </summary>
		public SyndicationModuleExclusionAttribute[] Attributes {
			get {
				return m_attributes;
			}
		}
		#endregion

	}// class
}// namespace
