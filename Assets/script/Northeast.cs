﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Northeast : MonoBehaviour
{
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
		sound1_once.clip = sounds.Post_Tree_leaverustling;
		sound1_once.minDistance = 50;

		sound2_once = gameObject.AddComponent <AudioSource> ();
		sound2_once.clip = sounds.Post_Stone_rockhittingleaves;
		sound2_once.minDistance = 50;
	}
	
	public void sound1 ()
	{
		sound1_once.Play ();
		leavefall = true;
		StartCoroutine(goback(5f));
	}

	public void sound2 ()
	{
		sound2_once.Play ();
	}

	public IEnumerator goback (float delay)
	{
		yield return new WaitForSeconds(delay);
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
