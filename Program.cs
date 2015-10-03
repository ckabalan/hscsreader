using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Replay;
using HSCSReader.Support.CardDefinitions;

namespace HSCSReader {
	class Program {
		static void Main(string[] args) {
			Console.SetBufferSize(150, 2000);
			Console.SetWindowSize(150, 50);
			CardDefs.Load(@"E:\Programming\C#\150920 HSCSReader\_External\CardDefs.xml");
			//HSReplay Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\jleclanche.xml");
			HSReplay Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\ControlWarRecording.xml");
			Console.ReadLine();
		}
	}
}
