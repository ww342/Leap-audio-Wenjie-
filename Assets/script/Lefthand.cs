﻿using UnityEngine;
using System.Collections;
using Leap;

public class Lefthand : MonoBehaviour
{
	Controller Controller = new Controller ();
	public Hands Hands;
	public Sounds Sounds;
	public Narrator Narrator;
	public hint hint;
	public Metrics Metrics;
	public Gesture.State Bike = Gesture.State.none;
	private float cooldownTime;
	public float MaxcooldownTime;
	public float MaxcooldownTime1;
	public int hit;
	private AudioSource Gesturehint;

	void Start ()
	{
		cooldownTime = MaxcooldownTime;
		hit = 0;

		Gesturehint = gameObject.AddComponent <AudioSource> ();
		Gesturehint.minDistance = 5;
	}

	void Update ()
	{
		//Frame variables
		Frame startframe = Controller.Frame ();
		Frame perviousframe = Controller.Frame (3);
		Frame perviousframe1 = Controller.Frame (10);

		//Left Hand variables
		Hand leftmost = startframe.Hands.Leftmost;

		if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {
			Finger thumb = startframe.Fingers [0];
			Finger index = startframe.Fingers [1];
			Finger middle = startframe.Fingers [2];
			Finger ring = startframe.Fingers [3];
			Finger pinky = startframe.Fingers [4];

			float thumbtip_x = thumb.TipPosition.x;
			float thumbtip_y = thumb.TipPosition.y;
			float thumbtip_z = thumb.TipPosition.z;
			           
			float indextip_x = index.TipPosition.x;
			float indextip_y = index.TipPosition.y;
			float indextip_z = index.TipPosition.z;

			float middletip_x = middle.TipPosition.x;
			float middletip_y = middle.TipPosition.y;
			float middletip_z = middle.TipPosition.z;

			float ringtip_x = ring.TipPosition.x;
			float ringtip_y = ring.TipPosition.y;
			float ringtip_z = ring.TipPosition.z;

			float pinkytip_x = pinky.TipPosition.x;
			float pinkytip_y = pinky.TipPosition.y;
			float pinkytip_z = pinky.TipPosition.z;

			Vector3 thumbtip = new Vector3 (thumbtip_x, thumbtip_y, -thumbtip_z);
			Vector3 indextip = new Vector3 (indextip_x, indextip_y, -indextip_z);
			Vector3 middletip = new Vector3 (middletip_x, middletip_y, -middletip_z);
			Vector3 ringtip = new Vector3 (ringtip_x, ringtip_y, -ringtip_z);
			Vector3 pinkytip = new Vector3 (pinkytip_x, pinkytip_y, -pinkytip_z);

			float pitch = leftmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = leftmost.Direction.Yaw * 180.0f / Mathf.PI;
			float Grab = leftmost.GrabStrength;
			float Pinch = leftmost.PinchStrength;
			float radius = leftmost.SphereRadius;
			float handmove_x = leftmost.PalmPosition.x;
			float handmove_y = leftmost.PalmPosition.y;
			float handmove_z = leftmost.PalmPosition.z;
			Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);

			Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);
		
			float transRadius = perviousframe.Hands.Leftmost.SphereRadius - radius;
			float transPitch = perviousframe.Hands.Leftmost.Direction.Pitch - pitch;
			float transPaddle = perviousframe1.Hands.Leftmost.Direction.Pitch - pitch;
			float transRoll = perviousframe1.Hands.Leftmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y = perviousframe.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z = perviousframe.Hands.Leftmost.PalmPosition.z - handmove_z;
			// gestures bool 
		
			bool wavearm = radius < 40 && pitch >= 95 && pitch <= 100;
			bool grabstone = transRadius > 10 && Grab > 0.4 && pitch < 10;
			bool catchbird = roll >= 170 && Pinch >= 0.5 && Pinch <= 1;
		
			bool yawforward = yaw <= 25 && yaw >= -25;
			bool yawside = yaw < -40 || yaw > 40;
			bool pitchforward = pitch <= 90 && pitch >= -90;
			bool pitchupforward = pitch <= 45 && pitch >= 40;
			bool palmdown = roll < 20 && roll > -20;
			bool palmup = roll <= -90 || roll >= 180; 
			bool palmright = roll > 90 && roll < 110;
			bool palmrightin = roll > 130 && roll < 160;
			bool openhand = thumb.IsExtended && index.IsExtended && middle.IsExtended && ring.IsExtended && pinky.IsExtended;
			          
			//keep sending left hand index tip position to script"Hands" to check SOS WATCH TAPPING MESSAGE 
			//GameObject.Find ("Hands").SendMessage ("TaptheWatch", indextip);


			if (Metrics.levelcount > 2) {



			//Bike


			//if (Metrics.levelcount == 5) {

					switch (Bike) {
				
					case Gesture.State.none:

						if (pitchforward) {
							Bike = Gesture.State.ready;
						}
				
						break;


				
					case Gesture.State.ready:

						if (Metrics.levelcount == 5 && Metrics.bellcount < 6) {

							if (palmdown && Grab > 0.4) {

								Gesturehint.PlayOneShot (Sounds.bike); // TODO: should be played on finger
								Bike = Gesture.State.detected;

							}
						}
						break;


				
					case Gesture.State.detected:
				//if(middle.IsExtended){
						Bike = Gesture.State.action;
				//}
						break;

					case Gesture.State.action:
				//if(!middle.IsExtended){
						Bike = Gesture.State.cooldown;	
				//}
						break;

					case Gesture.State.cooldown:
						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
							Bike = Gesture.State.ready;
							cooldownTime = MaxcooldownTime;
						}
						break;

					}

			}

		}
	}
}
