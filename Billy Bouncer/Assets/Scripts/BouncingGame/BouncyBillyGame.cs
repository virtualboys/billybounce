using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBillyGame : BillyGame {

	public static float forceMult;
	public static bool niceHit;

	public float moveForce;
	public Rigidbody rigidbod;
	public AudioSource billySource;
	public AudioClip billyClip;

	public Rigidbody leg1;
	public Rigidbody leg2;

	private const float c_timeToNiceHit = 1f;
	private float niceHitTimer;
	private Vector3 freezePos;
	private float niceHitForce;
	private Vector3 driftDir;

	private Vector3 startPos;
	private Vector3 startRot;
	private int numReds;

	private Transform legParent;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		rigidbod = GetComponentInChildren<Rigidbody> ();
		legParent = leg1.transform.parent;
	}

	public override void TakeInput (int x, int y)
	{
		if (niceHit) {
			if (niceHitTimer <= 0) {
				Vector3 desired = rigidbod.transform.position;
				Vector3 d = desired - rigidbod.transform.position;
				freezePos = desired;
				driftDir = rigidbod.velocity.normalized;
				rigidbod.useGravity = false;
				rigidbod.velocity = 2f * rigidbod.velocity.normalized;
				leg1.velocity = Vector3.zero;
				leg2.velocity = Vector3.zero;
				leg1.transform.position += d;
				leg2.transform.position += d;


				//leg1.isKinematic = true;
//				leg2.isKinematic = true;
				leg1.transform.SetParent (rigidbod.transform);
				leg2.transform.SetParent (rigidbod.transform);
			}
//			iTween.PunchScale (rigidbod.gameObject, 1.5f * Vector3.one, .5f);

			rigidbod.angularVelocity += (new Vector3 (0, 10, 0));

			niceHitTimer = c_timeToNiceHit;

			forceMult = 10;
			niceHit = false;

			niceHitForce += ComputeForceMag ();
		} else {
			// end the nice hit
			ReleaseNiceHit();
			rigidbod.AddForce (ComputeForce (x, y));
		}

		billySource.pitch = Random.Range (0.25f, 0.3f);
		billySource.PlayOneShot (billyClip, 1f);

	}

	Vector3 ComputeForce(int x, int y) {
		Vector3 dir = new Vector3 (x, y, 0);
		dir.Normalize ();
		Vector3 force = ComputeForceMag() * dir;
		return force;
	}

	float ComputeForceMag() {
		float mag = ShakeCabinet.billyForce * forceMult * moveForce * rigidbod.mass / Time.deltaTime;
		forceMult = .3f;
		return mag;
	}

	void LateUpdate() {
		if (niceHitTimer > 0) {
			niceHitTimer -= Time.deltaTime;
			//rigidbod.transform.position = freezePos;
			if (niceHitTimer <= 0) {
				ReleaseNiceHit ();
			}
			//rigidbod.constraints = RigidbodyConstraints.FreezePositionZ;
		}
	}

	public void ReleaseNiceHit() {
		rigidbod.useGravity = true;
		Vector2 dir = Random.insideUnitCircle.normalized;
		rigidbod.AddForce(niceHitForce * new Vector3(dir.x, dir.y, 0));
		niceHitForce = 0;
		niceHitTimer = 0;

		leg1.isKinematic = false;
		leg2.isKinematic = false;
		leg1.transform.SetParent (legParent);
		leg2.transform.SetParent (legParent);
	}

	public override void StartGame ()
	{
		rigidbod.useGravity = true;
	}

	public override void EndGame ()
	{
		rigidbod.useGravity = false;
		rigidbod.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		//out of bounds
		if (rigidbod.transform.localPosition.x > 5 || rigidbod.transform.localPosition.x < -5
		   || rigidbod.transform.localPosition.z > 5.5 || rigidbod.transform.localPosition.z < -5) {
			rigidbod.transform.localPosition = Vector3.zero;
		}

		if (niceHitTimer > 0) {
			rigidbod.transform.RotateAround (rigidbod.transform.right, niceHitForce * Time.deltaTime * .05f);
			//rigidbod.transform.position += .01f * driftDir;
		}
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
