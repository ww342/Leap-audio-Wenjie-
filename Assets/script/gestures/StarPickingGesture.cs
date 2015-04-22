using UnityEngine;
using System.Collections;

public class StarPickingGesture : Gesture {

	public void StarCount () {
		this.count ++;
		if (this.count == 1) {
			Sounds.Ambience_D.PlayOneShot (Sounds.lightning);
			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}
		if (this.count == 2) {
			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}
	}

	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.pitchupforward) {
				this.state = State.ready;
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.openhand && (right.transPitch > 10)) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmin) {
				Sounds.Environment.PlayOneShot (Sounds.star);
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmup) {
				StarCount();
				Sounds.Environment.PlayOneShot (Sounds.win);
				Sounds.Environment.PlayOneShot (Sounds.shiny);
				this.SetCooldown();
			}
		}
	}

}
