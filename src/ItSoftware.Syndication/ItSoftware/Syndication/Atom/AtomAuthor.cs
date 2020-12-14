using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomAuthor = element atom:author { atomPersonConstruct }    
    /// </summary>
    public class AtomAuthor : AtomPerson
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "author";
        #endregion

        #region Private Const Data
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomAuthor()
        {            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="email"></param>
        public AtomAuthor(string name, string uri, string email) 
            : base(name,uri,email)
        {            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnAuthor"></param>
        internal AtomAuthor(XmlNode xnAuthor) : base(xnAuthor)
        {
            if (xnAuthor == null)
            {
                throw new ArgumentNullException("xnAuthor");
            }
            this.DeserializeFromXml(xnAuthor);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnAuthor"></param>
        private void DeserializeFromXml(XmlNode xnAuthor) {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnAuthor.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnAuthor.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);
                        
            this.Modules = new SyndicationModuleCollection(xnAuthor, base.ExclusionElements);
        }
        #endregion

        #region Protected Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            XmlElement xeAuthor = xdAtom.CreateElement(XML_TAG);
            base.SerializeToXml(AtomVersion.Atom_1_0, xeAuthor);

            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeAuthor);
            }

            return xeAuthor;                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            //
            // Validate base member Name. It is the only required element.
            //
            if (this.Name == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, AtomPerson.XMLTAG_URI);
                throw new SyndicationValidationException(msg);
            }           
        }
        #endregion
    }// class
}// namespace
