using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// atomContributor = element atom:contributor { atomPersonConstruct }
    /// </summary>
    public class AtomContributor : AtomPerson
    {
        #region Internal Static ReadOnly Fields
        internal static readonly string XML_TAG = "contributor";
        #endregion

        #region Private Const Data
        private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
        private const string ATOM_ATTRIBUTE_XMLNS = "xmlns";
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomContributor()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="email"></param>
        public AtomContributor(string name, string uri, string email)
            : base(name, uri, email)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnContributor"></param>
        internal AtomContributor(XmlNode xnContributor)
            : base(xnContributor)
        {
            if (xnContributor == null)
            {
                throw new ArgumentNullException("xnContributor");
            }
            this.DeserializeFromXml(xnContributor);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnContributor"></param>
        private void DeserializeFromXml(XmlNode xnContributor)
        {
            //
            // Set Namespace Manager.
            //
            string namespaceUri = xnContributor.OwnerDocument.DocumentElement.Attributes[ATOM_ATTRIBUTE_XMLNS].InnerText;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnContributor.OwnerDocument.NameTable);
            nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS, namespaceUri);

            //
            // Deserialize modules.
            //
            this.Modules = new SyndicationModuleCollection(xnContributor, base.ExclusionElements);
        }
        #endregion

        #region Protected Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal override XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            XmlElement xeContributor = xdAtom.CreateElement(XML_TAG);
            base.SerializeToXml(AtomVersion.Atom_1_0, xeContributor);
                        
            //
            // Always serialize modules last.
            //
            if (this.Modules != null)
            {
                this.Modules.SerializeToXml(xeContributor);
            }

            return xeContributor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal override void ValidateAtom_1_0(bool validateContent)
        {
            if (this.Name == null)
            {
                string msg = string.Format(Atom.ATOM_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID, XML_TAG, AtomPerson.XMLTAG_URI);
                throw new SyndicationValidationException(msg);
            }
        }
        #endregion
    }// class
}// namespace
