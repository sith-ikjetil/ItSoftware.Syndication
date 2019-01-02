using System;
using System.Runtime.Serialization;
namespace ItSoftware.Syndication {
	[Serializable]
	public class SyndicationSerializationException : Exception
    {
        #region Constructor
        public SyndicationSerializationException() {			
		}
		public SyndicationSerializationException(string msg) : base(msg) {
		}
		public SyndicationSerializationException(string msg, Exception inner) : base(msg, inner) {
		}
        protected SyndicationSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }// class
}// namespace

