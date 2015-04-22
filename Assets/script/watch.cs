using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof(AudioSource))]
public class watch : MonoBehaviour
{
	Controller Controller = new Controller ();
	
	public Gesture.State TapWatch = Gesture.State.none;
	public Sounds Sounds;
	public Metrics Metrics;
	public Hands Hands;
	public Narrator Narrator;
	public Vector3 wristcenter;
	private float cooldownTime;
	public float MaxcooldownTime;
	public int voiceon = 1;
	
	private AudioSource _audio;
	
	void Awake ()
	{
		this._audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.name == ("bone_distal_index_L")) {
			_audio.PlayOneShot (Sounds.watchbeep, 5.0f);

			switch (TapWatch) {

			case Gesture.State.none:
				if (voiceon == 1) {
					//Narrator.audiosource.Stop ();
					//Metrics.Nar_Check = false;
					voiceon = 0;
					TapWatch = Gesture.State.cooldown;
				} else if (voiceon == 0) {
					TapWatch = Gesture.State.other;
				}
				break;

			case Gesture.State.other:
				voiceon = 1;
				TapWatch = Gesture.State.cooldown;
				if (Metrics.levelcount == 0) {
					//Narrator.audiosource.PlayOneShot (Narrator.stonewrong);
				}
				break;

			case Gesture.State.cooldown:
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					TapWatch = Gesture.State.none;
					//voiceon = 1;
					cooldownTime = MaxcooldownTime;
				}
			break;
			}
		}
	}

	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;
		float wrist_x = rightmost.Arm.WristPosition.x;
		float wrist_y = rightmost.Arm.WristPosition.y;
		float wrist_z = rightmost.Arm.WristPosition.z;
		wristcenter = new Vector3 (wrist_x, wrist_y, -wrist_z);

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			transform.position = wristcenter * 0.05f;
		}
	}



	// CODE that was commented out and previously located in Hands:
	// A void that gets wrist position from the right hand
	// TODO: CHECK IF STILL NEEDED?


	//SOS GESTURE VARIABLES
	/*
	public Vector3 watchposition;
	public Vector3 tapwatch;
	public Vector3 difference;
	public float tappingdistance;
*/
	
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

	// Update is called once per frame
	public void Update ()
	{
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

	}
*/



}
