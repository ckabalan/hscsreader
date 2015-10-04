using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using HSCSReader.Replay;
using NLog;
using Logger = Cassandra.Logger;

namespace HSCSReader.DataStorage {
	public static class Uploader {
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
		private static ISession session;

		private static void CreateSession() {
			if (session == null) {
				Cluster cluster = Cluster.Builder().AddContactPoint("192.168.1.197").Build();
				session = cluster.Connect("hscs");
			}
		}

		public static void UploadReplay(Dictionary<String, List<Metric>> metrics) {
			CreateSession();
            foreach (KeyValuePair<String, List<Metric>> metricKVP in metrics) {
				SubmitCardMetrics(metricKVP.Key, metricKVP.Value);
			}
		}

		private static void SubmitCardMetrics(String cardID, List<Metric> metrics) {
			CreateSession();
			Row result = session.Execute("SELECT * FROM cards WHERE cardid='" + cardID + "'").FirstOrDefault();
			if (result == null) {
				PreparedStatement statement = session.Prepare("INSERT INTO cards (cardid) VALUES (:CARDID)");
				session.Execute(statement.Bind(new { CARDID = cardID }));
				result = session.Execute("SELECT * FROM cards WHERE cardid='" + cardID + "'").FirstOrDefault();
			}
			foreach (Metric curMetric in metrics) {
				String[] nameValSplit = curMetric.Name.Split('.');
				String[] nameSplit = nameValSplit[0].Split('_');
				String metricName = nameValSplit[0];
				Int32 metricSubIndex = -1;
                if (nameValSplit.Length == 2) { metricSubIndex = Convert.ToInt32(nameValSplit[1]); }
				String metricType = nameSplit[0];

				if (metricType == "COUNT") {
					if (nameValSplit.Length == 2) {
						SortedDictionary<Int32, Int32> existingMap = (SortedDictionary<Int32, Int32>)result[metricName];
						Int32 oldValue = 0;
						if ((existingMap != null) && (existingMap.ContainsKey(metricSubIndex))) {
							oldValue = existingMap[metricSubIndex];
						}
						String cql = $"UPDATE cards SET \"{metricName}\"[{nameValSplit[1]}] = :NEWVALUE WHERE cardid = :CARDID";
                        PreparedStatement statement = session.Prepare(cql);
						session.Execute(statement.Bind(new {CARDID = cardID, NEWVALUE = (oldValue + curMetric.Values[0])}));
						logger.Debug($"Updating {cardID}: {metricName}[{nameValSplit[1]}] {oldValue} -> {(oldValue + curMetric.Values[0])}");
					} else {
						
					}
				} else {
					
				}
			}
			//Console.WriteLine("{0} {1}", result["firstname"], result["age"]);
		}

		public static void MarkGamesConsumed(List<Game> games) {
			CreateSession();
			String cql = $"INSERT INTO games (id, filename, submitted, xmlmd5) VALUES (:ID, :FILENAME, :SUBMITTED, :XMLMD5)";
			foreach (Game curGame in games) {
				if (curGame.IsNewGame) {
					PreparedStatement statement = session.Prepare(cql);
					// TODO: Get Filename of Game.
					session.Execute(statement.Bind(new {ID = Guid.NewGuid(), FILENAME = "", SUBMITTED = new DateTimeOffset(DateTime.Now), XMLMD5 = curGame.Md5Hash}));
					logger.Debug($"Marking Game Consumed: {curGame.Md5Hash}");
				}
			}
		}

		public static Boolean IsNewGame(String md5String) {
			CreateSession();
			logger.Trace($"Checking if MD5 Exists in Games table...");
			PreparedStatement statement = session.Prepare("SELECT * FROM games WHERE xmlmd5=:MD5STR");
			Row result = session.Execute(statement.Bind(new { MD5STR = md5String })).FirstOrDefault();
			if (result == null) {
				// No existing game with this MD5 Hash so assume it is new.
				return true;
			}
			// We found a matching hash, so return false.
			return false;
		}

	}
}
