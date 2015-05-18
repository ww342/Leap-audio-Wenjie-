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
	public Hints Hints;
	public bgm BackgroundMusic;

	// the mixer groups used by sounds played here:
	public AudioMixerGroup HintsMix;
	public AudioMixerGroup EnvironmentMix;
	public AudioMixerGroup BackgroundMix;
	
	[Header("Ambience sounds")]
	public AudioClip Ambience_Boat_floatcreaks;
	public AudioClip Ambience_cavewaterdripping;
	public AudioClip Ambience_crickets;
	public AudioClip Ambience_fishswimming;
	public AudioClip Ambience_frog;
	public AudioClip Ambience_grassinthewind;
	public AudioClip Ambience_meteorshower;
	public AudioClip Ambience_space;
	public AudioClip Ambience_wavelapping;
	public AudioClip Ambience_thunder;

	[Header("Stone gesture sounds")]
	public AudioClip Pre_Stone_snore;
	public AudioClip Dur_Stone_grabstone;
	public AudioClip Dur_Stone_rightsleevedown;
	public AudioClip Dur_Stone_rightsleevelift;
	public AudioClip Dur_Stone_gentlewaterdrop;
	public AudioClip Dur_Stone_gentlesplash;
	public AudioClip Post_Stone_dropontheboat;
	public AudioClip Post_Stone_Correctthrow;
	public AudioClip Post_Stone_Fishjump_LefttoRight;
	public AudioClip Post_Stone_Fishjump_righttoleft;
	public AudioClip Post_Stone_fishspashing;
	public AudioClip Post_Stone_hitthefish;
	public AudioClip Post_Stone_longcreak;
	public AudioClip Post_Stone_rockhittingleaves;
	public AudioClip Post_Stone_stoneskipping;
	public AudioClip Post_Stone_waterdrop;
	public AudioClip Post_Stone_longlean;

	[Header("Flower sounds")]
	public AudioClip Pre_Flower_duck;
	public AudioClip Dur_Flower_pinch;
	public AudioClip Post_Flower_sparkle;

	[Header("Bird sounds")]
	public AudioClip Pre_Bird_birdflyin;
	public AudioClip Pre_Bird_boatshiffer1;
	public AudioClip Pre_Bird_boatshiffer2;
	public AudioClip Pre_Bird_landonflapping;
	public AudioClip Pre_Bird_pecking;
	public AudioClip Dur_Bird_grabseed;
	public AudioClip Dur_Bird_longflapping;
	public AudioClip Dur_Bird_onehandsqueeze;
	public AudioClip Dur_Bird_Panicflapping1;
	public AudioClip Dur_Bird_Panicflapping2;
	public AudioClip Dur_Bird_Panicfrog;
	public AudioClip Dur_Bird_flyalonghand;
	public AudioClip Dur_Bird_shortflapping;
	public AudioClip Dur_Bird_smallflapping;
	public AudioClip Dur_Bird_struggleflapping;
	public AudioClip Dur_Bird_twohandgrabbing;
	public AudioClip Dur_Bird_weakflpping;
	public AudioClip Post_Bird_onehandcatch;
	public AudioClip Post_Bird_paniccry;
	public AudioClip Post_Bird_seedpouring;
	public AudioClip Post_Bird_twohandcatch;

	[Header("Paddle sounds")]
	public AudioClip Pre_Paddle_thunder_rain;
	public AudioClip Dur_Paddle_Boat_shake1;
	public AudioClip Dur_Paddle_Boat_shake2;
	public AudioClip Dur_Paddle_droppaddle_response01;
	public AudioClip Dur_Paddle_droppaddle_response02;
	public AudioClip Dur_Paddle_row1;
	public AudioClip Dur_Paddle_row2;
	public AudioClip Dur_Paddle_Boat_shiffer;
	public AudioClip Dur_Paddle_creak1;
	public AudioClip Dur_Paddle_creak2;
	public AudioClip Dur_Paddle_grabbing;
	public AudioClip Post_Paddle_rowing;
	public AudioClip Post_Paddle_wave;

	[Header("Tree sounds")]
	public AudioClip Pre_Tree;
	public AudioClip Dur_Tree;
	public AudioClip Post_Tree_branchshaking;
	public AudioClip Post_Tree_leaverustling;

	[Header("Rope sounds")]
	public AudioClip Pre_Rope;
	public AudioClip Dur_Rope_creaks;
	public AudioClip Post_Rope;

	[Header("Bike sounds")]
	public AudioClip Pre_Bike_grassfootstep;
	public AudioClip Dur_Bike_brake;
	public AudioClip Dur_Bike_fall;
	public AudioClip Dur_Bike_wheelslowdown;
	public AudioClip Post_Bike_belltrimble;
	public AudioClip Post_Bike_twohandles;

	[Header("Star sounds")]
	public AudioClip Pre_Star;
	public AudioClip Dur_Star_starrain;
	public AudioClip Post_Star_shiny;
	public AudioClip Post_Star_glow;
	public AudioClip Post_Star_timetravel;

	[Header("Watch sounds")]
	public AudioClip Dur_Watch_middlespeed;
	public AudioClip Dur_Watch_slowspeed;
	public AudioClip Dur_Watch_fastspeed;
	public AudioClip Post_Watch_watchbeep;
	public AudioClip Dur_Ringinghint_01;
	public AudioClip Dur_Ringinghint_02;
	public AudioClip Dur_Ringinghint_03;





	public ReverbControl ReverbControl;
	
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
		this.Hints = GameObject.Find("/Hints").GetComponent<Hints>();
		this.BackgroundMusic = GameObject.Find("/Environment/BackgroundMusic").GetComponent<bgm>();
	}

	void Start() {
		Ambience_A = gameObject.AddComponent<AudioSource> ();
		Ambience_A.outputAudioMixerGroup = EnvironmentMix;
		Ambience_A.clip = Ambience_fishswimming;
		Ambience_A.loop = true;
		Ambience_A.minDistance = 10;
		//Ambience_A.Play ();

		Ambience_B = gameObject.AddComponent<AudioSource> ();
		Ambience_B.outputAudioMixerGroup = EnvironmentMix;
		Ambience_B.clip = Ambience_frog;
		Ambience_B.loop = true;
		Ambience_B.minDistance = 10;
		Ambience_B.Play ();
		
		Ambience_C = gameObject.AddComponent <AudioSource> ();
		Ambience_C.outputAudioMixerGroup = EnvironmentMix;
		Ambience_C.clip = Ambience_wavelapping;
		Ambience_C.loop = true;
		Ambience_C.pitch = 1;
		Ambience_C.minDistance = 6;
		Ambience_C.Play ();
		
		Ambience_D = gameObject.AddComponent<AudioSource> ();
		Ambience_D.outputAudioMixerGroup = EnvironmentMix;
		Ambience_D.minDistance = 10;
		
		watch = GameObject.Find("/Watch").GetComponent <AudioSource> ();
		watch.outputAudioMixerGroup = HintsMix;
		watch.clip = Dur_Watch_slowspeed;
		watch.loop = true;
		watch.minDistance = 30;
		watch.Play ();
		
		transition_hint = gameObject.AddComponent <AudioSource> ();
		transition_hint.outputAudioMixerGroup = HintsMix;
		//transtion_hint.clip  = Hints.Stone_correct_hint0;
		transition_hint.clip = Dur_Ringinghint_01;
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
		Handfree.clip = Pre_Stone_snore;
		Handfree.loop = true;
		Handfree.pitch = 2;
		Handfree.minDistance = 8;
		Handfree.Play ();
		ReverbControl.BlendSnapShot (0);
	}

	public void StopInitialSetup() {
		Handfree.Stop ();
		Handfree.loop = false;
	}

	public void quickenwatch ()
	{
		watch.clip = Dur_Watch_fastspeed;
		watch.Play ();
	}
	
	public void transitwatch ()
	{
		watch.clip = Dur_Watch_middlespeed;
		watch.Play ();
	}
	
	public void normalwatch ()
	{
		watch.clip = Dur_Watch_slowspeed;
		watch.Play ();
	}
	
	public void watchstop ()
	{
		watch.Stop ();
	}
	
	public void hint1 ()
	{
		transition_hint.clip = Dur_Ringinghint_01;
		transition_hint.Play ();
	}
	
	public void hint2 ()
	{
		transition_hint.clip = Dur_Ringinghint_02;
		transition_hint.Play ();
	}
	
	public void hint3 ()
	{
		transition_hint.clip = Dur_Ringinghint_03;
		transition_hint.Play ();
	}
	
	public void Glow ()
	{
		Ambience_D.PlayOneShot (Post_Star_glow);
	}
	
	public void Timetravel ()
	{
		Ambience_D.PlayOneShot (Post_Star_timetravel, 2.0f);
	}


	public void ChangeBackgroundMusic ()
	{
		BackgroundMusic._audio.Stop ();
		BackgroundMusic._audio.clip = BackgroundMusic.land;
		BackgroundMusic._audio.loop = false;
		BackgroundMusic._audio.minDistance = 10;
	}

	public void RestartBackgroundMusic ()
	{
		BackgroundMusic._audio.Play ();
	}

	public void PlayIfPossible(AudioSource audiosource, AudioClip clip)
	{
		if (! audiosource.isPlaying) {
			audiosource.clip = clip;
			audiosource.Play();
		}
	}
	
	public void PlayImmediately(AudioSource audiosource, AudioClip clip)
	{
		if (audiosource.isPlaying) {
			if (audiosource.clip == clip) {
				return;
			}
			audiosource.Stop();
		}
		audiosource.clip = clip;
		audiosource.Play();
	}
}
