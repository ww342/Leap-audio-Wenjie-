using UnityEngine;
using System.Collections;
using UnityEngine.Audio ;

public class ReverbControl : MonoBehaviour {

	public AudioMixerSnapshot[] snapshots;
	public AudioMixer mixer;
	public float[] weights;



	// Use this for initialization
	void Start () {
	
	}

	public void BlendSnapShot(int ReverbNum){

		switch (ReverbNum) {

				case 4:
						weights [0] = 1.0f;
						weights [1] = 0.0f;
						mixer.TransitionToSnapshots (snapshots, weights, 2.0f);
						break;
				case 3:
						weights [0] = .75f;
						weights [1] = .25f;
						mixer.TransitionToSnapshots (snapshots, weights, 2.0f);
						break;
				case 2:
						weights [0] = 0.5f;
						weights [1] = 0.5f;
						mixer.TransitionToSnapshots (snapshots, weights, 2.0f);
						break;
				case 1:
						weights [0] = .25f;
						weights [1] = .75f;
						mixer.TransitionToSnapshots (snapshots, weights, 2.0f);
						break;
				case 0:
						weights [0] = 0.0f;
						weights [1] = 1.0f;
						mixer.TransitionToSnapshots (snapshots, weights, 2.0f);
						break;
				}


		}


		}
	


