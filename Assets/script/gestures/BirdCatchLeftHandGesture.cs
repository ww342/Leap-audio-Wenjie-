using UnityEngine;
using System.Collections;

public class BirdCatchLeftHandGesture : Gesture {

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.openhand && left.palmdown) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.palmrightin) {
				this.state = State.action;
			}
			if (left.Grab == 1) {
				this.state = State.ready;
				Sounds.audiosource.PlayOneShot (Sounds.Dur_Bird_Panicflapping1);
				Sounds.audiosource.PlayOneShot (Sounds.Dur_Bird_onehandsqueeze);
				Sounds.audiosource.PlayOneShot (Sounds.Pre_Bird_boatshiffer2, 5.0f);
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.Grab < 0.8) {
				this.state = State.none;
				Sounds.audiosource.PlayOneShot (Sounds.Dur_Bird_weakflpping);
				Sounds.audiosource.PlayOneShot (Sounds.Dur_Bird_flyalonghand);
			}
		}

		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (!left.ring.IsExtended) {
				this.SetCooldown();
				// cooldown function like lhhit now
				// we could use a separate state like (ing) for that.
			}
		}
	}

}
