# ItSoftware.Syndication.2017
An RSS/ATOM/RDF syndication library for .NET. See (http://www.ikjetil.no/Home/Item/10) for a downloadable assembly.
Also see (http://www.ikjetil.no/Home/Item/11) for a blog post about it.

An example of how to create an rss feed with comments:

```
Rss rss = new Rss();
rss.Channel.Category.Add(new RssCategory(".NET"));
rss.Channel.Cloud = new RssCloud("www.ikjetil.no", "80", "path", "registerProcedure", "soap");
rss.Channel.Copyright = "Copyright (c) Kjetil Kristoffer Solberg";
rss.Channel.Description = "Description here.";
rss.Channel.Generator = "ItSoftware.Syndication";
rss.Channel.Language = "English";
rss.Channel.PubDate = RssDateTime.ToString(DateTime.Now, new TimeSpan(1, 0, 0));
rss.Channel.Title = "Showcase RSS feed.";
rss.Channel.Description = "RSS Channel Description.";
rss.Channel.Link = "http://www.itsoftware.no";

RssItem item = new RssItem();
rss.Channel.Items.Add(item);

item.Title = "A blog about a C# LINQ Provider";
item.PubDate = new RssDateTime(DateTime.Now, new TimeSpan(1, 0, 0)).ToString();
item.Link = "http://www.mylinkdomain.domain/rss.aspx?T=123";
item.Description = "I've just created a C# linq provider...";
item.Author = "Kjetil Kristoffer Solberg";
item.Category = new RssCategoryCollection();
item.Category.Add(new RssCategory(".NET"));
item.Category.Add(new RssCategory("C#"));
item.Category.Add(new RssCategory("LINQ"));
item.Comments = "http://www.mylinkdomain.domain/rss.aspx?T123#WhatDoYouThink";
item.GUID = new RssGuid(Guid.NewGuid().ToString(),"true");

item.Modules = new ItSoftware.Syndication.SyndicationModuleCollection();

SyndicationModule moduleCommentRss = item.Modules.Add( new SyndicationModule("wfw", "http://wellformedweb.org/CommentAPI/", SyndicationModuleNamespacePosition.DocumentElement));

SyndicationModuleElement moduleElementCommentRss = new SyndicationModuleElement("commentRss", "http://www.mydomain.domain/rss.aspx?T=123&C=1",
SyndicationModuleElementValueType.InnerText);
moduleCommentRss.Elements.Add(moduleElementCommentRss);

SyndicationModule moduleComments = item.Modules.Add( new SyndicationModule("slash", "http://purl.org/rss/1.0/modules/slash/", SyndicationModuleNamespacePosition.DocumentElement));
            
SyndicationModuleElement moduleElementComments = new SyndicationModuleElement("comments", "1", SyndicationModuleElementValueType.InnerText);
moduleComments.Elements.Add(moduleElementComments);

string rssXml = rss.Save(RssVersion.RSS_2_0_1, "utf-8").OuterXml;
```
