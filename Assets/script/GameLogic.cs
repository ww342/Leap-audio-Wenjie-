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
	public Sounds Sounds;
	public Narrator Narrator;
	public int GameVersion = 0;
	protected MetricsLogger DataLogger;

	void Start () {
		DataLogger = gameObject.GetComponent<MetricsLogger>();
		StartCoroutine(MainGame());
	}

	// Main flow of the game:
	// linear progression of gestures being activated and reacted to
	// also allows to simply comment out parts you want to skip for testing
	IEnumerator MainGame() {
		GameVersion = Random.Range(1, 3);
		Sounds.InitialSetup();
		Debug.Log ("Waiting for random game-start signal (space). Use 1 or 2 to select a specific game version.");
		while (! Input.GetKeyDown(KeyCode.Space)) {
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				GameVersion = 1;
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				GameVersion = 2;
			}
			yield return null;
		}
		DataLogger.LogData("Starting game version " + GameVersion);

		Debug.Log ("Main game started!");
		BareHandsGesture barehands = gameObject.AddComponent<BareHandsGesture>();
		yield return StartCoroutine(CheckBareHands(barehands));

		Debug.Log ("Story Begins!");
		yield return Narrator.PlayAndWait(Narrator.StoneIntro);

		Debug.Log ("Stone throwing");
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.StoneGesture);
		}
		Gesture stonethrow = gameObject.AddComponent<StoneThrowGesture>();
		yield return StartCoroutine(DoStoneThrowing(stonethrow));
		bool doFlowerGesture = (stonethrow.wrongcount <= 2);
		Destroy (stonethrow);

		if (doFlowerGesture) {
			Debug.Log ("Flower petal picking (optional reward)");
			yield return StartCoroutine(DoFlowerPicking());

		} else {
			Debug.Log ("NO Flower petal picking (optional reward)");
			yield return Narrator.PlayAndWait(Narrator.BirdIntro_afterStone);
		}

		// example code for stopping here:
		//yield break;

		Debug.Log ("Bird catching");
		yield return StartCoroutine(DoBirdCatching());

		Debug.Log ("Paddle rowing");
		if (GameVersion == 1) {
						yield return Narrator.PlayAndWait (Narrator.PaddleGesture);
				}
		PaddleRowingGesture paddlerowing = gameObject.AddComponent<PaddleRowingGesture>();
		yield return StartCoroutine(DoPaddleRowing(paddlerowing));
		bool doTreeGesture = (paddlerowing.wrongcount <= 2);
		Destroy (paddlerowing);

		if (doTreeGesture) {
			Debug.Log ("Tree shaking (optional reward)");
			yield return StartCoroutine(DoTreeShaking());
		} else {
			Debug.Log ("NO Tree shaking (optional reward)");
			yield return new WaitForSeconds(5f);
			yield return Narrator.PlayAndWait(Narrator.RopeIntro_afterPaddle);

		}

		Debug.Log ("Rope binding");
		yield return StartCoroutine(DoRopeBinding());

		Sounds.ChangeBackgroundMusic ();

		Debug.Log ("Bike riding");
		Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_grassinthewind);
		yield return StartCoroutine(DoBikeRiding());
		
		Debug.Log ("Star");
		Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_space);
		Sounds.Ambience_B.minDistance = 10;
		yield return StartCoroutine(DoStarPicking());

		// remove parallel no-hands/hands sound effects:
		barehands.DeactivateInParallel();
		Destroy (barehands);

		// the timing of those might be off now:
		Sounds.Glow();
		Sounds.Timetravel();
		yield return new WaitForSeconds(2f);
		Sounds.watchstop();
		DataLogger.LogData("Main game finished!");
	}

	IEnumerator CheckBareHands(BareHandsGesture barehands) {
		yield return StartCoroutine(barehands.Activate());
		Debug.Log ("First hand seen!");
		Sounds.StopInitialSetup();
		// barehands detected once, restart it in parallel now for hands-present related sound effects:
		barehands.state = Gesture.State.none;
		barehands.onlyonce = false; // keep running
		barehands.ActivateInParallel();
	}

	IEnumerator DoStoneThrowing(Gesture stonethrow) {
		System.DateTime startTime = System.DateTime.Now;
		while (stonethrow.count < 1) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		Sounds.Ambience_A.minDistance = 6;
		yield return Narrator.PlayAndWait(Narrator.Stone_Correct_response_01);
		while (stonethrow.count < 2) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		Sounds.Ambience_A.minDistance = 2;
		yield return Narrator.PlayAndWait(Narrator.Stone_Correct_response_02);
		while (stonethrow.count < 3) {
			yield return StartCoroutine(stonethrow.Activate());
		}
		Sounds.Ambience_A.minDistance = 0;
		LogGestureEnd("Stone Throwing", stonethrow, startTime);
		yield return Narrator.PlayAndWait(Narrator.Stone_Correct_response_03);
	}

	IEnumerator DoFlowerPicking() {
		yield return Narrator.PlayAndWait(Narrator.FlowerIntro);
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.FlowerGesture);
		}
		System.DateTime startTime = System.DateTime.Now;
		Gesture flower = gameObject.AddComponent<FlowerGesture>();
		while (flower.count < 1) {
			yield return StartCoroutine(flower.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Flower_Correct_response_01);
		while (flower.count < 4) {
			yield return StartCoroutine(flower.Activate());
		}
		LogGestureEnd("Flower", flower, startTime);
		Destroy (flower);
		yield return Narrator.PlayAndWait(Narrator.BirdIntro_afterFlower);
	}

	IEnumerator DoBirdCatching() {
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.BirdGesture);
		}
		System.DateTime startTime = System.DateTime.Now;
		BirdCatchGesture birdcatch = gameObject.AddComponent<BirdCatchGesture>();
		birdcatch.StartHands(); // separate hands in parallel!	
		while (birdcatch.count < 1) {
			yield return StartCoroutine(birdcatch.Activate());
			if (birdcatch.wrongcount > 0) {
				//yield return Narrator.PlayAndWait(Narrator.birdwrong);
				birdcatch.wrongcount = 0;
			}
		}
		yield return Narrator.PlayAndWait(Narrator.Bird_Correct_response_01);
		while (birdcatch.count < 2) {
			yield return StartCoroutine(birdcatch.Activate());
		}
		birdcatch.StopHands();
		LogGestureEnd("Bird Catching", birdcatch, startTime);
		Destroy (birdcatch);
		yield return Narrator.PlayAndWait(Narrator.Bird_Correct_response_02);
	}

	IEnumerator DoPaddleRowing(PaddleRowingGesture paddlerowing) {
		System.DateTime startTime = System.DateTime.Now;
		paddlerowing.StartHands(); // separate hands in parallel!
		while (paddlerowing.count < 1) {
			yield return StartCoroutine(paddlerowing.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Paddle_Correct_response_01);
		while (paddlerowing.count < 4) {
			yield return StartCoroutine(paddlerowing.Activate());
		}
		paddlerowing.StopHands();
		LogGestureEnd("Paddle Rowing", paddlerowing, startTime);
		yield return Narrator.PlayAndWait(Narrator.Paddle_Correct_response_03);
	}

	IEnumerator DoTreeShaking() {
		yield return Narrator.PlayAndWait(Narrator.TreeIntro);
		System.DateTime startTime = System.DateTime.Now;
		Gesture treeshake = gameObject.AddComponent<TreeShakingGesture>();
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.TreeGesture);
		}
		while (treeshake.count < 8) {
			yield return StartCoroutine(treeshake.Activate());
		}
		LogGestureEnd("Tree Shaking", treeshake, startTime);
		Destroy (treeshake);
		yield return Narrator.PlayAndWait(Narrator.RopeIntro_afterTree);
	}

	IEnumerator DoRopeBinding() {
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.RopeGesture);
		}
		System.DateTime startTime = System.DateTime.Now;
		Gesture ropebind = gameObject.AddComponent<RopeBindingGesture>();
		while (ropebind.count < 1) {
			yield return StartCoroutine(ropebind.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Rope_Correct_response_01);
		while (ropebind.count < 2) {

			yield return StartCoroutine(ropebind.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Rope_Correct_response_02);
		while (ropebind.count < 3) {

			yield return StartCoroutine(ropebind.Activate());
		}
		LogGestureEnd("Rope Binding", ropebind, startTime);
		Destroy (ropebind);
		yield return Narrator.PlayAndWait(Narrator.Rope_Correct_response_03);
	}

	IEnumerator DoBikeRiding() {
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.BikeGesture);
		}
		System.DateTime startTime = System.DateTime.Now;
		BikeRidingGesture bikeride = gameObject.AddComponent<BikeRidingGesture>();
		Sounds.RestartBackgroundMusic ();
		bikeride.StartHands(); // separate hands in parallel!
		while (bikeride.count < 6) {
			yield return StartCoroutine(bikeride.Activate());
		}
		bikeride.StopHands();
		LogGestureEnd("Bike Riding", bikeride, startTime);
		Destroy (bikeride);
		yield return Narrator.PlayAndWait(Narrator.StarIntro);
	}

	IEnumerator DoStarPicking() {
		if (GameVersion == 1) {
			yield return Narrator.PlayAndWait (Narrator.StarGesture);
		}
		System.DateTime startTime = System.DateTime.Now;
		Gesture starpick = gameObject.AddComponent<StarPickingGesture>();
		while (starpick.count < 1) {
			yield return StartCoroutine(starpick.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Star_Correct_response_01);
		while (starpick.count < 2) {
			yield return StartCoroutine(starpick.Activate());
		}
		yield return Narrator.PlayAndWait(Narrator.Star_Correct_response_02);
		while (starpick.count < 3) {
			yield return StartCoroutine(starpick.Activate());
		}
		LogGestureEnd("Star Picking", starpick, startTime);
		Destroy (starpick);
		yield return Narrator.PlayAndWait(Narrator.Star_Correct_response_03);
	}

	private void LogGestureEnd(string gestureName, Gesture gest, System.DateTime startTime)
	{
		DataLogger.LogData(System.String.Format(
			"Gesture complete (name, duration, count, wrongcount), {0}, {1}, {2}, {3}",
			gestureName, (System.DateTime.Now - startTime).TotalSeconds, gest.count, gest.wrongcount));
	}
}
