using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plushSound : MonoBehaviour {
	public AudioSource Asource;
	public AudioClip sfxSqueak;
	public bool bigBilly = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(bigBilly == false)
			{
				Asource.pitch = Random.Range(0.65f,0.9f);
				Asource.PlayOneShot(sfxSqueak,1f);
			}
			else
			{
				Asource.pitch = Random.Range(0.25f,0.3f);
				Asource.PlayOneShot(sfxSqueak,1f);
			}
		}
	}
}
