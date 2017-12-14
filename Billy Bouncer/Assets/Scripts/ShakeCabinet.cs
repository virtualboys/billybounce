using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCabinet : MonoBehaviour {

	public int dir;
	public Rigidbody cabinet;
	public Transform forcePos;

	private Vector3 baseForce;

	// Use this for initialization
	void Start () {
		baseForce = new Vector3 (dir * cabinet.mass / Time.deltaTime * .02f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Vector3 force = Vector3.Dot (baseForce, collision.relativeVelocity) * new Vector3 (dir, 0, 0);
		cabinet.AddForceAtPosition (force, forcePos.position);
	}
}
