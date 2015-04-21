using UnityEngine;
using System.Collections;

public class BareHandsGesture : Gesture {

	override public IEnumerator Activate () {
		while (handcount < 1) {
			yield return null;
		}
		this.state = State.detected;
	}
}
