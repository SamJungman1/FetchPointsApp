using Newtonsoft.Json;
using System;
namespace FetchPointsApp.Models
{
	/// <summary>
	/// Serializeable Class setup to handle add transaction requests
	/// </summary>
	[Serializable]
	public class Payers
	{
		///Name of the payer
		public string Payer { get; set; }

		///Points submitted by payer, cannot be less than 0 or is set to 0
		public int Points { get; set; }

		///Timestamp of transaction of payer, must be unique as transactions are asyncronous 
		public DateTime Timestamp { get; set; }

		/// <summary>
		/// Returns formatted string of the payer name and points of this Payer
		/// </summary>
		/// <returns>Formatted string</returns>
		public string GetNamePoints()
		{
			return "{ \"payer\": \"" + Payer + "\", \"points\": \"" + Points + "\"}";
		}

		/// <summary>
		/// Reduces points by param to no lower than 0 
		/// </summary>
		/// <param name="reduction">int points to reduce</param>
		/// <returns>The amount of points reduced</returns>
		public int DeductPoints(int reduction)
		{
			int _temp = Points;
			Points = Math.Max(Points - reduction, 0);
			return _temp - Points;
		}
	}
}