using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour {
	// audiosources: TODO need to be simplified, and maybe attached to other component on Start!
	public AudioSource audiosource;
	private AudioSource Handfree;
	public AudioSource Environment;
	public AudioSource Ambience_A;
	public AudioSource Ambience_B;
	public AudioSource Ambience_C;
	public AudioSource Ambience_D;
	private AudioSource watch; // attached to /Watch
	private AudioSource transition_hint;
	public AudioSource Gesturehint;

	// specialized sounds scripts:
	public Northdown Northdown;
	public Northeast Northeast;
	public Northforward Northforward;
	public Northwest Northwest;
	public hint hint;

	// the mixer groups used by sounds played here:
	public AudioMixerGroup HintsMix;
	public AudioMixerGroup EnvironmentMix;
	public AudioMixerGroup BackgroundMix;

	// all clips:
	public AudioClip upanddownhint;
	public AudioClip gentlewaterdrop;
	public AudioClip gentlesplash;
	public AudioClip rightsleevelift;
	public AudioClip rightsleevedown;
	public AudioClip transitionhint1;
	public AudioClip transitionhint2;
	public AudioClip transitionhint3;
	public AudioClip slowwatch;
	public AudioClip middlewatch;
	public AudioClip quickwatch;
	public AudioClip watchbeep;
	public AudioClip timetravel;
	public AudioClip stoneskipping;
	public AudioClip rockhitleave;
	public AudioClip snore;
	public AudioClip fish;
	public AudioClip spash;
	public AudioClip lake;
	public AudioClip frog;
	public AudioClip wave;
	public AudioClip duck;
	public AudioClip waterdrop;
	public AudioClip grabstone;
	public AudioClip hitfish;
	public AudioClip stone;
	public AudioClip bird;
	public AudioClip paddle;
	public AudioClip rope;
	public AudioClip star;
	public AudioClip glow;
	public AudioClip paddlewrong;
	public AudioClip rain;
	public AudioClip win;
	public AudioClip flower;
	public AudioClip leave;
	public AudioClip wind;
	public AudioClip grass;
	public AudioClip footstep;
	public AudioClip boat;
	public AudioClip crickets;
	public AudioClip sky;
	public AudioClip lightning;
	public AudioClip shiny;
	public AudioClip lakewaveslapping;
	public AudioClip  fishploppy;
	public AudioClip  wingflpping;
	public AudioClip landonflpping;
	public AudioClip panicflapping;
	public AudioClip weakflapping;
	public AudioClip struggleflapping;
	public AudioClip shortflapping;
	public AudioClip smallflapping;
	public AudioClip seedpouring;
	public AudioClip grabseed;
	public AudioClip birdflyslonghand;
	public AudioClip birdonboat;
	public AudioClip birdlandonboat;
	public AudioClip birdstandonpaddle;
	public AudioClip birdpecking;
	public AudioClip panicfrog;
	public AudioClip panicbird;
	public AudioClip grabbird;
	public AudioClip boatshake;
	public AudioClip boatshake1;
	public AudioClip boatshiffer;
	public AudioClip boatshiffer1;
	public AudioClip boatshiffer2;
	public AudioClip longcreak;
	public AudioClip longlean;
	// finger sounds: TODO: make sure they are played with an AudioSource at the correct location:
	public AudioClip creak1;
	public AudioClip creak2;
	public AudioClip bike;
	public AudioClip brake;
	public AudioClip bell;
	public AudioClip stonedrop;

	
	void Awake ()
	{
		this.audiosource = GetComponent<AudioSource>();
		Environment = gameObject.AddComponent <AudioSource> ();
		//assign RSE as Environment audiosource's outputaudiomixergroup 
		Environment.outputAudioMixerGroup = EnvironmentMix;
		Environment.minDistance = 7;
		this.Northdown = GameObject.Find("/Environment/Northdown").GetComponent<Northdown>();
		this.Northeast = GameObject.Find("/Environment/Northeast").GetComponent<Northeast>();
		this.Northforward = GameObject.Find("/Environment/Northforward").GetComponent<Northforward>();
		this.Northwest = GameObject.Find("/Environment/Northwest").GetComponent<Northwest>();
		this.hint = GameObject.Find("/hint").GetComponent<hint>();
	}

	void Start() {
		Ambience_A = gameObject.AddComponent<AudioSource> ();
		Ambience_A.outputAudioMixerGroup = EnvironmentMix;
		Ambience_A.clip = this.fish;
		Ambience_A.loop = true;
		Ambience_A.minDistance = 10;
		//Ambience_A.Play ();

		Ambience_B = gameObject.AddComponent<AudioSource> ();
		Ambience_B.outputAudioMixerGroup = EnvironmentMix;
		Ambience_B.clip = this.frog;
		Ambience_B.loop = true;
		Ambience_B.minDistance = 10;
		Ambience_B.Play ();
		
		Ambience_C = gameObject.AddComponent <AudioSource> ();
		Ambience_C.outputAudioMixerGroup = EnvironmentMix;
		Ambience_C.clip = this.lakewaveslapping;
		Ambience_C.loop = true;
		Ambience_C.pitch = 1;
		Ambience_C.minDistance = 6;
		Ambience_C.Play ();
		
		Ambience_D = gameObject.AddComponent<AudioSource> ();
		Ambience_D.outputAudioMixerGroup = EnvironmentMix;
		Ambience_D.minDistance = 10;
		
		watch = GameObject.Find("/Watch").GetComponent <AudioSource> ();
		watch.outputAudioMixerGroup = HintsMix;
		watch.clip = this.slowwatch;
		watch.loop = true;
		watch.minDistance = 30;
		watch.Play ();
		
		transition_hint = gameObject.AddComponent <AudioSource> ();
		transition_hint.outputAudioMixerGroup = HintsMix;
		//transtion_hint.clip  = hint.Stone_correct_hint0;
		transition_hint.clip = this.transitionhint1;
		//transtion_hint .loop = true;
		//transtion_hint.pitch = 1;
		transition_hint.minDistance = 2;

		Gesturehint = gameObject.AddComponent <AudioSource> ();
		Gesturehint.outputAudioMixerGroup = HintsMix;
		Gesturehint.minDistance = 1;
	}


	public void InitialSetup() {
		// default background sound before anything starts:
		Handfree = gameObject.AddComponent<AudioSource> ();
		Handfree.outputAudioMixerGroup = BackgroundMix;
		Handfree.clip = this.snore;
		Handfree.loop = true;
		Handfree.pitch = 2;
		Handfree.minDistance = 8;
		Handfree.Play ();
	}

	public void StopInitialSetup() {
		Handfree.Stop ();
		Handfree.loop = false;
	}

	public void quickenwatch ()
	{
		watch.clip = this.quickwatch;
		watch.Play ();
	}
	
	public void transitwatch ()
	{
		watch.clip = this.middlewatch;
		watch.Play ();
	}
	
	public void normalwatch ()
	{
		watch.clip = this.slowwatch;
		watch.Play ();
	}
	
	public void watchstop ()
	{
		watch.Stop ();
	}
	
	public void hint1 ()
	{
		transition_hint.clip = this.transitionhint1;
		transition_hint.Play ();
	}
	
	public void hint2 ()
	{
		transition_hint.clip = this.transitionhint2;
		transition_hint.Play ();
	}
	
	public void hint3 ()
	{
		transition_hint.clip = this.transitionhint3;
		transition_hint.Play ();
	}
	
	public void Glow ()
	{
		Ambience_D.PlayOneShot (this.glow);
	}
	
	public void Timetravel ()
	{
		Ambience_D.PlayOneShot (this.timetravel, 2.0f);
	}
	
}
