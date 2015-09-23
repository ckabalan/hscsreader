using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Replay;

namespace HSCSReader {
	class Program {
		static void Main(string[] args) {
			MetricTable.Load(@"E:\Programming\C#\150920 HSCSReader\_External\MetricList.csv");
			HSReplay Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\jleclanche.xml");
			Console.ReadLine();
		}
	}
}
