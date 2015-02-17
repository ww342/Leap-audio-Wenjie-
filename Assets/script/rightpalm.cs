using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class rightpalm : MonoBehaviour {
	Controller Controller = new Controller();
	
	// Use this for initialization
	

	public Sounds sounds;
	public Hands script;
	public Narrator voice;

	public Vector3 handcenter;
	public Vector3 losetrack;
	public float losetrack_x;
	public float losetrack_y;
	public float losetrack_z;

	

	
	void PositionSave(){
		
		PlayerPrefs.SetFloat("x",transform.position.x);
		PlayerPrefs.SetFloat("y",transform.position.y);
		PlayerPrefs.SetFloat("z",transform.position.z);
		
		losetrack = new Vector3(transform.position.x * 20.0f, transform.position.y*20.0f,transform.position.z*20.0f);
		losetrack_x = transform.position.x * 20.0f;
		losetrack_y = transform.position.y * 20.0f;
		losetrack_z = transform.position.z * 20.0f;
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		Frame startframe = Controller.Frame ();
		Frame perviousframe3 = Controller.Frame (3);
		Frame perviousframe10 = Controller.Frame (10);
		Frame perviousframe6 = Controller.Frame (6);
		Hand rightmost = startframe.Hands.Rightmost;
		
		
		float handmove_x = rightmost.PalmPosition.x;
		float handmove_y = rightmost.PalmPosition.y;
		float handmove_z = rightmost.PalmPosition.z;
		handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);


		float transWave_y_10 = perviousframe10.Hands.Rightmost.PalmPosition.y - handmove_y;
		float transWave_z_10 = perviousframe10.Hands.Rightmost.PalmPosition.z - handmove_z;
		float transWave_x_10 = perviousframe10.Hands.Rightmost.PalmPosition.x - handmove_x;
		float transWave_y_6 = perviousframe6.Hands.Rightmost.PalmPosition.y - handmove_y;
		float transWave_z_6 = perviousframe6.Hands.Rightmost.PalmPosition.z - handmove_z;
		float transWave_x_6 = perviousframe6.Hands.Rightmost.PalmPosition.x - handmove_x;
		float transWave_y_3 = perviousframe3.Hands.Rightmost.PalmPosition.y - handmove_y;
		float transWave_z_3 = perviousframe3.Hands.Rightmost.PalmPosition.z - handmove_z;
		float transWave_x_3 = perviousframe3.Hands.Rightmost.PalmPosition.x - handmove_x;

		
		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			
			
			transform.position = handcenter * 0.05f;
			
			
		}
		
		
		//Save the palm position while it loses track from the camera
		
		else{
			
			PositionSave();
			
		}  
	}
}
