using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader {
	class Program {
		static void Main(string[] args) {
			HearthstoneReplay Temp = new HearthstoneReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\jleclanche.xml");
			Console.ReadLine();
		}
	}
}
