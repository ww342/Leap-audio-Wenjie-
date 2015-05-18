﻿using UnityEngine;
using System.Collections;

public class StoneThrowGesture : Gesture {


	private void StoneCount () {
		this.count ++;
		Sounds.Northdown.sound1();
		Sounds.Northdown.sound2();
		Sounds.Northdown.sound6();		
		if (this.count == 1) {
			Sounds.Ambience_A.minDistance = 7;
		}
		if (this.count == 2) {
			Sounds.Ambience_A.minDistance = 4;
		}
		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.Post_Stone_Fishjump_righttoleft, 0.5f);
			Sounds.Ambience_A.minDistance = 1;
			if (this.wrongcount <= 2) {
				Sounds.Ambience_A.clip = Sounds.Pre_Flower_duck;
				Sounds.Ambience_A.minDistance = 8;
				Sounds.Ambience_B.loop = true;
				Sounds.Ambience_A.Play ();
			}
			if (this.wrongcount > 2) {
				Sounds.Ambience_D.PlayOneShot (Sounds.Pre_Bird_landonflapping);
				Sounds.Ambience_A.clip = Sounds.Dur_Bird_shortflapping;
				Sounds.Ambience_A.minDistance = 20;
				Sounds.Ambience_A.loop = false;
				Sounds.Ambience_A.Play ();
			}
		}
	}

	private void GrabStone(bool grabbed) {
		Stone stone = GameObject.Find ("/MovingElements/Stone").GetComponent<Stone>();
		stone.GrabStone = grabbed;
	}
	
	override public IEnumerator Activate () {
		GrabStone(false);
		Sinus.frequency = 0;
		Sinus.gain = 0;

		yield return StartCoroutine(this.CheckAndWaitForCooldown());

		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.pitchforward && right.palmdown) {
				this.state = State.detected;
			}
		}

		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.Grab > 0.9) {
				if (right.wristforward) { // success
					this.state = State.action;
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_grabstone);
					GrabStone(true);
					Sounds.hint3 ();
				}
				if (right.wristleft || right.wristright) { // grabbed the water
					this.state = State.none;

					if(GameLogic.GameVersion ==1){
						Narrator.PlayIfPossible(Narrator.Stone_grabsides_v1);
					}

					if(GameLogic.GameVersion ==2){
						PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_gentlesplash, 15.0f);
						PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_gentlewaterdrop, 15.0f);
						Sounds.Environment.PlayOneShot (Sounds.Post_Stone_longcreak,0.8f);
						Narrator.PlayIfPossible(Narrator.Stone_grabsides_v2);
					}

					Sounds.transitwatch ();
					yield break; // restart!

				}
			} 
		}

		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.wristforward && right.openhand) {
				Sounds.transitwatch ();
				this.state = State.none;
				Sounds.hint2 ();
				PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_dropontheboat); 
				GrabStone(false);
			}
			if ((right.wristleft || right.wristright) && right.openhand) {
				Sounds.transitwatch ();
				this.state = State.none;
				GrabStone(false);
				Sounds.hint2 ();
				PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_waterdrop, 10.0f);

				if(GameLogic.GameVersion == 1){
					Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v1);
				}
				
				if(GameLogic.GameVersion == 2){
					Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v2);
				}

				yield break; // restart!
			}
			if (Mathf.Abs (right.transWave_z_10) > 50) {
				Sounds.quickenwatch ();
				this.state = State.ready;
			}
			if (right.palmleft || right.palmleftin || right.palmup || right.palmright){
				this.state = State.other;
				Sounds.transitwatch ();
				Sounds.hint2 ();
			}
			if (right.palmdown) {
				if (right.pitch > 10 && right.pitch <= 20) {
					Sinus.gain = 0.01;
					Sinus.frequency = 450;
				}
				if (right.pitch > 20 && right.pitch <= 30) {
					Sinus.gain = 0.02;
					Sinus.frequency = 500;
				}
				if (right.pitch > 30 && right.pitch <= 40) {
					Sinus.gain = 0.03;
					Sinus.frequency = 550;
				}
				if (right.pitch > 40 && right.pitch <= 50) {
					Sinus.gain = 0.04;
					Sinus.frequency = 600;
				}
				if (right.pitch > 50 && right.pitch <= 60) {
					Sinus.gain = 0.05;
					Sinus.frequency = 650;
				}
				if (right.pitch > 60 && right.pitch <= 70) {
					Sinus.gain = 0.06;
					Sinus.frequency = 700;
				}
				if (right.pitch > 70 && right.pitch <= 80) {
					Sinus.gain = 0.07;
					Sinus.frequency = 750;
				}
				if (right.pitch > 80 && right.pitch <= 90) {
					Sinus.gain = 0.08;
					Sinus.frequency = 800;
				}
				if (right.pitch > 90 && right.pitch <= 100) { 
					Sinus.gain = 0.09;
					Sinus.frequency = 850;
				}
				if (right.transWave_y_10 < - 60) {
					Sounds.normalwatch ();
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_rightsleevelift, 6.0f);
					if(GameLogic.GameVersion == 1 && this.count <1){
						Narrator.PlayIfPossible(Narrator.Stone_armlift_v1);
					}
					if(GameLogic.GameVersion == 2 && this.count <1){
						Narrator.PlayIfPossible(Narrator.Stone_armlift_v2);
					}
					Sounds.hint3 ();
					this.state = State.ing;
				}
			}
		}

		while (this.state == State.ready) {
			yield return StartCoroutine(this.WaitForRightHand());
			Sinus.frequency = 0;
			Sinus.gain = 0;
			if (right.Grab == 0) {
				PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_waterdrop, 8.0f);
				Sounds.quickenwatch ();
				this.wrongcount++;
				Sounds.hint2 ();
				this.state = State.none;

				if(GameLogic.GameVersion == 1){
					Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v1);
				}

				if(GameLogic.GameVersion == 2){
					Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v2);
				}

				yield break; // restart!
			}
		}
				
		while (this.state == State.other) {
			yield return StartCoroutine(this.WaitForRightHand());
			Sinus.frequency = 0;
			Sinus.gain = 0;
			if (right.palmdown) {
				if (Mathf.Abs (right.transWave_y_10 / right.transWave_x_10) < 0.6 && Mathf.Abs (right.transWave_y_10 / right.transWave_x_10) > 0.1) {
					if (right.openhand) {
						Sounds.quickenwatch ();
						PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_waterdrop, 8.0f);
						Sounds.hint1 ();
						this.wrongcount++;
						this.state = State.none;
						yield break; // restart!
					}
				}
				if (right.transWave_y_10 < - 50) {
					Sounds.hint3 ();
					PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_rightsleevelift, 3.0f);
					Sounds.normalwatch ();
					if(GameLogic.GameVersion == 1 && this.count <1){
						Narrator.PlayIfPossible(Narrator.Stone_armlift_v1);
					}
					if(GameLogic.GameVersion == 2 && this.count <1){
						Narrator.PlayIfPossible(Narrator.Stone_armlift_v2);
					}

					this.state = State.ing;

				}
			}
			if (right.palmleft || right.palmleftin || right.palmup || right.palmright) {
				Sounds.hint2 ();
//				if (Mathf.Abs (right.transWave_y_6 / right.transWave_x_6) < 0.6 && Mathf.Abs (right.transWave_y_6 / right.transWave_x_6) > 0.1) {
//					if (right.transPitch > 20 && right.Grab < 0.5) {
				if(right.transWave_x_3>20){
						Sounds.Northdown.MidStoneSkip();
						Sounds.quickenwatch ();
						this.wrongcount++;
					if(GameLogic.GameVersion == 1){
						Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v1);
					}
					if(GameLogic.GameVersion == 2){
						Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v2);
					}
						this.state = State.none;

						yield break; // restart!
					    
				}
//					}
//				}
//				if (Mathf.Abs (right.transWave_y_3 / right.transWave_x_3) < 0.6 && Mathf.Abs (right.transWave_y_3 / right.transWave_x_3) > 0.1) {
//					if (right.transPitch > 20 && right.Grab < 0.5) {
				if(right.transWave_x_10>30){
						Sounds.Northdown.QuickStoneSkip();
						Sounds.quickenwatch ();
						this.wrongcount++;

					if(GameLogic.GameVersion == 1){
						Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v1);
					}
					if(GameLogic.GameVersion == 2){
						Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v2);
					}
						this.state = State.none;
						yield break; // restart!
				}
//					}
//				}
//				if (Mathf.Abs (right.transWave_y_10 / right.transWave_x_10) > 0.6) {
//					if (right.transPitch > 20 && right.Grab < 0.5) {
//				if(right.transWave_x_6 >10){
//						Sounds.Northdown.SlowStoneSkip();
//						Sounds.quickenwatch ();
//						this.wrongcount++;
//						this.state = State.none;
//						yield break; // restart!
//				}
//					}
//				}
			}
		}
		
		while (this.state == State.ing) {
			yield return StartCoroutine(this.WaitForRightHand());
			Sinus.frequency = 0;
			Sinus.gain = 0;
			if (right.openhand && right.wristforward) {
				if (right.wristmiddle) {
					if ((right.transWave_y_10 > 60 && right.transWave_z_3 > 10) || (right.losetrack_trans_y > 100)) {
						PlayFromRighthand.PlayOneShot (Sounds.Dur_Stone_rightsleevedown, 3.0f);
						this.StoneCount ();
						Sounds.normalwatch ();
						Sounds.audiosource.PlayOneShot (Sounds.Hints.Stone_correct_hint4);
						this.state = State.none;
					} else {
						PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_waterdrop, 8.0f);
						Sounds.Environment.PlayOneShot (Sounds.Post_Stone_longlean);
						Sounds.quickenwatch ();
						this.state = State.none;
						if(GameLogic.GameVersion == 1){
							Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v1);
						}
						
						if(GameLogic.GameVersion == 2){
							Narrator.PlayIfPossible(Narrator.Stone_tinythrow_v2);
						}
						yield break; // restart!
					}
				}
				if (right.wristhigh) {
					if ((right.transWave_y_10 > 10 && right.transWave_z_3 > 5) || (right.losetrack_trans_y > 10 && right.losetrack_trans_z > 5)) {
						Sounds.hint1 ();
						Sounds.quickenwatch ();
						Sounds.Northforward.farskip();

						if(GameLogic.GameVersion == 1){
							Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v1);
						}
						if(GameLogic.GameVersion == 2){
							Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v2);
						}

						this.state = State.none;
						yield break; // restart!
					}
				}
			}
			if (right.openhand && right.wristleft) {
				Sounds.hint1 ();
				if ((right.transWave_y_10 > 10 && right.transWave_z_3 > 5) || (right.losetrack_trans_y > 10 && right.losetrack_trans_z > 5)) {
					if (right.wristhigh) {
						Sounds.quickenwatch ();
						Sounds.Northwest.sound1();
						Sounds.Northwest.sound2();
						PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_creak1); 

						if(GameLogic.GameVersion == 1){
							Narrator.PlayIfPossible(Narrator.Stone_faraway_v1);
						}
						if(GameLogic.GameVersion == 2){
							Narrator.PlayIfPossible(Narrator.Stone_faraway_v2);
						}

						this.state = State.none;
						yield break; // restart!
					}
					if (right.wristmiddle) {
						Sounds.quickenwatch ();
						Sounds.Northforward.leftskip();
						this.state = State.none;
						yield break; // restart!
					}
				}
				if ((right.transWave_y_10 < 10) || (right.losetrack_trans_y < 10)) {
					PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_dropontheboat, 10.0f);
					Sounds.quickenwatch ();
					this.state = State.none;
					yield break; // restart!
				}
			}
			if (right.openhand && right.wristright) {
				Sounds.hint1 ();
				if ((right.transWave_y_10 > 10 && right.transWave_z_3 > 5) || (right.losetrack_trans_y > 10 && right.losetrack_trans_z > 5)) {
					if (right.wristhigh) {
						Sounds.quickenwatch ();
						Sounds.Northeast.sound1();
						Sounds.Northeast.sound2();
						PlayFromRighthand.PlayOneShot (Sounds.Dur_Paddle_creak1); 
						if(GameLogic.GameVersion == 1){
							Narrator.PlayIfPossible(Narrator.Stone_faraway_v1);
						}
						if(GameLogic.GameVersion == 2){
							Narrator.PlayIfPossible(Narrator.Stone_faraway_v2);
						}
						this.state = State.none;
						yield break; // restart!
					}
					if (right.wristmiddle) {
						Sounds.quickenwatch ();
						Sounds.Northforward.rightskip();
						if(GameLogic.GameVersion == 1){
							Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v1);
						}
						if(GameLogic.GameVersion == 2){
							Narrator.PlayIfPossible(Narrator.Stone_sidethrow_v2);
						}
						this.state = State.none;
						yield break; // restart!
					}
				}
				if ((right.transWave_y_10 < 10) || (right.losetrack_trans_y < 10)) {
					PlayFromRighthand.PlayOneShot (Sounds.Post_Stone_dropontheboat, 10.0f); 
					Sounds.quickenwatch ();
					this.state = State.none;
					yield break; // restart!
				}
			}
		}
		
	}
}
