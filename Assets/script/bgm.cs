using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class bgm : MonoBehaviour
{
	public Metrics Metrics;
	public AudioClip lake;
	public AudioClip land;

	private AudioSource _audio;

	void Awake ()
	{
		this._audio = GetComponent<AudioSource>();
	}

	void Start ()
	{
		_audio.clip = lake;
		_audio.minDistance = 1;
		_audio.loop = true;
		_audio.Play ();
	}

	void Replay ()
	{
		_audio.Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Metrics.levelcount == 4) {
			_audio.Stop ();
			_audio.clip = land;
			_audio.loop = false;
			_audio.minDistance = 10;
			Replay ();
		}
	}
}
