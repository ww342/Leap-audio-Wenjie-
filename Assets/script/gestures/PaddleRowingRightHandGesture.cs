using UnityEngine;
using System.Collections;

public class PaddleRowingRightHandGesture : Gesture {

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.pitchforward) {
				this.state = State.detected;
			}
			if (right.wristright && left.wristleft) {
				PlayFromRighthand.PlayOneShot(Sounds.Dur_Stone_gentlesplash);
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.transPitch > 5) {
				this.SetCooldown(); // previously this was the hit notification
				PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_creak1,2.0f);
				PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_row2,3.0f);
			}
		}		
	}

}
