using UnityEngine;
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
	public AudioClip Stone_Correct_response_02;
	public AudioClip Stone_Correct_response_03;

	public AudioClip FlowerIntro;
	public AudioClip Flower_Correct_response_01;

	public AudioClip BirdIntro_afterStone;
	public AudioClip BirdIntro_afterFlower;
	public AudioClip Bird_Correct_response_01;
	public AudioClip Bird_Correct_response_02;
	public AudioClip Bird_Correct_response_03;

	public AudioClip PaddleIntro;
	public AudioClip Paddle_Correct_response_01;
	public AudioClip Paddle_Correct_response_02;
	public AudioClip Paddle_Correct_response_03;

	public AudioClip TreeIntro;
	public AudioClip Tree_Correct_response_01;
	public AudioClip Tree_Correct_response_02;
	public AudioClip Tree_Correct_response_03;

	public AudioClip RopeIntro_afterTree;
	public AudioClip RopeIntro_afterPaddle;
	public AudioClip Rope_Correct_response_01;
	public AudioClip Rope_Correct_response_02;
	public AudioClip Rope_Correct_response_03;

	public AudioClip BikeIntro;
	public AudioClip Bike_Correct_response_01;
	public AudioClip Bike_Correct_response_02;
	public AudioClip Bike_Correct_response_03;

	public AudioClip StarIntro;
	public AudioClip Star_Correct_response_01;
	public AudioClip Star_Correct_response_02;
	public AudioClip Star_Correct_response_03;
	
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
		this.audiosource.PlayOneShot (clip);
		return new WaitForSeconds(clip.length);
	}
}
