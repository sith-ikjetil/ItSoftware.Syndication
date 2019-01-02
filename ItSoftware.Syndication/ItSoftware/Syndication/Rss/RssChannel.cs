using System;
using System.Xml;
using System.Collections.Generic;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Contains information about the channel (metadata) and its contents.
	/// </summary>
	public sealed class RssChannel : RssElementBase {

		#region Private Const Data
		private const int RSS_MAXLENGTH_TITLE = 100;
		private const int RSS_MAXLENGTH_DESCRIPTION = 500;
		private const int RSS_MAXLENGTH_COPYRIGHT = 100;
		private const int RSS_MAXLENGTH_MANAGINGEDITOR = 100;
		private const int RSS_MAXLENGTH_WEBMASTER = 100;
		private const int RSS_MAXLENGTH_RATING = 500;
		private const int RSS_MAXLENGTH_DOCS = 500;
		private const string RSS_ELEMENT_CHANNEL = "channel";
		private const string RSS_ELEMENT_RATING = "rating";
		private const string RSS_ELEMENT_TTL = "ttl";
		private const string RSS_ELEMENT_DOCS = "docs";
		private const string RSS_ELEMENT_GENERATOR = "generator";
		private const string RSS_ELEMENT_LASTBUILDDATE = "lastBuildDate";
		private const string RSS_ELEMENT_PUBDATE = "pubDate";
		private const string RSS_ELEMENT_WEBMASTER = "webMaster";
		private const string RSS_ELEMENT_MANAGINGEDITOR = "managingEditor";
		private const string RSS_ELEMENT_COPYRIGHT = "copyright";
		private const string RSS_ELEMENT_LANGUAGE = "language";
		private const string RSS_ELEMENT_DESCRIPTION = "description";
		private const string RSS_ELEMENT_LINK = "link";
		private const string RSS_ELEMENT_TITLE = "title";		
		private const string RSS_ELEMENT_ITEM = "item";
		private const string RSS_ELEMENT_CATEGORY = "category";
		private const string RSS_ELEMENT_CLOUD = "cloud";
		private const string RSS_ELEMENT_IMAGE = "image";		
		private const string RSS_ELEMENT_TEXTINPUT = "textInput";
		private const string RSS_ELEMENT_SKIPHOURS = "skipHours";		
		#endregion		

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		public RssChannel() {
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		public RssChannel(string title) {			
			this.Title = title;						
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		public RssChannel(string title, string link) : this(title) {						
			this.Link = link;			
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor. 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		public RssChannel(string title, string link, string description) : this(title,link){			
			this.Title = title;
			this.Link = link;
			this.Description = description;
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		/// <param name="language"></param>
		public RssChannel(string title, string link, string description, RssImage image ) : this(title,link,description) {
			this.Image = image;
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		/// <param name="language"></param>
		/// <param name="image"></param>
		public RssChannel(string title, string link, string description, string language) : this(title,link,description) {
			this.Language = language;
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		/// <param name="language"></param>
		/// <param name="image"></param>
		public RssChannel(string title, string link, string description, string language, RssImage image) : this(title,link,description) {
			this.Language = language;
			this.Image = image;
			this.Category = new RssCategoryCollection();
			this.Items = new RssItemCollection();
			this.Modules = new SyndicationModuleCollection();
		}
		/// <summary>
		/// Internal deserialization constructor.
		/// </summary>
		/// <param name="xnChannel"></param>
		internal RssChannel(XmlNode xnChannel) {			
			this.DeserializeFromXml(xnChannel);			
		}
		#endregion

		#region Deserialization Methods
		/// <summary>
		/// Deserializes the object from XML.
		/// </summary>
		/// <param name="xnChannel"></param>
		private void DeserializeFromXml(XmlNode xnChannel) {
			//
			// Required Elements 0.91+0.92+2.0.
			//
			XmlNode xnTitle = xnChannel.SelectSingleNode(RSS_ELEMENT_TITLE);
			if ( xnTitle != null ) {
				this.Title = xnTitle.InnerText;			
			}
			XmlNode xnLink = xnChannel.SelectSingleNode(RSS_ELEMENT_LINK);
			if ( xnLink != null ) {
				this.Link = xnLink.InnerText;
			}
			XmlNode xnDescription = xnChannel.SelectSingleNode(RSS_ELEMENT_DESCRIPTION);
			if ( xnDescription != null ) {
				this.Description = xnDescription.InnerText;
			}
			XmlNode xnLanguage = xnChannel.SelectSingleNode(RSS_ELEMENT_LANGUAGE);
			if ( xnLanguage != null ) {
				this.Language = xnLanguage.InnerText;
			}
			XmlNode xnImage = xnChannel.SelectSingleNode(RSS_ELEMENT_IMAGE);
			if ( xnImage != null ) {
				this.Image = new RssImage(xnImage);
			}
			
			//
			// Optional Elements
			//
			XmlNodeList xnlItems = xnChannel.SelectNodes(RSS_ELEMENT_ITEM);
			if ( xnlItems != null ) {
				this.Items = new RssItemCollection(xnlItems);
			}			
			XmlNode xnCopyright = xnChannel.SelectSingleNode(RSS_ELEMENT_COPYRIGHT);
			if ( xnCopyright != null ) {
				this.Copyright = xnCopyright.InnerText;
			}
			XmlNode xnManagingEditor = xnChannel.SelectSingleNode(RSS_ELEMENT_MANAGINGEDITOR);
			if ( xnManagingEditor != null ) {
				this.ManagingEditor = xnManagingEditor.InnerText;
			}
			XmlNode xnWebMaster = xnChannel.SelectSingleNode(RSS_ELEMENT_WEBMASTER);
			if ( xnWebMaster != null ) {
				this.WebMaster = xnWebMaster.InnerText;
			}
			XmlNode xnPubDate = xnChannel.SelectSingleNode(RSS_ELEMENT_PUBDATE);
			if ( xnPubDate != null ) {
				this.PubDate = xnPubDate.InnerText;
			}
			XmlNode xnLastBuildDate = xnChannel.SelectSingleNode(RSS_ELEMENT_LASTBUILDDATE);
			if ( xnLastBuildDate != null ) {
				this.LastBuildDate = xnLastBuildDate.InnerText;
			}
			XmlNodeList xnlCategories = xnChannel.SelectNodes(RSS_ELEMENT_CATEGORY);
			if ( xnlCategories != null ) {
				this.Category = new RssCategoryCollection(xnlCategories);
			}
			XmlNode xnGenerator = xnChannel.SelectSingleNode(RSS_ELEMENT_GENERATOR);
			if ( xnGenerator != null ) {
				this.Generator = xnGenerator.InnerText;
			}
			XmlNode xnDocs = xnChannel.SelectSingleNode(RSS_ELEMENT_DOCS);
			if ( xnDocs != null ) {
				this.Docs = xnDocs.InnerText;
			}
			XmlNode xnCloud = xnChannel.SelectSingleNode(RSS_ELEMENT_CLOUD);
			if ( xnCloud != null ) {
				this.Cloud = new RssCloud(xnCloud);
			}
			XmlNode xnTtl = xnChannel.SelectSingleNode(RSS_ELEMENT_TTL);
			if ( xnTtl != null ) {
				this.TTL = xnTtl.InnerText;
			}			
			XmlNode xnRating = xnChannel.SelectSingleNode(RSS_ELEMENT_RATING);
			if ( xnRating != null ) {
				this.Rating = xnRating.InnerText;
			}
			XmlNode xnTextInput = xnChannel.SelectSingleNode(RSS_ELEMENT_TEXTINPUT);
			if ( xnTextInput != null ) {
				this.TextInput = new RssTextInput(xnTextInput);
			}
			XmlNode xnSkipHours = xnChannel.SelectSingleNode(RSS_ELEMENT_SKIPHOURS);
			if ( xnSkipHours != null ) {
				this.SkipHours = new RssSkipHourCollection(xnSkipHours);
			}

			//
			// Deserialize From Modules
			//
            List<SyndicationModuleExclusionElement> exclusionElements = new List<SyndicationModuleExclusionElement>();
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_RATING,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_TTL,string.Empty) );
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_DOCS,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_GENERATOR,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_LASTBUILDDATE,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_PUBDATE,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_WEBMASTER,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_MANAGINGEDITOR,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_COPYRIGHT,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_LANGUAGE,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_DESCRIPTION,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_LINK,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_TITLE,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_ITEM,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_CATEGORY,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_CLOUD,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_IMAGE,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_TEXTINPUT,string.Empty));
            exclusionElements.Add( new SyndicationModuleExclusionElement(RSS_ELEMENT_SKIPHOURS,string.Empty));
		
			this.Modules = new SyndicationModuleCollection(xnChannel,exclusionElements);
		}
		#endregion

		#region Serialization Methods
		/// <summary>
		/// Serializes the object to XML.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="elementName"></param>
		/// <param name="rssVersion"></param>
		/// <returns></returns>
		internal XmlNode SerializeToXml(XmlDocument xdRss, RssVersion version) {
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
		/// Serializes the object to RSS 0.91.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_91(XmlDocument xdRss) {			
			XmlElement xeChannel = xdRss.CreateElement(RSS_ELEMENT_CHANNEL);

			//
			// Required Elements
			//			
			if ( this.Title != null ) {
				xeChannel.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeChannel.AppendChild( SerializeLinkToXml(xdRss) );
			}
			if ( this.Description != null ) {
				xeChannel.AppendChild( SerializeDescriptionToXml(xdRss) );			
			}
			if ( this.Language != null ) {
				xeChannel.AppendChild( SerializeLanguageToXml(xdRss) );			
			}
			if ( this.Image != null ) {
				xeChannel.AppendChild( SerializeImageToXml(xdRss,RssVersion.RSS_0_91) );			
			}
			//
			// Optional Elements
			//
			if ( this.Copyright != null ) {
				xeChannel.AppendChild( SerializeCopyrightToXml(xdRss) );
			}
			if ( this.ManagingEditor != null ) {
				xeChannel.AppendChild( SerializeManagingEditorToXml(xdRss) );
			}
			if ( this.WebMaster != null ) {
				xeChannel.AppendChild( SerializeWebMasterToXml(xdRss) );
			}
			if ( this.Rating != null ) {
				xeChannel.AppendChild( SerializeRatingToXml(xdRss) );
			}
			if ( this.PubDate != null ) {
				xeChannel.AppendChild( SerializePubDateToXml(xdRss) );
			}
			if ( this.LastBuildDate != null ) {
				xeChannel.AppendChild( SerializeLastBuildDateToXml(xdRss) );
			}
			if ( this.Docs != null ) {
				xeChannel.AppendChild( SerializeDocsToXml(xdRss) );
			}
			if ( this.TextInput != null ) {
				xeChannel.AppendChild( SerializeTextInputToXml(xdRss,RssVersion.RSS_0_91) );
			}
			if ( this.SkipDays != null ) {
				if ( this.SkipDays.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipDaysToXml(xdRss,RssVersion.RSS_0_91) );
				}
			}
			if ( this.SkipHours != null ) {
				if ( this.SkipHours.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipHoursToXml(xdRss,RssVersion.RSS_0_91) );
				}
			}
			//
			// Always serialize modules, and always before items.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeChannel);
			}
			//
			// Serialize Items last.
			//
			if ( this.Items != null ) {
				this.Items.SerializeToXml(xdRss,RssVersion.RSS_0_91,xeChannel);				
			}			
			return xeChannel;			
		}
		/// <summary>
		/// Serializes the object to RSS 0.92.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_0_92(XmlDocument xdRss) {
			XmlElement xeChannel = xdRss.CreateElement(RSS_ELEMENT_CHANNEL);

			//
			// Required Elements
			//			
			if ( this.Title != null ) {
				xeChannel.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeChannel.AppendChild( SerializeLinkToXml(xdRss) );
			}			
			if ( this.Description != null ) {
				xeChannel.AppendChild( SerializeDescriptionToXml(xdRss) );			
			}		
			if ( this.Image != null ) {
				xeChannel.AppendChild( SerializeImageToXml(xdRss,RssVersion.RSS_0_92) );			
			}
			//
			// Optional Elements
			//
			if ( this.Language != null ) {
				xeChannel.AppendChild( SerializeLanguageToXml(xdRss) );			
			}
			if ( this.Copyright != null ) {
				xeChannel.AppendChild( SerializeCopyrightToXml(xdRss) );
			}
			if ( this.ManagingEditor != null ) {
				xeChannel.AppendChild( SerializeManagingEditorToXml(xdRss) );
			}
			if ( this.WebMaster != null ) {
				xeChannel.AppendChild( SerializeWebMasterToXml(xdRss) );
			}
			if ( this.Rating != null ) {
				xeChannel.AppendChild( SerializeRatingToXml(xdRss) );
			}
			if ( this.PubDate != null ) {
				xeChannel.AppendChild( SerializePubDateToXml(xdRss) );
			}
			if ( this.LastBuildDate != null ) {
				xeChannel.AppendChild( SerializeLastBuildDateToXml(xdRss) );
			}
			if ( this.Docs != null ) {
				xeChannel.AppendChild( SerializeDocsToXml(xdRss) );
			}
			if ( this.TextInput != null ) {
				xeChannel.AppendChild( SerializeTextInputToXml(xdRss,RssVersion.RSS_0_92) );
			}
			if ( this.SkipDays != null ) {
				if ( this.SkipDays.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipDaysToXml(xdRss,RssVersion.RSS_0_92) );
				}
			}
			if ( this.SkipHours != null ) {
				if ( this.SkipHours.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipHoursToXml(xdRss,RssVersion.RSS_0_92) );
				}
			}	
			if ( this.Cloud != null ) {
				xeChannel.AppendChild( SerializeCloudToXml(xdRss,RssVersion.RSS_0_92) );
			}
			//
			// Always serialize modules, and always before items.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeChannel);
			}
			//
			// Serialize Items last.
			//
			if ( this.Items != null ) {
				this.Items.SerializeToXml(xdRss,RssVersion.RSS_0_91,xeChannel);				
			}			
			return xeChannel;			
		}
		/// <summary>
		/// Serializes the object to RSS 2.0.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeToXml_2_0_1(XmlDocument xdRss) {
			XmlElement xeChannel = xdRss.CreateElement(RSS_ELEMENT_CHANNEL);

			//
			// Required Elements
			//
			if ( this.Title != null ) {
				xeChannel.AppendChild( SerializeTitleToXml(xdRss) );
			}
			if ( this.Link != null ) {
				xeChannel.AppendChild( SerializeLinkToXml(xdRss) );
			}			
			if ( this.Description != null ) {
				xeChannel.AppendChild( SerializeDescriptionToXml(xdRss) );			
			}
			//
			// Optional Elements
			//
			if ( this.Language != null ) {
				xeChannel.AppendChild( SerializeLanguageToXml(xdRss) );
			}
			if ( this.Copyright != null ) {
				xeChannel.AppendChild( SerializeCopyrightToXml(xdRss) );
			}
			if ( this.ManagingEditor != null ) {
				xeChannel.AppendChild( SerializeManagingEditorToXml(xdRss) );
			}
			if ( this.WebMaster != null ) {
				xeChannel.AppendChild( SerializeWebMasterToXml(xdRss) );
			}
			if ( this.PubDate != null ) {
				xeChannel.AppendChild( SerializePubDateToXml(xdRss) );
			}
			if ( this.LastBuildDate != null ) {
				xeChannel.AppendChild( SerializeLastBuildDateToXml(xdRss) );
			}
			if ( this.Category != null ) {
				if ( this.Category.Count > 0 ) {
					SerializeCategoryToXml(xdRss,xeChannel,RssVersion.RSS_2_0_1);
				}
			}
			if ( this.Generator != null ) {
				xeChannel.AppendChild( SerializeGeneratorToXml(xdRss) );
			}
			if ( this.Docs != null ) {
				xeChannel.AppendChild( SerializeDocsToXml(xdRss) );
			}
			if ( this.Cloud != null ) {
				xeChannel.AppendChild( SerializeCloudToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			if ( this.TTL != null ) {
				xeChannel.AppendChild( SerializeTtlToXml(xdRss) );
			}
			if ( this.Image != null ) {
				xeChannel.AppendChild( SerializeImageToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			if ( this.Rating != null ) {
				xeChannel.AppendChild( SerializeRatingToXml(xdRss) );
			}
			if ( this.TextInput != null ) {
				xeChannel.AppendChild( SerializeTextInputToXml(xdRss,RssVersion.RSS_2_0_1) );
			}
			if ( this.SkipHours != null ) {
				if ( this.SkipHours.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipHoursToXml(xdRss,RssVersion.RSS_2_0_1) );
				}
			}
			if ( this.SkipDays != null ) {
				if ( this.SkipDays.Count > 0 ) {
					xeChannel.AppendChild( SerializeSkipDaysToXml(xdRss,RssVersion.RSS_2_0_1) );
				}
			}
			//
			// Always serialize modules, and always before items.
			//
			if ( this.Modules != null ) {
				this.Modules.SerializeToXml(xeChannel);
			}
			//
			// Serialize Items last.
			//
			if ( this.Items != null ) {
				this.Items.SerializeToXml(xdRss,RssVersion.RSS_2_0_1,xeChannel);				
			}		
			return xeChannel;
		}
		#endregion

		#region Serialization Helper Methods
		/// <summary>
		/// Serializes SkipDays.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeSkipDaysToXml(XmlDocument xdRss,RssVersion version) {
			return this.SkipDays.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes SkipHours.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeSkipHoursToXml(XmlDocument xdRss,RssVersion version) {
			return this.SkipHours.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes TextInput.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeTextInputToXml(XmlDocument xdRss,RssVersion version) {
			return this.TextInput.SerializeToXml(xdRss, version);			
		}
		/// <summary>
		/// Serializes Rating.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeRatingToXml(XmlDocument xdRss) {
			XmlElement xdRating = xdRss.CreateElement(RSS_ELEMENT_RATING);
			xdRating.InnerText = this.Rating;
			return xdRating;
		}
		/// <summary>
		/// Serializes Image.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeImageToXml(XmlDocument xdRss,RssVersion version) {
			return this.Image.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes TTL.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeTtlToXml(XmlDocument xdRss) {
			XmlElement xdTTL = xdRss.CreateElement(RSS_ELEMENT_TTL);
			xdTTL.InnerText = this.TTL;
			return xdTTL;
		}
		/// <summary>
		/// Serializes Cloud.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeCloudToXml(XmlDocument xdRss,RssVersion version) {
			return this.Cloud.SerializeToXml(xdRss,version);			
		}
		/// <summary>
		/// Serializes Docs.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDocsToXml(XmlDocument xdRss) {
			XmlElement xdDocs = xdRss.CreateElement(RSS_ELEMENT_DOCS);
			xdDocs.InnerText = this.Docs;
			return xdDocs;
		}
		/// <summary>
		/// Serializes Generator.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeGeneratorToXml(XmlDocument xdRss) {
			XmlElement xdGenerator = xdRss.CreateElement(RSS_ELEMENT_GENERATOR);
			xdGenerator.InnerText = this.Generator;
			return xdGenerator;
		}
		/// <summary>
		/// Serializes Category.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <param name="xeChannel"></param>
		private void SerializeCategoryToXml(XmlDocument xdRss,XmlElement xeChannel,RssVersion version) {
			this.Category.SerializeToXml(xdRss,version,xeChannel);			
		}
		/// <summary>
		/// Serializes LastBuildDate.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeLastBuildDateToXml(XmlDocument xdRss) {
			XmlElement xdLastBuildDate = xdRss.CreateElement(RSS_ELEMENT_LASTBUILDDATE);
			xdLastBuildDate.InnerText = this.LastBuildDate;
			return xdLastBuildDate;
		}
		/// <summary>
		/// Serialize PubDate.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializePubDateToXml(XmlDocument xdRss) {
			XmlElement xePubDate = xdRss.CreateElement(RSS_ELEMENT_PUBDATE);
			xePubDate.InnerText = this.PubDate;
			return xePubDate;
		}
		/// <summary>
		/// Serializes WebMaster.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeWebMasterToXml(XmlDocument xdRss) {
			XmlElement xeWebMaster = xdRss.CreateElement(RSS_ELEMENT_WEBMASTER);
			xeWebMaster.InnerText = this.WebMaster;
			return xeWebMaster;
		}
		/// <summary>
		/// Serializes ManagingEditor.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeManagingEditorToXml(XmlDocument xdRss) {
			XmlElement xeManagingEditor = xdRss.CreateElement(RSS_ELEMENT_MANAGINGEDITOR);
			xeManagingEditor.InnerText = this.ManagingEditor;
			return xeManagingEditor;
		}
		/// <summary>
		/// Serializes Copyright.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeCopyrightToXml(XmlDocument xdRss) {
			XmlElement xeCopyright = xdRss.CreateElement(RSS_ELEMENT_COPYRIGHT);
			xeCopyright.InnerText = this.Copyright;
			return xeCopyright;
		}
		/// <summary>
		/// Serializes Language.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeLanguageToXml(XmlDocument xdRss) {
			XmlElement xeLanguage = xdRss.CreateElement(RSS_ELEMENT_LANGUAGE);
			xeLanguage.InnerText = this.Language;
			return xeLanguage;
		}
		/// <summary>
		/// Serializes Description.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeDescriptionToXml(XmlDocument xdRss) {
			XmlElement xeDescription = xdRss.CreateElement(RSS_ELEMENT_DESCRIPTION);
			xeDescription.InnerText = this.Description;
			return xeDescription;
		}
		/// <summary>
		/// Serializes Link.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeLinkToXml(XmlDocument xdRss) {
			XmlElement xeLink = xdRss.CreateElement(RSS_ELEMENT_LINK);
			xeLink.InnerText = this.Link;
			return xeLink;
		}
		/// <summary>
		/// Serializes Title.
		/// </summary>
		/// <param name="xdRss"></param>
		/// <returns></returns>
		private XmlNode SerializeTitleToXml(XmlDocument xdRss) {
			XmlElement xeTitle = xdRss.CreateElement(RSS_ELEMENT_TITLE);
			xeTitle.InnerText = this.Title;
			return xeTitle;
		}				
		#endregion

		#region Validation Methods
		/*/// <summary>
		/// Validates the channel according to RSS spec.
		/// </summary>
		/// <param name="version"></param>
		/// <param name="validateContent"></param>
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
		/// Validates the channel to RSS 0.91 spec.
		/// </summary>
		/// <param name="validateContent"></param>
		protected override void Validate_0_91(bool validateContent) {
			//
			// Validate required elements have non-null values
			//
			if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Language == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_LANGUAGE);
				throw new SyndicationValidationException(msg);				
			}
			else if ( this.Image == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_IMAGE);
				throw new SyndicationValidationException(msg);
			}

			//
			// Validate complex objects
			//
			this.Image.Validate(RssVersion.RSS_0_91,validateContent);
			if ( this.TextInput != null ) {
				this.TextInput.Validate(RssVersion.RSS_0_91,validateContent);
			}
			if ( this.SkipHours != null ) {
				this.SkipHours.Validate(RssVersion.RSS_0_91,validateContent);
			}
			if ( this.SkipDays != null ) {
				this.SkipDays.Validate(RssVersion.RSS_0_91,validateContent);
			}
			if ( this.Items != null ) {
				this.Items.Validate(RssVersion.RSS_0_91,validateContent);
			}


			//
			// Validate values of all non complex objects.			
			//
			if ( validateContent == true ) {
				ValidateTitle(RssVersion.RSS_0_91);
				ValidateLink(RssVersion.RSS_0_91);
				ValidateDescription(RssVersion.RSS_0_91);
				ValidateLanguage(RssVersion.RSS_0_91);							
				ValidateCopyright(RssVersion.RSS_0_91);
				ValidateManagingEditor(RssVersion.RSS_0_91);
				ValidateWebMaster(RssVersion.RSS_0_91);
				ValidateRating(RssVersion.RSS_0_91);
				ValidatePubDate(RssVersion.RSS_0_91);
				ValidateLastBuildDate(RssVersion.RSS_0_91);
				ValidateDocs(RssVersion.RSS_0_91);				
			}			
		}
		/// <summary>
		/// Validates the channel to RSS 0.92 spec.
		/// </summary>
		/// <param name="validateContent"></param>
		protected override void Validate_0_92(bool validateContent) {
			//
			// Validate required elements have non-null values
			//
			if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}			
			else if ( this.Image == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_IMAGE);
				throw new SyndicationValidationException(msg);
			}

			//
			// Validate complex objects
			//			
			this.Image.Validate(RssVersion.RSS_0_92,validateContent);
			if ( this.Cloud != null ) {
				this.Cloud.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.TextInput != null ) {
				this.TextInput.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.SkipHours != null ) {
				this.SkipHours.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.SkipDays != null ) {
				this.SkipDays.Validate(RssVersion.RSS_0_92,validateContent);
			}
			if ( this.Items != null ) {
				this.Items.Validate(RssVersion.RSS_0_92,validateContent);
			}


			//
			// if validateContent == true then {
			//		Validate values of all non complex objects.
			// }
			//
			if ( validateContent == true ) {
				ValidateTitle(RssVersion.RSS_0_92);
				ValidateLink(RssVersion.RSS_0_92);
				ValidateDescription(RssVersion.RSS_0_92);
				ValidateLanguage(RssVersion.RSS_0_92);
				ValidateCopyright(RssVersion.RSS_0_92);
				ValidateManagingEditor(RssVersion.RSS_0_92);
				ValidateWebMaster(RssVersion.RSS_0_92);
				ValidateRating(RssVersion.RSS_0_92);
				ValidatePubDate(RssVersion.RSS_0_92);
				ValidateLastBuildDate(RssVersion.RSS_0_92);
				ValidateDocs(RssVersion.RSS_0_92);	
			}			
		}
		/// <summary>
		/// Validates the channel to RSS 2.0 spec.
		/// </summary>
		/// <param name="validateContent"></param>
		protected override void Validate_2_0_1(bool validateContent) {
			//
			// Validate required elements have non-null values
			//
			if ( this.Title == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_TITLE);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Link == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_LINK);
				throw new SyndicationValidationException(msg);
			}
			else if ( this.Description == null ) {
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED, RSS_ELEMENT_CHANNEL, RSS_ELEMENT_DESCRIPTION);
				throw new SyndicationValidationException(msg);
			}

			//
			// Validate complex objects
			//
			if ( this.Category != null ) {
				this.Category.Validate(RssVersion.RSS_2_0_1,validateContent);				
			}
			if ( this.SkipHours != null ) {
				this.SkipHours.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.SkipDays != null ) {
				this.SkipDays.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Image != null ) {
				this.Image.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Items != null ) {
				this.Items.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Cloud != null ) {
				this.Cloud.Validate(RssVersion.RSS_2_0_1,validateContent);
			}
			if ( this.Category != null ) {
				this.Category.Validate(RssVersion.RSS_2_0_1,validateContent);
			}			

			//
			// If validateContent == true then {
			//		Validate values of all elements 
			// }
			//
			if ( validateContent == true ) {
				ValidateTitle(RssVersion.RSS_2_0_1);
				ValidateLink(RssVersion.RSS_2_0_1);
				ValidateDescription(RssVersion.RSS_2_0_1);
				ValidateLanguage(RssVersion.RSS_2_0_1);
				ValidateCopyright(RssVersion.RSS_2_0_1);
				ValidateManagingEditor(RssVersion.RSS_2_0_1);
				ValidateWebMaster(RssVersion.RSS_2_0_1);
				ValidateRating(RssVersion.RSS_2_0_1);
				ValidatePubDate(RssVersion.RSS_2_0_1);
				ValidateLastBuildDate(RssVersion.RSS_2_0_1);
				ValidateDocs(RssVersion.RSS_2_0_1);	
				ValidateTTL(RssVersion.RSS_2_0_1);
				ValidateGenerator(RssVersion.RSS_2_0_1);
			}			
		}
		/// <summary>
		/// Validates generator element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateGenerator(RssVersion version) {
			if ( this.Generator == null ) {
				return;
			}
			//
			// All values valid.
			//
		}
		/// <summary>
		/// Validates ttl element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateTTL(RssVersion version) {
			if ( this.TTL == null ) {
				return;
			}
			if ( version == RssVersion.RSS_2_0_1 ) {
				try {
					long num = Convert.ToInt64(this.TTL);
				}
				catch ( FormatException ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_INVALID_NUMBER_VALUE, RSS_ELEMENT_TTL, this.TTL);
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates copyright element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateCopyright(RssVersion version) {
			if ( this.Copyright == null ) {
				return;
			}

			if ( version== RssVersion.RSS_0_91 ) {
				if ( this.Copyright.Length > RSS_MAXLENGTH_COPYRIGHT ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH, RSS_ELEMENT_COPYRIGHT, RSS_MAXLENGTH_COPYRIGHT.ToString(), this.Copyright.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates managing editor element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateManagingEditor(RssVersion version) {
			if ( this.ManagingEditor == null ) {
				return;
			}
			if ( version== RssVersion.RSS_0_91 ) {
				if ( this.ManagingEditor.Length > RSS_MAXLENGTH_MANAGINGEDITOR ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_MANAGINGEDITOR,RSS_MAXLENGTH_MANAGINGEDITOR.ToString(),this.ManagingEditor.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates web master element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateWebMaster(RssVersion version) {
			if ( this.WebMaster == null ) {
				return;
			}
			if ( version== RssVersion.RSS_0_91 ) {
				if ( this.WebMaster.Length > RSS_MAXLENGTH_WEBMASTER ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_WEBMASTER,RSS_MAXLENGTH_WEBMASTER.ToString(),this.WebMaster.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates rating element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateRating(RssVersion version) {
			if ( this.Rating == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Rating.Length > RSS_MAXLENGTH_RATING ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH, RSS_ELEMENT_RATING, RSS_MAXLENGTH_RATING.ToString(), this.Rating.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates pubdate element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidatePubDate(RssVersion version) {
			if ( this.PubDate == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ||
				 version == RssVersion.RSS_0_92 ||
				 version == RssVersion.RSS_2_0_1 ) {
				try {
					RssDateTime rdt = new RssDateTime(this.PubDate);
				}
				catch ( Exception x ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_FAILED,RSS_ELEMENT_CHANNEL,RSS_ELEMENT_PUBDATE);
					throw new SyndicationValidationException(msg,x);
				}
			}
		}
		/// <summary>
		/// Validates lastbuilddate element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLastBuildDate(RssVersion version) {
			if ( this.LastBuildDate == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ||
				version == RssVersion.RSS_0_92 ||
				version == RssVersion.RSS_2_0_1 ) {

			}
		}
		/// <summary>
		/// Validates docs element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDocs(RssVersion version) {
			if ( this.Docs == null ) {
				return;
			}
			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Rating.Length > RSS_MAXLENGTH_DOCS ) {
                    string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH, RSS_ELEMENT_DOCS, RSS_MAXLENGTH_DOCS.ToString(), this.Docs.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// Validates language element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateLanguage(RssVersion version) {
			if ( this.Language == null ) {
				return;
			}
			//
			// Allow all language codes to pass through, even if they may be wrong.
			//
		}
		/// <summary>
		/// Validates description element.
		/// </summary>
		/// <param name="version"></param>
		private void ValidateDescription(RssVersion version) {
			if ( this.Description == null ) {
				return;
			}

			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Description.Length > RSS_MAXLENGTH_DESCRIPTION ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_DESCRIPTION,RSS_MAXLENGTH_DESCRIPTION.ToString(),this.Description.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		private void ValidateTitle(RssVersion version) {
			if ( this.Title == null ) {
				return;
			}

			if ( version == RssVersion.RSS_0_91 ) {
				if ( this.Title.Length > RSS_MAXLENGTH_TITLE ) {
					string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_ELEMENT_HAS_WRONG_LENGTH,RSS_ELEMENT_TITLE,RSS_MAXLENGTH_TITLE.ToString(),this.Title.Length.ToString());
					throw new SyndicationValidationException(msg);
				}
			}

		}
		/// <summary>
		/// Validates the link element.
		/// </summary>
		/// <returns></returns>
		private void ValidateLink(RssVersion version) {
			if ( this.Link == null ) {
				return;
			}
			
			if ( version == RssVersion.RSS_0_91 ) {				
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_91)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_91, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg);				
			}// if
			else if ( version == RssVersion.RSS_0_92 ) {				
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_0_92)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_0_92, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg);				
			}// if
			else if ( version == RssVersion.RSS_2_0_1 ) {
				string link = this.Link.ToLower().Trim();
                foreach (string validPrefix in Rss.RSS_VALIDVALUES_LINK_URL_2_0_1)
                {
					if ( link.StartsWith( validPrefix ) ) {
						return;
					}
				}
                string msg = string.Format(Rss.RSS_ERRORMESSAGE_VALIDATION_LINK_URL_WRONG_TYPE_2_0_1, RSS_ELEMENT_LINK, this.Link);
				throw new SyndicationValidationException(msg);				
			}
		}
		#endregion		
		
		#region Required Elements
		/// <summary>
		/// Title backing field.
		/// </summary>
		private string m_title;
		/// <summary>
		/// The name of the channel. It's how people refer to your service. 
		/// If you have an HTML website that contains the same information as 
		/// your RSS file, the title of your channel should be the same as the 
		/// title of your website.  
		/// </summary>
		public string Title {
			get {
				return m_title;
			}
			set {
				m_title = value;
			}
		}		 
		/// <summary>
		/// Link backing field.
		/// </summary>
		private string m_link;
		/// <summary>
		/// The URL to the HTML website corresponding to the channel.
		/// </summary>
		public string Link {
			get {
				return m_link;
			}
			set {
				m_link = value;
			}
		}		
		/// <summary>
		/// Description backing field.
		/// </summary>
		private string m_description;
		/// <summary>
		/// Phrase or sentence describing the channel.
		/// </summary>
		public string Description {
			get {
				return m_description;
			}
			set {
				m_description = value;
			}
		} 
		/// <summary>
		/// Language backing field.
		/// </summary>
		private string m_language;
		/// <summary>
		/// The language the channel is written in. This allows aggregators to 
		/// group all Italian language sites, for example, on a single page. A list 
		/// of allowable values for this element, as provided by Netscape, is here. You 
		/// may also use values defined by the W3C.
		/// </summary>
		public string Language {
			get {
				return m_language;
			}
			set {
				m_language = value;
			}
		}
		/// <summary>
		/// Image backing field.
		/// </summary>
		private RssImage m_image;
		/// <summary>
		/// Specifies a GIF, JPEG or PNG image that can be displayed with the channel. 
		/// </summary>
		public RssImage Image {
			get {
				return m_image;
			}
			set {
				m_image = value;
			}
		}		
		#endregion

		#region Optional Elements
		/// <summary>
		/// Items backing field.
		/// </summary>
		private RssItemCollection m_items;
		/// <summary>
		/// A channel may contain any number of <item>s. An item may represent a 
		/// "story" -- much like a story in a newspaper or magazine; if so its 
		/// description is a synopsis of the story, and the link points to the 
		/// full story. An item may also be complete in itself, if so, the description 
		/// contains the text (entity-encoded HTML is allowed; see examples), and the link 
		/// and title may be omitted. All elements of an item are optional, however at 
		/// least one of title or description must be present.
		/// </summary>
		public RssItemCollection Items {
			get {
				return m_items;
			}
			set {
				m_items = value;
			}
		}		
		/// <summary>
		/// Copyright backing field.
		/// </summary>
		private string m_copyright;
		/// <summary>
		/// Copyright notice for content in the channel.
		/// </summary>
		public string Copyright {
			get { 
				return m_copyright;
			}
			set {
				m_copyright = value;
			}
		}
		/// <summary>
		/// ManagingEditor backing field.
		/// </summary>
		private string m_managingEditor;
		/// <summary>
		/// Email address for person responsible for editorial content. 
		/// </summary>
		public string ManagingEditor {
			get {
				return m_managingEditor;
			}
			set {
				m_managingEditor = value;
			}
		}
		/// <summary>
		/// WebMaster backing field.
		/// </summary>
		private string m_webMaster;
		/// <summary>
		/// Email address for person responsible for technical issues relating to channel. 
		/// </summary>
		public string WebMaster {
			get {
				return m_webMaster;
			}
			set {
				m_webMaster = value;
			}
		}
		/// <summary>
		/// PubDate backing field.
		/// </summary>
		private string m_pubDate;
		/// <summary>
		/// The publication date for the content in the channel. For example, 
		/// the New York Times publishes on a daily basis, the publication date 
		/// flips once every 24 hours. That's when the pubDate of the channel changes. 
		/// All date-times in RSS conform to the Date and Time Specification of RFC 822, 
		/// with the exception that the year may be expressed with two characters or four 
		/// characters (four preferred). 
		/// 
		/// All date-times in RSS conform to the Date and 
		/// Time Specification of RFC 822.
		/// </summary>
		public string PubDate {
			get {
				return m_pubDate;
			}
			set {
				m_pubDate = value;
			}
		}
		/// <summary>
		/// LastBuildDate backing field.
		/// </summary>
		private string m_lastBuildDate;
		/// <summary>
		/// The last time the content of the channel changed. 
		/// </summary>
		public string LastBuildDate {
			get {
				return m_lastBuildDate;
			}
			set {
				m_lastBuildDate = value;
			}
		}
		/// <summary>
		/// Category backing field.
		/// </summary>
		private RssCategoryCollection m_category;
		/// <summary>
		/// Specify one or more categories that the channel belongs to. 
		/// Follows the same rules as the <item>-level category element.  
		/// </summary>
		public RssCategoryCollection Category {
			get {
				return m_category;
			}
			set {
				m_category = value;
			}
		}
		/// <summary>
		/// Generator backing field.
		/// </summary>
		private string m_generator;
		/// <summary>
		/// A string indicating the program used to generate the channel. 
		/// </summary>
		public string Generator {
			get {
				return m_generator;
			}
			set {
				m_generator = value;
			}
		}
		/// <summary>
		/// Docs backing field.
		/// </summary>
		private string m_docs;
		/// <summary>
		/// A URL that points to the documentation for the format used in the 
		/// RSS file. It's probably a pointer to this page. It's for people who 
		/// might stumble across an RSS file on a Web server 25 years from now and 
		/// wonder what it is. 
		/// </summary>
		/// <example>
		/// 
		/// </example>
		public string Docs {
			get {
				return m_docs;
			}
			set {
				m_docs = value;
			}
		}
		/// <summary>
		/// Cloud backing field.
		/// </summary>
		private RssCloud m_cloud;
		/// <summary>
		/// Allows processes to register with a cloud to be notified of updates 
		/// to the channel, implementing a lightweight publish-subscribe protocol for 
		/// RSS feeds.
		/// </summary>
		public RssCloud Cloud {
			get {
				return m_cloud;
			}
			set {
				m_cloud = value;
			}
		}
		/// <summary>
		/// TTL backing field.
		/// </summary>
		private string m_ttl;
		/// <summary>
		/// TTL stands for time to live. It's a number of minutes that indicates how 
		/// long a channel can be cached before refreshing from the source. 
		/// It's a number of minutes that indicates how long a channel can be cached 
		/// before refreshing from the source.
		/// </summary>
		public string TTL {
			get {
				return m_ttl;
			}
			set {
				m_ttl = value;
			}
		}		
		/// <summary>
		/// Rating backing field.
		/// </summary>
		private string m_rating;
		/// <summary>
		/// The PICS rating for the channel.
		/// </summary>
		public string Rating {
			get {
				return m_rating;
			}
			set {
				m_rating = value;
			}
		}
		/// <summary>
		/// TextInput backing field.
		/// </summary>
		private RssTextInput m_textInput;
		/// <summary>
		/// Specifies a text input box that can be displayed with the channel. 
		/// </summary>
		public RssTextInput TextInput {
			get {
				return m_textInput;
			}
			set {
				m_textInput = value;
			}
		}
		/// <summary>
		/// SkipHours backing field.
		/// </summary>
		private RssSkipHourCollection m_skipHours;
		/// <summary>
		/// A hint for aggregators telling them which hours they can skip.
		/// </summary>
		public RssSkipHourCollection SkipHours {
			get {
				return m_skipHours;
			}
			set {
				m_skipHours = value;
			}
		}
		/// <summary>
		/// SkipHours backing field.
		/// </summary>
		private RssSkipDayCollection m_skipDays;
		/// <summary>
		/// A hint for aggregators telling them which hours they can skip.
		/// </summary>
		public RssSkipDayCollection SkipDays {
			get {
				return m_skipDays;
			}
			set {
				m_skipDays = value;
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
		#endregion		

	}// class
}// namespace
