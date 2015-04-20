using UnityEngine;
using System.Collections;

/// <summary>
/// Game logic.
/// Top-level game flow, from start of the game, over narration and
/// different gestures, to the conclusion.
/// 
/// Initial start of the game is triggered with space.
/// </summary>
public class GameLogic : MonoBehaviour {
	public Hands Hands;
	public Sounds Sounds;
	public Narrator Narrator;
	public Metrics Metrics;

	void Start () {
		StartCoroutine(MainGame());
	}
	
	IEnumerator MainGame() {
		Metrics.levelcount = -1;
		Sounds.InitialSetup();
		Debug.Log ("Waiting for game-start signal (space)");
		while (! Input.GetKeyDown("space")) {
			yield return null;
		}
		Debug.Log ("Main game started!");
		Gesture barehands = gameObject.AddComponent<BareHandsGesture>();
		yield return StartCoroutine(barehands.Activate());
		Destroy (barehands);

		Sounds.StopInitialSetup();
		Hands.LevelCount ();
		Debug.Log ("Story Begins!");
		yield return Narrator.PlayAndWait(Narrator.begin);

		Debug.Log ("Stone throwing");
		Gesture stonethrow = gameObject.AddComponent<StoneThrowGesture>();
		while (stonethrow.count < 1) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.stone1);
		while (stonethrow.count < 2) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.stone2);
		while (stonethrow.count < 3) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		if (stonethrow.wrongcount <= 2) {
			yield return Narrator.PlayAndWait(Narrator.flowerpose);
		} else {
			yield return Narrator.PlayAndWait(Narrator.stone3);
		}
		Destroy (stonethrow);

		// example code for stop here:
		//yield break;

		Debug.Log ("TODO: next gesture");
	}
	
}
