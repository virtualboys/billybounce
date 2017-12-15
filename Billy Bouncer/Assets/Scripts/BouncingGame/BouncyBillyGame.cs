using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBillyGame : BillyGame {

	public float moveForce;
	public Rigidbody rigidbod;

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
		rigidbod.AddForce (moveForce * rigidbod.mass * dir / Time.deltaTime);
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
