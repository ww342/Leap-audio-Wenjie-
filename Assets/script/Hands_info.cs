using UnityEngine;
using UnityEngine.UI;
using Leap;

/// <summary>
/// Hands_info. Update UI Text components with status info on Leap hand detection.
/// </summary>
public class Hands_info : MonoBehaviour {
	Controller Controller;
	public Text statusText;
	public Text frameText;
	//public Text otherText;
	public Text Righthanddata;
	public Text Lefthanddata;

	void Start () {
		Controller = new Controller ();
		if (statusText != null) {
			statusText.text = "Controller object";
		}
	}
	
	void Update () {
		if (statusText != null) {
			string status = (Controller.IsServiceConnected() ? "Svc" : "No Leap Service");
			status += (Controller.HasFocus ? "" : ", no focus");
			status += ", " + Controller.Devices.Count + " dev's";
			status += ", " + (Controller.IsConnected ? "" : "not ") + "connected";
			statusText.text = status;
		}
		Frame frame = Controller.Frame();
		if (frameText != null) {
			frameText.text = frame.Id.ToString();
		}
		int handnumbers = frame.Hands.Count;

		//Hand variables
		string [] handnames = new string[2] {"Left","Right"};



		// data for right hand
		Hand rightmost = frame.Hands.Rightmost;

		float r_pitch = rightmost.Direction.Pitch* 180.0f/Mathf.PI ;
		float r_roll= rightmost.PalmNormal.Roll * 180.0f/Mathf.PI;
		float r_yaw= rightmost.Direction.Yaw * 180.0f/Mathf.PI;
		float r_frameID = frame.Id;
		float r_Grab = rightmost.GrabStrength;
		float r_Pinch = rightmost.PinchStrength;
		float r_radius = rightmost.SphereRadius;
		float r_handmove_x = rightmost.PalmPosition.x;
		float r_handmove_y = rightmost.PalmPosition.y;
		float r_handmove_z = rightmost.PalmPosition.z;

		float r_wrist_x = rightmost.Arm.WristPosition.x;
		float r_wrist_y = rightmost.Arm.WristPosition.y;
		float r_wrist_z = rightmost.Arm.WristPosition.z;
		float r_elbow_x = rightmost.Arm.ElbowPosition.x;
		float r_elbow_y = rightmost.Arm.ElbowPosition.y;
		float r_elbow_z = rightmost.Arm.ElbowPosition.z;



		// data for left hand
		Hand leftmost = frame.Hands.Leftmost;

		float l_pitch = leftmost.Direction.Pitch* 180.0f/Mathf.PI ;
		float l_roll= leftmost.PalmNormal.Roll * 180.0f/Mathf.PI;
		float l_yaw= leftmost.Direction.Yaw * 180.0f/Mathf.PI;
		float l_frameID = frame.Id;
		float l_Grab = leftmost.GrabStrength;
		float l_Pinch = leftmost.PinchStrength;
		float l_radius = leftmost.SphereRadius;
		float l_handmove_x = leftmost.PalmPosition.x;
		float l_handmove_y = leftmost.PalmPosition.y;
		float l_handmove_z = leftmost.PalmPosition.z;
		
		float l_wrist_x = leftmost.Arm.WristPosition.x;
		float l_wrist_y = leftmost.Arm.WristPosition.y;
		float l_wrist_z = leftmost.Arm.WristPosition.z;
		float l_elbow_x = leftmost.Arm.ElbowPosition.x;
		float l_elbow_y = leftmost.Arm.ElbowPosition.y;
		float l_elbow_z = leftmost.Arm.ElbowPosition.z;



		/*
		int toolnum = rightmost.Tools.Count;
		Tool paddle = rightmost.Tools [0];

		bool thumb =  rightmost.Fingers [0].IsExtended;
		bool index =  rightmost.Fingers [1].IsExtended;
		bool middle =  rightmost.Fingers [2].IsExtended;
		bool ring =  rightmost.Fingers [3].IsExtended;
		bool pinky =  rightmost.Fingers [4].IsExtended;
		*/

		//Fingers variables
		int extendedfingers = 0;

		if (Righthanddata != null) {
			if ((rightmost.IsRight)&&(frame.Hands.Count >0)) {
				Righthanddata.text = "Hand Type: " + handnames[1]+"\n"
					+"Palm Position:"+rightmost.PalmPosition+"\n"
						+"Pitch :  " + r_pitch + "\n" 
						+ "Yaw :  " + r_yaw + "\n" 
						+ "Roll :  " + r_roll+"\n"
						+ "Radius :  " + r_radius+"\n"
						+" Grab :"+ r_Grab+ "\n"
					        +" Wrist:"+  rightmost.Arm.WristPosition  +"\n"
					+" Elbow:"+  rightmost.Arm.ElbowPosition +"\n"
					+ rightmost.Fingers[0].Type().ToString()+ rightmost.Fingers[0].IsExtended+"\n"
					+ rightmost.Fingers[1].Type().ToString() + rightmost.Fingers[1].IsExtended+"\n"
					+ rightmost.Fingers[2].Type().ToString()+ rightmost.Fingers[2].IsExtended+"\n"
					+ rightmost.Fingers[3].Type().ToString()+ rightmost.Fingers[3].IsExtended+"\n"
					+ rightmost.Fingers[4].Type().ToString() + rightmost.Fingers[4].IsExtended+"\n"
					+ rightmost.Fingers[4].TipVelocity +"\n"
					//+ "\n"+ "tool number:" + toolnum 
					;
			} else {
				Righthanddata.text = "No right hand at the moment.";
			}
		}

		if (Lefthanddata != null) {
			if ((leftmost.IsLeft)&&(frame.Hands.Count >0)) {
				Lefthanddata.text = "Hand Type: " + handnames[0]+"\n"
					+"Palm Position:"+leftmost.PalmPosition+"\n"
						+"Pitch :  " + l_pitch + "\n" 
						+ "Yaw :  " + l_yaw + "\n" 
						+ "Roll :  " + l_roll+"\n"
						+ "Radius :  " + l_radius+"\n"
						+" Grab :"+ l_Grab+ "\n"
						+" Wrist:"+  leftmost.Arm.WristPosition  +"\n"
						+" Elbow:"+  leftmost.Arm.ElbowPosition +"\n"
						+ leftmost.Fingers[0].Type().ToString()+ leftmost.Fingers[0].IsExtended+"\n"
						+ leftmost.Fingers[1].Type().ToString() + leftmost.Fingers[1].IsExtended+"\n"
						+ leftmost.Fingers[2].Type().ToString()+ leftmost.Fingers[2].IsExtended+"\n"
						+ leftmost.Fingers[3].Type().ToString()+ leftmost.Fingers[3].IsExtended+"\n"
						+ leftmost.Fingers[4].Type().ToString() + leftmost.Fingers[4].IsExtended+"\n"
						+ leftmost.Fingers[4].TipVelocity +"\n"
						//+ "\n"+ "tool number:" + toolnum 
						;
			} else {
				Lefthanddata.text = "No right hand at the moment.";
			}
		}
	}	
}
