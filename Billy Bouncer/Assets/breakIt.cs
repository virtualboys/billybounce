using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakIt : MonoBehaviour {
	public GameObject bottom;
	public AudioSource aSource;
	public AudioClip[] sfxBreak;
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
			int rando = Random.Range(0,3);
			bottom.transform.parent = null;
			bottom.AddComponent<BoxCollider>();
			bottom.AddComponent<Rigidbody>();
			aSource.PlayOneShot(sfxBreak[rando],1f);
		}
	}
}
