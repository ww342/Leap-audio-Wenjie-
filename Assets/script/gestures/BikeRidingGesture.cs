using UnityEngine;
using System.Collections;

public class BikeRidingGesture : TwoHandGesture<BikeRidingLeftHandGesture, BikeRidingRightHandGesture> {

	public void BellCount () {
		this.count++;
		if (this.count == 1) {
			Sounds.Ambience_B.minDistance = 10;
		}
		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_thunder);
		}
		if (this.count == 5) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bike_grassfootstep);
			Sounds.Ambience_B.clip = Sounds.Ambience_grassinthewind;
			Sounds.Ambience_B .Play ();
		}
		if (this.count == 6) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Dur_Bike_fall);
		}
	}

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		if ((leftHandGesture == null) || (rightHandGesture == null)) {
			Debug.LogWarning("BikeRidingGesture activated without activating sub-hand-gestures!");
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
			if (handcount > 1) {
				this.state = State.action;
			}
			if (rightHandGesture.state == State.detected || rightHandGesture.state == State.cooldown) {
				PlayFromRighthand.clip = Sounds.Dur_Bike_fall; // TODO: change to more appropriate voice/sound
				PlayFromRighthand.Play();
			}
			if (leftHandGesture.state == State.detected || leftHandGesture.state == State.cooldown) {
				PlayFromLefthand.clip = Sounds.Dur_Bike_brake; // TODO: change to more appropriate voice/sound
				PlayFromLefthand.Play();
			}
		}

		while (this.state == State.action) { // two hands
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 2) {
				if (rightHandGesture.state == State.detected || rightHandGesture.state == State.cooldown) {
					if (PlayFromRighthand.clip != Sounds.Post_Bike_twohandles || !PlayFromRighthand.isPlaying) {
						PlayFromRighthand.clip = Sounds.Post_Bike_twohandles;
						PlayFromRighthand.Play();
					}
				}
				if (leftHandGesture.state == State.detected || leftHandGesture.state == State.cooldown) {
					if (PlayFromLefthand.clip != Sounds.Post_Bike_twohandles || !PlayFromLefthand.isPlaying) {
						PlayFromLefthand.clip = Sounds.Post_Bike_twohandles;
						PlayFromLefthand.Play();
					}
				}
				if (rightHandGesture.state == State.cooldown) { // only the right is required?
					BellCount();
					this.SetCooldown();
				}
			}
			if (handcount == 1) { 
				yield return Narrator.PlayAndWait(Narrator.Bike_onehand_response);
				if (handcount == 1) {
					PlayFromRighthand.clip = Sounds.Dur_Bike_fall;
					PlayFromRighthand.Play();
					yield return Narrator.PlayAndWait(Narrator.Bike_fall_response);
					this.state = State.detected;
				}
			}
		}
	}

}
