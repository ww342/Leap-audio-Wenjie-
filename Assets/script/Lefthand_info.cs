using UnityEngine;
using System.Collections;
using Leap;

public class Lefthand_info : MonoBehaviour {
	Controller Controller;
	
	
	
	// Use this for initialization
	
	void Start () {
		
		Controller = new Controller ();
		
	}
	
	
	
	// Update is called once per frame
	
	void Update () {
		// Frame variables
		Frame frame = Controller.Frame();
		int handnumbers = frame.Hands.Count;



		// Hand variables
		string [] handnames = new string[2] {"Left","Right"};
		Hand leftmost = frame.Hands.Leftmost;


		float pitch = leftmost.Direction.Pitch* 180.0f/Mathf.PI ;
		float roll= leftmost.PalmNormal.Roll * 180.0f/Mathf.PI;
		float yaw= leftmost.Direction.Yaw * 180.0f/Mathf.PI;
		float Grab = leftmost.GrabStrength;
		float Pinch = leftmost.PinchStrength;
		float radius = leftmost.SphereRadius;
	

		//Fingers variables
		
		
		string[] fingernames = new string[5] {"Thumb", "Index finger", "Middle finger", "Ring finger", "Pinky finger"};



		
		guiText.lineSpacing = 1.5F;
		guiText.text = "Frame ID:"+ frame.Id +"\n"
			    + "Hand Type: " +"\n"
				+"Palm Position:"+"\n"
				+"Pitch :  "  + "\n" 
				+ "Yaw :  " + "\n"  
				+ "Roll :  " ;
		

			if ((leftmost.IsLeft)&&(frame.Hands.Count >0)) {
				
			guiText.lineSpacing = 1.5F;
			guiText.text = "Frame ID:"+ frame.Id+"\n"
				    + "Hand Type: " + handnames[0]+"\n"
					+"Palm Position:"+leftmost.PalmPosition+"\n"
					+"Pitch :  " + pitch + "\n" 
					+ "Yaw :  " + yaw + "\n" 
					+ "Roll :  " + roll;
		}
		
		
	}
	
}