using UnityEngine;
using System.Collections;

public class FlowerGesture : Gesture {

	private void FlowerCount () {
		this.count++;
		Sounds.audiosource.PlayOneShot (Sounds.flower);		
		if (this.count == 4) {
			this.wrongcount = 0;
			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.clip = Sounds.wave;
			Sounds.Ambience_D.PlayOneShot (Sounds.landonflpping, 5.0f);
			Sounds.Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);
		}
		// TODO: previously allowed: more than 4 flower gestures.
		// currently impossible, need to change GameLogic for that to be possible again!
		if (this.count == 5) {
			Sounds.audiosource.PlayOneShot (Sounds.leave);
		}
		if (this.count == 7) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
		}
		if (this.count == 10) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
			Sounds.Ambience_D.PlayOneShot (Sounds.wind, 5.0f);
		}
		if (this.count == 12) {
			//LevelCount ();
			this.wrongcount = 0;
		}
	}
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());

		while (this.state == State.none) {
			yield return null;
			if (right.pitchforward && right.palmdown) {
				this.state = State.detected;
			}
		}

		while (this.state == State.detected) {
			yield return null;
			if (right.Pinch == 1) {
				this.state = State.action;
			}
		}

		while (this.state == State.action) {
			yield return null;
			if (right.palmup) {//transRoll > 50 && transRoll < 120 && Pinch > 0.8)
				this.FlowerCount();
				this.SetCooldown();
			}
			if (right.transRoll > 50 && right.transRoll < 120 && right.Pinch < 0.5) {
				Sounds.Environment.PlayOneShot (Sounds.paddlewrong);
				this.SetCooldown();
			}
		}
	}
}