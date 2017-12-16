using UnityEngine;

public class DDRTile : MonoBehaviour
{
	public float maxY;
	public Renderer rend;
	public Color greenColor;

	private bool isGreen;

	const float disableTime = .3f;
	float timer;

	void Start() {
		if (Random.value > .8f) {
			isGreen = true;
			rend.material.color = greenColor;
		}
	}

	void Update() {
		float t = Time.deltaTime;
		if (t == 0) {
			t = 1 / 60.0f;
		}
		if (timer > 0) {
			timer -= t;
		}

		transform.position += Vector3.up * DDRGame.singleton.speed * t;
		if (transform.localPosition.y > maxY) {
			GameObject.DestroyImmediate (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (timer > 0) {
			return;
		}

		if (!isGreen) {
			DDRGame.singleton.HitRed ();
			rend.material.color = greenColor;
			isGreen = true;
			timer = disableTime;
		} else {
			DDRGame.singleton.HitGreen ();
		}
	}

	/*void OnCollisionEnter(Collision collision)
	{
		//DestroyImmediate(gameObject);
		Debug.Log ("Bounce");

		PlayerController.singleton.fpd.BounceUp (100);
	

	}*/
}

