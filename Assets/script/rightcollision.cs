using UnityEngine;
using System.Collections;
using Leap;

public class rightcollision : MonoBehaviour
{
	Controller Controller = new Controller ();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;
		float hand_x = rightmost.PalmPosition.x;
		float hand_y = rightmost.PalmPosition.y;
		float hand_z = rightmost.PalmPosition.z;
		Vector3 handcenter = new Vector3 (hand_x, hand_y, -hand_z);
		
		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			transform.position = handcenter * 0.05f;
		}
	}
}
