using System;
using System.Xml;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Allows processes to register with a cloud to be notified 
	/// of updates to the channel, implementing a lightweight publish-subscribe 
	/// protocol for RSS feeds.
	/// </summary>
	public sealed class RssCloud : RssElementBase {	

		#region Private Const Data
		private const string RSS_ELEMENT_CLOUD = "cloud";
		private const string RSS_ELEMENT_DOMAIN = "domain";
		private const string RSS_ELEMENT_PORT = "port";
		private const string RSS_ELEMENT_PATH = "path";
		private const string RSS_ELEMENT_REGISTERPROCEDURE = "registerProcedure";
		private const string RSS_ELEMENT_PROTOCOL = "protocol";
		#endregion

		#region Constructors
		/// <summary>
		/// Public constuctor.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="port"></param>
		/// <param name="path"></param>
		/// <param name="registerProcedure"></param>
		/// <param name="protocol"></param>
		public RssCloud(string domain,string port,string path,string registerProcedure,string protocol) {			
			this.Domain = domain;
			this.Port = port;
			this.Path = path;
			this.RegisterProcedure = registerProcedure;
			this.Protocol = protocol;
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnCloud"></param>
		internal RssCloud(XmlNode xnCloud) {
			this.DeserializeFromXml(xnCloud);
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from xml.
		/// </summary>
		/// <param name="xnCloud"></param>
		private void DeserializeFromXml(XmlNode xnCloud) {
			//
			// All elements are required, but must be able to read feed non standardized.
			//
			XmlNode xnDomain = xnCloud.Attributes[RSS_ELEMENT_DOMAIN];
			if ( xnDomain != null ) {
				this.Domain = xnDomain.InnerText;			
			}
			XmlNode xnPort = xnCloud.Attributes[RSS_ELEMENT_PORT];
			if ( xnPort != null ) {
				this.Port = xnPort.InnerText;
			}
			XmlNode xnPath = xnCloud.Attributes[RSS_ELEMENT_PATH];
			if ( xnPath != null ) {
				this.Path = xnPath.InnerText;
			}
			XmlNode xnRegisterProcedure = xnCloud.Attributes[RSS_ELEMENT_REGISTERPROCEDURE];
			if ( xnRegisterProcedure != null ) {
				this.RegisterProcedure = xnRegisterProcedure.InnerText;
			}
			XmlNode xnProtocol = xnCloud.Attributes[RSS_ELEMENT_PROTOCOL];
			if ( xnProtocol != null ) {
				this.Protocol = xnProtocol.InnerText;
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRss,RssVersion version) {
			if ( version == RssVersion.RSS_0_91 ) {
				return SerializeToXml_0_91(xdRss);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				return SerializeToXml_0_92(xdRss);
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				return SerializeToXml_2_0_1(xdRss);
			}
            throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE, "version");
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {
            string msg = string.Format(Rss.RSS_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION, RSS_ELEMENT_CLOUD);
			throw new SyndicationSerializationException(msg);
		}
		/// <summary>
		/// Serializes the object to xml.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeCloud = xdRss.CreateElement(RSS_ELEMENT_CLOUD);
			//
			// Required Elements
			//
			if ( this.Domain != null ) {
				xeCloud.Attributes.Append( SerializeDomainToXml(xdRss) );
			}			
			if ( this.Port != null ) {
				xeCloud.Attributes.Append( SerializePortToXml(xdRss) );
			}			
			if ( this.Path != null ) {
				xeCloud.Attributes.Append( SerializePathToXml(xdRss) );				
			}
			if ( this.RegisterProcedure != null ) {
				xeCloud.Attributes.Append( SerializeRegisterProcedureToXml(xdRss) );
			}
			if ( this.Protocol != null ) {
				xeCloud.Attributes.Append( SerializeProtocolToXml(xdRss) );
			}			
			return xeCloud;
		}
		/// <summary>
		/// Serializes the object to xml.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeCloud = xdRss.CreateElement(RSS_ELEMENT_CLOUD);
			//
			// Required Elements
			//
			if ( this.Domain != null ) {
				xeCloud.Attributes.Append( SerializeDomainToXml(xdRss) );
			}			
			if ( this.Port != null ) {
				xeCloud.Attributes.Append( SerializePortToXml(xdRss) );
			}			
			if ( this.Path != null ) {
				xeCloud.Attributes.Append( SerializePathToXml(xdRss) );				
			}
			if ( this.RegisterProcedure != null ) {
				xeCloud.Attributes.Append( SerializeRegisterProcedureToXml(xdRss) );
			}
			if ( this.Protocol != null ) {
				xeCloud.Attributes.Append( SerializeProtocolToXml(xdRss) );
			}			
			return xeCloud;
		}
		/// <summary>
		/// Serializes Domain to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeDomainToXml(XmlDocument xdRss) {
			XmlAttribute xaDomain = xdRss.CreateAttribute(RSS_ELEMENT_DOMAIN);
			xaDomain.InnerText = this.Domain;
			return xaDomain;
		}
		/// <summary>
		/// Serializes Port to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializePortToXml(XmlDocument xdRss) {
			XmlAttribute xaPort = xdRss.CreateAttribute(RSS_ELEMENT_PORT);
			xaPort.InnerText = this.Port;
			return xaPort;
		}
		/// <summary>
		/// Serializes Path to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializePathToXml(XmlDocument xdRss) {			
			XmlAttribute xaPath = xdRss.CreateAttribute(RSS_ELEMENT_PATH);
			xaPath.InnerText = this.Path;
			return xaPath;
		}
		/// <summary>
		/// Serializes RegisterProcedure to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeRegisterProcedureToXml(XmlDocument xdRss) {
			XmlAttribute xaRegisterProcedure = xdRss.CreateAttribute(RSS_ELEMENT_REGISTERPROCEDURE);
			xaRegisterProcedure.InnerText = this.RegisterProcedure;
			return xaRegisterProcedure;
		}
		/// <summary>
		/// Serializes Protocol to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlAttribute SerializeProtocolToXml(XmlDocument xdRss) {
			XmlAttribute xaProtocol = xdRss.CreateAttribute(RSS_ELEMENT_PROTOCOL);
			xaProtocol.InnerText = this.Protocol;
			return xaProtocol;
		}
		#endregion

		#region Validation Methods
		/*/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
		/// <returns></returns>
		internal bool Validate(RssVersion version, bool validateContent) {
			if ( version == RssVersion.RSS_0_91 ) {
				return Validate_0_91(validateContent);
			}
			else if ( version == RssVersion.RSS_0_92 ) {
				return Validate_0_92(validateContent);
			}
			else if  ( version == RssVersion.RSS_2_0_1 ) {
				return Validate_2_0_1(validateContent);
			}
            throw new ArgumentException(Rss.RSS_ERRORMESSAGE_UNKNOWN_VALUE, "version");		
		}
         * */
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
		protected override void Validate_0_91(bool validateContent) {
            string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_NOT_SUPPORTED, RSS_ELEMENT_CLOUD);
			throw new SyndicationValidationException(msg);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
        protected override void Validate_0_92(bool validateContent)
        {		
			//
			// Required Fields.
			//
			if ( this.Domain == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_DOMAIN);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Port == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PORT);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Path == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PATH);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.RegisterProcedure == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_REGISTERPROCEDURE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Protocol == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PROTOCOL);
				throw new SyndicationValidationException(msg);
			}

			//
			// Validate content
			//
			if ( validateContent ) {
				ValidateDomain(RssVersion.RSS_0_92);
				ValidatePort(RssVersion.RSS_0_92);
				ValidatePath(RssVersion.RSS_0_92);
				ValidateRegisterProcuedure(RssVersion.RSS_0_92);
				ValidateProtocol(RssVersion.RSS_0_92);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="validateContent"></param>
        protected override void Validate_2_0_1(bool validateContent)
        {		
			//
			// Required Fields.
			//
			if ( this.Domain == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_DOMAIN);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Port == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PORT);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Path == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PATH);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.RegisterProcedure == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_REGISTERPROCEDURE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Protocol == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PROTOCOL);
				throw new SyndicationValidationException(msg);
			}

			//
			// Validate content
			//
			if ( validateContent ) {
				ValidateDomain(RssVersion.RSS_2_0_1);
				ValidatePort(RssVersion.RSS_2_0_1);
				ValidatePath(RssVersion.RSS_2_0_1);
				ValidateRegisterProcuedure(RssVersion.RSS_2_0_1);
				ValidateProtocol(RssVersion.RSS_2_0_1);
			}			
		}
		private void ValidateDomain(RssVersion version) {
			if ( this.Domain == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		private void ValidatePort(RssVersion version) {
			if ( this.Port == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_92 || version == RssVersion.RSS_2_0_1 ) {
				try {
					int port = Convert.ToInt32(this.Port);
				}
				catch ( FormatException ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PORT);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		private void ValidatePath(RssVersion version) {
			if ( this.Path == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateRegisterProcuedure(RssVersion version) {
			if ( this.RegisterProcedure == null ) {
				return;
			}
			//
			// All values ok.
			//
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateProtocol(RssVersion version) {
			if ( this.Protocol == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_92 ) {				
				string protocol = this.Protocol;
				foreach ( string validProtocol in Rss.RSS_VALIDVALUES_CLOUD_PROTOCOL_0_92 ) {
					if ( protocol == validProtocol ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PROTOCOL);
				throw new SyndicationValidationException(msg);				
			}
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string protocol = this.Protocol;
                foreach (string validProtocol in Rss.RSS_VALIDVALUES_CLOUD_PROTOCOL_2_0_1)
                {
					if ( protocol == validProtocol ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CLOUD, RSS_ELEMENT_PROTOCOL);
				throw new SyndicationValidationException(msg);				
			}
		}
		#endregion

		#region Required Elements
		/// <summary>
		/// Domain backing field.
		/// </summary>
		private string m_domain;
		/// <summary>
		/// Domain name or IP address of the cloud.
		/// </summary>
		public string Domain {
			get {
				return m_domain;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_DOMAIN, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_domain = value;
			}
		}
		/// <summary>
		/// Port backing field.
		/// </summary>
		private string m_port;
		/// <summary>
		/// The TCP port that the cloud is running on
		/// </summary>
		public string Port {
			get {
				return m_port;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_PORT, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);
				}
				m_port = value;
			}
		}
		/// <summary>
		/// Path backing field.
		/// </summary>
		private string m_path;
		/// <summary>
		/// Location of responder.
		/// </summary>
		public string Path {
			get {
				return m_path;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_PATH, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);					
				}
				m_path = value;
			}
		}
		/// <summary>
		/// RegisterProcedure backing field.
		/// </summary>
		private string m_registerProcedure;
		/// <summary>
		/// Name of the procedure to call to request notification.
		/// </summary>
		public string RegisterProcedure {
			get {
				return m_registerProcedure;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_REGISTERPROCEDURE, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);					
				}
				m_registerProcedure = value;
			}
		}
		/// <summary>
		/// Protocol backing field.
		/// </summary>
		private string m_protocol;
		/// <summary>
		/// Xml-rpc, soap or http-post (case-sensitive), indicating which 
		/// protocol is to be used.
		/// </summary>
		public string Protocol {
			get {
				return m_protocol;
			}
			set {
				if ( value == null ) {
                    throw new ArgumentNullException(RSS_ELEMENT_PROTOCOL, Rss.RSS_ERRORMESSAGE_REQUIRED_FIELD_NULL);					
				}
				m_protocol = value;
			}
		}		 
		#endregion

	}// class
}// namespace
