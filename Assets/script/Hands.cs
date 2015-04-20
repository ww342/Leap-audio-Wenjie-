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
	
	public enum HandState // TODO: how is this different from GestureState?
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
	public int rhhit = 0;
	public int lhhit = 0;

	//SOS GESTURE VARIABLES
/*
	public Vector3 watchposition;
	public Vector3 tapwatch;
	public Vector3 difference;
	public float tappingdistance;
*/

	public void Start ()
	{

		cooldownTime = MaxcooldownTime;


	}
	
	public void playropepose ()
	{
		//yield return Narrator.PlayAndWait(Narrator.ropepose);
	}


	public void PaddleCount ()
	{

		Metrics.paddlecount ++;
		Sounds.Ambience_D.PlayOneShot (Sounds.paddle);
		
		if (Metrics.paddlecount == 1) {
			//yield return Narrator.PlayAndWait(Narrator.paddle1);
			Sounds.Ambience_A.minDistance = 5;
			Sounds.Ambience_D.PlayOneShot (Sounds.lakewaveslapping);
			Sounds.Ambience_A.Play ();
			Sounds.Ambience_B.minDistance = 8;
		}
		if (Metrics.paddlecount == 2) {
			
			Sounds.Ambience_B.minDistance = 6;
		}
		if (Metrics.paddlecount == 3) {
			
			Sounds.Ambience_B.minDistance = 4;
			Sounds.Ambience_C.minDistance = 2;
		}
		if (Metrics.paddlecount == 4) {
			//yield return Narrator.PlayAndWait(Narrator.paddle4);

			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_B.minDistance = 1;
			Sounds.Ambience_C.minDistance = 1;
			LevelCount ();
			if (Metrics.wrongcount > 2) {
				Invoke ("playropepose", 5);
			}
			if (Metrics.wrongcount <= 2) {
				//yield return Narrator.PlayAndWait(Narrator.treepose);
			}
		}
	}
		
	public void RopeCount ()
	{
		Metrics.ropecount ++;
			
		if (Metrics.ropecount == 1) {
				
			//yield return Narrator.PlayAndWait(Narrator.rope1);

			Sounds.Ambience_D.PlayOneShot (Sounds.rope);
			Sounds.Ambience_B.minDistance = 0;
			Sounds.Ambience_B.clip = Sounds.crickets;
		}
		
		if (Metrics.ropecount == 2) {

			Sounds.Ambience_D.PlayOneShot (Sounds.rope);
			//yield return Narrator.PlayAndWait(Narrator.rope2);

			Sounds.Ambience_B.minDistance = 2;
		}

		if (Metrics.ropecount == 3) {

			//yield return Narrator.PlayAndWait(Narrator.rope3);


			Sounds.Ambience_D.PlayOneShot (Sounds.grass);
			Sounds.Ambience_B.minDistance = 5;
			Sounds.Ambience_B.Play ();
			LevelCount ();
			Metrics.wrongcount = 0;
		}
	}
	
	public void BirdCount ()
	{
		Metrics.birdcount ++;
		Sounds.Ambience_D.PlayOneShot (Sounds.bird);
		LevelCount ();
		Sounds.normalwatch ();
		Sounds.hint3 ();
		Sounds.Ambience_D.PlayOneShot (Sounds.grabseed);

		
		if (Metrics.birdcount == 1) {
			//yield return Narrator.PlayAndWait(Narrator.bird1);

		}
		if (Metrics.birdcount == 2) {
			//yield return Narrator.PlayAndWait(Narrator.bird2);

			Sounds.Ambience_D.PlayOneShot (Sounds.rain);
			Sounds.Ambience_A.clip = Sounds.wave;
			Sounds.Ambience_A.loop = true;
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.Play ();
			Metrics.wrongcount = 0;
		}
	}

	public void BirdWrong ()
	{

		Sounds.Ambience_D.PlayOneShot (Sounds.birdonboat);
		Sounds.Ambience_D.PlayOneShot (Sounds.shortflapping);
		//yield return Narrator.PlayAndWait(Narrator.birdwrong);
		Metrics.wrongcount++;
		Sounds.quickenwatch ();
		Sounds.hint3 ();

	}

	public void FlowerCount ()
	{
		Metrics.flowercount ++;
		Sounds.audiosource.PlayOneShot (Sounds.flower);
		        
		if (Metrics.flowercount == 1) {
			//yield return Narrator.PlayAndWait(Narrator.flower1);
		}

		if (Metrics.flowercount == 4) {
			Metrics.wrongcount = 0;
			Sounds.Ambience_A.Stop ();
			Sounds.Ambience_A.minDistance = 8;
			Sounds.Ambience_A.clip = Sounds.wave;
			Sounds.Ambience_D.PlayOneShot (Sounds.landonflpping, 5.0f);
			Sounds.Ambience_D.PlayOneShot (Sounds.birdstandonpaddle);

			//yield return Narrator.PlayAndWait(Narrator.flyfromtree);
		}

		if (Metrics.flowercount == 5) {
			Sounds.audiosource.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 7) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
		}

		if (Metrics.flowercount == 10) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
			Sounds.Ambience_D.PlayOneShot (Sounds.wind, 5.0f);
		}
			            
		if (Metrics.flowercount == 12) {
			//yield return Narrator.PlayAndWait(Narrator.tietheboat);
			//LevelCount ();
			Metrics.wrongcount = 0;
		}
	}
	
	public void BellCount ()
	{
		Metrics.bellcount ++;

		if (Metrics.bellcount == 1) {
			Sounds.Ambience_B.minDistance = 10;
		}
		if (Metrics.bellcount == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.lightning);
		}

		if (Metrics.bellcount == 4) {
		}
		
		if (Metrics.bellcount == 5) {

			Sounds.Ambience_D.PlayOneShot (Sounds.wind);
			Sounds.Ambience_B.clip = Sounds.grass;
			Sounds.Ambience_B .Play ();
		}

		if (Metrics.bellcount == 6) {

			Sounds.Ambience_D.PlayOneShot (Sounds.brake);
			//yield return Narrator.PlayAndWait(Narrator.bell6);

			LevelCount ();
		}
	}
	
	public void StarCount ()
	{
		Metrics.starcount ++;

		if (Metrics.starcount == 1) {

			//yield return Narrator.PlayAndWait(Narrator.star1);

			Sounds.Ambience_D.PlayOneShot (Sounds.lightning);
			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}


		if (Metrics.starcount == 2) {

			//yield return Narrator.PlayAndWait(Narrator.star2);

			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}

		
		if (Metrics.starcount == 3) {
			//yield return Narrator.PlayAndWait(Narrator.star3);

			Invoke ("Glow", 2);
			Invoke ("Timetravel", 6);
			Invoke ("watchstop", 8);
			LevelCount ();
		}
	}

	public void LHhit (int statenumber)
	{
		lhhit = statenumber;
	}

	public void RHhit (int statenumber)
	{
		rhhit = statenumber;
	}

	// A void that gets wrist position from the right hand

	/*
	public void WatchSurface(Vector3 wristposition)
	{
		watchposition = wristposition;
		Update ();
	}

	// A void that gets index position from the left hand
	public void TaptheWatch(Vector3 indextip)
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

	public void LevelCount () // TODO: main game logic here, needs to move into GameLogic!
	{
		Metrics.levelcount ++;
		if (Metrics.levelcount == 5) {
			Sounds.Ambience_D.PlayOneShot (Sounds.wind);
		}
		if (Metrics.levelcount == 6) {
			Sounds.Ambience_D.PlayOneShot (Sounds.sky);
			Sounds.Ambience_B.minDistance = 10;
		}
	}

	// Update is called once per frame
	public void Update ()
	{
		Frame frame = Controller.Frame ();
		int handnumbers = frame.Hands.Count;

		if (Metrics.levelcount > -1 && Metrics.levelcount < 6) {
			if (handnumbers < 1) {
				Sounds.Ambience_C.pitch = 3;
			}
			if (handnumbers > 0) {
				Sounds.Ambience_C.pitch = 1;
			}
		}

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

		if (Metrics.levelcount > 2) {
		// Paddle
		//if (Metrics.levelcount == 3) {
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
							Sounds.Ambience_D.PlayOneShot (Sounds.paddlewrong);
							Sounds.quickenwatch();
							Metrics.wrongcount++;
							Paddle = HandState.cooldown;
						}
		
						if (lhhit == 2 && rhhit == 0) {
							Sounds.Ambience_D.PlayOneShot (Sounds.paddlewrong);
							Sounds.quickenwatch();
 					Metrics.wrongcount++;
							Paddle = HandState.cooldown;
						}
					}

					if (handnumbers == 2) {
						Paddle = HandState.twohands;
						Sounds.transitwatch();
					}
					break;

				case HandState.twohands:
					if (handnumbers == 2) {
						if (rhhit == 2 && lhhit == 2) {
							PaddleCount ();
							Sounds.normalwatch();

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

		//Bird
		//if (Metrics.levelcount == 1 || Metrics.levelcount == 2) {

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
