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

	// Main flow of the game:
	// linear progression of gestures being activated and reacted to
	IEnumerator MainGame() {
		Metrics.levelcount = -1;
		Sounds.InitialSetup();
		Debug.Log ("Waiting for game-start signal (space)");
		while (! Input.GetKeyDown("space")) {
			yield return null;
		}
		Debug.Log ("Main game started!");
		BareHandsGesture barehands = gameObject.AddComponent<BareHandsGesture>();
		yield return StartCoroutine(barehands.Activate());
		Debug.Log ("First hand seen!");
		Sounds.StopInitialSetup();
		// barehands detected once, restart it in parallel now for hands-present related sound effects:
		barehands.state = Gesture.State.none;
		barehands.onlyonce = false; // keep running
		barehands.ActivateInParallel();

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
		bool doFlowerGesture = (stonethrow.wrongcount <= 2);
		Destroy (stonethrow);

		if (doFlowerGesture) {
			Debug.Log ("Flower petal picking (optional reward)");
			yield return Narrator.PlayAndWait(Narrator.flowerpose);
			Gesture flower = gameObject.AddComponent<FlowerGesture>();
			while (flower.count < 1) {
				yield return StartCoroutine(flower.Activate());
			}
			yield return Narrator.PlayAndWait(Narrator.flower1);
			while (flower.count < 4) {
				yield return StartCoroutine(flower.Activate());
			}
			yield return Narrator.PlayAndWait(Narrator.flyfromtree);

			// TODO: the above narration (flyfromtree) and the narration in the "else" below
			// (stone3) that's played instead of the flower gesture overlap!
			// ideally we would split the instructions at the end from the two response parts
			// (actual sound clip names: flying from tree, fishresponse3)

			// TODO: previously, flowers worked up to 12 and then triggered tieing directly.
			// now only four flowers are possible
			//yield return Narrator.PlayAndWait(Narrator.tietheboat);

			Destroy (flower);
		} else {
			Debug.Log ("NO Flower petal picking (optional reward)");
			yield return Narrator.PlayAndWait(Narrator.stone3);
		}

		// example code for stopping here:
		//yield break;

		Debug.Log ("Bird catching");
		BirdCatchGesture birdcatch = gameObject.AddComponent<BirdCatchGesture>();
		birdcatch.StartHands(); // separate hands in parallel!
		while (birdcatch.count < 1) {
			yield return StartCoroutine(birdcatch.Activate());
			if (birdcatch.wrongcount > 0) {
				yield return Narrator.PlayAndWait(Narrator.birdwrong);
				birdcatch.wrongcount = 0;
			}
		}
		yield return Narrator.PlayAndWait(Narrator.bird1);
		while (birdcatch.count < 2) {
			yield return StartCoroutine(birdcatch.Activate());
		}
		birdcatch.StopHands();
		yield return Narrator.PlayAndWait(Narrator.bird2);
		Destroy (birdcatch);

		Debug.Log ("Paddle");
		
		Debug.Log ("Rope & Tree");
		
		Debug.Log ("Bike");
		Sounds.Ambience_D.PlayOneShot (Sounds.wind);
		
		Debug.Log ("Star");
		Sounds.Ambience_D.PlayOneShot (Sounds.sky);
		Sounds.Ambience_B.minDistance = 10;

		// remove parallel no-hands/hands sound effects:
		barehands.DeactivateInParallel();
		Destroy (barehands);
	}
	
}
