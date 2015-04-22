﻿using UnityEngine;
using System.Collections;

public class TreeShakingGesture : Gesture {
	
	private void TreeCount () {
		this.count++;
		Sounds.audiosource.PlayOneShot (Sounds.flower);
		if (this.count == 1) {
			Sounds.audiosource.PlayOneShot (Sounds.leave);
		}
		if (this.count == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
		}
		if (this.count == 6) {
			Sounds.Ambience_D.PlayOneShot (Sounds.leave);
			Sounds.Ambience_D.PlayOneShot (Sounds.wind, 5.0f);
		}
	}
	
	override public IEnumerator Activate () {
		yield return StartCoroutine(this.CheckAndWaitForCooldown());
		
		while (this.state == State.none) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.palmleft) {
				this.state = State.detected;
			}
		}
		
		while (this.state == State.detected) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (!right.ring.IsExtended) {
				this.state = State.action;
			}
		}
		
		while (this.state == State.action) {
			yield return StartCoroutine(this.WaitForRightHand());
			if (right.transWave_x_10 > 10) {
				this.TreeCount();
				this.SetCooldown();
			}
		}
	}

}