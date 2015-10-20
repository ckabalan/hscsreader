// <copyright file="Program.cs" company="SpectralCoding.com">
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
using System.IO;
using System.Reflection;
using HSCSReader.Replay;
using HSCSReader.Support.CardDefinitions;
using NLog;

namespace HSCSReader {
	internal class Program {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry function for the application.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		private static void Main(String[] args) {
			Console.SetBufferSize(150, 20000);
			Console.SetWindowSize(150, 50);
			Logger.Info("HSCSReader v" + Assembly.GetExecutingAssembly().GetName().Version + " started.");
			CardDefs.Load(@"E:\Programming\C#\150920 HSCSReader\_External\CardDefs.xml");
			HSReplay Temp;
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\ComplicatedControlWar.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\ControlWarRecording.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\jleclanche.xml");
			String[] logFiles = Directory.GetFiles(@"E:\Programming\C#\150920 HSCSReader\_External\Master Logs\{Combined XML\");
			foreach (String fileName in logFiles) {
				Temp = new HSReplay(fileName);
			}
			Logger.Info("Waiting for User Input before exiting.");
			Console.ReadLine();
		}
	}
}
