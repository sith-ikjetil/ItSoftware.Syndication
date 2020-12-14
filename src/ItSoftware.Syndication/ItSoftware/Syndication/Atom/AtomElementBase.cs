using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// Abstract ATOM element class. All ATOM elements must inherit from this base class.
    /// </summary>
    public abstract class AtomElementBase : IAtomValidate
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AtomElementBase()
        {
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
            throw new SyndicationValidationException(Atom.ATOM_ERRORMESSAGE_CANNOT_VALIDATE_UNRECOGNIZED_VERSION);
        }
        #endregion

        #region Protected Internal Methods
        /// <summary>
        /// This method should never be called but overriden in all inherited classes.
        /// </summary>
        /// <param name="validateContent"></param>
        protected internal virtual void ValidateAtom_1_0(bool validateContent) 
        {
        }
        /// <summary>
        /// This method should never be called but overriden in all inherithed classes.
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal virtual XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            return null;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Serializes the class to XML.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        internal XmlNode SerializeToXml(AtomVersion version, XmlDocument xdAtom)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                return SerializeToXml_1_0(xdAtom);                
            }
            throw new ArgumentException(Atom.ATOM_ERRORMESSAGE_CANNOT_SERIALIZE_UNRECOGNIZED_VERSION);
        }
        #endregion
    }// class
}// namespace
