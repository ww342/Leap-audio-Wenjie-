using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Metrics logger.
/// Write a log file for every game played with a random ID for the player/subject
/// and a bit of data about the gameplay.
/// </summary>
public class MetricsLogger : MonoBehaviour {
	private string _randomID = null;
	public string RandomID { // to identify the subject
		get {
			if (_randomID == null) {
				System.TimeSpan ts = (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1));
				int timestamp = (int) ts.TotalSeconds;
				_randomID = Random.Range(0, 10000).ToString("0000") + timestamp.ToString().Substring(4);
				Debug.Log("Created random ID: " + _randomID);
			}
			return _randomID;
		}
	}
	const string METRICS_DIR = "Metrics/";
	private string destinationFilename {
		get { return METRICS_DIR + "Log." + RandomID + ".txt"; }
	}

	void Start () {
		System.IO.Directory.CreateDirectory(METRICS_DIR);
		LogData("Started game for subject " + RandomID);
	}
	
	public void LogData(string dataLine) {
		dataLine = System.DateTime.Now.ToString("u") + ", " + dataLine;
		File.AppendAllText(destinationFilename, dataLine + "\n");
		Debug.Log(dataLine);
	}
}
