using UnityEngine;
using System.Collections;
using Leap;

public class lefthandmodel : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth ;
	public Hands script;

	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Frame startframe = Controller.Frame ();
		// Right Hand variables
		Hand leftmost = startframe.Hands.Leftmost;
		float pitch = leftmost.Direction.Pitch * 180.0f / Mathf.PI;
		float roll = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		float yaw = leftmost.Direction.Yaw * 180.0f / Mathf.PI;
		float handmove_x = leftmost.PalmPosition.x;
		float handmove_y = leftmost.PalmPosition.y;
		float handmove_z = leftmost.PalmPosition.z;
		Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
		Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);

		//if (script.levelcount == 2 || script.levelcount == 3) {
		if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {
			transform.rotation = Quaternion.Slerp (transform.rotation, wrist, Time.deltaTime * smooth); 
				transform.position = handcenter * 0.05f;
		}
		//}
	}
}
