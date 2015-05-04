using UnityEngine;
using System.Collections;

public class PaddleRowingGesture : TwoHandGesture<PaddleRowingLeftHandGesture, PaddleRowingRightHandGesture> {

	public void PaddleCount () {
		this.count ++;
		Sounds.Ambience_D.PlayOneShot (Sounds.paddle);
		// initially ReverbControl is set to 0
		if (this.count == 1) {
			Sounds.Ambience_A.minDistance = 5;
			Sounds.Ambience_D.PlayOneShot (Sounds.lakewaveslapping);
			Sounds.Ambience_A.Play ();
			Sounds.Ambience_B.minDistance = 8;
		}
		if (this.count == 2) {
			Sounds.Ambience_B.minDistance = 6;
		}
		if (this.count == 3) {
			Sounds.Ambience_B.minDistance = 4;
			Sounds.Ambience_C.minDistance = 2;
		}
		if (this.count == 4) {
			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_B.minDistance = 1;
			Sounds.Ambience_C.minDistance = 1;
		}
		Sounds.ReverbControl.BlendSnapShot (this.count);
		Debug.Log (this.count + " meter away from cave");
	}
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		if ((leftHandGesture == null) || (rightHandGesture == null)) {
			Debug.LogWarning("PaddleRowingGesture activated without activating sub-hand-gestures!");
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
					Sounds.Ambience_D.PlayOneShot (Sounds.paddlewrong);
					Sounds.quickenwatch();
					this.wrongcount++;
					this.SetCooldown();
				}
			} else if (handcount == 2) {
				Sounds.transitwatch();
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) { // two hands
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 2) {
				if (rightHandGesture.state == State.cooldown
				    && leftHandGesture.state == State.cooldown) {
					PaddleCount ();
					Sounds.normalwatch();
					this.SetCooldown();
				}
			}
			if (handcount == 1) { 
				this.state = State.detected;
			}
		}
	}

}