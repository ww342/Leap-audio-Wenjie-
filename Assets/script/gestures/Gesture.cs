using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
/// Gesture class.
/// Superclass for gestures, defines the helper methods that allow detection of
/// relevant changes in the hand state, such as:
/// 
/// - how many hands are present: handnumbers = frame.Hands.Count;
/// For each hand:
/// - lhhit, rhhit ???
/// - openhand
/// - palmdown
/// - palmrightin
/// - pitchforward
/// - wristleft, right, forward
/// - GrabStrength
/// - pitch
/// For each finger:
/// - IsExtended
/// 
/// </summary>
abstract public class Gesture : MonoBehaviour {
	protected Controller Controller = new Controller ();
	public Sounds Sounds;
	private rightpalm rightpalm; // TODO: try to simplify this so that the calculation is included here!

	public enum State {
		none,
		ready,
		detected,
		action,
		ing,
		other,
		cooldown
	}
	
	public State state = State.none;
	public int count = 0; // how often was it successfully detected?
	public int wrongcount = 0; // count failures too

	public class HandState {
		public float Grab = 0f;
		public float pitch = 0f;
		public bool pitchforward = false;
		public bool palmdown = false;
		public bool palmup = false;
		public bool palmleft = false;
		public bool palmleftin = false;
		public bool palmright = false;
		public bool wristforward = false;
		public bool wristleft = false;
		public bool wristright = false;
		public bool wristmiddle = false;
		public bool wristhigh = false;
		public bool openhand = false;
		public float transPitch = 0f;
		public float transWave_x_3 = 0f;
		public float transWave_y_3 = 0f;
		public float transWave_z_3 = 0f;
		public float transWave_x_6 = 0f;
		public float transWave_y_6 = 0f;
		public float transWave_z_6 = 0f;
		public float transWave_x_10 = 0f;
		public float transWave_y_10 = 0f;
		public float transWave_z_10 = 0f;
		public float losetrack_trans_x = 0f;
		public float losetrack_trans_y = 0f;
		public float losetrack_trans_z = 0f;
	}

	public HandState left = new HandState();
	public HandState right = new HandState();
	
	// base function for starting detection of a gesture:
	public abstract IEnumerator Activate();

	public void Start() {
		this.Sounds = GameObject.Find("/Sounds").GetComponent<Sounds>();
		rightpalm = GameObject.Find("rightpalm").GetComponent<rightpalm>();
	}

	void Update ()
	{
		//Frame variables
		Frame startframe = Controller.Frame ();
		Frame previousframe3 = Controller.Frame (3);
		Frame previousframe10 = Controller.Frame (10);
		Frame previousframe6 = Controller.Frame (6);
		
		// Right Hand variables
		Hand rightmost = startframe.Hands.Rightmost;

		if ((! rightmost.IsRight) || (startframe.Hands.Count < 1)) {
			right = new HandState(); // reset to default values
		} else {
			Finger thumb = rightmost.Fingers [0];
			Finger index = rightmost.Fingers [1];
			Finger middle = rightmost.Fingers [2];
			Finger ring = rightmost.Fingers [3];
			Finger pinky = rightmost.Fingers [4];
			
			float ringtipSpeed_x = ring.TipVelocity.x;
			float ringtipSpeed_y = ring.TipVelocity.y;
			float ringtipSpeed_z = ring.TipVelocity.z;
			float trans_ringtipSpeed_z = previousframe3.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;
			float Throw = trans_ringtipSpeed_z;
			
			
			right.pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
			right.Grab = rightmost.GrabStrength;
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
			
			float transRadius = previousframe6.Hands.Rightmost.SphereRadius - radius;
			right.transPitch = previousframe10.Hands.Rightmost.Direction.Pitch - right.pitch;
			float transYaw = previousframe10.Hands.Rightmost.Direction.Yaw - yaw;
			float transRoll = previousframe10.Hands.Rightmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			right.transWave_y_10 = previousframe10.Hands.Rightmost.PalmPosition.y - handmove_y;
			right.transWave_z_10 = previousframe10.Hands.Rightmost.PalmPosition.z - handmove_z;
			right.transWave_x_10 = previousframe10.Hands.Rightmost.PalmPosition.x - handmove_x;
			right.transWave_y_6 = previousframe6.Hands.Rightmost.PalmPosition.y - handmove_y;
			right.transWave_z_6 = previousframe6.Hands.Rightmost.PalmPosition.z - handmove_z;
			right.transWave_x_6 = previousframe6.Hands.Rightmost.PalmPosition.x - handmove_x;
			right.transWave_y_3 = previousframe3.Hands.Rightmost.PalmPosition.y - handmove_y;
			right.transWave_z_3 = previousframe3.Hands.Rightmost.PalmPosition.z - handmove_z;
			right.transWave_x_3 = previousframe3.Hands.Rightmost.PalmPosition.x - handmove_x;
			
			Quaternion wrist = Quaternion.Euler (-right.pitch, yaw, roll);
			
			// variables calculate losing track distance
			
			right.losetrack_trans_x = rightpalm.losetrack_x - handmove_x;
			right.losetrack_trans_y = rightpalm.losetrack_y - handmove_y;
			right.losetrack_trans_z = rightpalm.losetrack_z - handmove_z;
			
			// gestures bool 
			
			bool wavearm = radius < 40 && right.pitch >= 95 && right.pitch <= 100;
			bool grabstone = transRadius > 10;//&& Grab >0.4 && pitch <10;
			bool catchbird = roll >= 170 && Pinch >= 0.5 && Pinch <= 1;
			
			bool yawforward = yaw <= 20 && yaw >= -20;
			bool yawside = yaw < -20 || yaw > 20;
			bool yawleft = yaw < -20;
			bool yawright = yaw > 20;
			right.pitchforward = right.pitch <= 10 && right.pitch >= 0;
			bool pitchupforward = right.pitch <= 45 && right.pitch >= 40;
			right.palmdown = roll < 20 && roll > -20;
			right.palmup = roll <= -140 || roll >= 140; 
			right.palmright = roll > 50 && roll < 60;
			right.palmleft = roll > - 60 && roll < -50;
			right.palmleftin = roll > -160 && roll < -130;
			bool pitchdownforward = right.pitch <= -10 && right.pitch >= -30;
			bool palmin = yaw <= -50 && yaw >= -80;
			right.openhand = thumb.IsExtended && index.IsExtended && middle.IsExtended && ring.IsExtended && pinky.IsExtended;
			
			bool elbowforward = elbow_z < 200;
			right.wristhigh = wrist_y > 350;
			right.wristleft = wrist_x < -70;
			right.wristright = wrist_x > 70;
			right.wristmiddle = wrist_y < 350;
			right.wristforward = wrist_x > -70 && wrist_x < 70;

		}

		//Left Hand variables
		Hand leftmost = startframe.Hands.Leftmost;
		
		if ((! leftmost.IsLeft) || (startframe.Hands.Count < 1)) {
			left = new HandState(); // reset to default values
		} else {
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
			
			float transRadius = previousframe3.Hands.Leftmost.SphereRadius - radius;
			float transPitch = previousframe3.Hands.Leftmost.Direction.Pitch - pitch;
			float transPaddle = previousframe10.Hands.Leftmost.Direction.Pitch - pitch;
			float transRoll = previousframe10.Hands.Leftmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y = previousframe3.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z = previousframe3.Hands.Leftmost.PalmPosition.z - handmove_z;
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
		}
	}
}
