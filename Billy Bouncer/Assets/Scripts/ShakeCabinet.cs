using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCabinet : MonoBehaviour {

	public static float billyForce;


	public Renderer screenCrackRenderer;
	public Renderer screenCutoutRenderer;

	public Texture2D[] cracks;
	public Texture2D[] cutouts;

	public int dir;
	public Rigidbody cabinet;
	public Transform forcePos;

	private Vector3 baseForce;

	private static int crackInd = -1;
	private int numCollisions;

	public AudioSource crackSource;
	public AudioClip crackClip;

	// Use this for initialization
	void Start () {
		baseForce = forcePos.forward *  cabinet.mass / Time.deltaTime * .0195f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void UpdateForce(float billyPercent) {
		billyForce = .05f + billyPercent * 2;
	}

	void OnCollisionEnter(Collision collision)
	{
		Vector3 force = Vector3.Dot (billyForce * baseForce, collision.relativeVelocity) * new Vector3 (dir, 0, 0);
		cabinet.AddForceAtPosition (force, forcePos.position);

		numCollisions++;
		if (numCollisions > 1) { // enter tha zone
			numCollisions = 0;
			crackInd++;
			if (crackInd == cracks.Length - 1) {
				PlayerController.singleton.EnterGame ();
			} else if (crackInd >= cracks.Length) {
				return;
			}

			screenCrackRenderer.material.mainTexture = cracks [crackInd];
			screenCutoutRenderer.material.mainTexture = cutouts [crackInd];

			screenCrackRenderer.enabled = true;
			screenCutoutRenderer.enabled = true;

			crackSource.PlayOneShot (crackClip);

		}

	}
}
