using UnityEngine;
using System.Collections;

public class BareHandsGesture : Gesture {
	int handnumbers = 0;
	
	override public IEnumerator Activate () {
		while (handnumbers < 1) {
			yield return null;
			handnumbers = Controller.Frame().Hands.Count;
		}
		this.state = State.detected;
	}
}
