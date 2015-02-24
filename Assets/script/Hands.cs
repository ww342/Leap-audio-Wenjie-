using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof(AudioSource))]
public class Hands : MonoBehaviour
{
	Controller Controller = new Controller ();
	public Sounds sounds;
	public Narrator voice;
	public hint hint;

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
	public Vector3 watchposition;
	public Vector3 tapwatch;
	public Vector3 difference;
	public float tappingdistance;

	//
	private AudioSource Ambience_A;
	private AudioSource Ambience_B;
	private AudioSource Ambience_C;
	private AudioSource Ambience_D;
	private AudioSource Handfree;
	private AudioSource watch;
	private AudioSource transtion_hint;
	public AudioSource Narrator;
	public  int stonecount;
	public  int birdcount;
	public  int paddlecount;
	public  int levelcount;
	public  int ropecount;
	public  int flowercount;
	public  int starcount;
	public  int bellcount;
	public  int wrongcount;
	public  int hands;

	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
		cooldownTime = MaxcooldownTime;
		levelcount = -1;

		Handfree = gameObject.AddComponent<AudioSource> ();
		Handfree.clip = sounds.snore;
		Handfree.loop = true;
		Handfree.pitch = 2;
		Handfree.minDistance = 8;
		Handfree.Play ();

		Ambience_A = gameObject.AddComponent<AudioSource> ();
		Ambience_A.clip = sounds.fish;
		Ambience_A.loop = true;
		Ambience_A.minDistance = 10;
		Ambience_A.Play ();

		Ambience_B = gameObject.AddComponent<AudioSource> ();
		Ambience_B.clip = sounds.frog;
		Ambience_B.loop = true;
		Ambience_B.minDistance = 10;
		Ambience_B.Play ();

		Ambience_C = gameObject.AddComponent <AudioSource> ();
		Ambience_C.clip = sounds.lakewaveslapping;
		Ambience_C.loop = true;
		Ambience_C.pitch = 1;
		Ambience_C.minDistance = 6;
		Ambience_C.Play ();

		Ambience_D = gameObject.AddComponent<AudioSource> ();
		Ambience_D.minDistance = 10;

		watch = gameObject.AddComponent <AudioSource> ();
		watch.clip = sounds.slowwatch;
		watch.loop = true;
		watch.minDistance = 20;
		watch.Play ();

		transtion_hint = gameObject.AddComponent <AudioSource> ();

		//transtion_hint.clip  = hint.Stone_correct_hint0;
		transtion_hint.clip = sounds.transitionhint1;
		//transtion_hint .loop = true;
		//transtion_hint.pitch = 1;
		transtion_hint.minDistance = 2;

		Narrator = gameObject.AddComponent <AudioSource> ();
		Narrator.minDistance = 10;
	}
	
	void quickwatch ()
	{
		watch.clip = sounds.quickwatch;
		watch.Play ();
	}

	void transitwatch ()
	{
		watch.clip = sounds.middlewatch;
		watch.Play ();
	}

	void normalwatch ()
	{
		watch.clip = sounds.slowwatch;
		watch.Play ();
	}

	void hint1 ()
	{
		transtion_hint.clip = sounds.transitionhint1;
		transtion_hint.Play ();
	}

	void hint2 ()
	{
		transtion_hint.clip = sounds.transitionhint2;
		transtion_hint.Play ();
	}

	void hint3 ()
	{
		transtion_hint.clip = sounds.transitionhint3;
		transtion_hint.Play ();
	}

	void playropepose ()
	{
		audio.PlayOneShot (voice.ropepose);
	}

	void StoneCount ()
	{
		stonecount ++;
		//audio.PlayOneShot (stone);
		//audio.PlayOneShot (hitfish);
		GameObject.Find ("Northdown").SendMessage ("sound1");
		GameObject.Find ("Northdown").SendMessage ("sound2");
		GameObject.Find ("Northdown").SendMessage ("sound6");

		if (stonecount == 1) {
			Narrator.PlayOneShot (voice.stone1);
			Ambience_A.minDistance = 7;
		}

		if (stonecount == 2) {
			Narrator.PlayOneShot (voice.stone2);
			Ambience_A.minDistance = 4;
		}

		if (stonecount == 3) {
			Ambience_D.PlayOneShot (sounds.spash, 0.5f);
			LevelCount ();
			Ambience_A.minDistance = 1;
	
			if (wrongcount <= 2) {
				Narrator.PlayOneShot (voice.flowerpose);
				Ambience_A.clip = sounds.duck;
				Ambience_A.minDistance = 8;
				Ambience_B.loop = true;
				Ambience_A.Play ();
			}

			if (wrongcount > 2) {
				Narrator.PlayOneShot (voice.stone3);
				Ambience_D.PlayOneShot (sounds.birdstandonpaddle);
				Ambience_A.clip = sounds.shortflapping;
				Ambience_A.minDistance = 20;
				Ambience_A.loop = false;
				Ambience_A.Play ();
			}
		}
	}
	
	void RopeCount ()
	{
		ropecount ++;
				
		if (ropecount == 1) {
			Ambience_D.PlayOneShot (sounds.rope);
			Narrator.PlayOneShot (voice.rope1);
			Ambience_B.minDistance = 0;
			Ambience_B.clip = sounds.crickets;
		}
		
		if (ropecount == 2) {
			Ambience_D.PlayOneShot (sounds.rope);
			Narrator.PlayOneShot (voice.rope2);
			Ambience_B.minDistance = 2;
		}

		if (ropecount == 3) {
			Narrator.PlayOneShot (voice.rope3);
			Narrator.PlayOneShot (sounds.grass);
			Ambience_B.minDistance = 5;
			Ambience_B.Play ();
			LevelCount ();
			wrongcount = 0;
		}
	}
	
	void BirdCount ()
	{
		birdcount ++;
		Ambience_D.PlayOneShot (sounds.bird);

		if (birdcount == 1) {
			Narrator.PlayOneShot (voice.bird1);
			LevelCount ();
		}
		if (birdcount == 2) {
			Narrator.PlayOneShot (voice.bird2);
			Ambience_D.PlayOneShot (sounds.rain);
			Ambience_A.clip = sounds.wave;
			Ambience_A.loop = true;
			Ambience_A.Play ();
			LevelCount ();
			wrongcount = 0;
		}
	}

	void FlowerCount ()
	{
		flowercount ++;
		        
		if (flowercount == 1) {
			Narrator.PlayOneShot (voice.flower1);
		}

		if (flowercount == 4) {
			wrongcount = 0;
			Ambience_A.Stop ();
			Ambience_A.minDistance = 20;
			Ambience_A.clip = sounds.wave;
			Ambience_D.PlayOneShot (sounds.landonflpping, 5.0f);
			Ambience_D.PlayOneShot (sounds.birdstandonpaddle);
			Narrator.PlayOneShot (voice.flyfromtree);
		}

		if (flowercount == 5) {
			audio.PlayOneShot (sounds.leave);
		}

		if (flowercount == 7) {
			Ambience_D.PlayOneShot (sounds.leave);
		}

		if (flowercount == 10) {
			Ambience_D.PlayOneShot (sounds.leave);
			Ambience_D.PlayOneShot (sounds.wind, 10.0f);
		}
			            
		if (flowercount == 12) {
			Narrator.PlayOneShot (voice.tietheboat);
			//LevelCount ();
			wrongcount = 0;
		}
	}
	
	void BellCount ()
	{
		bellcount ++;
		if (bellcount == 1) {
			Ambience_B.minDistance = 10;
		}
		if (bellcount == 3) {
			Ambience_D.PlayOneShot (sounds.lightning);
		}

		if (bellcount == 4) {
		}
		
		if (bellcount == 5) {
			Ambience_D.PlayOneShot (sounds.wind);
			Ambience_B.clip = sounds.grass;
			Ambience_B .Play ();
		}

		if (bellcount == 6) {
			Ambience_D.PlayOneShot (sounds.brake);
			Narrator.PlayOneShot (voice.bell6);

			LevelCount ();
		}
	}
	
	void StarCount ()
	{
		starcount ++;

		if (starcount == 1) {
			Ambience_D.PlayOneShot (sounds.lightning);
			Narrator.PlayOneShot (voice.star1);
			Ambience_D.PlayOneShot (sounds.footstep);
		}
		if (starcount == 2) {
			Narrator.PlayOneShot (voice.star2);
			Ambience_D.PlayOneShot (sounds.footstep);
		}
		
		if (starcount == 3) {
			Narrator.PlayOneShot (voice.star3);
			Invoke ("Glow", 2);
			Invoke ("Timetravel", 5);
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
		Ambience_D.PlayOneShot (sounds.glow);
	}

	void Timetravel ()
	{
		Ambience_D.PlayOneShot (sounds.timetravel);
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
		levelcount ++;
		if (levelcount == 5) {
			Ambience_D.PlayOneShot (sounds.wind);
		}
		if (levelcount == 6) {
			Ambience_D.PlayOneShot (sounds.sky);
			Ambience_B.minDistance = 10;
		}
	}

	void WrongCount ()
	{
		wrongcount ++;
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

		if (levelcount == -1) {
			if (handnumbers > 0) {
				Handfree.Stop ();
				Handfree.loop = false;
				//audio.PlayOneShot(begin);
				LevelCount ();
				Narrator.PlayOneShot (voice.begin);
			}
		}

		if (levelcount > -1 && levelcount < 6) {
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

		// Paddle
		if (levelcount == 3) {
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
						Ambience_D.PlayOneShot (sounds.paddlewrong);
						WrongCount ();
						Paddle = HandState.cooldown;
					}
		
					if (lhhit == 2 && rhhit == 0) {
						Ambience_D.PlayOneShot (sounds.paddlewrong);
						WrongCount ();
						Paddle = HandState.cooldown;
					}
				}

				if (handnumbers == 2) {
					Paddle = HandState.twohands;
				}
				break;

			case HandState.twohands:
				if (handnumbers == 2) {
					if (rhhit == 2 && lhhit == 2) {
						paddlecount ++;
						Ambience_D.PlayOneShot (sounds.paddle);
				
						if (paddlecount == 1) {
							Narrator.PlayOneShot (voice.paddle1);
							Ambience_A.minDistance = 5;
							Ambience_D.PlayOneShot (sounds.lakewaveslapping);
							Ambience_A.Play ();
							Ambience_B.minDistance = 8;
						}
						if (paddlecount == 2) {
							Narrator.PlayOneShot (voice.paddle2);
							Ambience_B.minDistance = 6;
						}
						if (paddlecount == 3) {
							Narrator.PlayOneShot (voice.paddle3);
							Ambience_B.minDistance = 4;
							Ambience_C.minDistance = 2;
						}
						if (paddlecount == 4) {
							Narrator.PlayOneShot (voice.paddle4);
							Ambience_A.Stop ();
							Ambience_B.minDistance = 1;
							Ambience_C.minDistance = 1;
							LevelCount ();
							if (wrongcount > 2) {
								Invoke ("playropepose", 5);
							}
							if (wrongcount <= 2) {
								Narrator.PlayOneShot (voice.treepose);
							}
						}

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

		//new bird
		if (levelcount == 1 || levelcount == 2) {
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
						Ambience_D.PlayOneShot (sounds.birdonboat);
						Ambience_D.PlayOneShot (sounds.shortflapping);
						Narrator.PlayOneShot (voice.birdwrong);
						WrongCount ();
						quickwatch ();
						hint3 ();
						Bird = HandState.cooldown;
					}
					
					if (lhhit == 1 && rhhit == 0) {
						Ambience_D.PlayOneShot (sounds.birdonboat);
						Ambience_D.PlayOneShot (sounds.shortflapping);
						Narrator.PlayOneShot (voice.birdwrong);
						WrongCount ();
						quickwatch ();
						hint3 ();
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
						birdcount ++;
						LevelCount ();
						normalwatch ();
						hint3 ();
						Ambience_D.PlayOneShot (sounds.grabseed);
						Ambience_D.PlayOneShot (sounds.bird);

						if (birdcount == 1) {
							Narrator.PlayOneShot (voice.bird1);
						}
						
						if (birdcount == 2) {
							Narrator.PlayOneShot (voice.bird2);
							Ambience_D.PlayOneShot (sounds.rain);
						}
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
