using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomEntryCollection : List<AtomEntry>, IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomEntryCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlEntries"></param>
        internal AtomEntryCollection(XmlNodeList xnlEntries)
        {
            if (xnlEntries == null)
            {
                throw new ArgumentNullException("xnlEntries");
            }
            this.DeserializeFromXml(xnlEntries);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnlEntries"></param>
        private void DeserializeFromXml(XmlNodeList xnlEntries)
        {           
            foreach (XmlNode xnEntry in xnlEntries)
            {
                this.Add(new AtomEntry(xnEntry));
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

        #region Protected Methods
        /// <summary>
        /// Validates the collection to ATOM 1.0.
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal virtual void ValidateAtom_1_0(bool validateContent)
        {
            foreach (AtomEntry entry in this)
            {
                entry.Validate(AtomVersion.Atom_1_0, validateContent);
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Serializes the collection.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        internal List<XmlNode> SerializeToXml(AtomVersion version, XmlDocument xdAtom)
        {
            List<XmlNode> nodes = new List<XmlNode>();            

            foreach (AtomEntry entry in this)
            {
                nodes.Add(entry.SerializeToXml(version, xdAtom));
            }
            return nodes;
        }
        #endregion
    }// class
}// namespace
