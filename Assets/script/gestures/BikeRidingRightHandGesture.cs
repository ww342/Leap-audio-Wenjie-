using UnityEngine;
using System.Collections;

public class BikeRidingRightHandGesture : Gesture {

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.pitchforward) {
				this.state = State.ready;
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmdown && right.Grab > 0.4 && right.thumb.IsExtended) {
				Sounds.Environment.PlayOneShot (Sounds.bike); // TODO: play on finger
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.thumb.IsExtended) {
				Sounds.Environment.PlayOneShot (Sounds.bell); // TODO: play on finger
				this.SetCooldown();
			}
		}
	}

}
