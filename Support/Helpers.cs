// <copyright file="Helpers.cs" company="SpectralCoding.com">
//     Copyright (c) 2015 SpectralCoding
// </copyright>
// <license>
// This file is part of HSCSReader.
// 
// HSCSReader is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// HSCSReader is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HSCSReader.Replay;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support {
	public static class Helpers {
		/// <summary>
		/// Converts a GameTag to a string.
		/// </summary>
		/// <param name="gameTagType">The type of GameTag.</param>
		/// <param name="enumValue">The related value.</param>
		/// <returns>A string descibing the GameTag's type.</returns>
		public static String GameTagValueToString(GameTag gameTagType, Int32 enumValue) {
			if (Enum.IsDefined(typeof(GameTag), gameTagType)) {
				switch (gameTagType) {
					case GameTag.CARDTYPE:
						return ((CardType)enumValue).ToString();
					case GameTag.RARITY:
						return ((Rarity)enumValue).ToString();
					case GameTag.ZONE:
						return ((Zone)enumValue).ToString();
					case GameTag.STEP:
						return ((Step)enumValue).ToString();
					case GameTag.NEXT_STEP:
						return ((Step)enumValue).ToString();
					case GameTag.STATE:
						return ((State)enumValue).ToString();
					case GameTag.CLASS:
						return ((CardClass)enumValue).ToString();
				}
				return enumValue.ToString();
			}
			return enumValue.ToString();
		}

		/// <summary>
		/// Combines all metrics together by removing duplicate entries, overriding previous values, or adding to a list.
		/// </summary>
		/// <param name="metricsNew">The list of metrics to add.</param>
		/// <param name="metricsExisting">The existing metric list.</param>
		/// <param name="forceAdditive">Forces override entries to be additive.</param>
		/// <returns></returns>
		public static void IntegrateMetrics(List<Metric> metricsNew, List<Metric> metricsExisting,
											Boolean forceAdditive = false) {
			foreach (Metric curNewMetric in metricsNew) {
				Boolean isNewMetric = true;
				foreach (Metric curExistingMetric in metricsExisting) {
					if (curExistingMetric.Name == curNewMetric.Name) {
						if (curExistingMetric.MetricType == curNewMetric.MetricType) {
							if (forceAdditive) {
								// For rolling up final minion metrics across games we want to force the values to be
								// added to the list, instead of overwritten for metrics like NUM_TURNS_IN_PLAY.
								curExistingMetric.Values.AddRange(curNewMetric.Values);
							} else {
								if (curExistingMetric.MetricType == MetricType.AddToList) {
									curExistingMetric.Values.AddRange(curNewMetric.Values);
								} else if (curExistingMetric.MetricType == MetricType.AddToValue) {
									if ((curExistingMetric.Values.Count > 1) || (curNewMetric.Values.Count > 1)) {
										throw new NotSupportedException(
											$"Cannot use {curExistingMetric.MetricType} with a List<Int32> with multiple items.");
									}
									curExistingMetric.Values[0] += curNewMetric.Values[0];
								} else if (curExistingMetric.MetricType == MetricType.Overwrite) {
									// Can't assign Values so have to clear and copy.
									curExistingMetric.Values.Clear();
									curExistingMetric.Values.AddRange(curNewMetric.Values);
								} else if (curExistingMetric.MetricType == MetricType.OverwriteMax) {
									if (curExistingMetric.Values.FirstOrDefault() < curNewMetric.Values.FirstOrDefault()) {
										curExistingMetric.Values[0] = curNewMetric.Values.FirstOrDefault();
									}
								}
							}
							isNewMetric = false;
						} else {
							throw new NotSupportedException(
								$"Cannot integrate {curNewMetric.MetricType} into {curExistingMetric.MetricType} ({curNewMetric.Name}).");
						}
					}
				}
				if (isNewMetric) {
					metricsExisting.Add(curNewMetric);
				}
			}
		}

		/// <summary>
		/// Returns the MD5 Hash of the input string.
		/// </summary>
		/// <param name="md5Hash">The MD5 Hash Object.</param>
		/// <param name="input">The string to compute the MD5 hash of.</param>
		/// <returns>A string containing the MD5 hash of the input string.</returns>
		public static String GetMd5Hash(MD5 md5Hash, String input) {
			// Convert the input string to a byte array and compute the hash.
			Byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			foreach (Byte t in data) {
				sBuilder.Append(t.ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}
	}
}
