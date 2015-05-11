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
				Sounds.Gesturehint.PlayOneShot (Sounds.Post_Bike_twohandles); // TODO: should be played on finger
				this.SetCooldown();
			}
		}
	}

}
