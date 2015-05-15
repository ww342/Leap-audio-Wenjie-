using UnityEngine;
using System.Collections;

public class PaddleRowingLeftHandGesture : Gesture {
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.pitchforward && left.palmdown) {
				this.state = State.detected;
			}
			if (left.wristleft && right.wristright) {
				Narrator.PlayIfPossible(Narrator.Paddle_siderowing_response);
				PlayFromLefthand.PlayOneShot(Sounds.Dur_Stone_gentlesplash);
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.transPitch > 30) {
				this.SetCooldown(); // previously this was the hit notification
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_creak1,3.0f); 
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_row1,5.0f);
			}
		}
	}

}
