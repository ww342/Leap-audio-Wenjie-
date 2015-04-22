using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Northforward : MonoBehaviour
{
	public Sounds sounds;
	public Narrator voice;
	private AudioSource sound1_once;
	private Vector3 originalposition = new Vector3 (0, -90, 600);
	private Vector3 far = new Vector3 (0, 0, 800);
	private Vector3 left = new Vector3 (-300, 0, 800);
	private Vector3 right = new Vector3 (300, 0, 800);
	bool farstoneskip ;
	bool leftstoneskip ;
	bool rightstoneskip ;

	private Rigidbody _rigidbody;
	
	public void Awake ()
	{
		this._rigidbody = GetComponent<Rigidbody>();
	}
	
	public void Start ()
	{
		sound1_once = gameObject.AddComponent <AudioSource> ();
		sound1_once.clip = sounds.stoneskipping;
		sound1_once.minDistance = 200;
	}
	
	public void rightskip ()
	{
		sound1_once.Play ();
		rightstoneskip = true;
		sound1_once.pitch = 2;
		Invoke ("goback", 4);
	}

	public void farskip ()
	{
		sound1_once.Play ();
		sound1_once.pitch = 1;
		farstoneskip = true;
		Invoke ("goback", 5);
	}

	public void leftskip ()
	{
		sound1_once.Play ();
		sound1_once.pitch = 3;
		leftstoneskip = true;
		Invoke ("goback", 3);
	}
	
	public void goback ()
	{
		farstoneskip = false;
		leftstoneskip = false;
		rightstoneskip = false;
	}

	// Update is called once per frame
	public void Update ()
	{
		if (farstoneskip == true) {
			_rigidbody.MovePosition (_rigidbody.position + far * Time.deltaTime);
		}
		
		if (farstoneskip == false) {
			_rigidbody.transform.position = originalposition;
		}

		if (leftstoneskip == true) {
			_rigidbody.MovePosition (_rigidbody.position + left * Time.deltaTime);
		}
		
		if (leftstoneskip == false) {
			_rigidbody.transform.position = originalposition;
		}

		if (rightstoneskip == true) {
			_rigidbody.MovePosition (_rigidbody.position + right * Time.deltaTime);
		}
		
		if (rightstoneskip == false) {
			_rigidbody.transform.position = originalposition;
		}
	}
}
