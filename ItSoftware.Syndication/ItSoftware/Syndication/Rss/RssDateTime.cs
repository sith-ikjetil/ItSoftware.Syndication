using System;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
namespace ItSoftware.Syndication.Rss {
	/// <summary>
	/// Summary description for RssDateTime.
	/// </summary>
	public sealed class RssDateTime {

		#region Internal Static Readonly Data
		internal static readonly string RSS_DATEFORMAT_REGEXP = 
			@"^((Mon|Tue|Wed|Thu|Fri|Sat|Sun), *)?(?<day>\d\d?) +" +
			@"(?<month>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) +" +
			@"(?<year>\d\d(\d\d)?) +" +
			@"(?<hour>\d\d):(?<min>\d\d)(:(?<sec>\d\d))? +" +
			@"(?<offset>([+\-]?\d\d\d\d)|UT|GMT|EST|EDT|CST|CDT|MST|MDT|PST|PDT)$";
		internal static readonly string[] RSS_DATEFORMAT_MONTHNAMES = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		internal static readonly string RSS_ERRORMESSAGE_DATEFORMAT_INVALID = "Invalid date/time format.";
		internal static readonly string RSS_ERRORMESSAGE_DATEFORMAT_INVALID_MONTH = "Invalid month name.";
		internal static readonly string RSS_ERRORMESSAGE_DATEFORMAT_INVALID_OFFSET = "Invalid time offset.";
		#endregion		

		#region Constructors
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="offset"></param>
		public RssDateTime(DateTime dateTime,TimeSpan offset) {			
			this.m_dateTime = dateTime;	
			this.m_offset = offset;
		}
		/// <summary>
		/// Public constructor.
		/// </summary>
		/// <param name="dateTime"></param>
		public RssDateTime(string dateTime) {
			Parse(dateTime);
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Parses the date/time string.
		/// </summary>
		/// <param name="dateTime"></param>
		private void Parse(string dateTime) {
			Regex regEx = new Regex(RSS_DATEFORMAT_REGEXP);
			Match m = regEx.Match(dateTime);
			if ( m.Success ) {
				try {					
					int year = int.Parse(m.Groups["year"].Value);					
					if (year < 1000) {
						if (year < 50) {
							year = year + 2000; 
						}
						else {
							year = year + 1999;
						}
					}
    
					int month = ParseMonth(m.Groups["month"].Value);											

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
					throw new FormatException(RSS_ERRORMESSAGE_DATEFORMAT_INVALID, ex);
				}

			}
			else {
				throw new FormatException(RSS_ERRORMESSAGE_DATEFORMAT_INVALID);
			}
		}		
		/// <summary>
		/// Parses the month part.
		/// </summary>
		/// <param name="monthName"></param>
		/// <returns></returns>
		private int ParseMonth(string monthName) {
			for (int i = 0; i < RSS_DATEFORMAT_MONTHNAMES.Length; i++) {
				if ( monthName == RSS_DATEFORMAT_MONTHNAMES[i] ) {
					return i+1;
				}
			}
			throw new FormatException(RSS_ERRORMESSAGE_DATEFORMAT_INVALID_MONTH);
		}
		/// <summary>
		/// Parses the offset part.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private TimeSpan ParseOffset(string s) {
			if ( s == string.Empty )
				return TimeSpan.Zero;

			int hours = 0;
			switch (s) {
				case "UT":
				case "GMT":
					break;
				case "EDT": 
					hours = -4; 
					break;
				case "EST": 
				case "CDT": 
					hours = -5; 
					break;
				case "CST": 
				case "MDT": 
					hours = -6; 
					break;
				case "MST":
				case "PDT": 
					hours = -7; 
					break;
				case "PST": 
					hours = -8; 
					break;
				default:
					if (s[0] == '+') {
						if ( s.Length == 5 ) {
							string timeSpan = s.Substring(1, 2) + ":" + s.Substring(3, 2);
							return TimeSpan.Parse(timeSpan);
						}
						throw new FormatException(RSS_ERRORMESSAGE_DATEFORMAT_INVALID_OFFSET);
					}
					else if ( s.Length > 2 ) {
						return TimeSpan.Parse(s.Insert(s.Length-2, ":"));
					}
					throw new FormatException(RSS_ERRORMESSAGE_DATEFORMAT_INVALID_OFFSET);
			}
			return TimeSpan.FromHours(hours);
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
				dateTime.Append((this.DateTime+this.Offset).ToString("ddd, dd MMM yyyy HH:mm:ss "));
				if ( this.Offset.Hours == 0 && this.Offset.Minutes == 0 ) {
					dateTime.Append("GMT");
				}
				else {
					if ( this.Offset > TimeSpan.Zero ) {
						dateTime.Append("+");
					}
					dateTime.Append(this.Offset.Hours.ToString("d02") + this.Offset.Minutes.ToString("d02"));
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
			RssDateTime rdt = new RssDateTime(dateTime,offset);
			return rdt.ToString();
		}		
		#endregion

	}// class
}// namespace
