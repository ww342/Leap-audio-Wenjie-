using UnityEngine;
using System.Collections;

public class BikeRidingGesture : TwoHandGesture<BikeRidingLeftHandGesture, BikeRidingRightHandGesture> {

	public void BellCount () {
		this.count++;
		if (this.count == 1) {
			Sounds.Ambience_B.minDistance = 10;
			Narrator.PlayIfPossible(Narrator.Bike_Correct_response_01_v2);

		}
		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_thunder);
		}
		if (this.count == 5) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bike_grassfootstep);
			Sounds.Ambience_B.clip = Sounds.Ambience_grassinthewind;
			Sounds.Ambience_B.Play ();
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
				Sounds.PlayImmediately(PlayFromRighthand, Sounds.Dur_Bike_wheelslowdown); 
				if(this.count<1){
				if(GameLogic.GameVersion == 1)
				{
					Narrator.PlayIfPossible(Narrator.Bike_onehandbell_v1);
				}
				if(GameLogic.GameVersion == 2)
				{
					Narrator.PlayIfPossible(Narrator.Bike_onehandbell_v2);
				}
				}
			}
			if (leftHandGesture.state == State.detected || leftHandGesture.state == State.cooldown) {
				Sounds.PlayImmediately(PlayFromLefthand, Sounds.Dur_Bike_brake); 
				if(this.count<1){
				if(GameLogic.GameVersion == 1)
				{
					Narrator.PlayIfPossible(Narrator.Bike_leftbrake_v1);
				}
				if(GameLogic.GameVersion == 2)
				{
					Narrator.PlayIfPossible(Narrator.Bike_leftbrake_v2);
				}
				}
			}
		}

		while (this.state == State.action) { // two hands
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 2) {
				if (rightHandGesture.state == State.detected || rightHandGesture.state == State.cooldown) {
					Sounds.PlayImmediately(PlayFromRighthand, Sounds.Post_Bike_twohandles);
				}
				if (leftHandGesture.state == State.detected || leftHandGesture.state == State.cooldown) {
					Sounds.PlayImmediately(PlayFromLefthand, Sounds.Post_Bike_twohandles);
				}
				if (rightHandGesture.state == State.cooldown) { // only the right is required?
					BellCount();
				}
//				if (this.count <1 && GameLogic.GameVersion == 2) {
//					Narrator.PlayIfPossible(Narrator.Bike_Correct_response_01_v2);
//				}
				this.SetCooldown();
				}
			}
			if (handcount == 1) {
				// actually wait for the second hand to come back during the response!
				yield return Narrator.PlayAndWait(Narrator.Bike_onehandle_v2);
				if (handcount == 1) {
					this.wrongcount++;
					Sounds.PlayImmediately(PlayFromRighthand, Sounds.Dur_Bike_fall);
					Narrator.PlayIfPossible(Narrator.Bike_falldown_v2);
					this.state = State.detected;
				}
			}
		}
	}



