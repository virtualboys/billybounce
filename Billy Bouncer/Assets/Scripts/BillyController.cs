using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyController : MonoBehaviour {

	public static BillyController singleton;

	public float moveForce;
	public Rigidbody rigidbod;

	private float startZ;

	void Awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start () {
		rigidbod = GetComponentInChildren<Rigidbody> ();
		startZ = transform.position.z;
	}

	public void Move(int x, int y) {
		Vector3 dir = new Vector3 (x, y, 0);
		dir.Normalize ();
		rigidbod.AddForce (moveForce * Time.deltaTime * rigidbod.mass * dir);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		//transform.position = Vec.SetZ (transform.position, startZ);
	}
}
