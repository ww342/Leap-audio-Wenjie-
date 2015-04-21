using UnityEngine;
using System.Collections;

public class PaddleRowingRightHandGesture : Gesture {

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.pitchforward && right.palmdown) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.transPitch > 30) {
				this.SetCooldown(); // previously this was the hit notification
				Sounds.Environment.PlayOneShot (Sounds.creak1); // TODO: should be played on finger
			}
		}		
	}

}
