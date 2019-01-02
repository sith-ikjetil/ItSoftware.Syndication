using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
namespace ItSoftware.Syndication.Atom
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomDateTime
    {
        #region Private Const Fields
		private const string ATOM_ERRORMESSAGE_DATEFORMAT_INVALID = "Invalid date/time format.";		
		private const string ATOM_ERRORMESSAGE_DATEFORMAT_INVALID_OFFSET = "Invalid time offset.";
        private const string ATOM_DATEFORMAT_REGEXP
            = @"^(?<year>\d\d\d\d)"
            + @"(-(?<month>\d\d)(-(?<day>\d\d)(T(?<hour>\d\d):(?<min>\d\d)(:(?<sec>\d\d)(?<ms>\.\d+)?)?"
            + @"(?<offset>(Z|[+\-]\d\d:\d\d)))?)?)?$";
		#endregion		

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="offset"></param>
		public AtomDateTime(DateTime dateTime,TimeSpan offset) {			
			this.m_dateTime = dateTime;	
			this.m_offset = offset;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="dateTime"></param>
        public AtomDateTime(string dateTime)
        {
			Parse(dateTime);
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Parses the date/time string.
		/// </summary>
		/// <param name="dateTime"></param>
		private void Parse(string dateTime) {
			Regex regEx = new Regex(ATOM_DATEFORMAT_REGEXP);
			Match m = regEx.Match(dateTime);
			if ( m.Success ) {
				try {					
					int year = int.Parse(m.Groups["year"].Value);										    
					int month = int.Parse(m.Groups["month"].Value);											
					int day = m.Groups["day"].Success ? int.Parse(m.Groups["day"].Value) : 1;
					int hour = m.Groups["hour"].Success ? int.Parse(m.Groups["hour"].Value) : 0;
					int min = m.Groups["min"].Success ? int.Parse(m.Groups["min"].Value) : 0;
					int sec = m.Groups["sec"].Success ? int.Parse(m.Groups["sec"].Value) : 0;
					int ms = m.Groups["ms"].Success ? (int)Math.Round((1000*double.Parse(m.Groups["ms"].Value))) : 0;

					TimeSpan ofs = TimeSpan.Zero;
					if (m.Groups["offset"].Success) {
						ofs = ParseOffset(m.Groups["offset"].Value);						
					}

					// datetime is stored in UTC
					this.m_dateTime = new DateTime(year, month, day, hour, min, sec, ms)-ofs;
					this.m_offset = ofs;
				}
				catch (Exception ex) {
					throw new FormatException(ATOM_ERRORMESSAGE_DATEFORMAT_INVALID, ex);
				}

			}
			else {
				throw new FormatException(ATOM_ERRORMESSAGE_DATEFORMAT_INVALID);
			}
		}				
		/// <summary>
		/// Parses the offset part.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private TimeSpan ParseOffset(string s) {
			if ( s == string.Empty )
				return TimeSpan.Zero;

            s = s.Trim();

            if (s == "Z")
            {
                return TimeSpan.Zero;
            }
            bool negate = (s[0] == '-');
            
            s = s.Substring(1);
            if (s.Length != 5)
            {
                throw new FormatException(ATOM_ERRORMESSAGE_DATEFORMAT_INVALID_OFFSET); 
            }

            TimeSpan t = TimeSpan.Parse(s);
            if (negate)
            {
                return t.Negate();
            }
            return t;
		}
		#endregion

		#region Public Overriden Methods
		/// <summary>
		/// Converts the DateTime and Offset to an RFC822 date/time string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			CultureInfo currentCultureInfo = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			StringBuilder dateTime = new StringBuilder();						
			try {
				dateTime.Append((this.DateTime+this.Offset).ToString("yyyy-MM-ddTHH:mm:ss"));
				if ( this.Offset.Hours == 0 && this.Offset.Minutes == 0 ) {
					dateTime.Append("Z");
				}
				else {
                    if (this.Offset > TimeSpan.Zero)
                    {
                        dateTime.Append("+");
                    }                    
					dateTime.Append(this.Offset.Hours.ToString("d02") + ":" + this.Offset.Minutes.ToString("d02"));
				}				
			}
			finally {
				Thread.CurrentThread.CurrentCulture = currentCultureInfo;
			}
			return dateTime.ToString();			
		}		
		#endregion

		#region Public Properties
		/// <summary>
		/// DateTime backing field.
		/// </summary>
		private DateTime m_dateTime;
		/// <summary>
		/// Gets the DateTime.
		/// </summary>
		public DateTime DateTime {
			get {
				return m_dateTime;
			}
		}
		/// <summary>
		/// Offset backing field.
		/// </summary>
		private TimeSpan m_offset;
		/// <summary>
		/// Gets the time offset.
		/// </summary>
		public TimeSpan Offset {
			get {
				return m_offset;
			}
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Convert a datetime and offset to a string.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static string ToString(DateTime dateTime, TimeSpan offset) {
            AtomDateTime rdt = new AtomDateTime(dateTime, offset);
			return rdt.ToString();
		}		
		#endregion
    }// class
}// namespace
