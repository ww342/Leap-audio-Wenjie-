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
	public AudioMixerGroup BackgroundMix;
	public AudioMixer  AudioMixer1;

	void Awake ()
	{
		this._audio = GetComponent<AudioSource>();
		this._audio.outputAudioMixerGroup = BackgroundMix;
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

	void SetMastervol() {
		// TODO: I assume this meas that the background musix should be lower when the narrator speaks?
		// should be done with snapshots (and Master_vol does not seem to exist as a control)
		return;
		if (Metrics.Nar_Check) {
			AudioMixer1.SetFloat("Master_vol", 2);
		} else {
			AudioMixer1.SetFloat("Master_vol", 10);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		SetMastervol();

		if (Metrics.levelcount == 4) {
			_audio.Stop ();
			_audio.clip = land;
			_audio.loop = false;
			_audio.minDistance = 10;
			Replay ();
		}
	}
}
