using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Northdown : MonoBehaviour {
	public Hands script;
	public Sounds sounds;
	public Narrator voice;

	private AudioSource sound1_once;
	private AudioSource sound2_once;
	private AudioSource sound3_once;
	private AudioSource sound4_once;
	private AudioSource sound_loop;

	private Vector3 originalposition = new Vector3 (0, -90, 600);
	private Vector3 speed = new Vector3(-800, 0, 0);

	bool stoneskip = false;
	
	// Use this for initialization
	void Start () {

		sound1_once = gameObject.AddComponent <AudioSource> ();
		sound1_once.clip = sounds.stone;
		sound1_once.minDistance = 200;

		sound2_once = gameObject.AddComponent <AudioSource> ();
		sound2_once.clip = sounds.hitfish;
		sound2_once.minDistance = 200;

		sound3_once = gameObject.AddComponent <AudioSource> ();
		sound3_once.clip = sounds.stoneskipping;
		sound3_once.minDistance = 200;

		sound4_once = gameObject.AddComponent <AudioSource> ();
		sound4_once.clip = sounds.fishploppy;
		sound4_once.pitch = 3;
		sound4_once.minDistance = 200;
		
	}
	
	void sound1(){
		
		sound1_once.Play ();
		
	}
	void sound2(){
		
		sound2_once.Play ();
		Invoke ("sound6", 1);
		
	}

	void sound3(){
				sound3_once.Play ();
		        stoneskip = true;
		        sound3_once.pitch = 2;
		        Invoke ("goback", 4);
		}
	void sound4(){
		sound3_once.Play ();
		sound3_once.pitch = 1;
		stoneskip = true;
		Invoke ("goback", 5);
	}
	void sound5(){
		sound3_once.Play ();
		sound3_once.pitch = 3;
		stoneskip = true;
		Invoke ("goback", 3);
	}
	void sound6(){
		sound4_once.Play ();
		}

	void goback(){
		stoneskip = false;
	}
	// Update is called once per frame
	void Update () {
		if(stoneskip==true){

			rigidbody.MovePosition (rigidbody.position + speed * Time.deltaTime);
		}

		if (stoneskip==false) {
			rigidbody.transform.position = originalposition ;
		}
	}
}
