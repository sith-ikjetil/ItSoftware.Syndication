using System;
using System.Runtime.Serialization;
namespace ItSoftware.Syndication {
	[Serializable]
	public class SyndicationValidationException : Exception {
		public SyndicationValidationException() {			
		}
		public SyndicationValidationException(string msg) : base(msg) {
		}
		public SyndicationValidationException(string msg, Exception inner) : base(msg, inner) {
		}
        protected SyndicationValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
	}// class
}// namespace

