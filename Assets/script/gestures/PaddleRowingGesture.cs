using UnityEngine;
using System.Collections;

public class PaddleRowingGesture : TwoHandGesture<PaddleRowingLeftHandGesture, PaddleRowingRightHandGesture> {

	public void PaddleCount () {
		this.count ++;
		PlayFromRighthand.PlayOneShot (Sounds.Post_Paddle_rowing,3.0f);
		PlayFromLefthand.PlayOneShot (Sounds.Post_Paddle_rowing,3.0f);
		//PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_row1);
		//PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_row2);
		// initially ReverbControl is set to 0
		if (this.count == 1) {
			Sounds.Ambience_A.minDistance = 5;
			Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_wavelapping);
			Sounds.Ambience_A.Play ();
			Sounds.Ambience_B.minDistance = 8;
		}
		if (this.count == 2) {
			Sounds.Ambience_B.minDistance = 6;
		}
		if (this.count == 3) {
			Sounds.Ambience_B.minDistance = 4;
			Sounds.Ambience_C.minDistance = 2;
		}
		if (this.count == 4) {
			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_B.minDistance = 1;
			Sounds.Ambience_C.minDistance = 1;
		}
		Sounds.ReverbControl.BlendSnapShot (this.count);
		Debug.Log (this.count + " meter away from cave");
	}
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		if ((leftHandGesture == null) || (rightHandGesture == null)) {
			Debug.LogWarning("PaddleRowingGesture activated without activating sub-hand-gestures!");
			yield break; // can't work, so break
		}
		
		while (this.state == State.none) { // no hand
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 1) {
				this.state = State.detected;
			} else if (handcount > 1) {
				this.state = State.action;
			}
		}
		
		while (this.state == State.detected) { // one hand
			yield return StartCoroutine(this.WaitForAnyHand());
			if (handcount == 1) {
				if (rightHandGesture.state == State.cooldown && leftHandGesture.state != State.cooldown){
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_creak2,3.0f);
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_droppaddle_response01,6.0f);

				}
				   if (rightHandGesture.state != State.cooldown && leftHandGesture.state == State.cooldown) {
					 PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_creak2,3.0f);
					PlayFromLefthand.PlayOneShot (Sounds.Dur_Paddle_droppaddle_response02, 6.0f);

				}

					Sounds.quickenwatch();
					this.wrongcount++;
					this.SetCooldown();

			} else if (handcount == 2) {
				Sounds.transitwatch();
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) { // two hands
			yield return StartCoroutine(this.WaitForAnyHand());

			if (handcount == 2) {
				if(Mathf.Abs(rightpalm.handmove_x-leftpalm.handmove_x) >=400) {
					if(GameLogic.GameVersion==2){
						Narrator.PlayIfPossible(Narrator.Paddle_palmsfarapart_v1);
					}
					if(GameLogic.GameVersion==1){
						Narrator.PlayIfPossible(Narrator.Paddle_palmsfarapart_v1);
					}
				}
				if (rightHandGesture.state == State.cooldown
				    && leftHandGesture.state == State.cooldown) {
					PaddleCount ();

					if (this.count == 3 && GameLogic.GameVersion == 2) {
						Narrator.PlayIfPossible(Narrator.Paddle_Correct_response_02_v2);
						
						// Need Help for detecting time period !! If player stop moving for 5 secs 
						//after hearing 'Narrator.Paddle_Correct_response_002",then play 'Narrator.Paddle_longstop_response'
						//Narrator.PlayIfPossible(Narrator.Paddle_longstop_response,3);
						
					}
					Sounds.normalwatch();
					this.SetCooldown();
				}
			}
			if (handcount == 1) { 
				this.state = State.detected;
			}
		}
	}

}