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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Replay;
using HSCSReader.Support.CardDefinitions;
using NLog;

namespace HSCSReader {
	internal class Program {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry function for the application.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		private static void Main(string[] args) {
			Console.SetBufferSize(150, 20000);
			Console.SetWindowSize(150, 50);
			logger.Info("HSCSReader v" + Assembly.GetExecutingAssembly().GetName().Version + " started.");
			CardDefs.Load(@"E:\Programming\C#\150920 HSCSReader\_External\CardDefs.xml");
			HSReplay Temp;
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\jleclanche.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\ControlWarRecording.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Druid_vs_Ascension-Paladin_1731-011015.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Druid_vs_TuwixTuwo-Mage_1724-011015.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_AnHerbWorm-Shaman_2046-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_click-Mage_1229-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_fradleybox-Druid_0943-200915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Jonova-Hunter_0928-200915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_KhayradDin-Priest_1254-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Mid-Priest_2107-210915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Montyman-Warlock_0934-200915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Nator-Mage_1246-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_pandaz0rd-Mage_0909-200915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Ratburn-Priest_1223-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_schan93-Hunter_2314-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_SpikyTortois-Mage_1821-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_SteadyTeddy-Warrior_1956-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Surter-Paladin_1237-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_terrokar-Paladin_2307-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Thebigniga90-Hunter_0921-200915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_toshiken-Paladin_2113-210915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Tx360mario-Priest_2146-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Tyruson-Hunter_2320-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_UNKNOWN_HUMAN_PLAYER-Paladin_2312-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Vetr-Warrior_2303-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Wallmee-Paladin_2243-110915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_xskattax-Mage_2134-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_ZangAO-Paladin_2156-130915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Zelgadisan-Rogue_2320-110915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\Dandelock-Warrior_vs_Zero-Paladin_2154-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\UNKNOWN_HUMAN_PLAYER-Paladin_vs_UNKNOWN_HUMAN_PLAYER-Shaman_2202-190915.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\c9285375596cde1d0a6df78c8da45189.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\5dee3f628ab434ddbaa3a073f151ad70.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\e448cd6031aa170ffbc2c8d18ae922fc.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\a8463342737a821d5e4703075b71e1b9.xml");
			Temp = new HSReplay(@"E:\Programming\C#\150920 HSCSReader\_External\Samples\DandelockLogs\e0a5150f9be265f8891af4f0309b7c05.xml");
			logger.Info("Waiting for User Input before exiting.");
			Console.ReadLine();
		}
	}
}