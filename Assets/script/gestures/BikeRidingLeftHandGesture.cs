using UnityEngine;
using System.Collections;

public class BikeRidingLeftHandGesture : Gesture {
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.pitchforward) {
				this.state = State.ready;
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.palmdown && left.Grab > 0.4) {
				this.state = State.detected;
			}
		}
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.Grab == 1 && !left.openhand && left.Pinch ==1) {
				Sounds.PlayImmediately(PlayFromLefthand, Sounds.Dur_Bike_brake);
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Bike_wheelslowdown);
				this.SetCooldown();
			} else if (left.Grab > 0.4) {
				this.SetCooldown();
			}
		}
	}

}
