using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
	public bool GrabStone = false;
	private float throwspeed = 0f;

	void Update () {
		if (GrabStone) {
			transform.position = GameObject.Find ("rightpalm").transform.position;
		} else {
			//TODO: throwspeed = trans_ringtipSpeed_z;
			transform.position += transform.localPosition * throwspeed * Time.deltaTime;
		}
	}
}
