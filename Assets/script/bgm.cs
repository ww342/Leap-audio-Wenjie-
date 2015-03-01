using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class bgm : MonoBehaviour
{

	public Metrics Metrics;
	public AudioClip lake;
	public AudioClip land;


	// Use this for initialization

	void Start ()
	{
		audio.clip = lake;
		audio.minDistance = 1;
		audio.loop = true;
		audio.Play ();
	}

	void Replay ()
	{
		audio.Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Metrics.levelcount == 4) {
			audio.Stop ();
			audio.clip = land;
			audio.loop = false;
			audio.minDistance = 10;
			Replay ();
		}
	}
}
