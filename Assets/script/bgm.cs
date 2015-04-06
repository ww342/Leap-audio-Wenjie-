using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class bgm : MonoBehaviour
{
	public Metrics Metrics;
	public AudioClip lake;
	public AudioClip land;


	private AudioSource _audio;

	//Audio Mixer Groups
	public AudioMixerGroup  Master;
	public AudioMixer  AudioMixer1;

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
//	void SetMastervol(){
//		if (Metrics.Nar_Check) {
//			AudioMixer1.SetFloat("Master_vol", 3);
//		}
//		if (!Metrics.Nar_Check) {
//			AudioMixer1.SetFloat("Master_vol", 10);
//		}
//		}
	// Update is called once per frame
	void Update ()
	{
		if (Metrics.Nar_Check) {
			AudioMixer1.SetFloat("Master-vol", 2);
		}
		if (!Metrics.Nar_Check) {
			AudioMixer1.SetFloat("Master-vol", 10);
		}

		if (Metrics.levelcount == 4) {
			_audio.Stop ();
			_audio.clip = land;
			_audio.loop = false;
			_audio.minDistance = 10;
			Replay ();
		}
	}
}
