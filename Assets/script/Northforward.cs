using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Northforward : MonoBehaviour {
	public Hands script;
	public Sounds sounds;
	public Narrator voice;

	private AudioSource sound1_once;

	
	private Vector3 originalposition = new Vector3 (0, -90, 600);
	private Vector3 far = new Vector3(0, 0, 800);
	private Vector3 left = new Vector3(-300, 0, 800);
	private Vector3 right = new Vector3(300, 0, 800);
	
	bool farstoneskip ;
	bool leftstoneskip ;
	bool rightstoneskip ;

	
	// Use this for initialization
	void Start () {
		
		sound1_once = gameObject.AddComponent <AudioSource> ();
		sound1_once.clip = sounds.stoneskipping;
		sound1_once.minDistance = 200;

		
	}

	
	void rightskip(){
		sound1_once.Play ();
		rightstoneskip = true;
		sound1_once.pitch = 2;
		Invoke ("goback", 4);
	}
	void farskip(){
		sound1_once.Play ();
		sound1_once.pitch = 1;
		farstoneskip  = true;
		Invoke ("goback", 5);
	}
	void leftskip(){
		sound1_once.Play ();
		sound1_once.pitch = 3;
		leftstoneskip = true;
		Invoke ("goback", 3);
	}
	
	void goback(){
		farstoneskip = false;
		leftstoneskip = false;
		rightstoneskip = false;
	}
	// Update is called once per frame
	void Update () {
		if(farstoneskip==true){
			
			rigidbody.MovePosition (rigidbody.position + far * Time.deltaTime);
		}
		
		if (farstoneskip==false) {
			rigidbody.transform.position = originalposition ;
		}

		if(leftstoneskip==true){
			
			rigidbody.MovePosition (rigidbody.position + left * Time.deltaTime);
		}
		
		if (leftstoneskip==false) {
			rigidbody.transform.position = originalposition ;
		}

		if(rightstoneskip==true){
			
			rigidbody.MovePosition (rigidbody.position + right * Time.deltaTime);
		}
		
		if (rightstoneskip==false) {
			rigidbody.transform.position = originalposition ;
		}
	}
}
