using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public static PlayerController singleton;

	public List<BBRControls> gameControls;

	public GameObject billySong;

	public Rigidbody rigidbod;

	
	public AudioClip paranoia;

	private BBRControls controls;
	public FirstPersonDrifter fpd;
	private HeadBob headBob;

	private bool isLeaving;
	public bool startTheGame = false;

	private bool isInGame;

	private int posX, posY;

	void Awake() {
		singleton = this;

		gameControls = new List<BBRControls> ();
	}

	// Use this for initialization
	void Start ()
	{
		fpd = GetComponent<FirstPersonDrifter> ();
		headBob = GetComponentInChildren<HeadBob> ();



		//EnterGame ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (controls == null) {
			controls = gameControls [0];
			controls.game.StartGame ();
		}

		if (isInGame) {
			UpdateGame ();
			return;
		}

		if (controls != null) {
			// get out of range before entering a new control zone
			if (isLeaving) {
				if (controls.IsOutOfRange (transform.position)) {
					controls = null;
					isLeaving = false;
				}
				return;
			}

			if (Input.GetKeyDown (KeyCode.E)) {
				ExitControlZone ();
				isLeaving = true;
				return;
			}

			if (Input.GetKeyDown (KeyCode.R)) {
				controls.game.EndGame ();
				controls.game.StartGame ();
			}

			int x = (int)Input.GetAxisRaw ("Horizontal");
			int y = (int)Input.GetAxisRaw ("Vertical");

			Vector3 buttonpos = controls.GetButtonPos (x, y);
			transform.position = new Vector3 (buttonpos.x, transform.position.y, buttonpos.z);

			int movex = 0;
			int movey = 0;
			GetInputDown (out movex, out movey);


			controls.game.TakeInput (movex, movey);

		} else {
			foreach (BBRControls machine in gameControls) {
				if (machine.IsInRange (transform.position)) {
					if(startTheGame == true)
					EnterControlZone (machine);
				}
			}

		}
	}

	void UpdateGame() {
		int x = (int)Input.GetAxisRaw ("Horizontal");
		int y = (int)Input.GetAxisRaw ("Vertical");

		if (x != posX || y != posY) {
			posX = x;
			posY = y;

			//Vector3 pos = Vec.SetY (DDRGame.singleton.billyPos.position + DDRGame.singleton.spread * new Vector3 (x, 0, y), transform.position.y);
			//transform.position = pos;
			//iTween.MoveTo (gameObject, iTween.Hash ("position", pos, "time", .02f, "easeType", 
			//	iTween.EaseType.easeInOutCubic));
		}
	}

	private void GetInputDown(out int x, out int y) {
		x = 0;
		y = 0;
		if (Input.GetButtonDown ("Up")) {
			y = 1;
		}
		if (Input.GetButtonDown ("Down")) {
			y = (y == 0) ? -1 : 2;
		}
		if (Input.GetButtonDown ("Left")) {
			x = -1;
		} 
		if (Input.GetButtonDown ("Right")) {
			x = (x == 0) ? 1 : 2;
		}
	}

	public void EnterGame() {
		fpd.enabled = false;
		fpd.walkSpeed = 70;
		//fpd.disableInput = true;
		headBob.enabled = false;


		//rigidbod.isKinematic = false;
		//rigidbod.useGravity = true;

		//ExitControlZone ();
		isInGame = true;

		if (controls != null) {
			controls.game.EndGame ();
			iTween.MoveTo (gameObject, iTween.Hash ("position", controls.enterGamePos, "time", 5.2f, "easeType", 
				iTween.EaseType.easeInCubic, "delay", .4, "oncomplete", "GoToDDRGame", "oncompletetarget", gameObject));
		} else {
			GoToDDRGame ();
		}


	}

	void GoToDDRGame() {
		transform.position = DDRGame.singleton.billyPos.position;
		billySong.GetComponent<AudioSource>().clip = paranoia;
		billySong.transform.SetParent (transform);
		billySong.transform.localPosition = Vector3.zero;
		//fpd.disableInput = true;
		//fpd.enabled = true;
		//billySong.SetActive(true);
		//billySong.GetComponent<AudioSource>().clip = paranoia;
		//aSource.clip = paranoia;
		fpd.walkSpeed = 40;
		fpd.jumpSpeed = 40;
		ExitControlZone();
		fpd.gravity = 35f;
	}

	private void EnterControlZone(BBRControls machine) {
		transform.SetParent (machine.transform);
		controls = machine;
		fpd.enabled = false;
		headBob.enabled = false;
		machine.game.StartGame ();
	}

	private void ExitControlZone() {
		fpd.enabled = true;
		headBob.enabled = true;
		controls.game.EndGame ();
	}
}

