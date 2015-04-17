using UnityEngine;
using System.Collections;

public class Gesture : MonoBehaviour {

	public enum State
	{
		none,
		ready,
		detected,
		action,
		ing,
		other,
		cooldown
	}

}
