using System;
using System.Collections.Generic;
using System.Text;

namespace ItSoftware.Syndication.Rss
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRssValidate
    {
        void Validate(RssVersion version, bool validateContent);
    }
}
