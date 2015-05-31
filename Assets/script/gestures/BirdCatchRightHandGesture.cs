using UnityEngine;
using System.Collections;

public class BirdCatchRightHandGesture : Gesture {
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.openhand ) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
						yield return StartCoroutine (this.WaitForRightHand ());
						if (right.palmleftin) {
								this.state = State.action;
						}
						if (right.Grab == 1) {
								this.state = State.ready;
								Sounds.quickenwatch ();
								Sounds.hint1 ();

								if (GameLogic.GameVersion == 1) {
										Narrator.PlayIfPossible (Narrator.Bird_onehandgrab_v1);
								}
				
								if (GameLogic.GameVersion == 2) {
										PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_Panicflapping1);
										PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_onehandsqueeze);
										Sounds.Environment.PlayOneShot (Sounds.Pre_Bird_boatshiffer1, 5.0f);
										Narrator.PlayIfPossible (Narrator.Bird_onehandgrab_v2);
								}
						}
						if (right.transWave_y_3 > 30) {
								this.state = State.none;
								Sounds.quickenwatch ();
								Sounds.hint1 ();
								Sounds.Environment.PlayOneShot (Sounds.Post_Bird_seedpouring);
								Sounds.Environment.PlayOneShot (Sounds.Post_Bird_paniccry);
								Sounds.Environment.PlayOneShot (Sounds.Dur_Bird_Panicflapping2);
								Sounds.Environment.PlayOneShot (Sounds.Dur_Bird_Panicfrog);
								Sounds.Environment.PlayOneShot (Sounds.Dur_Paddle_Boat_shake1, 5.0f);
								Sounds.Environment.PlayOneShot (Sounds.Post_Stone_longlean);
								Sounds.Environment.PlayOneShot (Sounds.Pre_Bird_pecking);

								if (GameLogic.GameVersion == 1) {
										Narrator.PlayIfPossible (Narrator.Bird_toomuchforce_v1);
								}
								if (GameLogic.GameVersion == 2) {
										Narrator.PlayIfPossible (Narrator.Bird_toomuchforce_v2);
								}
						}
				}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.Grab < 0.8) {
				this.state = State.none;
				Sounds.transitwatch();
				Sounds.hint2();
				PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_weakflpping);
				PlayFromRighthand.PlayOneShot (Sounds.Dur_Bird_flyalonghand);
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.ring.IsExtended && right.Grab < 0.8) {
				this.SetCooldown();
				// cooldown function like rhhit now
				// we could use a separate state like (ing) for that.
			}
		}
	}

}
