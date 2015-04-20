using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Northeast : MonoBehaviour
{
	public Hands script;
	public Sounds sounds;
	public Narrator voice;
	private AudioSource sound1_once;
	private AudioSource sound2_once;
	private AudioSource sound_loop;
	private Vector3 originalposition = new Vector3 (250, 1110, 520);
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
		sound1_once.clip = sounds.leave;
		sound1_once.minDistance = 50;

		sound2_once = gameObject.AddComponent <AudioSource> ();
		sound2_once.clip = sounds.rockhitleave;
		sound2_once.minDistance = 50;
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
}
