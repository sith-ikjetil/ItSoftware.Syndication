using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomCategoryCollection : List<AtomCategory>, IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomCategoryCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlCategories"></param>
        internal AtomCategoryCollection(XmlNodeList xnlCategories) {
            if (xnlCategories == null)
            {
                throw new ArgumentNullException("xnlCategories");
            }
            this.DeserializeFromXml(xnlCategories);
        }
        #endregion

        #region Private Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlCategories"></param>
        private void DeserializeFromXml(XmlNodeList xnlCategories)
        {
            foreach (XmlNode xnCategory in xnlCategories)
            {
                this.Add(new AtomCategory(xnCategory));
            }
        }
        #endregion

        #region Public Validate Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="validateContent"></param>
        public void Validate(AtomVersion version, bool validateContent)
        {
            foreach (AtomCategory category in this)
            {
                category.Validate(version, validateContent);
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
            foreach (AtomCategory category in this)
            {
                nodes.Add(category.SerializeToXml(version, xdAtom));
            }
            return nodes;
        }
        #endregion
    }// class
}// namespace
