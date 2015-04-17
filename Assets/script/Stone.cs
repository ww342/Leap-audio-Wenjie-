using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
	public Righthand Righthand;
	private float throwspeed;

	void Start () {
		throwspeed = Righthand.Throw;
	}
	
	void Update () {
		if (Righthand.GrabStone) {
			transform.position = GameObject.Find ("rightpalm").transform.position;
		} else {
			transform.position += transform.localPosition * throwspeed * Time.deltaTime;
		}
	}
}
