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
				PlayFromLefthand.PlayOneShot (Sounds.Post_Bike_twohandles); 
				this.SetCooldown();
			}
		}
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.Grab==1) {
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Bike_brake); 
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Bike_wheelslowdown);
				this.SetCooldown();
			}

			if (left.Grab<0.9) {
				PlayFromLefthand.PlayOneShot (Sounds.Post_Bike_twohandles);
				this.SetCooldown();
			}
		}
	}

}
