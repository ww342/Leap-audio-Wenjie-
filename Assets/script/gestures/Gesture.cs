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
	public Narrator Narrator;
	public GameLogic GameLogic;
	public rightpalm rightpalm; // TODO: try to simplify this so that the calculation is included here!
	public leftpalm leftpalm;

	public AudioSource PlayFromRighthand; 
	public AudioSource PlayFromLefthand;

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

	// base function for starting detection of a gesture:
	public abstract IEnumerator Activate();
	
	// helper functions to start detection in parallel in a separate coroutine:
	private IEnumerator startedCoroutine = null;
	
	private IEnumerator RunInParallel() { // repeatedly run ourselves
		while (true) {
			yield return StartCoroutine(this.Activate());
		}
	}
	
	public void ActivateInParallel() {
		startedCoroutine = this.RunInParallel();
		StartCoroutine(startedCoroutine);
	}
	
	public void DeactivateInParallel() {
		if (startedCoroutine != null) {
			StopCoroutine(startedCoroutine);
		}
	}

	public int count = 0; // how often was it successfully detected?
	public int wrongcount = 0; // count failures too

	// Cooldown Handling:
	private float CooldownTime = 1f; // seconds
	private float AfterCooldown = 0f;

	public void SetCooldown() {
		this.state = State.cooldown;
		this.AfterCooldown = Time.realtimeSinceStartup + CooldownTime;
	}

	public IEnumerator CheckAndWaitForCooldown() {
		if ((this.state == State.cooldown) && (AfterCooldown > Time.realtimeSinceStartup)) {
			yield return new WaitForSeconds(AfterCooldown - Time.realtimeSinceStartup);
		}
		this.state = State.none;
		AfterCooldown = 0f;
	}

	// Waiting for hand date to appear:
	public IEnumerator WaitForHands(bool leftneeded=true, bool rightneeded=true) {
		do {
			yield return null; // always wait at least one frame first
		} while ((rightneeded && ! right.seen) || (leftneeded && ! left.seen));
	}
	public IEnumerator WaitForLeftHand() {
		return WaitForHands(rightneeded : false);
	}
	public IEnumerator WaitForRightHand() {
		return WaitForHands(leftneeded : false);
	}
	public IEnumerator WaitForAnyHand() {
		do {
			yield return null; // always wait at least one frame first
		} while (! (right.seen || left.seen));
	}
	public IEnumerator WaitForNoHands() {
		do {
			yield return null; // always wait at least one frame first
		} while (right.seen || left.seen);
	}

	public class HandState {
		public bool seen = false; // indicates if the hand data is current
		public float Grab = 0f;
		public float Pinch = 0f;
		public float pitch = 0f;
		public bool pitchforward = false;
		public bool pitchupforward = false;
		public bool palmdown = false;
		public bool palmup = false;
		public bool palmin = false;
		public bool palmleft = false;
		public bool palmleftin = false;
		public bool palmright = false;
		public bool palmrightin = false;
		public bool wristforward = false;
		public bool wristleft = false;
		public bool wristright = false;
		public bool wristmiddle = false;
		public bool wristhigh = false;
		public bool openhand = false;
		public float transPitch = 0f;
		public float transYaw = 0f;
		public float transRoll = 0f;
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
		public Finger ring;
		public Finger thumb;
	}

	public int handcount = 0;
	public HandState left = new HandState();
	public HandState right = new HandState();
	
	public void Awake() {
		this.Sounds = GameObject.Find("/Sounds").GetComponent<Sounds>();
		this.Narrator = GameObject.Find ("/Narrator").GetComponent<Narrator>();
		this.GameLogic = GameObject.Find ("/GameFlowAndMetrics").GetComponent<GameLogic> ();
		this.rightpalm = GameObject.Find("rightpalm").GetComponent<rightpalm>();
		this.leftpalm = GameObject.Find("leftpalm").GetComponent<leftpalm>();
		this.PlayFromRighthand = GameObject.Find ("rightpalm").GetComponent<AudioSource> ();
		this.PlayFromLefthand = GameObject.Find ("leftpalm").GetComponent<AudioSource>();
	}

	void Update ()
	{
		//Frame variables
		Frame startframe = Controller.Frame ();
		Frame previousframe3 = Controller.Frame (3);
		Frame previousframe6 = Controller.Frame (6);
		Frame previousframe10 = Controller.Frame (10);
		handcount = startframe.Hands.Count;

		// Right Hand variables
		Hand rightmost = startframe.Hands.Rightmost;

		if ((! rightmost.IsRight) || (handcount < 1)) {
			right.seen = false;
		} else {
			right.seen = true;
			right.thumb = rightmost.Fingers [0];
			Finger index = rightmost.Fingers [1];
			Finger middle = rightmost.Fingers [2];
			right.ring = rightmost.Fingers [3];
			Finger pinky = rightmost.Fingers [4];
			
			float ringtipSpeed_x = right.ring.TipVelocity.x;
			float ringtipSpeed_y = right.ring.TipVelocity.y;
			float ringtipSpeed_z = right.ring.TipVelocity.z;
			float trans_ringtipSpeed_z = previousframe3.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;
			float Throw = trans_ringtipSpeed_z;
			
			right.pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
			right.Grab = rightmost.GrabStrength;
			right.Pinch = rightmost.PinchStrength;
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
			right.transYaw = previousframe10.Hands.Rightmost.Direction.Yaw - yaw;
			right.transRoll = previousframe10.Hands.Rightmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
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
			bool catchbird = roll >= 170 && right.Pinch >= 0.5 && right.Pinch <= 1;
			
			bool yawforward = yaw <= 20 && yaw >= -20;
			bool yawside = yaw < -20 || yaw > 20;
			bool yawleft = yaw < -20;
			bool yawright = yaw > 20;
			right.pitchforward = right.pitch <= 10 && right.pitch >= 0;
			right.pitchupforward = right.pitch <= 45 && right.pitch >= 40;
			right.palmdown = roll < 20 && roll > -20;
			right.palmup = roll <= -140 || roll >= 140; 
			right.palmright = roll > 50 && roll < 60;
			right.palmleft = roll > - 60 && roll < -50;
			right.palmleftin = roll > -160 && roll < -130;
			bool pitchdownforward = right.pitch <= -10 && right.pitch >= -30;
			right.palmin = yaw <= -50 && yaw >= -80;
			right.openhand = right.thumb.IsExtended && index.IsExtended && middle.IsExtended && right.ring.IsExtended && pinky.IsExtended;
			
			bool elbowforward = elbow_z < 200;
			right.wristhigh = wrist_y > 350;
			right.wristleft = wrist_x < -70;
			right.wristright = wrist_x > 70;
			right.wristmiddle = wrist_y < 350;
			right.wristforward = wrist_x > -70 && wrist_x < 70;

		}

		//Left Hand variables
		Hand leftmost = startframe.Hands.Leftmost;
		
		if ((! leftmost.IsLeft) || (handcount < 1)) {
			left.seen = false;
		} else {
			left.seen = true;
			left.thumb = startframe.Fingers [0];
			Finger index = startframe.Fingers [1];
			Finger middle = startframe.Fingers [2];
			left.ring = startframe.Fingers [3];
			Finger pinky = startframe.Fingers [4];
			
			float thumbtip_x = left.thumb.TipPosition.x;
			float thumbtip_y = left.thumb.TipPosition.y;
			float thumbtip_z = left.thumb.TipPosition.z;
			
			float indextip_x = index.TipPosition.x;
			float indextip_y = index.TipPosition.y;
			float indextip_z = index.TipPosition.z;
			
			float middletip_x = middle.TipPosition.x;
			float middletip_y = middle.TipPosition.y;
			float middletip_z = middle.TipPosition.z;
			
			float ringtip_x = left.ring.TipPosition.x;
			float ringtip_y = left.ring.TipPosition.y;
			float ringtip_z = left.ring.TipPosition.z;
			
			float pinkytip_x = pinky.TipPosition.x;
			float pinkytip_y = pinky.TipPosition.y;
			float pinkytip_z = pinky.TipPosition.z;
			
			Vector3 thumbtip = new Vector3 (thumbtip_x, thumbtip_y, -thumbtip_z);
			Vector3 indextip = new Vector3 (indextip_x, indextip_y, -indextip_z);
			Vector3 middletip = new Vector3 (middletip_x, middletip_y, -middletip_z);
			Vector3 ringtip = new Vector3 (ringtip_x, ringtip_y, -ringtip_z);
			Vector3 pinkytip = new Vector3 (pinkytip_x, pinkytip_y, -pinkytip_z);
			
			left.pitch = leftmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = leftmost.Direction.Yaw * 180.0f / Mathf.PI;
			left.Grab = leftmost.GrabStrength;
			left.Pinch = leftmost.PinchStrength;
			float radius = leftmost.SphereRadius;
			float handmove_x = leftmost.PalmPosition.x;
			float handmove_y = leftmost.PalmPosition.y;
			float handmove_z = leftmost.PalmPosition.z;
			Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
			
			Quaternion wrist = Quaternion.Euler (-left.pitch, yaw, roll);
			
			float transRadius = previousframe3.Hands.Leftmost.SphereRadius - radius;
			left.transPitch = previousframe3.Hands.Leftmost.Direction.Pitch - left.pitch;
			float transPaddle = previousframe10.Hands.Leftmost.Direction.Pitch - left.pitch;
			left.transRoll = previousframe10.Hands.Leftmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y = previousframe3.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z = previousframe3.Hands.Leftmost.PalmPosition.z - handmove_z;
			// gestures bool 
			
			bool wavearm = radius < 40 && left.pitch >= 95 && left.pitch <= 100;
			bool grabstone = transRadius > 10 && left.Grab > 0.4 && left.pitch < 10;
			bool catchbird = roll >= 170 && left.Pinch >= 0.5 && left.Pinch <= 1;
			
			bool yawforward = yaw <= 25 && yaw >= -25;
			bool yawside = yaw < -40 || yaw > 40;
			left.pitchforward = left.pitch <= 90 && left.pitch >= -90;
			left.pitchupforward = left.pitch <= 45 && left.pitch >= 40;
			left.palmdown = roll < 20 && roll > -20;
			left.palmup = roll <= -90 || roll >= 180; 
			left.palmright = roll > 90 && roll < 110;
			left.palmrightin = roll > 130 && roll < 160;
			left.openhand = left.thumb.IsExtended && index.IsExtended && middle.IsExtended && left.ring.IsExtended && pinky.IsExtended;
		}
	}
}
