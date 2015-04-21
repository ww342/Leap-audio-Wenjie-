using UnityEngine;
using System.Collections;

public class BirdCatchGesture : Gesture {
	private BirdCatchLeftHandGesture leftHandGesture;
	private BirdCatchRightHandGesture rightHandGesture;

	public void BirdCount () {
		this.count++;
		Sounds.Ambience_D.PlayOneShot (Sounds.bird);
		Sounds.normalwatch ();
		Sounds.hint3 ();
		Sounds.Ambience_D.PlayOneShot (Sounds.grabseed);
		if (this.count == 2) {
			Sounds.Ambience_D.PlayOneShot (Sounds.rain);
			Sounds.Ambience_A.clip = Sounds.wave;
			Sounds.Ambience_A.loop = true;
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.Play ();
			this.wrongcount = 0;
		}
	}
	
	public void BirdWrong () {
		Sounds.Ambience_D.PlayOneShot (Sounds.birdonboat);
		Sounds.Ambience_D.PlayOneShot (Sounds.shortflapping);
		this.wrongcount++;
		Sounds.quickenwatch ();
		Sounds.hint3 ();
	}

	public void StartHands() {
		if (leftHandGesture == null) {
			leftHandGesture = gameObject.AddComponent<BirdCatchLeftHandGesture>();
			leftHandGesture.ActivateInParallel();
		}
		if (rightHandGesture == null) {
			rightHandGesture = gameObject.AddComponent<BirdCatchRightHandGesture>();
			rightHandGesture.ActivateInParallel();
		}
	}

	public void StopHands() {
		if (leftHandGesture != null) {
			leftHandGesture.DeactivateInParallel();
			Destroy (leftHandGesture);
			leftHandGesture = null;
		}
		if (rightHandGesture != null) {
			rightHandGesture.DeactivateInParallel();
			Destroy (rightHandGesture);
			rightHandGesture = null;
		}
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