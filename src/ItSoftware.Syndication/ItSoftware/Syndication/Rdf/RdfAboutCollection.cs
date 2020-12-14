using System;
using System.Collections.Generic;

namespace ItSoftware.Syndication.Rdf {
	/// <summary>
	/// RdfAboutCollection class.
	/// </summary>
	internal sealed class RdfAboutCollection : List<string> {		

		#region Constructors
		/// <summary>
		/// Public Constructor.
		/// </summary>
		public RdfAboutCollection() {			
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Add all items in another RdfAboutCollection to this collection.
		/// </summary>
		/// <param name="rac"></param>
		public void Add( RdfAboutCollection rac ) {
			foreach ( string s in rac ) {
				this.Add( s );
			}
		}
		/// <summary>
		/// Get number of times item is in collection.
		/// </summary>
		/// <param name="about"></param>
		/// <returns></returns>
		public int HitRate(string about) {
			int count = 0;
			foreach ( string s in this ) {
				if ( s == about ) {
					count++;
				}
			}
			return count;
		}
		/// <summary>
		/// Find out if the item is unique in the collection. Exist 0 or 1 time.
		/// </summary>
		/// <param name="about"></param>
		/// <returns></returns>
		public bool IsUnique(string about) {
			if ( HitRate(about) > 1 ) {
				return false;
			}
			return true;
		}
		#endregion
	
	}// class
}// namespace
