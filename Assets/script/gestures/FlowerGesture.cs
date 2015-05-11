using UnityEngine;
using System.Collections;

public class FlowerGesture : Gesture {

	private void FlowerCount () {
		this.count++;
		Sounds.audiosource.PlayOneShot (Sounds.Post_Flower_sparkle,20.0f);		
		if (this.count == 4) {
			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.clip = Sounds.Post_Paddle_wave;
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bird_landonflapping, 5.0f);
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bird_pecking);
		}
	}
	
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
			if (right.Pinch == 1) {
				this.state = State.action;
			}
		}

		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmup) {//transRoll > 50 && transRoll < 120 && Pinch > 0.8)
				this.FlowerCount();
				this.SetCooldown();
			}
//			if (right.transRoll > 50 && right.transRoll < 120 && right.Pinch < 0.5) {
//				Sounds.Environment.PlayOneShot (Sounds.paddlewrong);
//				this.SetCooldown();
//			}
		}
	}
}