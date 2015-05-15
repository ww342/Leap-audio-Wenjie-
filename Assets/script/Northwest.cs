using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Northwest : MonoBehaviour
{
	public Sounds sounds;
	public Narrator voice;

	public enum SoundState
	{
		none,
		triggered,
		ing,
		cooldown
	}
	public SoundState Leavefall = SoundState.none;
	private float cooldownTime;
	public float MaxcooldownTime;
	private AudioSource sound1_once;
	private AudioSource sound2_once;
	private AudioSource sound_loop;
	private Vector3 originalposition = new Vector3 (-250, 1110, 520);
	private Vector3 speed = new Vector3 (0, -300, -200);
	bool leavefall = false;

	private Rigidbody _rigidbody;

	public void Awake ()
	{
		this._rigidbody = GetComponent<Rigidbody>();
	}

	public void Start ()
	{
		sound1_once = gameObject.AddComponent <AudioSource> ();
		sound1_once.clip = sounds.Post_Stone_rockhittingleaves;
		sound1_once.minDistance = 100;

		sound2_once = gameObject.AddComponent <AudioSource> ();
		sound2_once.clip = sounds.Post_Tree_leaverustling;
		sound2_once.minDistance = 100;
	}

	public void sound1 ()
	{
		sound1_once.Play ();
		leavefall = true;
		Invoke ("goback", 5);
	}
	
	public void sound2 ()
	{
		sound2_once.Play ();
	}
	
	public void goback ()
	{
		leavefall = false;
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (leavefall == true) {
			_rigidbody.MovePosition (_rigidbody.position + speed * Time.deltaTime);
		}
		
		if (leavefall == false) {
			_rigidbody.transform.position = originalposition;
		}
	}

/*
	public void Update () {
		switch (Leavefall) {
			case SoundState.none:

			    if(leavefall=false){
				rigidbody.transform.position = originalposition;}
				if (leavefall==true) {
				sound1_once.Play ();
				sound2_once.Play ();
				rigidbody.MovePosition (rigidbody.position + speed * Time.deltaTime);
				Leavefall = SoundState.cooldown;
			    }
						break;

				

			case SoundState.cooldown:
			cooldownTime -= Time.deltaTime;
			if (cooldownTime <= 0) {
				leavefall=false;
				Leavefall = SoundState.none;
				cooldownTime = MaxcooldownTime;}
						break;
				}
	}
*/

}
