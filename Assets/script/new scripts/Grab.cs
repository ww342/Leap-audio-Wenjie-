using UnityEngine;
using System.Collections;
using Leap;

using UnityEngine;
using System.Collections;
using Leap;

public class Grab : MonoBehaviour {
	
	Controller Controller = new Controller ();
	public bool Grabbed;
	public Button Button;
	
	
	
	public enum GestureState
	{

		start,
		middle_L,
		middle_R,
		end
	}
	public GestureState GrabSeed = GestureState.start;
	
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other){
		
		Debug.Log ("Object: " + this.name);
		
		
		
		if ((other.name == ("rightpalm")) || (other.name == ("leftpalm"))) {
			
			
			GameObject.Find ("Seed(Clone)").GetComponent<FallandFloat> ().maxRotationSpeed = 100;
			GameObject.Find ("Seed(Clone)").GetComponent<FallandFloat> ().minRotationSpeed = 80;
			
			Debug.Log ("Slow down Orbiting!");
			
		}
	}
	
	
	
	void OnTriggerExit(Collider other){
		
		Debug.Log("Object: " + this.name);
		
		
		
		if ((other.name == ("rightpalm")) || (other.name == ("leftpalm"))) {
			
			
			GameObject.Find ("Seed(Clone)").GetComponent<FallandFloat>().maxRotationSpeed = 400;
			GameObject.Find ("Seed(Clone)").GetComponent<FallandFloat>().minRotationSpeed = 300;
			
			Debug.Log ("Speed up Orbiting!");
			
		}
	}
	
	
	
	
	// Update is called once per frame
	
	void Update () {
		
		
		Frame frame = Controller.Frame ();
		Hand rightmost = frame.Hands.Rightmost;
		Hand leftmost = frame.Hands.Leftmost;
		float Grab_L = leftmost.GrabStrength;
		float Grab_R = rightmost.GrabStrength;


		
		switch (GrabSeed) {
		
		
		case GestureState.start:
			
			
			if (Grab_L > 0.8 ) {

				Grabbed = true;
				GrabSeed = GestureState.middle_L;
			} 

			if (Grab_R > 0.8 ) {

				Grabbed = true;
				GrabSeed = GestureState.middle_R;
			} 

			/*
			if (Grab_L > 0.8 && Grab_R >0.8){

				GrabSeed = GestureState.end;
			} 
			*/

			break;
			
		case GestureState.middle_R:

			GameObject.Find ("Seed(Clone)").transform.position = GameObject.Find ("rightpalm").transform.position;

			if (Grab_R == 0) {
				
				//GameObject.Find ("Seed(Clone)").transform.position -= new Vector3 (0, 15f, 0);
				
				//this.GetComponent<FallandFloat >().SendMessage("Fall");
				//Grabbed = false;
				GrabSeed = GestureState.end;

				gameObject.AddComponent <Rigidbody>().useGravity = true;
				
			}
			
			break;

		case GestureState.middle_L:
			
			GameObject.Find ("Seed(Clone)").transform.position = GameObject.Find ("leftpalm").transform.position;
			
			if (Grab_L == 0) {
				
				//GameObject.Find ("Seed(Clone)").transform.position -= new Vector3 (0, 15f, 0);
				
				//this.GetComponent<FallandFloat >().SendMessage("Fall");
				//Grabbed = false;
				GrabSeed = GestureState.end;
				
				gameObject.AddComponent <Rigidbody>().useGravity = true;
				
			}
			
			break;
			
		case GestureState.end:



			if( Button.seednumber == 0){
			
			     GrabSeed = GestureState.start;	 
			
			}

			break;
			
		}
		
	}
}
