using System;
using System.Xml;
using System.Collections.Generic;
using ItSoftware.Syndication;
namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RDF class.
	/// </summary>
	public sealed class Rdf : SyndicationBase {

		#region Private Const Data	
		//
		// RDF Elements.
		//		
		private const string RDF_ELEMENT_RDF = "RDF";				
		private const string RDF_ELEMENT_TEXTINPUT = "textinput";
		private const string RDF_ELEMENT_CHANNEL = "channel";
		private const string RDF_ELEMENT_IMAGE = "image";
		private const string RDF_ELEMENT_ITEM = "item";

		//
		// RDF Prefixes.
		//
		private const string RDF_PREFIX_RDF = "rdf";

		//
		// RDF Attributes.
		//	
		private const string RDF_ATTRIBUTE_XMLNS = "xmlns";		
		// 0.9
		private const string RDF_ATTRIBUTE_XMLNS_VALUE_0_90 = "http://my.netscape.com/rdf/simple/0.9/";
		private const string RDF_ATTRIBUTE_RDF_VALUE_0_90 = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
		// 1.0
		private const string RDF_ATTRIBUTE_XMLNS_VALUE_1_0 = "http://purl.org/rss/1.0/";		
		private const string RDF_ATTRIBUTE_RDF_VALUE_1_0 = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";		
		
		
		
		//
		// RDF Version.
		//
		private const string RDF_VERSION_0_90 = "0.90";
		private const string RDF_VERSION_1_0 = "1.0";		
		
		//
		// Misc.
		//
		private const string RDF_XML_VERSION_1_0 = "1.0";				
		private const string NAMESPACE_PREFIX_XMLNS = "kksxxy";
		
		#endregion

        #region RDF Internal Static ReadOnly Variables
        //
        // Valid Values
        //		
        internal static readonly string[] RDF_VALIDVALUES_URL_1_0 = { "http://", "https://", "ftp://" };
        internal static readonly string[] RDF_VALIDVALUES_LINK_1_0 = { "http://", "https://", "ftp://" };

        //
        // Max/Min Values
        //
        internal static readonly int RDF_MINVALUE_ITEM_MIN_NUMBER_OF_0_90 = 1;
        internal static readonly int RDF_MINVALUE_ITEM_MIN_NUMBER_OF_1_0 = 1;
        internal static readonly int RDF_MAXVALUE_ITEM_MAX_NUMBER_OF_0_90 = 15;

        //
        // Validation Error Messages
        //
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_IMAGE_URLS_NOT_EQUAL = "Image url's are not equal.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH = "{0} element can have a maxium length of {1} characters. Length was: {2} characters.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID = "Validation failed. '{0}' element had invalid '{1}' element.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_LINK_WRONG_TYPE_1_0 = "{0} element has invalid value.\r\nShould start with 'http://', 'https://' or 'ftp://'.\r\nValue was: '{1}'";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_URL_WRONG_TYPE_1_0 = "{0} element has invalid value.\r\nShould start with 'http://', 'https://' or 'ftp://'.\r\nValue was: '{1}'";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_NOT_UNIQUE_ABOUT_1_0 = "{0} element did not have a unique about attribute.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_ILLEGAL_NUMBER_OF_ITEMS_1_0 = "The RDF document must have at least one item.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL = "Required field '{0}' cannot be null.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_TEXTINPUT_NOT_MATCHING_VALUES = "TextInput elements does not have matching about attribute values.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_SEQUENCE_INVALID_NUMBER_OF_ITEMS = "Sequence did not contain the same number of items as there was items.";
        internal static readonly string RDF_ERRORMESSAGE_VALIDATION_SEQUENCE_INVALID_RESOURCE = "Sequence item contained an invalid resource attribute.";

        //
        // Misc. Error Messages
        //
        internal static readonly string RDF_ERRORMESSAGE_INVALID_RDF_FORMAT_1_0 = "Invalid RDF format.";
        internal static readonly string RDF_ERRORMESSAGE_UNKNOWN_VALUE = "Unknown value";


        //
        // Serialization Error Messages
        //
        //internal static readonly string RDF_ERRORMESSAGE_SERIALIZATION_ELEMENT_NOT_PART_OF_VERSION = "{0} element is not part of specified RSS version.";

        //
        // Misc. Messages
        //
        /*
        internal static readonly string RDF_ERRORMESSAGE_ITEM_TITLE_DESCRIPTION_REQUIRED_FIELD_NULL	= "Both Title and Description can't be null";
        internal static readonly string RDF_ERRORMESSAGE_ELEMENT_INVALID = "{0} element invalid. Value was: '{1}'";
        internal static readonly string RDF_ERRORMESSAGE_INVALID_VERSION_ATTRIBUTE_NOT_SET = "Version attribute not set.";
        internal static readonly string RDF_ERRORMESSAGE_INVALID_VERSION_ATTRIBUTE = "Version attribute invalid or not supported. Value was: '{0}'. Expected: '0.91', '0.92' or '2.0'.";
        internal static readonly string RDF_ERRORMESSAGE_INVALID_RDF_FORMAT_1_0_MISSING_CHANNEL_ELEMENT = "Invalid RSS format. Channel element missing.";
        */
        #endregion

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public Rdf() {
			this.Channel = new RdfChannel();
			this.Items = new RdfItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}		
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
		public Rdf( XmlDocument xdRdf ) {			
			if ( xdRdf == null ) {								
				throw new ArgumentNullException("xdRdf");
			}
			this.DeserializeFromXml( xdRdf );
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="rss"></param>
		public Rdf( string rdf ) {	
			if ( rdf == null ) {								
				throw new ArgumentNullException("rdf");
			}
			XmlDocument xdRdf = new XmlDocument();
			xdRdf.LoadXml(Syndication.NormalizeSyndication(rdf));
			this.DeserializeFromXml( xdRdf );
		}				
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xdRss"></param>
		private void DeserializeFromXml(XmlDocument xdRdf) {				
			//
			// Set default values.
			//
			this.m_version = null;
			this.m_recognizedVersion = RdfVersion.Unknown;			
			this.RdfNamespaceURI = null;
			this.NamespaceURI = null;	

			//
			// Load document element.
			//
			XmlNode xnRdf = xdRdf.DocumentElement;

			//
			// Check that we have RDF local name.
			//
			if ( xnRdf.LocalName != RDF_ELEMENT_RDF ) {
				throw new SyndicationFormatInvalidException(Rdf.RDF_ERRORMESSAGE_INVALID_RDF_FORMAT_1_0);
			}			

			//
			// Version 0.9
			//
			if ( xnRdf.NamespaceURI == RDF_ATTRIBUTE_RDF_VALUE_0_90 ) {
				this.RdfNamespaceURI = xnRdf.NamespaceURI;
				XmlAttribute xaXmlns = xnRdf.Attributes[RDF_ATTRIBUTE_XMLNS];
				if ( xaXmlns != null ) {
					this.NamespaceURI = xaXmlns.InnerText;
					if ( xaXmlns.InnerText == RDF_ATTRIBUTE_XMLNS_VALUE_0_90 ) {
						this.m_version = RDF_VERSION_0_90;
						this.m_recognizedVersion = RdfVersion.RDF_0_90;
					}
				}
			}			
			//
			// Version 1.0
			// 			
			if ( xnRdf.NamespaceURI == RDF_ATTRIBUTE_RDF_VALUE_1_0 ) {				
				this.RdfNamespaceURI = xnRdf.NamespaceURI;
				XmlAttribute xaXmlns = xnRdf.Attributes[RDF_ATTRIBUTE_XMLNS];
				if ( xaXmlns != null ) {										
					this.NamespaceURI = xaXmlns.InnerText;	
					if ( xaXmlns.InnerText == RDF_ATTRIBUTE_XMLNS_VALUE_1_0 ) {																		
						this.m_version = RDF_VERSION_1_0;				
						this.m_recognizedVersion = RdfVersion.RDF_1_0;													
					}					
				}						
			}			
			//
			// Deserialize from XML.
			//
			DeserializeFromXml(xnRdf);															
		}		
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnRss"></param>
		private void DeserializeFromXml(XmlNode xnRdf) {			
			//Try-Catch block to make sure the version is set back to NotSet if the deserialization fails.
			try {						
				//
				// Set prefix.
				//
				this.Prefix = xnRdf.Prefix;

				//
				// Set namespace manager.
				//
				string namespaceUri = xnRdf.Attributes[RDF_ATTRIBUTE_XMLNS].InnerText;
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(xnRdf.OwnerDocument.NameTable);				
				nsmgr.AddNamespace(NAMESPACE_PREFIX_XMLNS,namespaceUri);				

				//
				// Channel.
				//
				XmlNode xnChannel = xnRdf.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_CHANNEL,nsmgr);
				if ( xnChannel != null ) {
					this.Channel = new RdfChannel(xnChannel);						
				}				

				//
				// Image.
				//
				XmlNode xnImage = xnRdf.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_IMAGE,nsmgr);
				if ( xnImage != null ) {
					this.Image = new RdfImage(xnImage);
				}

				//
				// Items.
				//								
				XmlNodeList xnlItems = xnRdf.SelectNodes(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_ITEM,nsmgr);
				if ( xnlItems != null ) {
					this.Items = new RdfItemCollection(xnlItems);
				}

				//
				// TextInput.
				//
				XmlNode xnTextInput = xnRdf.SelectSingleNode(NAMESPACE_PREFIX_XMLNS+":"+RDF_ELEMENT_TEXTINPUT,nsmgr);
				if ( xnTextInput != null ) {
					this.TextInput = new RdfTextInput(xnTextInput);
				}

				//
				// Deserialize From Modules.
				//		
                List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
				exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_CHANNEL,namespaceUri) );			
				exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_IMAGE,namespaceUri) );			
				exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_ITEM,namespaceUri) );			
				exclusionElements.Add( new SyndicationModuleExclusionElement(RDF_ELEMENT_TEXTINPUT,namespaceUri) );
			        
				this.Modules = new SyndicationModuleCollection(xnRdf, exclusionElements);
			}
			catch ( Exception ) {
				this.Prefix = null;
				this.m_version = null;
				this.m_recognizedVersion = RdfVersion.NotSet;
				throw;
			}
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Saves the RDF feed as RDF 1.0.
		/// </summary>
		/// <returns></returns>
		private XmlDocument SerializeToXml_1_0(string encoding) {			
			XmlDocument xdRdf = new XmlDocument();

			XmlDeclaration xdDeclaration = xdRdf.CreateXmlDeclaration(RDF_XML_VERSION_1_0,encoding,null);
			xdRdf.AppendChild(xdDeclaration);

			//
			// Get Prefix.
			//
			string prefix = (this.Prefix == null) ? RDF_PREFIX_RDF : this.Prefix;			
			
			//
			// Create the rdf element.
			//
			XmlElement xeRdf = xdRdf.CreateElement(prefix,RDF_ELEMENT_RDF,((this.RdfNamespaceURI != null) ? this.RdfNamespaceURI : RDF_ATTRIBUTE_RDF_VALUE_1_0));			

			XmlAttribute xaXmlns = xdRdf.CreateAttribute(RDF_ATTRIBUTE_XMLNS);
			xaXmlns.InnerText = (this.NamespaceURI != null ) ? this.NamespaceURI : RDF_ATTRIBUTE_XMLNS_VALUE_1_0;			
			xeRdf.Attributes.Append(xaXmlns);

			xdRdf.AppendChild(xeRdf);			
						
			//
			// Append the channel element.
			//
			RdfChannelItems rci = (this.Items != null ) ? this.Items.GetChannelItems() : null;
			RdfChannelImage channelImage = (this.Image != null) ? this.Image.GetChannelImage() : null;
			RdfChannelTextInput channelTextInput = (this.TextInput != null) ? this.TextInput.GetChannelTextInput() : null;
			xeRdf.AppendChild(this.Channel.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,rci,prefix,channelImage,channelTextInput));

			if ( this.Image != null ) {
				xeRdf.AppendChild(this.Image.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix));
			}
			
			this.Items.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,xeRdf,prefix);

			if ( this.TextInput != null ) {
				xeRdf.AppendChild(this.TextInput.SerializeToXml(xdRdf,RdfVersion.RDF_1_0,prefix));
			}
			
			//
			// Return the created XML document.
			//
			return xdRdf;
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Validates that the object model has the elements it needs
		/// to produce an RDF XML of the desired version.
		/// </summary>
		/// <param name="version"></param>
		public bool Validate(RdfVersion version, bool validateContent) {			
			if ( version == RdfVersion.RDF_1_0 ) {
				//
				// Check for required elements.
				//				
				if ( this.Channel == null ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL,RDF_ELEMENT_CHANNEL);
					throw new SyndicationValidationException(msg);
				}
				if ( this.Items == null ) {
					string msg = string.Format(Rdf.RDF_ERRORMESSAGE_VALIDATION_REQUIRED_FIELD_NULL,"Items");
					throw new SyndicationValidationException(msg);
				}				
			
				RdfAboutCollection rac = GetAbouts();
						
				//
				// Ask each item to validate itself.
				//
				if ( this.Channel != null ) {
					RdfAboutCollection racItems = new RdfAboutCollection();
					if ( this.Items != null ) {
						racItems.Add( this.Items.GetAbouts() );
					}
					string textInputAbout = (this.TextInput != null) ? this.TextInput.GetAbout() : null;
					string imageAbout = (this.Image != null) ? this.Image.GetAbout() : null;
					string textinputAbout = (this.TextInput != null) ? this.TextInput.GetAbout() : null;
					this.Channel.Validate(version,validateContent,rac,racItems,textInputAbout,imageAbout,textinputAbout);
				}
				if ( this.Image != null ) {
					this.Image.Validate(version,validateContent,rac);
				}
				if ( this.Items != null ) {
					this.Items.Validate(version,validateContent,rac);
				}
				if ( this.TextInput != null ) {
					this.TextInput.Validate(version,validateContent,rac);
				}
			}
			return true;			
		}		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private RdfAboutCollection GetAbouts() {
			RdfAboutCollection rac = new RdfAboutCollection();

			if ( this.Channel != null ) {
				rac.Add( this.Channel.GetAbouts() );
			}
			if ( this.Items != null ) {
				rac.Add( this.Items.GetAbouts() );
			}
			if ( this.Image != null ) {
				rac.Add( this.Image.GetAbouts() );
			}
			if ( this.TextInput != null ) {
				rac.Add( this.TextInput.GetAbouts() );
			}
			
			return rac;
		}
		/// <summary>
		/// Save the RDF object to XML.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public XmlDocument Save(RdfVersion version, string encoding) {
			if ( version == RdfVersion.RDF_1_0 ) {
				if ( ValidateOnSave ) {
					Validate(version,this.ValidateContent);
				}
				return SerializeToXml_1_0(encoding);
			}			
			throw new ArgumentException(Rdf.RDF_ERRORMESSAGE_UNKNOWN_VALUE,"version");
		}		
		/// <summary>
		/// Saves the RDF object according to the version and encoding arguments.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public void Save(System.IO.Stream stream, RdfVersion version, string encoding) {
			if ( stream == null ) {
				throw new ArgumentNullException("stream");
			}
			this.Save(version,encoding).Save(stream);
		}
		/// <summary>
		/// Saves the RDF object according to the version and encoding arguments.
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="version"></param>
		/// <param name="encoding"></param>
		/// <param name="customVersion"></param>
		public void Save(string filename, RdfVersion version, string encoding) {
			if ( filename == null ) {
				throw new ArgumentNullException("filename");
			}
			this.Save(version,encoding).Save(filename);
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Version backing field
		/// </summary>
		private string m_version; 
		/// <summary>
		/// Gets the RDF version of the underlying document.
		/// </summary>
		public string Version {
			get {
				return m_version;
			}
		}
		/// <summary>
		/// RecognizedVersion backing field.
		/// </summary>
		private RdfVersion m_recognizedVersion = RdfVersion.NotSet;
		/// <summary>
		/// Gets the RecognizedVersion. The version known og unknown.
		/// </summary>
		public RdfVersion RecognizedVersion {
			get {
				return m_recognizedVersion;
			}
		}
		/// <summary>
		/// ValidateOnSave backing field.
		/// </summary>
		private bool m_validateOnSave = false;//true
		/// <summary>
		/// Gets or sets wheather or not to validate RDF on save.
		/// </summary>
		private bool ValidateOnSave {
			get {
				return m_validateOnSave;
			}
			set {
				m_validateOnSave = value;
			}
		}
		/// <summary>
		/// ValidateContent backing field.
		/// </summary>
		private bool m_validateContent = false;//true
		/// <summary>
		/// Gets or sets wheather or not to validate content on validation.
		/// </summary>
		private bool ValidateContent {
			get {
				return m_validateContent;
			}
			set {
				m_validateContent = value;
			}
		}
		/// <summary>
		/// Items backing field.
		/// </summary>
		private RdfItemCollection m_items;
		/// <summary>
		/// Get/Set Items.
		/// </summary>
		public RdfItemCollection Items {
			get {
				return m_items;
			}
			set {
				m_items = value;
			}
		}
		/// <summary>
		/// Image backing field.
		/// </summary>
		private RdfImage m_image;
		/// <summary>
		/// Get/Set Image.
		/// </summary>
		public RdfImage Image {
			get {
				return m_image;
			}
			set {
				m_image = value;
			}
		}
		/// <summary>
		/// TextInput backing field.
		/// </summary>
		private RdfTextInput m_textInput;
		/// <summary>
		/// Get/Set TextInput.
		/// </summary>
		public RdfTextInput TextInput {
			get {
				return m_textInput;
			}
			set {
				m_textInput = value;
			}
		}
		/// <summary>
		/// RdfNamespaceURI backing field.
		/// </summary>
		private string m_rdfNamespaceURI;
		/// <summary>
		/// Get/Set RdfNamespaceURI.
		/// </summary>
		public string RdfNamespaceURI {
			get {
				return m_rdfNamespaceURI;
			}
			set {
				m_rdfNamespaceURI = value;
			}
		}
		/// <summary>
		/// NamespaceURI backing field.
		/// </summary>
		private string m_namespaceURI;
		/// <summary>
		/// Get/Set NamespaceURI.
		/// </summary>
		public string NamespaceURI {
			get {
				return m_namespaceURI;
			}
			set {
				m_namespaceURI = value;
			}
		}
		/// <summary>
		/// Modules backing field.
		/// </summary>
		private SyndicationModuleCollection m_modules;
		/// <summary>
		/// Gets or sets the syndication modules.
		/// </summary>
	    public SyndicationModuleCollection Modules {
			get {
				return m_modules;
			}
			set {
				m_modules = value;
			}
		}
		/// <summary>
		/// Channel backing field.
		/// </summary>
		private RdfChannel m_rdfChannel;
		/// <summary>
		/// Contains information about the channel (metadata) and its contents.
		/// </summary>
		public RdfChannel Channel {
			get {
				return m_rdfChannel;
			}
			set {
				m_rdfChannel = value;
			}
		}		
		/// <summary>
		/// Prefix backing field.
		/// </summary>
		private string m_prefix;
		/// <summary>
		/// Get/Set Prefix.
		/// </summary>
		public string Prefix {
			get {				
				return m_prefix;			
			}
			set {
				m_prefix = value;
			}
		}
		#endregion		

	}// class
}// namespace
