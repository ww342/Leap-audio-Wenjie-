using UnityEngine;
using System.Collections;
using Leap;

public class Righthand : MonoBehaviour
{
	Controller Controller = new Controller ();

	public Sounds Sounds;
	public Narrator Narrator;
	public hint hint;
	public rightpalm rightpalm;
	public finger_right finger_right;
	public Metrics Metrics;

	public enum GestureState
	{
		none,
		ready,
		detected,
		action,
		ing,
		other,
		cooldown
	}
	public GestureState Stone = GestureState.none;
	public GestureState Bird = GestureState.none;
	public GestureState Paddle = GestureState.none;
	public GestureState Rope = GestureState.none;
	public GestureState Star = GestureState.none;
	public GestureState Flower = GestureState.none;
	public GestureState Tree = GestureState.none;
	public GestureState Bike = GestureState.none;


	private float cooldownTime;
	public float MaxcooldownTime;
	public float MaxcooldownTime1;
	public int hit ;
	public int armcount;
	public bool GrabStone;
	public bool GrabWater;
	public float Throw;
	//public bool OpenHand;

	private AudioSource Gesturehint;

	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
		cooldownTime = MaxcooldownTime;
		hit = 0;
		GameObject.Find ("Hands").SendMessage ("RHhit", hit); 


		Gesturehint = gameObject.AddComponent <AudioSource> ();
		Gesturehint.minDistance = 5;
	}

	// Update is called once per frame
	void Update ()
	{
				//Frame variables
				Frame startframe = Controller.Frame ();
				Frame perviousframe3 = Controller.Frame (3);
				Frame perviousframe10 = Controller.Frame (10);
				Frame perviousframe6 = Controller.Frame (6);

				// Right Hand variables
				Hand rightmost = startframe.Hands.Rightmost;
				Arm arm = rightmost.Arm;

				if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
						Finger thumb = rightmost.Fingers [0];
						Finger index = rightmost.Fingers [1];
						Finger middle = rightmost.Fingers [2];
						Finger ring = rightmost.Fingers [3];
						Finger pinky = rightmost.Fingers [4];

						float ringtipSpeed_x = ring.TipVelocity.x;
						float ringtipSpeed_y = ring.TipVelocity.y;
						float ringtipSpeed_z = ring.TipVelocity.z;
						float trans_ringtipSpeed_z = perviousframe3.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;
			            Throw = trans_ringtipSpeed_z ;


						float pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
						float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
						float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
						float Grab = rightmost.GrabStrength;
						float Pinch = rightmost.PinchStrength;
						float radius = rightmost.SphereRadius;
						float handmove_x = rightmost.PalmPosition.x;
						float handmove_y = rightmost.PalmPosition.y;
						float handmove_z = rightmost.PalmPosition.z;

						float wrist_x = rightmost.Arm.WristPosition.x;
						float wrist_y = rightmost.Arm.WristPosition.y;
						float wrist_z = rightmost.Arm.WristPosition.z;
						float elbow_x = rightmost.Arm.ElbowPosition.x;
						float elbow_y = rightmost.Arm.ElbowPosition.y;
						float elbow_z = rightmost.Arm.ElbowPosition.z;
		
						Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);

						Vector3 wristposition = new Vector3 (wrist_x, wrist_y, -wrist_z);
		
						float transRadius = perviousframe6.Hands.Rightmost.SphereRadius - radius;
						float transPitch = perviousframe10.Hands.Rightmost.Direction.Pitch - pitch;
						float transYaw = perviousframe10.Hands.Rightmost.Direction.Yaw - yaw;
						float transRoll = perviousframe10.Hands.Rightmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
						float transWave_y_10 = perviousframe10.Hands.Rightmost.PalmPosition.y - handmove_y;
						float transWave_z_10 = perviousframe10.Hands.Rightmost.PalmPosition.z - handmove_z;
						float transWave_x_10 = perviousframe10.Hands.Rightmost.PalmPosition.x - handmove_x;
						float transWave_y_6 = perviousframe6.Hands.Rightmost.PalmPosition.y - handmove_y;
						float transWave_z_6 = perviousframe6.Hands.Rightmost.PalmPosition.z - handmove_z;
						float transWave_x_6 = perviousframe6.Hands.Rightmost.PalmPosition.x - handmove_x;
						float transWave_y_3 = perviousframe3.Hands.Rightmost.PalmPosition.y - handmove_y;
						float transWave_z_3 = perviousframe3.Hands.Rightmost.PalmPosition.z - handmove_z;
						float transWave_x_3 = perviousframe3.Hands.Rightmost.PalmPosition.x - handmove_x;

						Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);

						// variables calculate losing track distance
			            
						float losetrack_trans_x = rightpalm.losetrack_x - handmove_x;
						float losetrack_trans_y = rightpalm.losetrack_y - handmove_y;
						float losetrack_trans_z = rightpalm.losetrack_z - handmove_z;

						// gestures bool 
		
						bool wavearm = radius < 40 && pitch >= 95 && pitch <= 100;
						bool grabstone = transRadius > 10;//&& Grab >0.4 && pitch <10;
						bool catchbird = roll >= 170 && Pinch >= 0.5 && Pinch <= 1;
		
						bool yawforward = yaw <= 20 && yaw >= -20;
						bool yawside = yaw < -20 || yaw > 20;
						bool yawleft = yaw < -20;
						bool yawright = yaw > 20;
						bool pitchforward = pitch <= 10 && pitch >= 0;
						bool pitchupforward = pitch <= 45 && pitch >= 40;
						bool palmdown = roll < 20 && roll > -20;
						bool palmup = roll <= -140 || roll >= 140; 
						bool palmright = roll > 50 && roll < 60;
						bool palmleft = roll > - 60 && roll < -50;
						bool palmleftin = roll > -160 && roll < -130;
						bool pitchdownforward = pitch <= -10 && pitch >= -30;
						bool palmin = yaw <= -50 && yaw >= -80;
						bool openhand = thumb.IsExtended && index.IsExtended && middle.IsExtended && ring.IsExtended && pinky.IsExtended;
			           
						bool elbowforward = elbow_z < 200;
						bool wristhigh = wrist_y > 350;
						bool wristleft = wrist_x < -70;
						bool wristright = wrist_x > 70;
						bool wristmiddle = wrist_y < 350;
						bool wristforward = wrist_x > -70 && wrist_x < 70;





						//Stone
		

						if (Metrics.levelcount == 0) {

								if (!Metrics.Nar_Check) {

										switch (Stone) {
			
										case GestureState.none:
					
												if (pitchforward && palmdown) {
														Stone = GestureState .detected;

												}
				
												break;
			
										case GestureState.detected:
								//if (transPitch > 10) {
												if (Grab > 0.9 && wristforward) {
														audio.PlayOneShot (Sounds.grabstone);
							                            //Instantiate (GameObject.Find ("Stone"));
							                            GrabStone = true;
														Stone = GestureState .action;
														GameObject.Find ("Hands").SendMessage ("hint3");
												}

						                        if (Grab > 0.9 ){
							                      if(wristleft||wristright) {
							                           audio.PlayOneShot (Sounds.gentlesplash, 3.0f);
								                       audio.PlayOneShot(Sounds.longcreak);
							                           Stone = GestureState.action;
								                       GrabWater = true;
								                       
							                           GameObject.Find ("Hands").SendMessage ("hint3");
						                         }
						                     } 
								//}
												break;
			
										case GestureState.action:
						                if(GrabStone){

						                 if(wristforward && openhand){
						               
														GameObject.Find ("Hands").SendMessage ("transitwatch");
														Stone = GestureState.none;
														GameObject.Find ("Hands").SendMessage ("hint2");
														audio.PlayOneShot (finger_right.stonedrop);
							                            GrabStone = false;
											}
							             if(wristleft||wristright) {
								                   if(openhand){
									GameObject.Find ("Hands").SendMessage ("transitwatch");
									Stone = GestureState.none;
									GrabStone = false;
									GameObject.Find ("Hands").SendMessage ("hint2");
									audio.PlayOneShot (Sounds.waterdrop,10.0f);
									
								}
							}

							if (Mathf.Abs (transWave_z_10) > 50) {
								//|| Mathf.Abs(losetrack_trans_z) >30)
								GameObject.Find ("Hands").SendMessage ("quickwatch");
								Stone = GestureState.ready;
							}
							if (palmleft || palmleftin || palmup || palmright) {
								Stone = GestureState.other;
								GameObject.Find ("Hands").SendMessage ("transitwatch");
								GameObject.Find ("Hands").SendMessage ("hint2");
							}
							if (palmdown) {
								//audio.PlayOneShot (hint.Stone_correct_hint0);
								//Switch(stonecorrect_hint){
								
								//case HintState.hitten:
								
								if (wrist_y > 250 && wrist_y <= 260) {
									Gesturehint.PlayOneShot (hint.Stone_correct_hint1);
									//stonecorrect_hint = HintState.cooldown ;
								}
								if (wrist_y > 300 && wrist_y <= 310) {
									Gesturehint.PlayOneShot (hint.Stone_correct_hint2);
									//stonecorrect_hint = HintState.cooldown;
								}
								if (wrist_y > 350 && wrist_y <= 360) {
									Gesturehint.PlayOneShot (hint.Stone_correct_hint3);
									//stonecorrect_hint = HintState.cooldown;
								}
								if (wrist_y > 400 && wrist_y <= 410) {
									Gesturehint.PlayOneShot (hint.Stone_correct_hint4);
									//stonecorrect_hint = HintState.cooldown;
								}
								/*break;

						case HintState.cooldown:

						if (cooldownTime <= 0) {
							cooldownTime = MaxcooldownTime1;
							stonecorrect_hint = HintState.hitten;
						}
						break;*/
								
								if (transWave_y_10 < - 50) {
									GameObject.Find ("Hands").SendMessage ("normalwatch");
									audio.PlayOneShot (Sounds.rightsleevelift,3.0f);
									GameObject.Find ("Hands").SendMessage ("hint3");
									Stone = GestureState.ing;
								}
							}
						}
							                  

								if(GrabWater){
								                      GameObject.Find ("Hands").SendMessage ("transitwatch");
								                      Stone = GestureState.none;
								                      GrabWater = false;
								                      GameObject.Find ("Hands").SendMessage ("hint2");
								                      audio.PlayOneShot (Sounds.gentlewaterdrop,2.5f);
							                    }
						               


												
												break;

										case GestureState.ready:
												if (Grab == 0) {
														//audio.PlayOneShot (script.stonewrong);
														audio.PlayOneShot (Sounds.waterdrop, 8.0f);
							                            GameObject.Find ("Hands").SendMessage ("quickwatch");
														GameObject.Find ("Hands").SendMessage ("WrongCount");
														GameObject.Find ("Hands").SendMessage ("hint2");
														Stone = GestureState .none;
												}
												break;

										case GestureState.other:
												if (palmdown) {
														if (Mathf.Abs (transWave_y_10 / transWave_x_10) < 0.6 && Mathf.Abs (transWave_y_10 / transWave_x_10) > 0.1) {
																//|| (  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)<0.6  &&  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)>0.1  ))
																if (openhand) {
																		//audio.PlayOneShot (script.stonewrong);
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		audio.PlayOneShot (Sounds.waterdrop, 8.0f);
																		GameObject.Find ("Hands").SendMessage ("hint1");
																		GameObject.Find ("Hands").SendMessage ("WrongCount");
																		Stone = GestureState.none;
																}
														}
														if (transWave_y_10 < - 50) {
																GameObject.Find ("Hands").SendMessage ("hint3");
																audio.PlayOneShot (Sounds.rightsleevelift, 3.0f);
																GameObject.Find ("Hands").SendMessage ("normalwatch");
																Stone = GestureState.ing;
														}
												}
												if (palmleft || palmleftin || palmup || palmright) {
														GameObject.Find ("Hands").SendMessage ("hint2");
														if (Mathf.Abs (transWave_y_6 / transWave_x_6) < 0.6 && Mathf.Abs (transWave_y_6 / transWave_x_6) > 0.1) {
																//|| (  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)<0.6  &&  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)>0.1  ))
																if (transPitch > 20 && Grab < 0.5) {
																		//audio.PlayOneShot (script.stonewrong);
																		GameObject.Find ("Northdown").SendMessage ("sound3");
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Hands").SendMessage ("WrongCount");
																		Stone = GestureState.none;
																}
														}

														if (Mathf.Abs (transWave_y_3 / transWave_x_3) < 0.6 && Mathf.Abs (transWave_y_3 / transWave_x_3) > 0.1) {
																//|| (  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)<0.6  &&  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)>0.1  ))
																if (transPitch > 20 && Grab < 0.5) {
																		//audio.PlayOneShot (script.stonewrong);
																		GameObject.Find ("Northdown").SendMessage ("sound5");
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Hands").SendMessage ("WrongCount");
																		Stone = GestureState.none;
																}
														}
														if (Mathf.Abs (transWave_y_10 / transWave_x_10) > 0.6) {
																//|| (  Mathf.Abs(losetrack_trans_y/losetrack_trans_x)>0.6 ))
																if (transPitch > 20 && Grab < 0.5) {
																		//audio.PlayOneShot (script.stonewrong);
																		GameObject.Find ("Northdown").SendMessage ("sound4");
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Hands").SendMessage ("WrongCount");
																		Stone = GestureState.none;
																}
														}
												}
												break;

										case  GestureState.ing:
												if (openhand && wristforward) {
							                           if (wristmiddle) {
								                          if ((transWave_y_10 > 60 && transWave_z_3 > 10) || (losetrack_trans_y > 100 )) {
																
																		audio.PlayOneShot (Sounds.rightsleevedown, 3.0f);
																		GameObject.Find ("Hands").SendMessage ("StoneCount");
																		GameObject.Find ("Hands").SendMessage ("normalwatch");
																		audio.PlayOneShot (hint.Stone_correct_hint4);
																		Stone = GestureState .cooldown;
																}
								else{
								
									audio.PlayOneShot (Sounds.waterdrop, 8.0f);
									audio.PlayOneShot (Sounds.longlean);
									GameObject.Find ("Hands").SendMessage ("quickwatch");
									Stone = GestureState .none;
								}
							}
							
							if (wristhigh) {
								if ((transWave_y_10 > 10 && transWave_z_3 > 5) || (losetrack_trans_y > 10 && losetrack_trans_z > 5)) {
								GameObject.Find ("Hands").SendMessage ("hint1");
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Northforward").SendMessage ("farskip");
																		Stone = GestureState .none;
							                                    }

																 
														
							                  }
						}
												
												if (openhand && wristleft) {
														GameObject.Find ("Hands").SendMessage ("hint1");
														if ((transWave_y_10 > 10 && transWave_z_3 > 5) || (losetrack_trans_y > 10 && losetrack_trans_z > 5)) {
																if (wristhigh) {
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Northwest").SendMessage ("sound1");
																		GameObject.Find ("Northwest").SendMessage ("sound2");
																		audio.PlayOneShot (finger_right.creak1);
																		Stone = GestureState .none;
																}
																if (wristmiddle) {
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Northforward").SendMessage ("leftskip");
																		Stone = GestureState.none;
																}
														}
														if ((transWave_y_10 < 10) || (losetrack_trans_y < 10)) {
																audio.PlayOneShot (finger_right.stonedrop, 10.0f);
								                                GameObject.Find ("Hands").SendMessage ("quickwatch");
																Stone = GestureState .none;
														}
												}
												if (openhand && wristright) {
														GameObject.Find ("Hands").SendMessage ("hint1");
														if ((transWave_y_10 > 10 && transWave_z_3 > 5) || (losetrack_trans_y > 10 && losetrack_trans_z > 5)) {
																if (wristhigh) {
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Northeast").SendMessage ("sound1");
																		GameObject.Find ("Northeast").SendMessage ("sound2");
																		audio.PlayOneShot (finger_right.creak1);
																		Stone = GestureState .none;
																}
																if (wristmiddle) {
																		GameObject.Find ("Hands").SendMessage ("quickwatch");
																		GameObject.Find ("Northforward").SendMessage ("rightskip");
																		Stone = GestureState .none;
																}
														}
														if ((transWave_y_10 < 10) || (losetrack_trans_y < 10)) {
																audio.PlayOneShot (finger_right.stonedrop, 10.0f);
								                                GameObject.Find ("Hands").SendMessage ("quickwatch");
																Stone = GestureState .none;
														}
												}
												break;
			
										case GestureState.cooldown:
												cooldownTime -= Time.deltaTime;
												if (cooldownTime <= 0) {
														Stone = GestureState.none;
														cooldownTime = MaxcooldownTime;
												}
												break;
										}

								}
						}



						//Bird

						if (Metrics.levelcount == 1 || Metrics.levelcount == 2) {
								if (Metrics.wrongcount > 2 || Metrics.flowercount == 4) {
										if (!Metrics.Nar_Check) {

												switch (Bird) {
			
												case GestureState.none:

														if (openhand && palmdown) {
																Bird = GestureState .detected;
														}
				
														break;
			
												case GestureState.detected:
														if (palmleftin) { 
																Bird = GestureState.action;
														}
														if (Grab == 1) {
																Bird = GestureState.ready;
																GameObject.Find ("Hands").SendMessage ("quickwatch");
																GameObject.Find ("Hands").SendMessage ("hint1");
																audio.PlayOneShot (Sounds.panicflapping);
																audio.PlayOneShot (Sounds.grabbird);
																audio.PlayOneShot (Sounds.boatshiffer2, 5.0f);
														}
														if (transWave_y_3 > 30) {
																Bird = GestureState .none;
																GameObject.Find ("Hands").SendMessage ("quickwatch");
																GameObject.Find ("Hands").SendMessage ("hint1");
																audio.PlayOneShot (Sounds.seedpouring);
																audio.PlayOneShot (Sounds.panicbird);
																audio.PlayOneShot (Sounds.panicflapping);
																audio.PlayOneShot (Sounds.panicfrog);
																audio.PlayOneShot (Sounds.boatshake, 5.0f);
																audio.PlayOneShot (Sounds.longlean);
																audio.PlayOneShot (Sounds.birdpecking);
														}
														break;

												case GestureState.ready:
														if (Grab < 0.8) {
																Bird = GestureState.none;
																GameObject.Find ("Hands").SendMessage ("transitwatch");
																GameObject.Find ("Hands").SendMessage ("hint2");
																audio.PlayOneShot (Sounds.weakflapping);
																audio.PlayOneShot (Sounds.birdflyslonghand);
														}
														break;
			
												case GestureState.action:
														if (!ring.IsExtended && Grab < 0.8) {
																hit = 1;
																GameObject.Find ("Hands").SendMessage ("RHhit", hit);
																Bird = GestureState .cooldown;
														}
														break;
			
												case GestureState.cooldown:
														cooldownTime -= Time.deltaTime;
														if (cooldownTime <= 0) {
																hit = 0;
																GameObject.Find ("Hands").SendMessage ("RHhit", hit);
																Bird = GestureState.none;
																cooldownTime = MaxcooldownTime1;
														}
														break;
												}

										}

								}

						}

						// Paddle

						if (Metrics.levelcount == 3) {
								if (!Metrics.Nar_Check) {

										switch (Paddle) {
			
										case GestureState.none:
								
												if (pitchforward && palmdown) {
														Paddle = GestureState .detected;
												}
							
												break;
			
										case GestureState.detected:
												if (transPitch > 30) { 
														hit = 2;
														audio.PlayOneShot (finger_right.creak1);
														GameObject.Find ("Hands").SendMessage ("RHhit", hit);
														Paddle = GestureState.action;
												} 
												break;
			
										case GestureState.action:
												Paddle = GestureState .cooldown;
												break;
			
										case GestureState.cooldown:
												cooldownTime -= Time.deltaTime;
												if (cooldownTime <= 0) {
														Paddle = GestureState.none;
														hit = 0;
														GameObject.Find ("Hands").SendMessage ("RHhit", hit);
														cooldownTime = MaxcooldownTime;
												}
												break;
										}
								}
						}

						// Rope
						switch (Rope) {
				
						case GestureState.none:
								if (Metrics.levelcount == 4) {
										if (Metrics.wrongcount > 2 || Metrics.flowercount == 12) {
												if (palmright) {
														Rope = GestureState.ready;
												}
										}
								}
								break;

						case  GestureState.ready:
								if (Metrics.levelcount == 4) {
										if (Metrics.wrongcount > 2 || Metrics.flowercount == 12) {
												if (!openhand) {
														if (transWave_z_10 > 50) {
																Rope = GestureState .detected;
														}
												}
										}
								}
								break;

						case GestureState.detected:
								if (!openhand) {
										if (transWave_x_10 > 5) {
												Rope = GestureState .action;
										}
								}
								break;

						case GestureState.action:
								if (!openhand) {
										if (transWave_z_10 < -50) {
												Rope = GestureState .ing;
										}
								}
								break;
				
						case GestureState.ing:
								if (!openhand) {
										if (transWave_x_10 < -5) {
												GameObject.Find ("Hands").SendMessage ("RopeCount");
												Rope = GestureState .cooldown;
										}
								}
								break;

						case GestureState.cooldown:
								cooldownTime -= Time.deltaTime;
								if (cooldownTime <= 0) {
										Rope = GestureState.ready;
										cooldownTime = MaxcooldownTime;
								}
								break;
						}


						// Flower

						if (Metrics.levelcount == 1 && Metrics.wrongcount <= 2 && Metrics.flowercount < 4) {
								if (!Metrics.Nar_Check) {

										switch (Flower) {
			
										case GestureState.none:

												if (pitchforward && palmdown) {
														Flower = GestureState .detected;
												}
				
												break;
			
										case GestureState.detected:
												if (Pinch == 1) {
														Flower = GestureState .action;
												}
												break;
			
										case GestureState.action:
												if (palmup) {//transRoll > 50 && transRoll < 120 && Pinch > 0.8)
														GameObject.Find ("Hands").SendMessage ("FlowerCount");
														Flower = GestureState .cooldown;	
												}
												if (transRoll > 50 && transRoll < 120 && Pinch < 0.5) {
														audio.PlayOneShot (Sounds.paddlewrong);
														Flower = GestureState .cooldown;	
												}
												break;

			
										case GestureState.cooldown:
												cooldownTime -= Time.deltaTime;
												if (cooldownTime <= 0) {
														Flower = GestureState.none;
														cooldownTime = MaxcooldownTime;
												}
												break;
										}

								}
						} 


						// Tree
						if (Metrics.levelcount == 4 && Metrics.wrongcount <= 2 && Metrics.flowercount < 12) {
								if (!Metrics.Nar_Check) {

										switch (Tree) {
			
										case GestureState.none:

												if (palmleft) {
														Tree = GestureState .detected;
												}
				
												break;
			
										case GestureState.detected:
												if (Metrics.levelcount == 4 && Metrics.wrongcount <= 2 && Metrics.flowercount < 12) {
														if (!ring.IsExtended) {
																Tree = GestureState .action;
														}
												}
												break;
			
										case GestureState.action:
												if (transWave_x_10 > 10) {
														GameObject.Find ("Hands").SendMessage ("FlowerCount");
														Tree = GestureState .cooldown;	
												}
												break;
			
										case GestureState.cooldown:
												cooldownTime -= Time.deltaTime;
												if (cooldownTime <= 0) {
														Tree = GestureState.detected;
														cooldownTime = MaxcooldownTime;
												}
												break;
										}
								}
						}
		
						// Bike

						if (Metrics.levelcount == 5) {
								if (!Metrics.Nar_Check) {

										switch (Bike) {
			
										case GestureState.none:

												if (pitchforward) {
														Bike = GestureState.ready;
												}
			
												break;
			
										case GestureState.ready:
												if (Metrics.levelcount == 5 && Metrics.bellcount < 6) {
														if (palmdown && Grab > 0.4 && thumb.IsExtended) {
																audio.PlayOneShot (finger_right.bike);
																Bike = GestureState .detected;
														}
												}
												break;
			
										case GestureState.detected:
												if (!thumb.IsExtended) {
														audio.PlayOneShot (finger_right.bell);
														GameObject.Find ("Hands").SendMessage ("BellCount");
														Bike = GestureState.action;
				
												}
												break;
			
										case GestureState.action:
												Bike = GestureState .cooldown;	
												break;
			
										case GestureState.cooldown:
												cooldownTime -= Time.deltaTime;
												if (cooldownTime <= 0) {
														Bike = GestureState.ready;
														cooldownTime = MaxcooldownTime;
												}
												break;
										}
								}
						}

						// Star

						if (Metrics.levelcount == 6) {
				                  if (!Metrics.Nar_Check) {

								switch (Star) {
			
								case GestureState.none:

										if (pitchupforward) {
												Star = GestureState.ready;
										}
			
										break;
			
								case GestureState.ready:
										if (transPitch > 10) {
												if (!openhand) {
														Star = GestureState .detected;
												}
										}
										break;
			
								case GestureState.detected:
				                        //if(transYaw>3){
										if (palmin) {
												audio.PlayOneShot (Sounds.star);
												Star = GestureState .action;
										}
										break;
			
								case GestureState.action:
										if (palmup) {
												GameObject.Find ("Hands").SendMessage ("StarCount");
												audio.PlayOneShot (Sounds.win);
												audio.PlayOneShot (Sounds.shiny);
												Star = GestureState .cooldown;	
										}
										break;
			
								case GestureState.cooldown:
										cooldownTime -= Time.deltaTime;
										if (cooldownTime <= 0) {
												Star = GestureState.detected;
												cooldownTime = MaxcooldownTime;
										}
										break;
								}
						}
				}
		}
	}
}
