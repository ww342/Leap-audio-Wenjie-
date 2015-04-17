using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof(AudioSource))]
public class watch : MonoBehaviour
{
	Controller Controller = new Controller ();
	
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
	
	public GestureState TapWatch = GestureState.none;
	public Sounds Sounds;
	public Metrics Metrics;
	public Hands Hands;
	public Narrator Narrator;
	public Vector3 wristcenter;
	private float cooldownTime;
	public float MaxcooldownTime;
	public int voiceon;
	
	private AudioSource _audio;
	
	void Awake ()
	{
		this._audio = GetComponent<AudioSource>();
	}

	void Start ()
	{
		voiceon = 1;
	}
	
	void OnTriggerEnter (Collider other)
	{

		if (other.name == ("bone_distal_index_L")) {
			_audio.PlayOneShot (Sounds.watchbeep, 5.0f);

			switch (TapWatch) {

			case GestureState.none:
				if (voiceon == 1) {
					Narrator.audiosource.Stop ();
					Metrics.Nar_Check = false;
					voiceon = 0;
					TapWatch = GestureState.cooldown;
				}
				if (voiceon == 0) {
					TapWatch = GestureState.other;

				}
				break;


			case GestureState.other:
				voiceon = 1;
				TapWatch = GestureState.cooldown;
				if (Metrics.levelcount == 0) {
					Narrator.audiosource.PlayOneShot (Narrator.stonewrong);
				}
			
			break;


			case GestureState.cooldown:
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					TapWatch = GestureState.none;
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
}
