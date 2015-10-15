using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.HSEnumerations;
using NLog;

namespace HSCSReader.Replay {
	public class EntityState {
		protected static Logger Logger = LogManager.GetCurrentClassLogger();
		public Int32 Id;
		public Dictionary<GameTag, Int32> Tags;
		public Dictionary<String, String> Attributes;
		public List<Metric> Metrics;

		public EntityState() {
			Tags = new Dictionary<GameTag, Int32>();
			Attributes = new Dictionary<String, String>();
			Metrics = new List<Metric>();
		}
	}
}
