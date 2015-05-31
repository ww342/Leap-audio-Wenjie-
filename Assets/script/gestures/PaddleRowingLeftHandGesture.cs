using UnityEngine;
using System.Collections;

public class PaddleRowingLeftHandGesture : Gesture {
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.pitchforward) {
				this.state = State.detected;
			}
			if (left.wristleft && right.wristright) {
				if(GameLogic.GameVersion == 1){
				Narrator.PlayIfPossible(Narrator.Paddle_palmsfarapart_v1);
				}
				if(GameLogic.GameVersion == 2){
					Narrator.PlayIfPossible(Narrator.Paddle_palmsfarapart_v2);
				}
				PlayFromLefthand.PlayOneShot(Sounds.Dur_Stone_gentlesplash);
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.transPitch > 5) {
				this.SetCooldown(); // previously this was the hit notification
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_creak1,2.0f); 
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_row1,3.0f);
			}
		
		}
	}

}
