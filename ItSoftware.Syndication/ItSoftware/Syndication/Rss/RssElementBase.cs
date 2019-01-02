using System;
using System.Collections.Generic;
using System.Text;

namespace ItSoftware.Syndication.Rss
{
    public abstract class RssElementBase : IRssValidate 
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public RssElementBase()
        {
        }
        #endregion

        #region IAtomValidate Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="validateContent"></param>
        public void Validate(RssVersion version, bool validateContent)
        {
            bool bHit = false;
            if (version == RssVersion.RSS_0_91)
            {
                Validate_0_91(validateContent);
                bHit = true;
            }
            else if (version == RssVersion.RSS_0_92)
            {
                Validate_0_92(validateContent);
                bHit = true;
            }
            else if (version == RssVersion.RSS_2_0_1)
            {
                Validate_2_0_1(validateContent);
                bHit = true;
            }
            
            if (!bHit)
            {
                throw new SyndicationValidationException(Rss.RSS_ERRORMESSAGE_CANNOT_VALIDATE_UNRECOGNIZED_VERSION);
            }
        }
        #endregion

        #region Protected Internal Methods
        /// <summary>
        /// This method should never be called but overriden in all inherited classes.
        /// </summary>
        /// <param name="validateContent"></param>
        protected virtual void Validate_0_91(bool validateContent) 
        {
        }
        /// <summary>
        /// This method should never be called but overriden in all inherited classes.
        /// </summary>
        /// <param name="validateContent"></param>
        protected virtual void Validate_0_92(bool validateContent)
        {
        }
        /// <summary>
        /// This method should never be called but overriden in all inherited classes.
        /// </summary>
        /// <param name="validateContent"></param>
        protected virtual void Validate_2_0_1(bool validateContent)
        {
        }
        /*/// <summary>
        /// This method should never be called but overriden in all inherithed classes.
        /// </summary>
        /// <param name="xdAtom"></param>
        /// <returns></returns>
        protected internal virtual XmlNode SerializeToXml_1_0(XmlDocument xdAtom)
        {
            return null;
        }
        */
        #endregion

        #region Internal Methods
        /*/// <summary>
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
         * */
        #endregion
    }// class
}// namespace
