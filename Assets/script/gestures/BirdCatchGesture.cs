using UnityEngine;
using System.Collections;

public class BirdCatchGesture : TwoHandGesture<BirdCatchLeftHandGesture, BirdCatchRightHandGesture> {

	public void BirdCount () {
		this.count++;
		Sounds.Ambience_D.PlayOneShot (Sounds.Post_Bird_twohandcatch);
		Sounds.normalwatch ();
		Sounds.hint3 ();
		Sounds.Ambience_D.PlayOneShot (Sounds.Dur_Bird_grabseed);
		if (this.count == 2) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Paddle_thunder_rain);
			Sounds.Ambience_A.clip = Sounds.Post_Paddle_wave;
			Sounds.Ambience_A.loop = true;
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.Play ();
			this.wrongcount = 0;
		}
	}
	
	public void BirdWrong () {
		Sounds.Ambience_D.PlayOneShot (Sounds.Post_Bird_onehandcatch);
		Sounds.Ambience_D.PlayOneShot (Sounds.Dur_Bird_shortflapping);
		this.wrongcount++;
		Sounds.quickenwatch ();
		Sounds.hint3 ();
	}

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());

		if ((leftHandGesture == null) || (rightHandGesture == null)) {
			Debug.LogWarning("BirdCatchGesture activated without activating sub-hand-gestures!");
			yield break; // can't work, so break
		}

		while (this.state == State.none) { // no hand
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 1) {
				this.state = State.detected;
			} else if (handcount > 1) {
				this.state = State.action;
			}
		}
		
		while (this.state == State.detected) { // one hand
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 1) {
				if ((rightHandGesture.state == State.cooldown && leftHandGesture.state != State.cooldown)
				    || (rightHandGesture.state != State.cooldown && leftHandGesture.state == State.cooldown)) {
					BirdWrong ();
					this.SetCooldown();
				}
			} else if (handcount == 2) {
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) { // two hands
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 2) {
				if (rightHandGesture.state == State.cooldown
				    && leftHandGesture.state == State.cooldown) {
					BirdCount ();
					this.SetCooldown();
				}
			}
			if (handcount == 1) { 
				this.state = State.detected;
			}
		}
	}

}