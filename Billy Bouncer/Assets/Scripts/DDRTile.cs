using System;
using UnityEngine;

[ExecuteInEditMode]
public class DDRTile : MonoBehaviour
{
	public float speed;
	public float maxY;

	void Start() {

	}

	void Update() {
		float t = Time.deltaTime;
		if (t == 0) {
			t = 1 / 60.0f;
		}
		transform.position += Vector3.up * speed * t;
		if (transform.localPosition.y > maxY) {
			GameObject.DestroyImmediate (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);

		PlayerController.singleton.rigidbod.AddForce (Vector3.up * 300);
	}

	void OnCollisionEnter(Collision collision)
	{
		//DestroyImmediate(gameObject);
		Debug.Log ("Bounce");

		PlayerController.singleton.fpd.BounceUp (100);
	

	}
}

