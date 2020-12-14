using System;
using System.Runtime.Serialization;
namespace ItSoftware.Syndication
{
	[Serializable]
	public class SyndicationFormatInvalidException : Exception
    {
        #region Constructors
        public SyndicationFormatInvalidException() {			
		}
		public SyndicationFormatInvalidException(string msg) : base(msg) {
		}
		public SyndicationFormatInvalidException(string msg, Exception inner) : base(msg, inner) {
		}
        protected SyndicationFormatInvalidException(SerializationInfo info, StreamingContext context) : base(info,context)
        {
        }
        #endregion
    }// class
}// namespace
