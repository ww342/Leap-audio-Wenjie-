using UnityEngine;
using System.Collections;

/// <summary>
/// Bare hands gesture.
/// Simply detects if there are any hands. If onlyonce is false it keeps running and applies some sound effects.
/// </summary>
public class BareHandsGesture : Gesture {
	public bool onlyonce = true;

	override public IEnumerator Activate () {
		while (this.state == State.none) {
			if (handcount < 1) {
				Sounds.Ambience_C.pitch = 3;
				yield return StartCoroutine(this.WaitForAnyHand());
				this.count++;
			} else {
				Sounds.Ambience_C.pitch = 1;
				if (onlyonce) {
					this.state = State.detected;
				} else { // otherwise we just keep running
					yield return StartCoroutine(this.WaitForNoHands());
					this.wrongcount++;
				}
			}
		}
	}
}
