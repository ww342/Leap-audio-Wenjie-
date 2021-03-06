﻿using UnityEngine;
using System.Collections;
using Leap;

public class lefthandmodel : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth ;

	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand leftmost = startframe.Hands.Leftmost;
		float pitch = leftmost.Direction.Pitch * 180.0f / Mathf.PI;
		float roll = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		float yaw = leftmost.Direction.Yaw * 180.0f / Mathf.PI;
		float handmove_x = leftmost.PalmPosition.x;
		float handmove_y = leftmost.PalmPosition.y;
		float handmove_z = leftmost.PalmPosition.z;
		Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
		Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);

		if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {
			transform.rotation = Quaternion.Slerp (transform.rotation, wrist, Time.deltaTime * smooth); 
			transform.position = handcenter * 0.05f;
		}
	}
}
