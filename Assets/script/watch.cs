using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class watch : MonoBehaviour {
	Controller Controller = new Controller();

	// Use this for initialization

	public enum GestureState { none,ready,detected,action,ing,other,cooldown}

	public GestureState TapWatch = GestureState.none;

	public Sounds sounds;

	private float cooldownTime;
	public float MaxcooldownTime;

	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
				
				if (other.name == ("bone_distal_index_L")) {

			switch (TapWatch) {
			case GestureState.none:
			{
				
				audio.PlayOneShot (sounds.watchbeep, 30.0f);
				
				TapWatch = GestureState.cooldown;
			}
				
				break;
				
			case GestureState.cooldown:
				
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					TapWatch = GestureState.none;
					cooldownTime = MaxcooldownTime;
				}
				
				break;
			}

						//GameObject.Find ("Hands").SendMessage ("Beep");
				}
		}

	// Update is called once per frame
	void Update () {
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;
		float wrist_x = rightmost.Arm.WristPosition.x;
		float wrist_y = rightmost.Arm.WristPosition.y;
		float wrist_z = rightmost.Arm.WristPosition.z;
		Vector3 wristcenter = new Vector3 ( wrist_x, wrist_y, -wrist_z);

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			

			transform.position = wristcenter * 0.05f;
	
			
		}
	}
}
