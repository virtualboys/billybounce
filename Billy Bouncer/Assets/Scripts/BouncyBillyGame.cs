using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBillyGame : BillyGame {

	public float moveForce;
	public Rigidbody rigidbod;

	private float startZ;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		rigidbod = GetComponentInChildren<Rigidbody> ();
		startZ = transform.position.z;
	}

	public override void TakeInput (int x, int y)
	{
		Vector3 dir = new Vector3 (x, y, 0);
		dir.Normalize ();
		rigidbod.AddForce (moveForce * rigidbod.mass * dir / Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		//transform.position = Vec.SetZ (transform.position, startZ);
	}
}
