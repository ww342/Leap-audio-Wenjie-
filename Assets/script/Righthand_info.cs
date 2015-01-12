using UnityEngine;
using UnityEngine;
using System.Collections;
using Leap;

public class Righthand_info : MonoBehaviour {
	Controller Controller;
	
	
	
	// Use this for initialization
	
	void Start () {
		
		Controller = new Controller ();
		
	}
	
	
	
	// Update is called once per frame
	
	void Update () {

		//Frame variables
		Frame frame = Controller.Frame();
		int handnumbers = frame.Hands.Count;


		//Hand variables
		string [] handnames = new string[2] {"Left","Right"};
		Hand rightmost = frame.Hands.Rightmost;


		float pitch = rightmost.Direction.Pitch* 180.0f/Mathf.PI ;
		float roll= rightmost.PalmNormal.Roll * 180.0f/Mathf.PI;
		float yaw= rightmost.Direction.Yaw * 180.0f/Mathf.PI;
		float frameID = frame.Id;
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

		
		
		guiText.lineSpacing = 1.5F;
		guiText.text = "Frame ID:"+ frame.Id +"\n"
			           + "Hand Type: "+"\n"
				     
				       +"Palm Position:"+"\n"
				       +"Pitch :  "  + "\n" 
				       + "Yaw :  " + "\n"  
				       + "Roll :  " +"\n"
				       +" Radius :"+"\n"
				       +" Grab :"+"\n"
				+" Wrist:"+  "\n"
				+" Elbow:"+ "\n"

				//+ "\n"+ "tool number:"
				;
		
		
		if ((rightmost.IsRight)&&(frame.Hands.Count >0)) {
			
			guiText.lineSpacing = 1.5F;
			guiText.text = "Frame ID:"+ frame.Id+"\n"
				           + "Hand Type: " + handnames[1]+"\n"

					+"Palm Position:"+rightmost.PalmPosition+"\n"
					       +"Pitch :  " + pitch + "\n" 
					       + "Yaw :  " + yaw + "\n" 
					       + "Roll :  " + roll+"\n"
					       + "Radius :  " + radius+"\n"
					        +" Grab :"+ Grab+ "\n"
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
			
			
		}
		
		
	}
	
}