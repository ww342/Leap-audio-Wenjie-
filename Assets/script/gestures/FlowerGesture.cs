using UnityEngine;
using System.Collections;

public class FlowerGesture : Gesture {

	private void FlowerCount () {
		this.count++;
		Sounds.audiosource.PlayOneShot (Sounds.Post_Flower_sparkle,3.0f);		
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
				PlayFromRighthand.PlayOneShot (Sounds.Dur_Star_pickup,5.0f);
				this.state = State.action;
				if(this.count<1 && GameLogic.GameVersion ==1){
					Narrator.PlayIfPossible(Narrator.Flower_pinch_v1);
				}
				if(this.count<1 && GameLogic.GameVersion ==2){
						Narrator.PlayIfPossible(Narrator.Flower_pinch_v2);
				}
			}
		}

		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmup) {//transRoll > 50 && transRoll < 120 && Pinch > 0.8)
				this.FlowerCount();
				this.SetCooldown();

				if(this.count<1 && GameLogic.GameVersion ==1){
					Narrator.PlayIfPossible(Narrator.Flower_Correct_response_01_v1);
				}

				}
				if(this.count<1 && GameLogic.GameVersion ==2){
					Narrator.PlayIfPossible(Narrator.Flower_Correct_response_01_v2);
				}
				if(this.count==1 && GameLogic.GameVersion ==2){
					Narrator.PlayIfPossible(Narrator.Flower_Correct_response_02_v2);
				}
				if(this.count==2 && GameLogic.GameVersion ==2){
					Narrator.PlayIfPossible(Narrator.Flower_Correct_response_03_v2);
				}
			}
//			if (right.transRoll > 50 && right.transRoll < 120 && right.Pinch < 0.5) {
//				Sounds.Environment.PlayOneShot (Sounds.paddlewrong);
//				this.SetCooldown();
//			}
		}
	}
