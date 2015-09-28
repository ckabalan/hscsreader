using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Replay;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support {
	public static class Helpers {
		public static String GameTagValueToString(GameTag gameTagType, Int32 enumValue) {
			if (Enum.IsDefined(typeof(GameTag), enumValue)) {
				switch (gameTagType) {
					case GameTag.CARDTYPE: return ((CardType)enumValue).ToString();
					case GameTag.RARITY: return ((Rarity)enumValue).ToString();
					case GameTag.ZONE: return ((Zone)enumValue).ToString();
				}
				return enumValue.ToString();
			}
			return enumValue.ToString();
		}

		public static List<Metric> IntegrateMetrics(List<Metric> metricsNew, List<Metric> metricsExisting) {
			foreach (Metric curNewMetric in metricsNew) {
				Boolean isNewMetric = true;
				foreach (Metric curExistingMetric in metricsExisting) {
					if (curExistingMetric.Name == curNewMetric.Name) {
						if (curExistingMetric.MetricType == curNewMetric.MetricType) {
							if (curExistingMetric.MetricType == MetricType.AddToList) {
								curExistingMetric.Values.AddRange(curNewMetric.Values);
							} else if (curExistingMetric.MetricType == MetricType.AddToValue) {
								if ((curExistingMetric.Values.Count > 1) || (curNewMetric.Values.Count > 1)) {
									throw new NotSupportedException($"Cannot use {curExistingMetric.MetricType} with a List<Int32> with multiple items.");
								}
								curExistingMetric.Values[0] += curNewMetric.Values[0];
							} else if (curExistingMetric.MetricType == MetricType.Overwrite) {
								// Can't assign Values so have to clear and copy.
								curExistingMetric.Values.Clear();
								curExistingMetric.Values.AddRange(curNewMetric.Values);
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



	}
}
