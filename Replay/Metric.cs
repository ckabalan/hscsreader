using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	public class Metric {
		public MetricType MetricType;
		public string Name;
		public List<int> Values = new List<int>();

		/// <summary>
		/// Initializes an instance of the Metric class.
		/// </summary>
		/// <param name="name">The name of the metrics.</param>
		/// <param name="metricType">The type of metric.</param>
		/// <param name="value">The value of the metric.</param>
		public Metric(string name, MetricType metricType, int value) {
			Name = name;
			MetricType = metricType;
			Values.Add(value);
		}
	}
}
