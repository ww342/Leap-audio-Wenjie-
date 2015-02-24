using UnityEngine;
using System.Collections;
using Leap;

public class finger_right : MonoBehaviour
{
	Controller Controller = new Controller ();
	public Finger.FingerType fingerType;
	public AudioClip creak1;
	public AudioClip creak2;
	public AudioClip bike;
	public AudioClip brake;
	public AudioClip bell;
	public AudioClip stonedrop;
	private AudioSource musicControl_B;

	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
	}

	// Update is called once per frame
	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			Finger finger_ = rightmost.Fingers [(int)fingerType];
		}
	}
}
