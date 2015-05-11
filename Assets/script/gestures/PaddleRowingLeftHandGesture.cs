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
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.transPitch > 30) {
				this.SetCooldown(); // previously this was the hit notification
				Sounds.Gesturehint.PlayOneShot (Sounds.Dur_Paddle_creak1); // should be played on finger!
			}
		}
	}

}
