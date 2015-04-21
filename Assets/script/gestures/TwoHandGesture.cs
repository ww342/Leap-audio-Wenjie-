using UnityEngine;
using System.Collections;

/// <summary>
/// Two hand gesture. Subclass of Gesture that keeps track of two sub-gestures for the left and right hand.
/// </summary>
abstract public class TwoHandGesture<TLeft, TRight> : Gesture where TLeft : Gesture where TRight: Gesture {
	public TLeft leftHandGesture;
	public TRight rightHandGesture;

	public void StartHands() {
		if (leftHandGesture == null) {
			leftHandGesture = gameObject.AddComponent<TLeft>();
			leftHandGesture.ActivateInParallel();
		}
		if (rightHandGesture == null) {
			rightHandGesture = gameObject.AddComponent<TRight>();
			rightHandGesture.ActivateInParallel();
		}
	}
	
	public void StopHands() {
		if (leftHandGesture != null) {
			leftHandGesture.DeactivateInParallel();
			Destroy (leftHandGesture);
			leftHandGesture = null;
		}
		if (rightHandGesture != null) {
			rightHandGesture.DeactivateInParallel();
			Destroy (rightHandGesture);
			rightHandGesture = null;
		}
	}

}
