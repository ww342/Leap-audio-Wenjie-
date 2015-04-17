using UnityEngine;
using System.Collections;
using Leap;

public class righthandmodel : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth ;

	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;
		float pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
		float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
		float handmove_x = rightmost.PalmPosition.x;
		float handmove_y = rightmost.PalmPosition.y;
		float handmove_z = rightmost.PalmPosition.z;
		Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
		Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			transform.rotation = Quaternion.Slerp (transform.rotation, wrist, Time.deltaTime * smooth); 
			transform.position = handcenter * 0.05f;
		}
	}
}
