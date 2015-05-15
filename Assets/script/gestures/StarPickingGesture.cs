using UnityEngine;
using System.Collections;

public class StarPickingGesture : Gesture {

	public void StarCount () {
		this.count ++;
		if (this.count == 1) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Ambience_meteorshower);
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bike_grassfootstep);

		}
		if (this.count == 2) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bike_grassfootstep);
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

					if (GameLogic.GameVersion == 2) {
					if(this.count<1){
						Narrator.PlayIfPossible(Narrator.Star_grab_response_01);
					}

				}
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmin) {
				Sounds.Environment.PlayOneShot (Sounds.Dur_Star_starrain);
				if (GameLogic.GameVersion == 2) {
					if(this.count<1){
						Narrator.PlayIfPossible(Narrator.Star_flipover_response_01);
					}
					if(this.count==2){
						Narrator.PlayIfPossible(Narrator.Star_flipover_response_02);
					}
				}
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmup) {
				StarCount();
				if (this.count<1 && GameLogic.GameVersion == 2) {
					Narrator.PlayIfPossible(Narrator.Star_openhand_response_01);
				}
				Sounds.Environment.PlayOneShot (Sounds.Post_Star_shiny);
				this.SetCooldown();
			}
		}
	}

}
