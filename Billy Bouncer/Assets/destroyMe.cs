using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour {
	public AudioSource aSource;
	public AudioClip coinSound;
	public GameObject player;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5);
		player = GameObject.FindGameObjectWithTag("Player");
		//StartCoroutine(DestroyIt(10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "coinslot")
		{
			player.GetComponent<PlayerController>().startTheGame = true;
			aSource.PlayOneShot(coinSound,1f);
		}
	}
   // {
    	// yield return new WaitForSeconds(waitTime);
    	// Destroy(gameObject);
    //}
}
