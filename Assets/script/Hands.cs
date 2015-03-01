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
	public AudioSource narrator;

	/*
	public  int stonecount;
	public  int birdcount;
	public  int paddlecount;
	public  int levelcount;
	public  int ropecount;
	public  int flowercount;
	public  int starcount;
	public  int bellcount;
	public  int wrongcount;
	*/
	public  int hands;

	// Use this for initialization
	void Start ()
	{
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
		Ambience_A.Play ();

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
		watch.minDistance = 20;
		watch.Play ();

		transtion_hint = gameObject.AddComponent <AudioSource> ();

		//transtion_hint.clip  = hint.Stone_correct_hint0;
		transtion_hint.clip = Sounds.transitionhint1;
		//transtion_hint .loop = true;
		//transtion_hint.pitch = 1;
		transtion_hint.minDistance = 2;

		narrator = GameObject.Find ("Narrator").GetComponent<AudioSource>();
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
		narrator.PlayOneShot (Narrator.ropepose);
	}

	void StoneCount ()
	{
		Metrics.stonecount ++;
		//audio.PlayOneShot (stone);
		//audio.PlayOneShot (hitfish);
		GameObject.Find ("Northdown").SendMessage ("sound1");
		GameObject.Find ("Northdown").SendMessage ("sound2");
		GameObject.Find ("Northdown").SendMessage ("sound6");

		if (Metrics.stonecount == 1) {
			narrator.PlayOneShot (Narrator.stone1);
			Ambience_A.minDistance = 7;
		}

		if (Metrics.stonecount == 2) {
			narrator.PlayOneShot (Narrator.stone2);
			Ambience_A.minDistance = 4;
		}

		if (Metrics.stonecount == 3) {
			Ambience_D.PlayOneShot (Sounds.spash, 0.5f);
			LevelCount ();
			Ambience_A.minDistance = 1;
	
			if (Metrics.wrongcount <= 2) {
				narrator.PlayOneShot (Narrator.flowerpose);
				Ambience_A.clip = Sounds.duck;
				Ambience_A.minDistance = 8;
				Ambience_B.loop = true;
				Ambience_A.Play ();
			}

			if (Metrics.wrongcount > 2) {
				narrator.PlayOneShot (Narrator.stone3);
				Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);
				Ambience_A.clip = Sounds.shortflapping;
				Ambience_A.minDistance = 20;
				Ambience_A.loop = false;
				Ambience_A.Play ();
			}
		}
	}
	
	void RopeCount ()
	{
		Metrics.ropecount ++;
				
		if (Metrics.ropecount == 1) {
			Ambience_D.PlayOneShot (Sounds.rope);
			narrator.PlayOneShot (Narrator.rope1);
			Ambience_B.minDistance = 0;
			Ambience_B.clip = Sounds.crickets;
		}
		
		if (Metrics.ropecount == 2) {
			Ambience_D.PlayOneShot (Sounds.rope);
			narrator.PlayOneShot (Narrator.rope2);
			Ambience_B.minDistance = 2;
		}

		if (Metrics.ropecount == 3) {
			narrator.PlayOneShot (Narrator.rope3);
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

		if (Metrics.birdcount == 1) {
			narrator.PlayOneShot (Narrator.bird1);
			LevelCount ();
		}
		if (Metrics.birdcount == 2) {
			narrator.PlayOneShot (Narrator.bird2);
			Ambience_D.PlayOneShot (Sounds.rain);
			Ambience_A.clip = Sounds.wave;
			Ambience_A.loop = true;
			Ambience_A.Play ();
			LevelCount ();
			Metrics.wrongcount = 0;
		}
	}

	void FlowerCount ()
	{
		Metrics.flowercount ++;
		        
		if (Metrics.flowercount == 1) {
			narrator.PlayOneShot (Narrator.flower1);
		}

		if (Metrics.flowercount == 4) {
			Metrics.wrongcount = 0;
			Ambience_A.Stop ();
			Ambience_A.minDistance = 20;
			Ambience_A.clip = Sounds.wave;
			Ambience_D.PlayOneShot (Sounds.landonflpping, 5.0f);
			Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);
			narrator.PlayOneShot (Narrator.flyfromtree);
		}

		if (Metrics.flowercount == 5) {
			audio.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 7) {
			Ambience_D.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 10) {
			Ambience_D.PlayOneShot (Sounds.leave);
			Ambience_D.PlayOneShot (Sounds.wind, 10.0f);
		}
			            
		if (Metrics.flowercount == 12) {
			narrator.PlayOneShot (Narrator.tietheboat);
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
			narrator.PlayOneShot (Narrator.bell6);

			LevelCount ();
		}
	}
	
	void StarCount ()
	{
		Metrics.starcount ++;

		if (Metrics.starcount == 1) {
			Ambience_D.PlayOneShot (Sounds.lightning);
			narrator.PlayOneShot (Narrator.star1);
			Ambience_D.PlayOneShot (Sounds.footstep);
		}
		if (Metrics.starcount == 2) {
			narrator.PlayOneShot (Narrator.star2);
			Ambience_D.PlayOneShot (Sounds.footstep);
		}
		
		if (Metrics.starcount == 3) {
			narrator.PlayOneShot (Narrator.star3);
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
		Ambience_D.PlayOneShot (Sounds.glow);
	}

	void Timetravel ()
	{
		Ambience_D.PlayOneShot (Sounds.timetravel);
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
				//audio.PlayOneShot(begin);
				LevelCount ();
				narrator.PlayOneShot (Narrator.begin);
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

		// Paddle
		if (Metrics.levelcount == 3) {
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
						WrongCount ();
						Paddle = HandState.cooldown;
					}
		
					if (lhhit == 2 && rhhit == 0) {
						Ambience_D.PlayOneShot (Sounds.paddlewrong);
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
						Metrics.paddlecount ++;
						Ambience_D.PlayOneShot (Sounds.paddle);
				
						if (Metrics.paddlecount == 1) {
							narrator.PlayOneShot (Narrator.paddle1);
							Ambience_A.minDistance = 5;
							Ambience_D.PlayOneShot (Sounds.lakewaveslapping);
							Ambience_A.Play ();
							Ambience_B.minDistance = 8;
						}
						if (Metrics.paddlecount == 2) {
							narrator.PlayOneShot (Narrator.paddle2);
							Ambience_B.minDistance = 6;
						}
						if (Metrics.paddlecount == 3) {
							narrator.PlayOneShot (Narrator.paddle3);
							Ambience_B.minDistance = 4;
							Ambience_C.minDistance = 2;
						}
						if (Metrics.paddlecount == 4) {
							narrator.PlayOneShot (Narrator.paddle4);
							Ambience_A.Stop ();
							Ambience_B.minDistance = 1;
							Ambience_C.minDistance = 1;
							LevelCount ();
							if (Metrics.wrongcount > 2) {
								Invoke ("playropepose", 5);
							}
							if (Metrics.wrongcount <= 2) {
								narrator.PlayOneShot (Narrator.treepose);
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
		if (Metrics.levelcount == 1 || Metrics.levelcount == 2) {
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
						Ambience_D.PlayOneShot (Sounds.birdonboat);
						Ambience_D.PlayOneShot (Sounds.shortflapping);
						narrator.PlayOneShot (Narrator.birdwrong);
						WrongCount ();
						quickwatch ();
						hint3 ();
						Bird = HandState.cooldown;
					}
					
					if (lhhit == 1 && rhhit == 0) {
						Ambience_D.PlayOneShot (Sounds.birdonboat);
						Ambience_D.PlayOneShot (Sounds.shortflapping);
						narrator.PlayOneShot (Narrator.birdwrong);
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
						Metrics.birdcount ++;
						LevelCount ();
						normalwatch ();
						hint3 ();
						Ambience_D.PlayOneShot (Sounds.grabseed);
						Ambience_D.PlayOneShot (Sounds.bird);

						if (Metrics.birdcount == 1) {
							narrator.PlayOneShot (Narrator.bird1);
						}
						
						if (Metrics.birdcount == 2) {
							narrator.PlayOneShot (Narrator.bird2);
							Ambience_D.PlayOneShot (Sounds.rain);
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
