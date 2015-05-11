using UnityEngine;
using System.Collections;

public class RopeBindingGesture : Gesture {

	public void RopeCount () {
		this.count++;
		if (this.count == 1) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Dur_Rope_creaks);
			Sounds.Ambience_B.minDistance = 0;
			Sounds.Ambience_B.clip = Sounds.Ambience_crickets;
		}
		if (this.count == 2) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Dur_Rope_creaks);
			Sounds.Ambience_B.minDistance = 2;
		}
		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bike_grassfootstep);
			Sounds.Ambience_B.minDistance = 5;
			Sounds.Ambience_B.Play ();
		}
	}

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmright) {
				this.state = State.ready;
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.openhand && (right.transWave_z_10 > 50)) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.openhand && (right.transWave_x_10 > 5)) {
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.openhand && (right.transWave_z_10 < -50)) {
				this.state = State.ing;
			}
		}
		
		while (this.state == State.ing) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.openhand && (right.transWave_x_10 < -5)) {
				this.RopeCount();
				this.SetCooldown();
			}
		}
	}

}
