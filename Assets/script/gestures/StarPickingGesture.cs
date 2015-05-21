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

		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot(Sounds.Ambience_meteor,0.6f);
			
		}
	}
	   
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmdown) {
				this.state = State.ready;

		}
		
		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.Grab>0.8 || right.Pinch==1 ) {
				this.state = State.detected;
					Sounds.Environment.PlayOneShot (Sounds.Dur_Star_pickup,3.0f);

					if (GameLogic.GameVersion == 2) {
						if(this.count<1){
							Narrator.PlayIfPossible(Narrator.Star_pickup_v2);
							
						}
					}
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
				if (right.palmin) {
				Sounds.Environment.PlayOneShot (Sounds.Dur_Star_starrain);
				}
				this.state = State.action;
				
				if (GameLogic.GameVersion == 2) {
					if(this.count<1){
						Narrator.PlayIfPossible(Narrator.Star_flipover_v2);
					}
					
				}
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmup) {
				StarCount();
				if (this.count<1 && GameLogic.GameVersion == 2) {
					Narrator.PlayIfPossible(Narrator.Star_Correct_response_01_v1);
				}
				Sounds.Environment.PlayOneShot (Sounds.Post_Star_shiny);
				Sounds.Environment.PlayOneShot (Sounds.Dur_Star_starrain);
				this.SetCooldown();
			}
		}
	}

}
