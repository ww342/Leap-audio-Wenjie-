using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Narrator : MonoBehaviour
{

	// Gesture Intro
	public AudioClip StoneIntro;
	public AudioClip FlowerIntro;
	public AudioClip BirdIntro_afterStone;
	public AudioClip BirdIntro_afterFlower;
	public AudioClip PaddleIntro;
	public AudioClip TreeIntro;
	public AudioClip RopeIntro_afterPaddle;
	public AudioClip RopeIntro_afterTree;
	public AudioClip BikeIntro;
	public AudioClip StarIntro;

	// Gesture Instruction

	public AudioClip StoneGesture;
	public AudioClip FlowerGesture;
	public AudioClip BirdGesture;
	public AudioClip PaddleGesture;
	public AudioClip TreeGesture;
	public AudioClip RopeGesture;
	public AudioClip BikeGesture;
	public AudioClip StarGesture;

	//During Gesture Feedback in Game Version 1

	public AudioClip Stone_grabsides_v1;
	public AudioClip Stone_faraway_v1;
	public AudioClip Stone_tinythrow_v1;
	public AudioClip Stone_armlift_v1;
	public AudioClip Stone_sidethrow_v1;
	public AudioClip Flower_pinch_v1;
	public AudioClip Bird_palmstooclose_v1;
	public AudioClip Bird_onehandcup_v1;
	public AudioClip Bird_onehandgrab_v1;
	public AudioClip Bird_toomuchforce_v1;
	public AudioClip Paddle_palmsfarapart_v1;
	public AudioClip Paddle_onehand_v1;
	public AudioClip Rope_updownbind_v1;
	public AudioClip Bike_leftbrake_v1;
	public AudioClip Bike_onehandbell_v1;
	public AudioClip Star_pickup_v1;




	//Correct Response in Game Version1

	public AudioClip Stone_Correct_response_01_v1;
	public AudioClip Stone_Correct_response_02_v1;
	public AudioClip Stone_Correct_response_03_v1;
	public AudioClip Flower_Correct_response_01_v1;
	public AudioClip Bird_Correct_response_01_v1;
	public AudioClip Bird_Correct_response_02_v1;
	public AudioClip Paddle_Correct_response_01_v1;
	public AudioClip Paddle_Correct_response_04_v1;
	public AudioClip Tree_Correct_response_02_v1;
	public AudioClip Rope_Correct_response_01_v1;
	public AudioClip Rope_Correct_response_02_v1;
	public AudioClip Rope_Correct_response_03_v1;
	public AudioClip Star_Correct_response_01_v1;
	public AudioClip Star_Correct_response_02_v1;
	public AudioClip Star_Correct_response_03_v1;


	//During Gesture Feedback in Game Version 2

	public AudioClip Stone_grabsides_v2;
	public AudioClip Stone_faraway_v2;
	public AudioClip Stone_tinythrow_v2;
	public AudioClip Stone_armlift_v2;
	public AudioClip Stone_sidethrow_v2;
	public AudioClip Flower_pinch_v2;
	public AudioClip Bird_palmstooclose_v2;
	public AudioClip Bird_onehandcup_v2;
	public AudioClip Bird_onehandgrab_v2;
	public AudioClip Bird_toomuchforce_v2;
	public AudioClip Paddle_palmsfarapart_v2;
	public AudioClip Paddle_onehand_01_v2;
	public AudioClip Paddle_onehand_02_v2;
	public AudioClip Paddle_longstop_v2;
	public AudioClip Rope_updownbind_v2;
	public AudioClip Bike_leftbrake_v2;
	public AudioClip Bike_onehandbell_v2;
	public AudioClip Bike_onehandle_v2;
	public AudioClip Bike_falldown_v2;
	public AudioClip Star_pickup_v2;
	public AudioClip Star_flipover_v2;


	//Correct Response in Game Version2
	
	public AudioClip Stone_Correct_response_01_v2;
	public AudioClip Stone_Correct_response_02_v2;
	public AudioClip Stone_Correct_response_03_v2;
	public AudioClip Flower_Correct_response_01_v2;
	public AudioClip Flower_Correct_response_02_v2;
	public AudioClip Flower_Correct_response_03_v2;
	public AudioClip Bird_Correct_response_01_v2;
	public AudioClip Bird_Correct_response_02_v2;
	public AudioClip Paddle_Correct_response_01_v2;
	public AudioClip Paddle_Correct_response_02_v2;
	public AudioClip Paddle_Correct_response_03_v2;
	public AudioClip Tree_Correct_response_01_v2;
	public AudioClip Tree_Correct_response_02_v2;
	public AudioClip Rope_Correct_response_01_v2;
	public AudioClip Rope_Correct_response_02_v2;
	public AudioClip Rope_Correct_response_03_v2;
	public AudioClip Bike_Correct_response_01_v2;
	public AudioClip Star_Correct_response_01_v2;
	public AudioClip Star_Correct_response_02_v2;
	public AudioClip Star_Correct_response_03_v2;



//	
	private AudioSource audiosource;
	
	void Awake ()
	{
		this.audiosource = GetComponent<AudioSource>();
	}

	public WaitForSeconds PlayAndWait(AudioClip clip) {
		this.audiosource.clip = clip;
		this.audiosource.Play();
		return new WaitForSeconds(clip.length);
	}

	public void PlayIfPossible(AudioClip clip)
	{
		if (! this.audiosource.isPlaying) {
			this.audiosource.clip = clip;
			this.audiosource.Play();
		}
	}

	public void PlayImmediately(AudioClip clip)
	{
		if (this.audiosource.isPlaying) {
			if (this.audiosource.clip == clip) {
				return;
			}
			this.audiosource.Stop();
		}
		this.audiosource.clip = clip;
		this.audiosource.Play();
	}
}
