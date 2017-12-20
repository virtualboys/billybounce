using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBillyGame : BillyGame {

	public static float forceMult;

	public float moveForce;
	public Rigidbody rigidbod;
	public AudioSource billySource;
	public AudioClip billyClip;

	private Vector3 startPos;
	private Vector3 startRot;
	private int numReds;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		rigidbod = GetComponentInChildren<Rigidbody> ();
	}

	public override void TakeInput (int x, int y)
	{
		Vector3 dir = new Vector3 (x, y, 0);
		dir.Normalize ();
		rigidbod.AddForce (ShakeCabinet.billyForce * forceMult * moveForce * rigidbod.mass * dir / Time.deltaTime);
		if (x != 0 || y != 0) {
			billySource.pitch = Random.Range (0.25f, 0.3f);
			billySource.PlayOneShot (billyClip, 1f);
		}
		forceMult = .3f;
	}

	public override void StartGame ()
	{
		rigidbod.useGravity = true;
	}

	public override void EndGame ()
	{
		rigidbod.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RegisterRedTile() {
		numReds++;
	}

	public void RemoveRedTile() {
		numReds--;
		if (numReds == 0) {
			Debug.Log ("You win!");
		}
	}
}
