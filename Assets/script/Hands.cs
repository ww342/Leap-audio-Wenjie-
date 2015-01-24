using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class Hands : MonoBehaviour {

	Controller Controller = new Controller();

	public Sounds sounds;
	public Narrator voice;

	public enum HandState { none,onehand,twohands,cooldown}
	public HandState Paddle = HandState.none;
	public HandState Bird = HandState.none;
	public HandState Bike = HandState.none;
	private float cooldownTime;
	public float MaxcooldownTime;
	public float MaxcooldownTime1;
	public int rhhit;
	public int lhhit;

	private AudioSource musicControl_A;
	private AudioSource musicControl_B;
	private AudioSource Handfree;
	private AudioSource musicControl_C;
	private AudioSource watch;
	private AudioSource transtion_hint;

	public  int stonecount;
	public  int birdcount;
	public  int paddlecount;
	public  int levelcount;
	public  int ropecount;
	public  int flowercount;
	public  int starcount;
	public  int bellcount;
	public  int wrongcount;
	public int hands;

	// Use this for initialization
	void Start () {

		Controller = new Controller ();
		cooldownTime = MaxcooldownTime;
		levelcount = -1;


		Handfree = gameObject.AddComponent <AudioSource> ();
		Handfree.clip = sounds.snore;
		Handfree.loop = true;
		Handfree.pitch = 2;
		Handfree.minDistance = 8;
		Handfree.Play ();


		musicControl_A = gameObject.AddComponent<AudioSource>();
		musicControl_A.clip = sounds.fish;
		musicControl_A.loop = true;
		musicControl_A.minDistance = 10;
		musicControl_A.Play ();


		musicControl_B = gameObject.AddComponent<AudioSource>();
		musicControl_B.clip = sounds.frog;
		musicControl_B.loop = true;
		musicControl_B.minDistance = 10;
		musicControl_B.Play ();

		musicControl_C  = gameObject.AddComponent <AudioSource> ();
		musicControl_C.clip = sounds.lakewaveslapping;
		musicControl_C.loop = true;
		musicControl_C.pitch = 1;
		musicControl_C.minDistance = 6;
		musicControl_C.Play ();

		watch  = gameObject.AddComponent <AudioSource> ();
		watch.clip  = sounds.watchticking;
		watch.loop = true;
		watch.pitch = 1;
		watch.minDistance = 20;
		watch.Play ();

		transtion_hint = gameObject.AddComponent <AudioSource> ();
		transtion_hint.clip  = sounds.transitionhint1;
		transtion_hint.pitch = 1;
		transtion_hint.minDistance = 5;
	


	}
	
    void quickwatch(){
	  watch.pitch = 3;
		}
	void transitwatch(){
		watch.pitch = 2;
	}
	void normalwatch(){
		watch.pitch = 1;
	}

	void hint1(){
		transtion_hint.clip  = sounds.transitionhint1;
		transtion_hint.Play ();
		}

	void hint2(){
		transtion_hint.clip  = sounds.transitionhint2;
		transtion_hint.Play ();
	}

	void hint3(){
		transtion_hint.clip  = sounds.transitionhint3;
		transtion_hint.Play ();
	}

	void playropepose(){
		audio.PlayOneShot (voice.ropepose);
		}

	void StoneCount(){
		stonecount ++;
		//audio.PlayOneShot (stone);
		//audio.PlayOneShot (hitfish);
		GameObject.Find ("Northdown").SendMessage ("sound1");
		GameObject.Find ("Northdown").SendMessage ("sound2");
		GameObject.Find ("Northdown").SendMessage ("sound6");


		if (stonecount == 1) {

			audio.PlayOneShot (voice.stone1);
			musicControl_A.minDistance = 7;
			
		}
		if (stonecount == 2) {

			audio.PlayOneShot (voice.stone2);
			musicControl_A.minDistance = 4;
		}
		if (stonecount == 3){
		
			audio.PlayOneShot (sounds.spash,0.5f);
			LevelCount ();
			musicControl_A.minDistance = 1;
	
			if( wrongcount<=2) {
				audio.PlayOneShot (voice.flowerpose);
				musicControl_A.clip = sounds.duck;
			musicControl_A.minDistance = 8;
			musicControl_B.loop = true;
				musicControl_A.Play ();}
			if(wrongcount>2){audio.PlayOneShot (voice.stone3);
				musicControl_A.clip = sounds.shortflapping;
				audio.PlayOneShot (sounds.birdstandonpaddle);
				musicControl_A.minDistance = 20;
				musicControl_A.loop=false;
				musicControl_A.Play ();
			}
		}
		
		
	}
	
	void RopeCount(){
				ropecount ++;
				
				if (ropecount == 1) {
			            audio.PlayOneShot (sounds.rope);
			            audio.PlayOneShot (voice.rope1);
			            musicControl_B.minDistance =0;
			            musicControl_B.clip = sounds.crickets;
		}
		
		if (ropecount == 2) {
			           audio.PlayOneShot (sounds.rope);
			           audio.PlayOneShot (voice.rope2);
			           musicControl_B.minDistance =2;
			
		}
		if (ropecount == 3) {
			            audio.PlayOneShot (voice.rope3);
			            audio.PlayOneShot (sounds.grass);
			            LevelCount ();
			musicControl_B.minDistance =5;
			musicControl_B.Play ();
			wrongcount=0;
				}
		
		}
	
	void BirdCount(){
		birdcount ++;
		audio.PlayOneShot (sounds.bird);

		if (birdcount == 1) {
			audio.PlayOneShot (voice.bird1);
			LevelCount ();
		}
		if (birdcount == 2) {
			audio.PlayOneShot (voice.bird2);
			audio.PlayOneShot (sounds.rain);
			musicControl_A.clip=sounds.wave;
			musicControl_A.loop=true;
			musicControl_A.Play ();
			LevelCount ();
			wrongcount=0;
		}
		
	}

    

	void FlowerCount(){
				flowercount ++;
		        audio.PlayOneShot (sounds.flower);

		    if (flowercount == 1) {
			audio.PlayOneShot (voice.flower1);

				}

		    if (flowercount == 4) {
			wrongcount = 0;
			musicControl_A.Stop ();
			musicControl_A.minDistance =20;
			musicControl_A.clip= sounds.wave;
			audio.PlayOneShot (sounds.landonflpping,5.0f);
			audio.PlayOneShot (sounds.birdstandonpaddle);
			audio.PlayOneShot (voice.flyfromtree);

				}

			if (flowercount == 5) {
			audio.PlayOneShot (sounds.leave);

			
				}
				if (flowercount == 7) {
			audio.PlayOneShot (sounds.leave);


				}

				if (flowercount == 10) {
			audio.PlayOneShot (sounds.leave);
			audio.PlayOneShot (sounds.wind, 10.0f);
				}
			            
			    if (flowercount ==12){
			       audio.PlayOneShot (voice.tietheboat);
				//LevelCount ();
				wrongcount=0;
			}
			            
			
				}
		



	
	void BellCount(){
		bellcount ++;
		if (bellcount == 1) {
			musicControl_B.minDistance =10;
		}
		if (bellcount == 3) {
			audio.PlayOneShot (sounds.lightning);
				
			
		}


		if (bellcount == 4) {

			
		}
		
		if (bellcount == 5) {
						
			audio.PlayOneShot (sounds.wind);
			musicControl_B.clip = sounds.grass;
			            musicControl_B .Play ();
		}

		
		if (bellcount == 6){
			audio.PlayOneShot (sounds.brake);
			audio.PlayOneShot (voice.bell6);

				LevelCount ();
		}

	}



	
	void StarCount(){
		starcount ++;
		audio.PlayOneShot (sounds.win);
		audio.PlayOneShot (sounds.shiny);
		if (starcount == 1) {
			audio.PlayOneShot (sounds.lightning);
			audio.PlayOneShot (voice.star1);
			audio.PlayOneShot (sounds.footstep);

		}
		if (starcount == 2) {
			audio.PlayOneShot (voice.star2);
			audio.PlayOneShot (sounds.footstep);

				
			
		}
		
		if (starcount == 3) {
			audio.PlayOneShot (voice.star3);
			Invoke ("Glow",2);
			Invoke ("Timetravel",5);
			Invoke ("watchstop",8);
			LevelCount();}
			
	}

	void watchstop(){

		watch.Stop ();

	}


	void Glow(){
		audio.PlayOneShot (sounds.glow);
		}

    void Timetravel(){
		audio.PlayOneShot (sounds.timetravel, 100.0f);
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


	void LevelCount(){
		levelcount ++;
		if (levelcount==5){

			audio.PlayOneShot (sounds.wind);
	
		}
		if( levelcount==6){
			audio.PlayOneShot (sounds.sky);
			musicControl_B.minDistance =10;

		}

	}

	void WrongCount(){
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
	void Update () {

				Frame frame = Controller.Frame ();
				int handnumbers = frame.Hands.Count;
  

		if (levelcount == -1) {
						if (handnumbers > 0) {
								Handfree.Stop ();
				                Handfree.loop = false;
				                //audio.PlayOneShot(begin);
				                LevelCount();
				               audio.PlayOneShot (voice.begin);
						}

				}


		if (levelcount >-1 && levelcount<6) {
			if (handnumbers<1) {
				/*
				Handfree.pitch =3;
				Handfree.minDistance =20;
				Handfree.clip=sounds.lean;
				Handfree.Play();
                */
				musicControl_C.pitch=3;
		
			}
			if (handnumbers >0) {
				musicControl_C.pitch=1;
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
		// Paddle
		if (levelcount==3){
			switch (Paddle) {

				case HandState.none:


						if (handnumbers == 1) {
				Paddle = HandState.onehand;}
			            if (handnumbers ==2){
				Paddle = HandState.twohands ;}
			    break;

		        case HandState.onehand :
			             if( handnumbers==1){

								if (rhhit == 2 && lhhit == 0) {
						audio.PlayOneShot (sounds.paddlewrong);
						                WrongCount ();
					                    Paddle = HandState.cooldown;
								}
		
								if (lhhit == 2 && rhhit == 0) {
						audio.PlayOneShot (sounds.paddlewrong);
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
						audio.PlayOneShot (sounds.paddle);
						              
				
										if (paddlecount == 1) {
							                    audio.PlayOneShot (voice.paddle1);
							                    musicControl_A.minDistance =5;
							                    audio.PlayOneShot (sounds.lakewaveslapping);
							                    musicControl_A.Play ();
							                    musicControl_B.minDistance =8;
										}
				
										if (paddlecount == 2) {
							                    audio.PlayOneShot (voice.paddle2);
							                    musicControl_B.minDistance =6;
										}
										if (paddlecount == 3) {
							                    audio.PlayOneShot (voice.paddle3);
							                    musicControl_B.minDistance =4;
							                    musicControl_C.minDistance =2;
										}
				
										if (paddlecount == 4) {
							                    audio.PlayOneShot (voice.paddle4);
							                    musicControl_A.Stop ();
							                    musicControl_B.minDistance =1;
							                    musicControl_C.minDistance =1;
							                    LevelCount ();
							                          if(wrongcount>2){
							                             Invoke ("playropepose",5);}
							                          if(wrongcount<=2){
								                         audio.PlayOneShot(voice.treepose);}
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
				         cooldownTime = MaxcooldownTime1;}
			       break;
				
				
						}
			}

		//new bird
		if (levelcount==1|| levelcount==2){
			switch (Bird) {
				
			case HandState.none:
				
				
				if (handnumbers == 1) {
					Bird = HandState.onehand;}
				if (handnumbers ==2){
					Bird = HandState.twohands ;}
				break;
				
			case HandState.onehand :
				if( handnumbers==1){
					
					if (rhhit == 1 && lhhit == 0) {
						audio.PlayOneShot (sounds.birdonboat);
						audio.PlayOneShot (sounds.shortflapping);
						audio.PlayOneShot (voice.birdwrong);
						WrongCount ();
						quickwatch ();
						hint3 ();
						Bird = HandState.cooldown ;
					}
					
					if (lhhit == 1 && rhhit == 0) {
						audio.PlayOneShot (sounds.birdonboat);
						audio.PlayOneShot (sounds.shortflapping);
						audio.PlayOneShot (voice.birdwrong);
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
						hint1 ();
						audio.PlayOneShot (sounds.grabseed);
						audio.PlayOneShot (sounds.bird);
					
						
						if (birdcount == 1) {
							audio.PlayOneShot (voice.bird1);
						}
						
						if (birdcount == 2) {
							audio.PlayOneShot (voice.bird2);
							audio.PlayOneShot (sounds.rain);

							
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
					cooldownTime = MaxcooldownTime;}
				break;
				
				
			}
		}


	}
	
	
}

