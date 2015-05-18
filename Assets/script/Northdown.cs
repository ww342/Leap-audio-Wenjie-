using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Northdown : MonoBehaviour
{
	public Sounds sounds;
	public Narrator voice;
	private AudioSource sound1_once;
	private AudioSource sound2_once;
	private AudioSource sound3_once;
	private AudioSource sound4_once;
	private AudioSource sound_loop;
	private Vector3 originalposition = new Vector3 (0, -90, 600);
	private Vector3 speed = new Vector3 (-800, 0, 0);
	bool stoneskip = false;
	
	private Rigidbody _rigidbody;
	
	public void Awake ()
	{
		this._rigidbody = GetComponent<Rigidbody>();
	}
	
	public void Start ()
	{
		sound1_once = gameObject.AddComponent <AudioSource> ();
		sound1_once.clip = sounds.Post_Stone_Correctthrow;
		sound1_once.minDistance = 300;

		sound2_once = gameObject.AddComponent <AudioSource> ();
		sound2_once.clip = sounds.Post_Stone_fishspashing;
		sound2_once.minDistance = 50;

		sound3_once = gameObject.AddComponent <AudioSource> ();
		sound3_once.clip = sounds.Post_Stone_stoneskipping;
		sound3_once.minDistance = 200;

		sound4_once = gameObject.AddComponent <AudioSource> ();
		sound4_once.clip = sounds.Post_Stone_hitthefish;
		sound4_once.pitch = 3;
		sound4_once.minDistance = 200;
	}
	
	public void sound1 ()
	{
		sound1_once.Play ();
	}

	public void sound2 ()
	{
		sound2_once.Play ();
		StartCoroutine(delaysound6(1f));
	}

	public void MidStoneSkip ()
	{
		sound3_once.Play ();
		stoneskip = true;
		sound3_once.pitch = 2;
		StartCoroutine(goback(4f));
	}

	public void SlowStoneSkip ()
	{
		sound3_once.Play ();
		sound3_once.pitch = 1;
		stoneskip = true;
		StartCoroutine(goback(5f));
	}

	public void QuickStoneSkip ()
	{
		sound3_once.Play ();
		sound3_once.pitch = 3;
		stoneskip = true;
		StartCoroutine(goback(3f));
	}

	public void sound6 ()
	{
		sound4_once.Play ();
	}

	public IEnumerator delaysound6 (float delay)
	{
		yield return new WaitForSeconds(delay);
		sound6 ();
	}

	public IEnumerator goback (float delay)
	{
		yield return new WaitForSeconds(delay);
		stoneskip = false;
	}
	// Update is called once per frame
	public void Update ()
	{
		if (stoneskip == true) {
			_rigidbody.MovePosition (_rigidbody.position + speed * Time.deltaTime);
		}
		if (stoneskip == false) {
			_rigidbody.transform.position = originalposition;
		}
	}
}
