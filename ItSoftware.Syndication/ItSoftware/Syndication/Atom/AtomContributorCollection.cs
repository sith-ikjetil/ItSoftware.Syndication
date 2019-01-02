using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomContributorCollection : List<AtomContributor>, IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomContributorCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlContributors"></param>
        internal AtomContributorCollection(XmlNodeList xnlContributors)
        {
            if (xnlContributors == null)
            {
                throw new ArgumentNullException("xnlContributors");
            }
            this.DeserializeFromXml(xnlContributors);
        }
        #endregion

        #region Private Deserialization Methods
        private void DeserializeFromXml(XmlNodeList xnlContributors)
        {
            foreach (XmlNode xnContributor in xnlContributors)
            {
                this.Add(new AtomContributor(xnContributor));
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
            foreach (AtomContributor contributor in this) {
                contributor.Validate(version, validateContent);
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
            foreach (AtomContributor contributor in this)
            {
                nodes.Add(contributor.SerializeToXml(version, xdAtom));
            }
            return nodes;
        }
        #endregion
    }// class
}// namespace
