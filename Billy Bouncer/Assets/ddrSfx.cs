using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddrSfx : MonoBehaviour {
	public AudioSource aSource;
	public AudioClip[] crashSfx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Floor")
		{
			int rando = Random.Range(0,9);
			aSource.PlayOneShot(crashSfx[rando],1f);
		}
	}
}
