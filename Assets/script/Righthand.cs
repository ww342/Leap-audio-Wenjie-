using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.Audio;

public class Righthand : MonoBehaviour
{
	Controller Controller = new Controller ();
	public Hands Hands;
	public Sounds Sounds;
	public Narrator Narrator;
	public hint hint;
	public rightpalm rightpalm;
	public Metrics Metrics;

	public Gesture.State Paddle = Gesture.State.none;
	public Gesture.State Rope = Gesture.State.none;
	public Gesture.State Star = Gesture.State.none;
	public Gesture.State Tree = Gesture.State.none;
	public Gesture.State Bike = Gesture.State.none;
	private float cooldownTime;
	public float MaxcooldownTime;
	public float MaxcooldownTime1;
	public int hit ;

	void Start ()
	{
		cooldownTime = MaxcooldownTime;
		hit = 0;

	}

	void Update ()
	{
		//Frame variables
		Frame startframe = Controller.Frame ();
		Frame perviousframe3 = Controller.Frame (3);
		Frame perviousframe10 = Controller.Frame (10);
		Frame perviousframe6 = Controller.Frame (6);

		// Right Hand variables
		Hand rightmost = startframe.Hands.Rightmost;

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			Finger thumb = rightmost.Fingers [0];
			Finger index = rightmost.Fingers [1];
			Finger middle = rightmost.Fingers [2];
			Finger ring = rightmost.Fingers [3];
			Finger pinky = rightmost.Fingers [4];

			float ringtipSpeed_x = ring.TipVelocity.x;
			float ringtipSpeed_y = ring.TipVelocity.y;
			float ringtipSpeed_z = ring.TipVelocity.z;
			float trans_ringtipSpeed_z = perviousframe3.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;

			float pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
			float Grab = rightmost.GrabStrength;
			float Pinch = rightmost.PinchStrength;
			float radius = rightmost.SphereRadius;
			float handmove_x = rightmost.PalmPosition.x;
			float handmove_y = rightmost.PalmPosition.y;
			float handmove_z = rightmost.PalmPosition.z;

			float wrist_x = rightmost.Arm.WristPosition.x;
			float wrist_y = rightmost.Arm.WristPosition.y;
			float wrist_z = rightmost.Arm.WristPosition.z;
			float elbow_x = rightmost.Arm.ElbowPosition.x;
			float elbow_y = rightmost.Arm.ElbowPosition.y;
			float elbow_z = rightmost.Arm.ElbowPosition.z;
		
			Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);

			Vector3 wristposition = new Vector3 (wrist_x, wrist_y, -wrist_z);
		
			float transRadius = perviousframe6.Hands.Rightmost.SphereRadius - radius;
			float transPitch = perviousframe10.Hands.Rightmost.Direction.Pitch - pitch;
			float transYaw = perviousframe10.Hands.Rightmost.Direction.Yaw - yaw;
			float transRoll = perviousframe10.Hands.Rightmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y_10 = perviousframe10.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_10 = perviousframe10.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_10 = perviousframe10.Hands.Rightmost.PalmPosition.x - handmove_x;
			float transWave_y_6 = perviousframe6.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_6 = perviousframe6.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_6 = perviousframe6.Hands.Rightmost.PalmPosition.x - handmove_x;
			float transWave_y_3 = perviousframe3.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_3 = perviousframe3.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_3 = perviousframe3.Hands.Rightmost.PalmPosition.x - handmove_x;

			Quaternion wrist = Quaternion.Euler (-pitch, yaw, roll);

			// variables calculate losing track distance
			            
			float losetrack_trans_x = rightpalm.losetrack_x - handmove_x;
			float losetrack_trans_y = rightpalm.losetrack_y - handmove_y;
			float losetrack_trans_z = rightpalm.losetrack_z - handmove_z;

			// gestures bool 
		
			bool wavearm = radius < 40 && pitch >= 95 && pitch <= 100;
			bool grabstone = transRadius > 10;//&& Grab >0.4 && pitch <10;
			bool catchbird = roll >= 170 && Pinch >= 0.5 && Pinch <= 1;
		
			bool yawforward = yaw <= 20 && yaw >= -20;
			bool yawside = yaw < -20 || yaw > 20;
			bool yawleft = yaw < -20;
			bool yawright = yaw > 20;
			bool pitchforward = pitch <= 10 && pitch >= 0;
			bool pitchupforward = pitch <= 45 && pitch >= 40;
			bool palmdown = roll < 20 && roll > -20;
			bool palmup = roll <= -140 || roll >= 140; 
			bool palmright = roll > 50 && roll < 60;
			bool palmleft = roll > - 60 && roll < -50;
			bool palmleftin = roll > -160 && roll < -130;
			bool pitchdownforward = pitch <= -10 && pitch >= -30;
			bool palmin = yaw <= -50 && yaw >= -80;
			bool openhand = thumb.IsExtended && index.IsExtended && middle.IsExtended && ring.IsExtended && pinky.IsExtended;
			           
			bool elbowforward = elbow_z < 200;
			bool wristhigh = wrist_y > 350;
			bool wristleft = wrist_x < -70;
			bool wristright = wrist_x > 70;
			bool wristmiddle = wrist_y < 350;
			bool wristforward = wrist_x > -70 && wrist_x < 70;


			if (Metrics.levelcount > 2) {




			// Paddle

			//if (Metrics.levelcount == 3) {

					switch (Paddle) {
			
					case Gesture.State.none:
								
						if (pitchforward && palmdown) {
							Paddle = Gesture.State.detected;
						}
							
						break;
			
					case Gesture.State.detected:
						if (transPitch > 30) { 
							hit = 2;
					Sounds.Environment.PlayOneShot (Sounds.creak1); // TODO: should be played on finger
							Hands.RHhit(hit);
							Paddle = Gesture.State.action;
						} 
						break;
			
					case Gesture.State.action:
						Paddle = Gesture.State.cooldown;
						break;
			
					case Gesture.State.cooldown:
						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
							Paddle = Gesture.State.none;
							hit = 0;
							Hands.RHhit(hit);
							cooldownTime = MaxcooldownTime;
						}
						break;
					}

			// Rope
			switch (Rope) {
				
			case Gesture.State.none:
				if (Metrics.levelcount == 4) {
					//if (Metrics.wrongcount > 2 || Metrics.flowercount == 12) {
						if (palmright) {
							Rope = Gesture.State.ready;
						}
					//}
				}
				break;

			case  Gesture.State.ready:
				if (Metrics.levelcount == 4) {
					//if (Metrics.wrongcount > 2 || Metrics.flowercount == 12) {
						if (!openhand) {
							if (transWave_z_10 > 50) {
								Rope = Gesture.State.detected;
							}
						}
					//}
				}
				break;

			case Gesture.State.detected:
				if (!openhand) {
					if (transWave_x_10 > 5) {
						Rope = Gesture.State.action;
					}
				}
				break;

			case Gesture.State.action:
				if (!openhand) {
					if (transWave_z_10 < -50) {
						Rope = Gesture.State.ing;
					}
				}
				break;
				
			case Gesture.State.ing:
				if (!openhand) {
					if (transWave_x_10 < -5) {
						Hands.RopeCount();
						Rope = Gesture.State.cooldown;
					}
				}
				break;

			case Gesture.State.cooldown:
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					Rope = Gesture.State.ready;
					cooldownTime = MaxcooldownTime;
				}
				break;
			}





			// Tree
			//if (Metrics.levelcount == 4 && Metrics.wrongcount <= 2 && Metrics.flowercount < 12) {

					switch (Tree) {
			
					case Gesture.State.none:

						if (palmleft) {
							Tree = Gesture.State.detected;
						}
				
						break;
			
					case Gesture.State.detected:
							if (!ring.IsExtended) {
								Tree = Gesture.State.action;
							}
						break;
			
					case Gesture.State.action:
						if (transWave_x_10 > 10) {
//							Hands.FlowerCount(); // TODO: flower counting used for tree and flower?
							Tree = Gesture.State.cooldown;	
						}
						break;
			
					case Gesture.State.cooldown:
						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
							Tree = Gesture.State.detected;
							cooldownTime = MaxcooldownTime;
						}
						break;
					}
		
			// Bike

			//if (Metrics.levelcount == 5) {

					switch (Bike) {
			
					case Gesture.State.none:

						if (pitchforward) {
							Bike = Gesture.State.ready;
						}
			
						break;
			
					case Gesture.State.ready:
						if (Metrics.levelcount == 5 && Metrics.bellcount < 6) {
							if (palmdown && Grab > 0.4 && thumb.IsExtended) {
								Sounds.Environment.PlayOneShot (Sounds.bike); // TODO: play on finger
								Bike = Gesture.State.detected;
							}
						}
						break;
			
					case Gesture.State.detected:
						if (!thumb.IsExtended) {
							Sounds.Environment.PlayOneShot (Sounds.bell); // TODO: play on finger
							Hands.BellCount();
							Bike = Gesture.State.action;
				
						}
						break;
			
					case Gesture.State.action:
						Bike = Gesture.State.cooldown;	
						break;
			
					case Gesture.State.cooldown:
						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
							Bike = Gesture.State.ready;
							cooldownTime = MaxcooldownTime;
						}
						break;
					}

			// Star

			//if (Metrics.levelcount == 6) {

					switch (Star) {
			
					case Gesture.State.none:

						if (pitchupforward) {
							Star = Gesture.State.ready;
						}
			
						break;
			
					case Gesture.State.ready:
						if (transPitch > 10) {
							if (!openhand) {
								Star = Gesture.State.detected;
							}
						}
						break;
			
					case Gesture.State.detected:
				                        //if(transYaw>3){
						if (palmin) {
							Sounds.Environment.PlayOneShot (Sounds.star);
							Star = Gesture.State.action;
						}
						break;
			
					case Gesture.State.action:
						if (palmup) {
							Hands.StarCount();
							Sounds.Environment.PlayOneShot (Sounds.win);
							Sounds.Environment.PlayOneShot (Sounds.shiny);
							Star = Gesture.State.cooldown;	
						}
						break;
			
					case Gesture.State.cooldown:
						cooldownTime -= Time.deltaTime;
						if (cooldownTime <= 0) {
							Star = Gesture.State.detected;
							cooldownTime = MaxcooldownTime;
						}
						break;
					}
			}
		}
	}
}
