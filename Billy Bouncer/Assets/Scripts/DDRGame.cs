using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRGame : MonoBehaviour {

	public static DDRGame singleton;

	public DDRTile cubePrefab;
	public float bpm;
	public Transform spawnPos;
	public Transform billyPos;
	public float spread;
	public float speed;

	public AudioClip gruntClip;
	public AudioSource playerSource;
	public AudioSource music;

	private float timer;
	private bool isRunning;

	private int streak;

	void Awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (!isRunning) {
		//	return;
		//}
		float t = Time.deltaTime;
		if (t == 0) {
			t = 1 / 60.0f;
		}

		timer += t;
		if (timer > 60.0f / bpm) {
			timer = 0;
			int x = Random.Range (-1, 2);
			int y = Random.Range (-1, 2);
			Vector3 pos = spawnPos.position + Vector3.right * x * spread + Vector3.forward * y * spread;
			GameObject newcube = GameObject.Instantiate (cubePrefab.gameObject, pos, Quaternion.identity) as GameObject;
			newcube.transform.SetParent (transform);
		}
	}
		
	public void StartGame() {
		isRunning = true;
	}

	public void HitGreen() {
		playerSource.PlayOneShot (gruntClip);
		streak = 0;
	}

	public void HitRed() {
		PlayerController.singleton.fpd.BounceUp (50);
		streak++;
		bpm += 10;
		speed += bpm / 60.0f;
		music.pitch += .1f;
	}
}
