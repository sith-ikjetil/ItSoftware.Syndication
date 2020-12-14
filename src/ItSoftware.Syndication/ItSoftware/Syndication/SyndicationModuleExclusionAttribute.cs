using System;

namespace ItSoftware.Syndication {
	/// <summary>
	/// Exclusion element.
	/// </summary>
	internal sealed class SyndicationModuleExclusionAttribute {

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="namespaceUri"></param>
		public SyndicationModuleExclusionAttribute(string name, string namespaceUri) {
			this.m_name = NormalizeName(name);
			this.m_namespaceUri = NormalizeNamespaceUri(namespaceUri);
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
		#endregion

	}// class
}// namespace
