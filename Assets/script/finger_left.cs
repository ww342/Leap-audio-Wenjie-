using UnityEngine;
using System.Collections;
using Leap;
public class finger_left : MonoBehaviour {
	Controller Controller = new Controller();
	public Finger.FingerType fingerType;
	public AudioClip creak1;
	public AudioClip creak2;
	public AudioClip bike;
	public AudioClip brake;
	
	private AudioSource musicControl_B;
	// Use this for initialization
	void Start () {
		Controller = new Controller ();
		
	}
	

	// Update is called once per frame
	void Update () {
		Frame startframe = Controller.Frame ();
		Hand leftmost = startframe.Hands.Leftmost;


		if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {
			
			Finger finger_ = leftmost.Fingers [(int)fingerType];


	}
 }
}
