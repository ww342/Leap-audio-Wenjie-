using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof(AudioSource))]
public class Hands : MonoBehaviour
{
	Controller Controller = new Controller ();

	public Sounds Sounds;
	public Narrator Narrator;
	public Metrics Metrics;
	
	public enum HandState
	{
		none,
		onehand,
		twohands,
		cooldown
	}
	public HandState Paddle = HandState.none;
	public HandState Bird = HandState.none;
	public HandState Bike = HandState.none;
	private float cooldownTime;
	public float MaxcooldownTime;
	public float MaxcooldownTime1;
	public int rhhit;
	public int lhhit;

	//SOS GESTURE VARIABLES
/*
	public Vector3 watchposition;
	public Vector3 tapwatch;
	public Vector3 difference;
	public float tappingdistance;
*/
	//
	private AudioSource Ambience_A;
	private AudioSource Ambience_B;
	private AudioSource Ambience_C;
	private AudioSource Ambience_D;
	private AudioSource Handfree;
	private AudioSource watch;
	private AudioSource transtion_hint;



	public  int hands;

	//public float VoiceLength;
	//public bool Checking;

	// Use this for initialization
	void Start ()
	{

		Metrics.Nar_Check = false;

		Controller = new Controller ();
		cooldownTime = MaxcooldownTime;
		Metrics.levelcount = -1;

		Handfree = gameObject.AddComponent<AudioSource> ();
		Handfree.clip = Sounds.snore;
		Handfree.loop = true;
		Handfree.pitch = 2;
		Handfree.minDistance = 8;
		Handfree.Play ();


		Ambience_A = gameObject.AddComponent<AudioSource> ();
		Ambience_A.clip = Sounds.fish;
		Ambience_A.loop = true;
		Ambience_A.minDistance = 10;
		//Ambience_A.Play ();


		Ambience_B = gameObject.AddComponent<AudioSource> ();
		Ambience_B.clip = Sounds.frog;
		Ambience_B.loop = true;
		Ambience_B.minDistance = 10;
		Ambience_B.Play ();

		Ambience_C = gameObject.AddComponent <AudioSource> ();
		Ambience_C.clip = Sounds.lakewaveslapping;
		Ambience_C.loop = true;
		Ambience_C.pitch = 1;
		Ambience_C.minDistance = 6;
		Ambience_C.Play ();

		Ambience_D = gameObject.AddComponent<AudioSource> ();
		Ambience_D.minDistance = 10;

		watch = gameObject.AddComponent <AudioSource> ();
		watch.clip = Sounds.slowwatch;
		watch.loop = true;
		watch.minDistance = 30;
		watch.Play ();

		transtion_hint = gameObject.AddComponent <AudioSource> ();

		//transtion_hint.clip  = hint.Stone_correct_hint0;
		transtion_hint.clip = Sounds.transitionhint1;
		//transtion_hint .loop = true;
		//transtion_hint.pitch = 1;
		transtion_hint.minDistance = 2;

		//narrator = GameObject.Find ("Narrator").GetComponent<AudioSource>();
		//narrator = gameObject.AddComponent <AudioSource> ();
		//narrator.minDistance = 50;
	}
	
	void quickwatch ()
	{
		watch.clip = Sounds.quickwatch;
		watch.Play ();
	}

	void transitwatch ()
	{
		watch.clip = Sounds.middlewatch;
		watch.Play ();
	}

	void normalwatch ()
	{
		watch.clip = Sounds.slowwatch;
		watch.Play ();
	}

	void hint1 ()
	{
		transtion_hint.clip = Sounds.transitionhint1;
		transtion_hint.Play ();
	}

	void hint2 ()
	{
		transtion_hint.clip = Sounds.transitionhint2;
		transtion_hint.Play ();
	}

	void hint3 ()
	{
		transtion_hint.clip = Sounds.transitionhint3;
		transtion_hint.Play ();
	}

	void playropepose ()
	{
		Narrator.audio.PlayOneShot (Narrator.ropepose);
		Metrics.Nar_Check= true;
		Metrics.VoiceLength = Narrator.ropepose.length;
	}

	void StoneCount ()
	{
		Metrics.stonecount ++;
		//audio.PlayOneShot (stone);
		//audio.PlayOneShot (hitfish);
		//normalwatch ();
		GameObject.Find ("Northdown").SendMessage ("sound1");
		GameObject.Find ("Northdown").SendMessage ("sound2");
		GameObject.Find ("Northdown").SendMessage ("sound6");
	

		if (Metrics.stonecount == 1) {

			Narrator.audio.PlayOneShot (Narrator.stone1);
			Metrics.Nar_Check =true;
			Metrics.VoiceLength = Narrator.stone1.length;

			Ambience_A.minDistance = 7;
		}

		if (Metrics.stonecount == 2) {

			Narrator.audio.PlayOneShot (Narrator.stone2);
			Metrics.Nar_Check =true;
			Metrics.VoiceLength = Narrator.stone2.length;

			Ambience_A.minDistance = 4;
		}

		if (Metrics.stonecount == 3) {
			Ambience_D.PlayOneShot (Sounds.spash, 0.5f);
			LevelCount ();
			Ambience_A.minDistance = 1;
	
			if (Metrics.wrongcount <= 2) {

				Narrator.audio.PlayOneShot (Narrator.flowerpose);
				Metrics.Nar_Check =true;
				Metrics.VoiceLength = Narrator.flowerpose.length;

				Ambience_A.clip = Sounds.duck;
				Ambience_A.minDistance = 8;
				Ambience_B.loop = true;
				Ambience_A.Play ();
			}

			if (Metrics.wrongcount > 2) {

				Narrator.audio.PlayOneShot (Narrator.stone3);
				Metrics.Nar_Check =true;
				Metrics.VoiceLength = Narrator.stone3.length;

				Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);
				Ambience_A.clip = Sounds.shortflapping;
				Ambience_A.minDistance = 20;
				Ambience_A.loop = false;
				Ambience_A.Play ();
			}
		}
	}

	void PaddleCount()

	{

				Metrics.paddlecount ++;
				Ambience_D.PlayOneShot (Sounds.paddle);
		
				if (Metrics.paddlecount == 1) {
						Narrator.audio.PlayOneShot (Narrator.paddle1);
						Metrics.Nar_Check = true;
						Metrics.VoiceLength = Narrator.paddle1.length;
						Ambience_A.minDistance = 5;
						Ambience_D.PlayOneShot (Sounds.lakewaveslapping);
						Ambience_A.Play ();
						Ambience_B.minDistance = 8;
				}
				if (Metrics.paddlecount == 2) {
			
						Ambience_B.minDistance = 6;
				}
				if (Metrics.paddlecount == 3) {
			
						Ambience_B.minDistance = 4;
						Ambience_C.minDistance = 2;
				}
				if (Metrics.paddlecount == 4) {
						Narrator.audio.PlayOneShot (Narrator.paddle4);
						Metrics.Nar_Check = true;
						Metrics.VoiceLength = Narrator.paddle4.length;
			
						Ambience_A.Stop ();
						Ambience_B.minDistance = 1;
						Ambience_C.minDistance = 1;
						LevelCount ();
						if (Metrics.wrongcount > 2) {
								Invoke ("playropepose", 5);
						}
						if (Metrics.wrongcount <= 2) {
								Narrator.audio.PlayOneShot (Narrator.treepose);
								Metrics.Nar_Check = true;
								Metrics.VoiceLength = Narrator.treepose.length;
						}
			
				}
		}
		
		
		
		
		void RopeCount ()
		{
			Metrics.ropecount ++;
			
			if (Metrics.ropecount == 1) {
				
				Narrator.audio.PlayOneShot (Narrator.rope1);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.rope1.length;

			Ambience_D.PlayOneShot (Sounds.rope);
			Ambience_B.minDistance = 0;
			Ambience_B.clip = Sounds.crickets;
		}
		
		if (Metrics.ropecount == 2) {

			Ambience_D.PlayOneShot (Sounds.rope);
			Narrator.audio.PlayOneShot (Narrator.rope2);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.rope2.length;

			Ambience_B.minDistance = 2;
		}

		if (Metrics.ropecount == 3) {

			Narrator.audio.PlayOneShot (Narrator.rope3);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.rope3.length;


			Ambience_D.PlayOneShot (Sounds.grass);
			Ambience_B.minDistance = 5;
			Ambience_B.Play ();
			LevelCount ();
			Metrics.wrongcount = 0;
		}
	}
	
	void BirdCount ()
	{
		Metrics.birdcount ++;
		Ambience_D.PlayOneShot (Sounds.bird);
		LevelCount ();
		normalwatch ();
		hint3 ();
		Ambience_D.PlayOneShot (Sounds.grabseed);

		
		if (Metrics.birdcount == 1) {
			Narrator.audio.PlayOneShot (Narrator.bird1);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.bird1.length;

		}
		if (Metrics.birdcount == 2) {
			Narrator.audio.PlayOneShot (Narrator.bird2);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.bird2.length;

			Ambience_D.PlayOneShot (Sounds.rain);
			Ambience_A.clip = Sounds.wave;
			Ambience_A.loop = true;
			Ambience_A.minDistance = 8;
			Ambience_A.Play ();
			Metrics.wrongcount = 0;
		}
	}

	void BirdWrong(){

		Ambience_D.PlayOneShot (Sounds.birdonboat);
		Ambience_D.PlayOneShot (Sounds.shortflapping);
		Narrator.audio.PlayOneShot (Narrator.birdwrong);
		Metrics.Nar_Check = true;
		Metrics.VoiceLength = Narrator.birdwrong.length;
		WrongCount ();
		quickwatch ();
		hint3 ();

	}
	void FlowerCount ()
	{
		Metrics.flowercount ++;
		Sounds.audiosource.PlayOneShot (Sounds.flower);
		        
		if (Metrics.flowercount == 1) {
			Narrator.audio.PlayOneShot (Narrator.flower1);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.flower1.length;
		}

		if (Metrics.flowercount == 4) {
			Metrics.wrongcount = 0;
			Ambience_A.Stop ();
			Ambience_A.minDistance = 8;
			Ambience_A.clip = Sounds.wave;
			Ambience_D.PlayOneShot (Sounds.landonflpping, 5.0f);
			Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);

			Narrator.audio.PlayOneShot (Narrator.flyfromtree);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.flyfromtree.length;
		}

		if (Metrics.flowercount == 5) {
			Sounds.audiosource.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 7) {
			Ambience_D.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 10) {
			Ambience_D.PlayOneShot (Sounds.leave);
			Ambience_D.PlayOneShot (Sounds.wind, 5.0f);
		}
			            
		if (Metrics.flowercount == 12) {
			Narrator.audio.PlayOneShot (Narrator.tietheboat);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.tietheboat.length;
			//LevelCount ();
			Metrics.wrongcount = 0;
		}
	}
	
	void BellCount ()
	{
		Metrics.bellcount ++;

		if (Metrics.bellcount == 1) {
			Ambience_B.minDistance = 10;
		}
		if (Metrics.bellcount == 3) {
			Ambience_D.PlayOneShot (Sounds.lightning);
		}

		if (Metrics.bellcount == 4) {
		}
		
		if (Metrics.bellcount == 5) {

			Ambience_D.PlayOneShot (Sounds.wind);
			Ambience_B.clip = Sounds.grass;
			Ambience_B .Play ();
		}

		if (Metrics.bellcount == 6) {

			Ambience_D.PlayOneShot (Sounds.brake);
			Narrator.audio.PlayOneShot (Narrator.bell6);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.bell6.length;

			LevelCount ();
		}
	}
	
	void StarCount ()
	{
		Metrics.starcount ++;

		if (Metrics.starcount == 1) {

			Narrator.audio.PlayOneShot (Narrator.star1);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.star1.length;

			Ambience_D.PlayOneShot (Sounds.lightning);
			Ambience_D.PlayOneShot (Sounds.footstep);
		}


		if (Metrics.starcount == 2) {

			Narrator.audio.PlayOneShot (Narrator.star2);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.star2.length;

			Ambience_D.PlayOneShot (Sounds.footstep);
		}

		
		if (Metrics.starcount == 3) {
			Narrator.audio.PlayOneShot (Narrator.star3);
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.star3.length;

			Invoke ("Glow", 2);
			Invoke ("Timetravel", 6);
			Invoke ("watchstop", 8);
			LevelCount ();
		}
	}

	void watchstop ()
	{
		watch.Stop ();
	}

	void Glow ()
	{
		Ambience_D.PlayOneShot (Sounds.glow);
	}

	void Timetravel ()
	{
		Ambience_D.PlayOneShot (Sounds.timetravel,2.0f);
	}

	void LHhit (int statenumber)
	{
		lhhit = statenumber;
		Update ();
	}

	void RHhit (int statenumber)
	{
		rhhit = statenumber;
		Update ();
	}

	// A void that gets wrist position from the right hand

	/*
	void WatchSurface(Vector3 wristposition)
	{
		watchposition = wristposition;
		Update ();
	}

	// A void that gets index position from the left hand
	void TaptheWatch(Vector3 indextip)
	{
		tapwatch = indextip;
		Update ();
	}

    void Beep(){
		switch (TapWatch) {
		case HandState.none:
		    {
				audio.PlayOneShot (sounds.watchbeep, 30.0f);
				
				TapWatch = HandState.cooldown;
			}
			break;
			
		case HandState.cooldown:
			cooldownTime -= Time.deltaTime;
			if (cooldownTime <= 0) {
				TapWatch = HandState.none;
				cooldownTime = MaxcooldownTime1;
			}
			break;
		}
	}
	*/

	void LevelCount ()
	{
		Metrics.levelcount ++;
		if (Metrics.levelcount == 0) {
			Metrics.Nar_Check= true;
			Metrics.VoiceLength = Narrator.begin.length;
			Debug.Log ("Story Begins!");
			
		}
		if (Metrics.levelcount == 5) {
			Ambience_D.PlayOneShot (Sounds.wind);
		}
		if (Metrics.levelcount == 6) {
			Ambience_D.PlayOneShot (Sounds.sky);
			Ambience_B.minDistance = 10;
		}
	}

	void WrongCount ()
	{
		Metrics.wrongcount ++;
	}

	/*void loadlandscene()
	{
		Application.LoadLevel("land");
	}

	void gameover()
	{
		Application.LoadLevel("gameover");
	}
	*/
	
	// Update is called once per frame
	void Update ()
	{


		Frame frame = Controller.Frame ();
		int handnumbers = frame.Hands.Count;

		if (Metrics.levelcount == -1) {
						if (handnumbers > 0) {
								Handfree.Stop ();
								Handfree.loop = false;
								LevelCount ();
								Narrator.audio.PlayOneShot (Narrator.begin);
				                
			}
				}
	 
		
		if (Metrics.levelcount > -1 && Metrics.levelcount < 6) {
			if (handnumbers < 1) {
				/*
				Handfree.pitch =3;
				Handfree.minDistance =20;
				Handfree.clip=sounds.lean;
				Handfree.Play();
                */
				Ambience_C.pitch = 3;
			}
			if (handnumbers > 0) {
				Ambience_C.pitch = 1;
			}
		}

		/*
		 * if (levelcount == 0) {
						if (handnumbers ==0) {
								Handfree.Play ();


						} if (handnumbers>0){
								Handfree.Stop ();
			
						}
				}
*/

		// Tap Watch to Send SOS message

		/*
		 * difference = new Vector3( watchposition.x  - tapwatch.x, watchposition.y - tapwatch.y, watchposition.z - tapwatch.z);
		
		tappingdistance = Mathf.Sqrt(
			Mathf.Pow(difference.x, 2f) +
			Mathf.Pow(difference.y, 2f) +
			Mathf.Pow(difference.z, 2f));
		

		switch (TapWatch) {
				case HandState.none:
						if (tappingdistance > 90 && tappingdistance < 100) {
				
								audio.PlayOneShot (sounds.watchbeep, 30.0f);

								TapWatch = HandState.cooldown;
						}

						break;

				case HandState.cooldown:

						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
								TapWatch = HandState.none;
								cooldownTime = MaxcooldownTime1;
						}

						break;
				}
*/




		// Narrator Checking
		
		if (Metrics.Nar_Check) { 
			Metrics.VoiceLength -= Time.deltaTime;
			Debug.Log ("Narrator is talking, Stop Gesture Detection!");
			
		}				
		if (Metrics.VoiceLength < 0f) { //here you can check if clip is not playing
			Debug.Log ("Narrator Stops, Run Gesture Detection!");
			Metrics.Nar_Check = false;
		}







		
		// Paddle
		if (Metrics.levelcount == 3) {
						if (!Metrics.Nar_Check) {
								switch (Paddle) {

								case HandState.none:
										if (handnumbers == 1) {
												Paddle = HandState.onehand;
										}
										if (handnumbers == 2) {
												Paddle = HandState.twohands;
										}
										break;

								case HandState.onehand:
										if (handnumbers == 1) {
												if (rhhit == 2 && lhhit == 0) {
														Ambience_D.PlayOneShot (Sounds.paddlewrong);
							                            GameObject.Find ("Hands").SendMessage ("quickwatch");
														WrongCount ();
														Paddle = HandState.cooldown;
												}
		
												if (lhhit == 2 && rhhit == 0) {
														Ambience_D.PlayOneShot (Sounds.paddlewrong);
							                            GameObject.Find ("Hands").SendMessage ("quickwatch");
														WrongCount ();
														Paddle = HandState.cooldown;
												}
										}

										if (handnumbers == 2) {
												Paddle = HandState.twohands;
						                        GameObject.Find ("Hands").SendMessage ("transitwatch");
										}
										break;

								case HandState.twohands:
										if (handnumbers == 2) {
												if (rhhit == 2 && lhhit == 2) {
														PaddleCount ();
							                            GameObject.Find ("Hands").SendMessage ("normalwatch");

														Paddle = HandState.cooldown;
												}
										}

										if (handnumbers == 1) { 
												Paddle = HandState.onehand;
										}

										break;

								case HandState.cooldown:
										cooldownTime -= Time.deltaTime;
										if (cooldownTime <= 0) {
												Paddle = HandState.none;
												cooldownTime = MaxcooldownTime1;
										}
										break;
								}
						}
				}





		//Bird

		if (Metrics.levelcount == 1 || Metrics.levelcount == 2) {

						if (!Metrics.Nar_Check) {
								switch (Bird) {
				
								case HandState.none:
										if (handnumbers == 1) {
												Bird = HandState.onehand;
										}

										if (handnumbers == 2) {
												Bird = HandState.twohands;
										}
										break;
				
								case HandState.onehand:
										if (handnumbers == 1) {
												if (rhhit == 1 && lhhit == 0) {
														BirdWrong ();
														Bird = HandState.cooldown;
												}
					
												if (lhhit == 1 && rhhit == 0) {
														BirdWrong ();
														Bird = HandState.cooldown;
												}
										}

										if (handnumbers == 2) {
												Bird = HandState.twohands;
										}
										break;

				
								case HandState.twohands:
										if (handnumbers == 2) {
												if (rhhit == 1 && lhhit == 1) {
														BirdCount ();
														Bird = HandState.cooldown;
												}
										}

										if (handnumbers == 1) { 
												Bird = HandState.onehand;
										}
										break;


								case HandState.cooldown:
										cooldownTime -= Time.deltaTime;
										if (cooldownTime <= 0) {
												Bird = HandState.none;
												cooldownTime = MaxcooldownTime;
										}
										break;
								}

						}
				}
	}
}
