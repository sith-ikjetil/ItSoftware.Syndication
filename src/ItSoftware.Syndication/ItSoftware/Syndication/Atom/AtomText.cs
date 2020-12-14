using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ItSoftware.Syndication;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomText : AtomElementBase
    {
        public AtomText()
        {
        }

        public AtomText(string text, AtomTextType type)
        {
            this.Text = text;
            this.Type = type;
        }

        public AtomText(XmlNode xnText)
        {
            if (xnText == null)
            {
                throw new ArgumentNullException("xnText");
            }
            this.DeserializeFromXml(xnText);
        }

        private void DeserializeFromXml(XmlNode xnText)
        {
            this.Text = xnText.InnerText;                        
            XmlAttribute xaType = xnText.Attributes["type"];
            if (xaType != null)
            {
                switch (xaType.InnerText.ToLower())
                {
                    case "":
                    case "text":
                        this.Type = AtomTextType.Text;
                        break;
                    case "html":
                        this.Type = AtomTextType.Html;
                        break;
                    case "xhtml":
                        this.Type = AtomTextType.XHtml;
                        break;
                    default:
                        this.Type = AtomTextType.Unknown;
                        break;
                };                
            }
          
            //
            // Deserialize modules.
            //
            this.Modules = new SyndicationModuleCollection(xnText, new List<SyndicationModuleExclusionElement>() );
        }
        

        #region Required Fields
        private string m_text;
        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                m_text = value;
            }
        }
        #endregion

        #region Optional Fields
        private AtomTextType m_type = AtomTextType.NotSet;
        public AtomTextType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        #endregion

        /// <summary>
        /// Modules backing field.
        /// </summary>
        private SyndicationModuleCollection m_modules;
        /// <summary>
        /// Gets or sets the syndication modules.
        /// </summary>
        public SyndicationModuleCollection Modules
        {
            get
            {
                return m_modules;
            }
            set
            {
                m_modules = value;
            }
        }

        internal void SerializeToXml(AtomVersion version, XmlElement xe)
        {
            if (version == AtomVersion.Atom_1_0)
            {
                switch (this.Type)
                {
                    case AtomTextType.NotSet:
                        {
                            if (this.Text != null)
                            {
                                xe.InnerText = this.Text;
                            }
                            break;
                        }
                    case AtomTextType.Html:
                        {
                            if (this.Text != null)
                            {
                                xe.InnerText = this.Text;
                            }
                            XmlAttribute xaType = xe.OwnerDocument.CreateAttribute("type");
                            xaType.InnerText = "html";
                            xe.Attributes.Append(xaType);
                            break;
                        }                    
                    case AtomTextType.Text:
                        {
                            if (this.Text != null)
                            {
                                xe.InnerText = this.Text;
                            }
                            XmlAttribute xaType = xe.OwnerDocument.CreateAttribute("type");
                            xaType.InnerText = "text";
                            xe.Attributes.Append(xaType);
                            break;
                        }
                    case AtomTextType.XHtml:
                        {
                            if (this.Text != null)
                            {
                                xe.InnerXml = this.Text;
                            }
                            XmlAttribute xaType = xe.OwnerDocument.CreateAttribute("type");
                            xaType.InnerText = "xhtml";
                            xe.Attributes.Append(xaType);
                            break;
                        }
                    default:
                        {
                            string msg = string.Format(Atom.ATOM_ERRORMESSAGE_SERIALIZATION_ILLEGAL_VALUE, "AtomTextType", xe.Name);
                            throw new SyndicationSerializationException(msg);                                            
                        }
                };
                //
                // Always serialize modules last.
                //
                if (this.Modules != null)
                {
                    this.Modules.SerializeToXml(xe);
                }                
            }
        }
    }
}
