using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTile : MonoBehaviour {

	public GameObject greenTile;
	public BouncyBillyGame game;

	// Use this for initialization
	void Start () {
		game.RegisterRedTile ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		game.RemoveRedTile ();

		GameObject green = GameObject.Instantiate (greenTile, transform.position, 
			transform.rotation, transform.parent) as GameObject;
		
		ShakeCabinet oldshake = GetComponent<ShakeCabinet> ();
		ShakeCabinet newshake = green.GetComponent<ShakeCabinet>();
		newshake.cabinet = oldshake.cabinet;
		newshake.dir = oldshake.dir;
		newshake.forcePos = oldshake.forcePos;

		GameObject.Destroy (gameObject);
	}
}
