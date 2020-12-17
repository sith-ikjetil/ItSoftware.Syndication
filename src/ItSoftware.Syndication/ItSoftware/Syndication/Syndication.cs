using System;
using System.Threading;
using System.Net;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml;
namespace ItSoftware.Syndication {
	/// <summary>
	/// Syndication class.
	/// </summary>
	public static class Syndication {		

		#region Syndication Internal Static Readonly Variables
		internal static readonly string SYNDICATION_FORMAT_INVALID = "Syndication format was invalid.";
        internal static readonly string URI_FORMAT_UNSUPPORTED_OR_INVALID = "Unsupported or invalid uri. Only supports http, ftp and file.";
		#endregion              		

		#region Private Const Data
		private const string HTTP_HEADER_AUTHENTICATE = "WWW-Authenticate";
		private const string HTTP_HEADER_AUTHENTICATE_NTLM_VALUE ="Negotiate,NTLM";
	    private const string DEFAULT_CHARACTER_SET = "UTF-8";
		private const string CHARSET_TOKEN = "charset";
		#endregion

		#region Public Static Methods'
        /// <summary>
        /// Load Syndication.
        /// </summary>
        /// <param name="syndication"></param>
        /// <returns></returns>
        public static SyndicationBase LoadSyndication(string syndication)
        {
            if (syndication == null) {
                throw new ArgumentNullException("syndication");
            }            
            return CreateSyndicationObject(syndication);
        }
		/// <summary>
		/// Load syndication.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static SyndicationBase Load(System.Uri uri) {
            return Load(uri, null);            
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="pICredentials"></param>
        /// <returns></returns>
        public static SyndicationBase Load(System.Uri uri, ICredentials credentials)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            string syndication;
            if (uri.IsFile)
            {
                //
                // Let xmldocument load the xml file. If it succeeds then get the OuterXml.
                //
                XmlDocument xd = new XmlDocument();
                xd.Load(uri.AbsolutePath);
                syndication = xd.OuterXml;
            }
            else
            {
                //
                // Try http/ftp request with provided credentials first. If it fails the provide default credentials.
                //
                try
                {
                    syndication = Download(uri, credentials);                                
                }
                catch (WebException we)
                {
                    if (we.Response != null)
                    {
                        string authHeader = we.Response.Headers[HTTP_HEADER_AUTHENTICATE];
                        if (authHeader == HTTP_HEADER_AUTHENTICATE_NTLM_VALUE)
                        {
                            syndication = Download(uri, CredentialCache.DefaultCredentials);
                        }
                    }                    
                    throw;                    
                }
                
            }
            return CreateSyndicationObject(syndication);
        }
		/// <summary>
		/// Download.
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="useNTLM"></param>
		/// <returns></returns>
		private static string Download(System.Uri uri, ICredentials credentials) {            
            if (uri.Scheme.ToLower() == "http")
            {                
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                httpWebRequest.Method = WebRequestMethods.Http.Get;
                httpWebRequest.Timeout = 3600000;                
                if (credentials != null)
                {
                    httpWebRequest.Credentials = credentials;
                }
                string syndication = null;
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    Stream s = httpWebResponse.GetResponseStream();

                    
                    MemoryStream ms = new MemoryStream();                    
                    byte[] b = new byte[1];
                    int count = 0;
                    do {
                        count = s.Read(b, 0, 1);
                        if (count != 0)
                        {
                            ms.Write(b, 0, 1);
                        }
                    } while ( count != 0 );
                    ms.Seek(0, SeekOrigin.Begin);

                    byte[] buffer = new byte[ms.Length];
                    ms.Read(buffer, 0, Convert.ToInt32(ms.Length));                    
                    ms.Seek(0, SeekOrigin.Begin);
                    
                    Encoding srcEncoding = Encoding.GetEncoding(GetCharacterSet(httpWebResponse.CharacterSet, httpWebResponse.ContentType));
                    using (StreamReader sr = new StreamReader(ms, srcEncoding))
                    {
                        syndication = sr.ReadToEnd();
                    }// using


                    // If we did not have a proper encoding recode it
                    if (string.IsNullOrEmpty(httpWebResponse.CharacterSet) && IsXmlContentType(httpWebResponse.ContentType) )
                    {
                        ms = new MemoryStream(buffer);
                        ms.Seek(0, SeekOrigin.Begin);
                        syndication = ConvertToEncoding(syndication, ms);
                    }// if                 
                    
                }
                return syndication;
            }
            else if (uri.Scheme.ToLower() == "ftp")
            {                
                FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);                
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpWebRequest.Timeout = 3600000;                
                if (credentials != null)
                {
                    ftpWebRequest.Credentials = credentials;
                }                
                using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse())
                {                    
                    Stream s = ftpWebResponse.GetResponseStream();
                    string filename = Path.GetTempFileName();
                    using (FileStream fs = new FileStream(filename, FileMode.Create))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = s.Read(buffer, 0, 2048)) > 0)
                        {
                            fs.Write(buffer, 0, len);
                        }
                    }
                    XmlDocument xd = new XmlDocument();
                    xd.Load(filename);
                    return xd.OuterXml;                    
                }
            }
            throw new ArgumentException(Syndication.URI_FORMAT_UNSUPPORTED_OR_INVALID, "uri");
		}
        /// <summary>
        /// Check content type for xml data.
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private static bool IsXmlContentType(string contentType)
        {            
            if (string.IsNullOrEmpty(contentType))
            {
                return false;
            }

            string type = contentType.ToLower();

            if (type == "application/xml")
            {
                return true;
            }

            if (type == "text/xml")
            {
                return true;
            }

            if (type == "xml")
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Convert an xml document from one encoding to another.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="srcEncoding"></param>
        /// <returns></returns>
        private static string ConvertToEncoding(string xml, Stream s)
        {
            int indexStart = xml.IndexOf("encoding=\"");
            if (indexStart > 0)
            {
                int indexStop = xml.IndexOf("\"", indexStart + 10);
                string xmlEncoding = xml.Substring(indexStart + 10, indexStop - indexStart - 10);

                // Get dst encoding
                Encoding dstEncoding = Encoding.GetEncoding(xmlEncoding);

                using (StreamReader sr = new StreamReader(s, dstEncoding))
                {
                    xml = sr.ReadToEnd();
                }

/*                // Convert from srcEncoding to dstEncoding
                byte[] byteString = srcEncoding.GetBytes(xml);
                Encoding.Convert(srcEncoding, dstEncoding, byteString);
                char[] chrs = dstEncoding.GetChars(byteString);
                StringBuilder buffer = new StringBuilder();
                buffer.Append(chrs);
                xml = buffer.ToString();
 * */
            }
            return xml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syndication"></param>
        /// <returns></returns>
        internal static string NormalizeSyndication(string syndication)
        {
            if (syndication == null)
            {
                throw new ArgumentNullException("syndication");
            }

            int indexA = syndication.IndexOf('<');
            if (indexA != 0)
            {
                syndication = syndication.Substring(indexA, syndication.Length - indexA);
            }

            int indexB = syndication.LastIndexOf('>');
            if (indexB != syndication.Length - 1)
            {
                syndication = syndication.Substring(0, indexB+1);
            }
            return syndication;
        }
		/// <summary>
		/// Creates a syndication object from syndication data.
		/// </summary>
		/// <param name="syndication"></param>
		/// <returns></returns>
		private static SyndicationBase CreateSyndicationObject(string syndication) {
			if ( syndication == null ) {
				throw new SyndicationFormatInvalidException(Syndication.SYNDICATION_FORMAT_INVALID);
			}

            syndication = NormalizeSyndication(syndication);

			//
			// Try to create RSS.
			//
            try
            {
                return new ItSoftware.Syndication.Rss.Rss(syndication);
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (StackOverflowException)
            {
                throw;
            }            
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception)
            {
                // Swallow it.
            }

            //
            // Try to create RDF.
            //
			try {
				return new ItSoftware.Syndication.Rdf.Rdf(syndication);
			}
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (StackOverflowException)
            {
                throw;
            }            
            catch (ThreadAbortException)
            {
                throw;
            }
			catch ( Exception ) {
                // Swallow it.
			}

            //
            // Try to create ATOM.
            // 
            try
            {
                return new ItSoftware.Syndication.Atom.Atom(syndication);
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (StackOverflowException)
            {
                throw;
            }            
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception) {
                // Swallow it.
            }

			//
			// Failed to create syndication object.
			//
			throw new SyndicationFormatInvalidException(Syndication.SYNDICATION_FORMAT_INVALID);
		}
		/// <summary>
		/// Gets the characterset from content type.
		/// </summary>
		/// <param name="contentType"></param>
		/// <returns></returns>
		private static string GetCharacterSet(string characterSet, string contentType) {
            if (!string.IsNullOrEmpty(characterSet))
            {
                return characterSet.Trim(new char[] {' ', '\"', '\''});
            }

			if ( string.IsNullOrEmpty(contentType) ) {
				return DEFAULT_CHARACTER_SET;
			}			

			string[] elements = contentType.ToLower().Split(';');
			foreach ( string element in elements ) {
				string[] parts = element.Trim().Split('=');
				if ( parts[0] == CHARSET_TOKEN ) {
					return parts[1];
				}
			}
			return DEFAULT_CHARACTER_SET;
		}
		#endregion

	}// class
}// namespace
