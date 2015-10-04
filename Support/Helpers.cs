using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Replay;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support {
	public static class Helpers {
		public static String GameTagValueToString(GameTag gameTagType, Int32 enumValue) {
			if (Enum.IsDefined(typeof(GameTag), gameTagType)) {
				switch (gameTagType) {
					case GameTag.CARDTYPE: return ((CardType)enumValue).ToString();
					case GameTag.RARITY: return ((Rarity)enumValue).ToString();
					case GameTag.ZONE: return ((Zone)enumValue).ToString();
					case GameTag.STEP: return ((Step)enumValue).ToString();
					case GameTag.NEXT_STEP: return ((Step)enumValue).ToString();
					case GameTag.STATE: return ((State)enumValue).ToString();
					case GameTag.CLASS: return ((CardClass)enumValue).ToString();
				}
				return enumValue.ToString();
			}
			return enumValue.ToString();
		}

		public static List<Metric> IntegrateMetrics(List<Metric> metricsNew, List<Metric> metricsExisting, Boolean forceAdditive = false) {
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
								}
								else if (curExistingMetric.MetricType == MetricType.AddToValue) {
									if ((curExistingMetric.Values.Count > 1) || (curNewMetric.Values.Count > 1)) {
										throw new NotSupportedException(
											$"Cannot use {curExistingMetric.MetricType} with a List<Int32> with multiple items.");
									}
									curExistingMetric.Values[0] += curNewMetric.Values[0];
								}
								else if (curExistingMetric.MetricType == MetricType.Overwrite) {
									// Can't assign Values so have to clear and copy.
									curExistingMetric.Values.Clear();
									curExistingMetric.Values.AddRange(curNewMetric.Values);
								}
							}
							isNewMetric = false;
						} else {
							throw new NotSupportedException($"Cannot integrate {curNewMetric.MetricType} into {curExistingMetric.MetricType} ({curNewMetric.Name}).");
						}
					}
				}
				if (isNewMetric) {
					metricsExisting.Add(curNewMetric);
				}
			}
			return metricsExisting;
		}

		public static string GetMd5Hash(MD5 md5Hash, string input) {

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++) {
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

	}
}
