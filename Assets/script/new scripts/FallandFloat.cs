using UnityEngine;
using System.Collections;

public class FallandFloat : MonoBehaviour {
	public float minRotationSpeed;
	public float maxRotationSpeed;
	public float minMovementSpeed;
	public float maxMovementSpeed;
	private float rotationSpeed=4.0f; // Degrees per second
	private float movementSpeed =5.0f; // Units per second;
	private Transform target;
	private Quaternion qTo;
	public Button Button;

	// Use this for initialization
	void Start () {

		target = GameObject.Find ("SeedButton").transform;    
		rotationSpeed = Random.Range (minRotationSpeed, maxRotationSpeed);
		movementSpeed = Random.Range (minMovementSpeed, maxMovementSpeed);
	
	}

	void Fall(){
	   
	     transform.localPosition -= new Vector3 (0, 10f, 0);
	
			
		Debug.Log (" falling!");
		
	}

	void OnTriggerExit(Collider other){
		
		Debug.Log("Object: " + this.name);
		
		
		
		if (other.name == ("Marker")) {
			
			
			Destroy(GameObject.Find ("Seed(Clone)"));

			Debug.Log ("Seed Planted");

			Button.seednumber = 0;
			//this.GetComponent<Grab> ().Grabbed = false;

			GameObject.Find ("Seed").GetComponent<Grab>().Grabbed = false;

			Destroy(GameObject.Find ("Seed").GetComponent<Rigidbody>());
		}
	}


	// Update is called once per frame
	void Update () {
				if (this.name == "Seed(Clone)") {

						if (!this.GetComponent<Grab> ().Grabbed) {
								Vector3 v3 = target.position - transform.position;
								float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;
								qTo = Quaternion.AngleAxis (angle, Vector3.down);
								transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
								transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
						}
				}
		}
}
