using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.Extensions {
	public static class DictionaryExtension {
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) {
			if (dict.ContainsKey(key)) {
				return dict[key];
			}
			return default(TValue);
		}
	}
}
