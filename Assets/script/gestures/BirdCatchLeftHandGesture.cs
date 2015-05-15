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

				if(GameLogic.GameVersion == 1){
					Narrator.PlayIfPossible(Narrator.BirdGesture);// placeholder for new recordings!
				}
				
				if(GameLogic.GameVersion == 2){
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_Panicflapping1);
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_onehandsqueeze);
					Sounds.Environment.PlayOneShot (Sounds.Pre_Bird_boatshiffer1, 5.0f);
					Narrator.PlayIfPossible(Narrator.Bird_onehandgrab_response);
				}
			}
		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (left.Grab < 0.8) {
				this.state = State.none;
				Sounds.transitwatch();
				Sounds.hint2();
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Bird_weakflpping);
				PlayFromLefthand.PlayOneShot (Sounds.Dur_Bird_flyalonghand);
			}
		}

		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForLeftHand());
			if (!left.ring.IsExtended && left.Grab < 0.8) {
				this.SetCooldown();
				// cooldown function like lhhit now
				// we could use a separate state like (ing) for that.
			}
		}
	}

}
