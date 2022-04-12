using System;
using System.Collections.Generic;
using System.Linq;

namespace FetchPointsApp.Models
{
	/// <summary>
	/// Data structure class meant to store Payers in order of ascending timestamps
	/// </summary>
	public class PointsManipulator
	{
		private SortedList<DateTime, Payers> _payers;
		/// <summary>
		/// Instantiates sorted list of Payers
		/// </summary>
		public PointsManipulator()
		{
			_payers = new SortedList<DateTime, Payers>();
		}

		/// <summary>
		/// Attempts to add Payers object to sorted list. The sorted list uses timestamp as it's primary key so it will throw an exception upon a duplicate timestamp
		/// </summary>
		/// <param name="p">Payers object</param>
		public void AddTransaction(Payers p)
		{
			p.Points = Math.Max(p.Points, 0);

			try { _payers.Add(p.Timestamp, p); }
			catch (ArgumentException e)
			{
				Console.WriteLine("Payers must not have duplicate timestamps");
			}
		}

		/// <summary>
		/// Moves through the list in order of oldest transaction, subtracting points to 0 then moving forward
		/// </summary>
		/// <param name="points">Number of points requested to be spent</param>
		/// <returns></returns>
		public string SpendPoints(int points)
		{
			int pointsRemaining = points;
			int reduction = 0;
			string returnString = "{";
			foreach (KeyValuePair<DateTime, Payers> p in _payers)
			{
				if (pointsRemaining > 0)
				{
					reduction = p.Value.DeductPoints(pointsRemaining);
					pointsRemaining = Math.Max(pointsRemaining - reduction, 0);
					if (pointsRemaining > 0)
						returnString += "{ \"payer\": \"" + p.Value.Payer + "\", \"points\": \"-" + reduction + "\"},\n";
					else
						returnString += "{ \"payer\": \"" + p.Value.Payer + "\", \"points\": \"-" + reduction + "\"}";
				}
			}
			returnString += "}";
			return returnString;
		}

		/// <summary>
		/// Fetches all current payers in the system and their points remaining
		/// </summary>
		/// <returns>Formatted String</returns>
		public string GetBalance()
		{
			string returnString = "[";
			foreach (KeyValuePair<DateTime, Payers> p in _payers)
			{
				if (p.Equals(_payers.LastOrDefault()))
					returnString += p.Value.GetNamePoints();
				else
					returnString += p.Value.GetNamePoints() + ",\n";
			}
			returnString += "]";
			return returnString;
		}
	}
}
