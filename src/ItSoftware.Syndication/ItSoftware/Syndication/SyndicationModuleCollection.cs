using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
namespace ItSoftware.Syndication {
	/// <summary>
	/// 
	/// </summary>
	public sealed class SyndicationModuleCollection : IEnumerable {
		
		#region Private Fields
		private ArrayList m_items = new ArrayList();
		#endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public SyndicationModuleCollection() {			
		}
        /// <summary>
        /// Internal deserialization constructor.
        /// </summary>
        /// <param name="xnElement"></param>
        /// <param name="exclusionElements"></param>
        internal SyndicationModuleCollection(XmlNode xnElement, List<SyndicationModuleExclusionElement> exclusionElements)
        {
            if (xnElement == null)
            {
                throw new ArgumentNullException("xnElement");
            }
            if (exclusionElements == null)
            {
                throw new ArgumentNullException("exclusionElements");
            }
            this.DeserializeFromXml(xnElement, exclusionElements);
        }
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnElement"></param>
		/// <param name="exclusionElements"></param>
		private void DeserializeFromXml(XmlNode xnElement, List<SyndicationModuleExclusionElement> exclusionElements) {
			foreach ( XmlNode node in xnElement.ChildNodes ) {
				if ( node is XmlElement ) {
					if ( !InExclusionList(node,exclusionElements) ) {
						XmlElement xeNode = node as XmlElement;
                        SyndicationModule module;
						if ( IsNamespaceInDocumentElement(node.Prefix,node.NamespaceURI,xnElement.OwnerDocument.DocumentElement) ) {
                            module = new SyndicationModule(node.Prefix, node.NamespaceURI, SyndicationModuleNamespacePosition.DocumentElement);
						}
						else {
							module = new SyndicationModule(node.Prefix,node.NamespaceURI,SyndicationModuleNamespacePosition.Element);                            
						}
                        this.Add(module);			
						module.Elements.Add( new SyndicationModuleElement(xeNode) );
					}// if
				}// if ( node is XmlElement )
			}// foreach
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Returnes true if namespace is in document element. Otherwise 
		/// returnes false.
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="namespaceUri"></param>
		/// <param name="documentElement"></param>
		/// <returns></returns>
		private bool IsNamespaceInDocumentElement(string prefix, string namespaceUri,XmlElement documentElement) {
			if ( prefix == null || prefix == string.Empty ) {
				return false;
			}
			if ( namespaceUri == null || namespaceUri == string.Empty ) {
				return false;
			}
			string match = "xmlns:" + prefix;
			foreach ( XmlAttribute attribute in documentElement.Attributes ) {
				if ( attribute.Name == match ) {
					if ( attribute.InnerText == namespaceUri ) {
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Returnes true if node is in exclusionElements array.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="exclusionElements"></param>
		/// <returns></returns>
		private bool InExclusionList(XmlNode node, List<SyndicationModuleExclusionElement> exclusionElements) {
			foreach ( SyndicationModuleExclusionElement exclusionElement in exclusionElements ) {
				if ( node.NamespaceURI == exclusionElement.NamespaceURI ) {
					if (  node.LocalName == exclusionElement.Name ) {
						return true;
					}
				}
			}
			return false;
		}
		#endregion

		#region Serialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeElement"></param>
		internal void SerializeToXml(XmlElement xeElement) {
			foreach ( SyndicationModule module in this ) {
				module.SerializeToXml(xeElement);
			}
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the number of items in this collection.
		/// </summary>
		public int Count {
			get {
				return m_items.Count;
			}
		}
		/// <summary>
		/// Gets the specified SyndicationModule with given prefix, namespaceUri and namespacePosition.
		/// </summary>
		public SyndicationModule this[string prefix, string namespaceUrl, SyndicationModuleNamespacePosition namespacePosition] {
			get {
				foreach ( SyndicationModule module in this ) {
					if ( module.Prefix == prefix &&
						 module.NamespaceURI == namespaceUrl &&
						 module.NamespacePosition == namespacePosition ) 
					{
						return module;
					}
				}
				return null;
			}
		}	
		/// <summary>
		/// Gets the SyndicationModule at specified index.
		/// </summary>
		public SyndicationModule this[int index] {
			get {
				return m_items[index] as SyndicationModule;
			}						
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clears the collection.
		/// </summary>
		public void Clear() {			
			m_items.Clear();
		}		
        /// <summary>
        /// Finds an existing equal module or returnes null.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="namespaceUri"></param>
        /// <param name="namespacePosition"></param>
        /// <returns></returns>
        public SyndicationModule Add(SyndicationModule newModule)
        {
            foreach (SyndicationModule module in this)
            {
                if (module.Prefix == newModule.Prefix)
                {
                    string paramName = "namespaceUri";
                    if (module.NamespaceURI == newModule.NamespaceURI)
                    {
                        if (module.NamespacePosition == newModule.NamespacePosition)
                        {
                            return module;
                        }
                        else
                        {
                            paramName = "namespacePosition";
                        }
                    }
                    string msg = string.Format("Illegal to redefine an existing module. Module with prefix already exists.\r\nPrefix: {0}\r\nIllegal parameter: {1}", ((newModule.Prefix == null) ? "" : newModule.Prefix), paramName);
                    throw new ArgumentException(msg, "prefix");
                }
            }
            m_items.Add(newModule);
            return newModule;
        }
		#endregion

		#region Private Methods
		/// <summary>
		/// To a valid value.
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		private string ToValidModuleValue(string val) {
			if ( val == string.Empty ) {
				return null;
			}
			return val;
		}
		#endregion
		
		#region IEnumerable Interface Members
		/// <summary>
		/// IEnumerable interface member.
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() {
			return m_items.GetEnumerator();			
		}
		#endregion
	}// class
}// namespace
