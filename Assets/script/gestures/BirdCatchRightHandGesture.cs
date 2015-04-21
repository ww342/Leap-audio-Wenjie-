using UnityEngine;
using System.Collections;

public class BirdCatchRightHandGesture : Gesture {
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.openhand && right.palmdown) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmleftin) {
				this.state = State.action;
			}
			if (right.Grab == 1) {
				this.state = State.ready;
				Sounds.quickenwatch();
				Sounds.hint1();
				Sounds.Environment.PlayOneShot (Sounds.panicflapping);
				Sounds.Environment.PlayOneShot (Sounds.grabbird);
				Sounds.Environment.PlayOneShot (Sounds.boatshiffer2, 5.0f);
			}
			if (right.transWave_y_3 > 30) {
				this.state = State.none;
				Sounds.quickenwatch();
				Sounds.hint1();
				Sounds.Environment.PlayOneShot (Sounds.seedpouring);
				Sounds.Environment.PlayOneShot (Sounds.panicbird);
				Sounds.Environment.PlayOneShot (Sounds.panicflapping);
				Sounds.Environment.PlayOneShot (Sounds.panicfrog);
				Sounds.Environment.PlayOneShot (Sounds.boatshake, 5.0f);
				Sounds.Environment.PlayOneShot (Sounds.longlean);
				Sounds.Environment.PlayOneShot (Sounds.birdpecking);
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.Grab < 0.8) {
				this.state = State.none;
				Sounds.transitwatch();
				Sounds.hint2();
				Sounds.Environment.PlayOneShot (Sounds.weakflapping);
				Sounds.Environment.PlayOneShot (Sounds.birdflyslonghand);
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.ring.IsExtended && right.Grab < 0.8) {
				this.SetCooldown();
				// cooldown function like lhhit now
				// we could use a separate state like (ing) for that.
			}
		}
	}

}
