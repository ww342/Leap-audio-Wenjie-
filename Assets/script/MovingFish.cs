using UnityEngine;
using System.Collections;

public class MovingFish : MonoBehaviour {
	
	public float minRotationSpeed;
	public float maxRotationSpeed;
	public float minMovementSpeed;
	public float maxMovementSpeed;
	private float rotationSpeed=4.0f; // Degrees per second
	private float movementSpeed =5.0f; // Units per second;
	private Transform target;
	private Quaternion qTo;
	
	void Start() {
		target = GameObject.Find ("righthand").transform;    
		rotationSpeed = Random.Range (minRotationSpeed, maxRotationSpeed);
		movementSpeed = Random.Range (minMovementSpeed, maxMovementSpeed);
	}

	void Update() {
		Vector3 v3 = target.position - transform.position;
		float angle = Mathf.Atan2(v3.z, v3.x) * Mathf.Rad2Deg;
		qTo = Quaternion.AngleAxis (angle, Vector3.down);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
		transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
	}
}