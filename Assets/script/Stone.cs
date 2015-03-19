using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
	public Righthand Righthand;
	private float throwspeed;

	// Use this for initialization
	void Start () {

		throwspeed = Righthand.Throw;
	}
	
	// Update is called once per frame
	void Update () {


		if (Righthand.GrabStone) {
			//this.GetComponent<Collider>().attachedRigidbody.useGravity = false;
			transform.position = GameObject.Find ("rightpalm").transform.position;
				}
		if (!Righthand.GrabStone) {
			//this.GetComponent<Collider>().attachedRigidbody.useGravity = true;
			transform.position += transform.localPosition * throwspeed * Time.deltaTime;

				}
	}
}
