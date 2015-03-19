using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Button : MonoBehaviour {
	
	//scripts
	//public Sounds Sounds;
	public AudioClip seed;
	public int seednumber;



	
	// Use this for initialization
	void Start () {
		
		
		
	}


	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){

		Debug.Log("Object: " + this.name);



		if ((other.name == ("L_index_bone3")) || (other.name == ("R_index_bone3"))) {

						if (seednumber < 1) {
			
								audio.PlayOneShot (seed);

								Instantiate (GameObject.Find ("Seed"), transform.position = this.transform.localPosition, transform.rotation = this.transform.localRotation);

								GameObject.Find ("Seed(Clone)").GetComponent<FallandFloat>().SendMessage("Fall");

								seednumber ++ ;

								Debug.Log ("Seed Generated!");

						}
				}
		
	}
	// Update is called once per frame
	void Update () {
	


				}
		
	}
