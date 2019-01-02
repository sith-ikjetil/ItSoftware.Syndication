using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomLinkCollection : List<AtomLink>, IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomLinkCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlLinks"></param>
        internal AtomLinkCollection(XmlNodeList xnlLinks)
        {
            if (xnlLinks == null)
            {
                throw new ArgumentNullException("xnlLinks");
            }
            this.DeserializeFromXml(xnlLinks);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlLinks"></param>
        private void DeserializeFromXml(XmlNodeList xnlLinks) {
            foreach (XmlNode xnLink in xnlLinks)
            {
                this.Add(new AtomLink(xnLink));
            }
        }
        #endregion

        #region IAtomValidate Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="validateContent"></param>
        public void Validate(AtomVersion version, bool validateContent)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                ValidateAtom_1_0(validateContent);
                return;
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_UNKNOWN_VALUE, "version");
        }
        #endregion

        #region Protected Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal virtual void ValidateAtom_1_0(bool validateContent)
        {
            foreach (AtomLink link in this)
            {
                link.Validate(AtomVersion.Atom_1_0, validateContent);
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        internal List<XmlNode> SerializeToXml(AtomVersion version, XmlDocument xdAtom)
        {
            List<XmlNode> nodes = new List<XmlNode>();
            foreach (AtomLink link in this)
            {
                nodes.Add(link.SerializeToXml(version, xdAtom));
            }
            return nodes;
        }
        #endregion
    }// class
}// namespace
