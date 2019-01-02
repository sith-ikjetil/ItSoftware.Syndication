using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomAuthorCollection : List<AtomAuthor>, IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomAuthorCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlAuthors"></param>
        internal AtomAuthorCollection(XmlNodeList xnlAuthors)
        {
            if (xnlAuthors == null)
            {
                throw new ArgumentNullException("xnlAuthors");
            }
            this.DeserializeFromXml(xnlAuthors);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlAuthors"></param>
        private void DeserializeFromXml(XmlNodeList xnlAuthors)        
        {
            foreach (XmlNode xnAuthor in xnlAuthors)
            {
                this.Add(new AtomAuthor(xnAuthor));
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
            foreach (AtomAuthor author in this)
            {
                author.Validate(version, validateContent);
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
            foreach (AtomAuthor author in this)
            {
                nodes.Add(author.SerializeToXml(version, xdAtom));
            }
            return nodes;
        }
        #endregion
    }// class
}// namespace
