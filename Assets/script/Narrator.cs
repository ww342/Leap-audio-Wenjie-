﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Narrator : MonoBehaviour
{
	/*
	public AudioClip begin;
	public AudioClip flowerpose;
	public AudioClip flower1;
	public AudioClip flyfromtree;
	public AudioClip stone1;
	public AudioClip stone2;
	public AudioClip stone3;
	public AudioClip bird1;
	public AudioClip bird2;
	public AudioClip paddle1;
	public AudioClip paddle2;
	public AudioClip paddle3;
	public AudioClip paddle4;
	public AudioClip treepose;
	public AudioClip tietheboat;
	public AudioClip ropepose;
	public AudioClip rope1;
	public AudioClip rope2;
	public AudioClip rope3;
	public AudioClip bell6;
	public AudioClip star1;
	public AudioClip star2;
	public AudioClip star3;
	public AudioClip starwrong;
	public AudioClip stonewrong;
	public AudioClip birdwrong;
*/

	// Game Story Narration
	public AudioClip StoneIntro;
	public AudioClip Stone_Correct_response_01;
	public AudioClip Stone_Correct_response_001;
	public AudioClip Stone_Correct_response_02;
	public AudioClip Stone_Correct_response_002;
	public AudioClip Stone_Correct_response_03;
	public AudioClip Stone_Correct_response_003;
	public AudioClip Stone_grabbingwater_response;
	public AudioClip Stone_hittingleaves_response;
	public AudioClip Stone_liftarm_response;
	public AudioClip Stone_skipstone_response;
	public AudioClip Stone_tinythrow_response;
	public AudioClip Stone_wrongdirection_response;
	public AudioClip Stone_wrong_response;

	public AudioClip FlowerIntro;
	public AudioClip Flower_Correct_response_01;
	public AudioClip Flower_Correct_response_002;
	public AudioClip Flower_Correct_response_003;
	public AudioClip Flower_flipover_response_01;
	public AudioClip Flower_pinch_response;

	public AudioClip BirdIntro_afterStone;
	public AudioClip BirdIntro_afterFlower;
	public AudioClip Bird_Correct_response_01;
	public AudioClip Bird_Correct_response_02;
	public AudioClip Bird_cannotbreath_response;
	public AudioClip Bird_onehandgrab_response;
	public AudioClip Bird_walkingaway_response;

	
	public AudioClip Paddle_Correct_response_01;
	public AudioClip Paddle_Correct_response_001;
	public AudioClip Paddle_Correct_response_02;
	public AudioClip Paddle_Correct_response_002;
	public AudioClip Paddle_Correct_response_03;
	public AudioClip Paddle_onepaddle_response;
	public AudioClip Paddle_droppaddles_response;
	public AudioClip Paddle_longstop_response;
	public AudioClip Paddle_siderowing_response;


	public AudioClip TreeIntro;
	public AudioClip Tree_Correct_response_001;
	public AudioClip Tree_Correct_response_002;


	public AudioClip RopeIntro_afterTree;
	public AudioClip RopeIntro_afterPaddle;
	public AudioClip Rope_Correct_response_01;
	public AudioClip Rope_Correct_response_001;
	public AudioClip Rope_Correct_response_02;
	public AudioClip Rope_Correct_response_03;
	public AudioClip Rope_Correct_response_003;
	public AudioClip Rope_updownbind_response_01;
	public AudioClip Rope_updownbind_response_02;

	public AudioClip Bike_Intro;
	public AudioClip Bike_onehand_response;
	public AudioClip Bike_brake_onehand_response;
	public AudioClip Bike_ringbell_onehand_response;
	public AudioClip Bike_ringbell_twohands_response;
	public AudioClip Bike_getthebrake_response;
	public AudioClip Bike_fall_response;

	public AudioClip StarIntro;
	public AudioClip Star_Correct_response_01;
	public AudioClip Star_Correct_response_02;
	public AudioClip Star_Correct_response_03;
	public AudioClip Star_grab_response_01;
	public AudioClip Star_flipover_response_01;
	public AudioClip Star_flipover_response_02;
	public AudioClip Star_openhand_response_01;

	
	// Gesture Vocal Instruction
	public AudioClip StoneGesture;
	public AudioClip Stone_hint_01;
	public AudioClip FlowerGesture;
	public AudioClip Flower_hint_01;
	public AudioClip BirdGesture;
	public AudioClip Bird_hint_01;
	public AudioClip PaddleGesture;
	public AudioClip Paddle_hint_01;
	public AudioClip TreeGesture;
	public AudioClip Tree_hint_01;
	public AudioClip RopeGesture;
	public AudioClip Rope_hint_01;
	public AudioClip BikeGesture;
	public AudioClip Bike_hint_01;
	public AudioClip StarGesture;
	public AudioClip Star_hint_01;
	
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
