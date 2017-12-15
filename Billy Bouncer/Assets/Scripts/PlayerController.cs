using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public BBRControls[] gameControls;

	private BBRControls controls;
	private FirstPersonDrifter fpd;

	private bool isLeaving;
	public bool startTheGame = false;

	// Use this for initialization
	void Start ()
	{
		fpd = GetComponent<FirstPersonDrifter> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
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

			int x = (int)Input.GetAxisRaw ("Horizontal");
			int y = (int)Input.GetAxisRaw ("Vertical");

			Vector3 buttonpos = controls.GetButtonPos (x, y);
			transform.position = new Vector3 (buttonpos.x, transform.position.y, buttonpos.z);

			int movex = 0;
			int movey = 0;

			if (Input.GetButtonDown ("Up")) {
				movey = 1;
			} else if (Input.GetButtonDown ("Down")) {
				movey = -1;
			}
			if (Input.GetButtonDown ("Left")) {
				movex = -1;
			} else if (Input.GetButtonDown ("Right")) {
				movex = 1;
			}

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

	private void EnterControlZone(BBRControls machine) {
		controls = machine;
		fpd.enabled = false;
	}

	private void ExitControlZone() {
		fpd.enabled = true;
	}
}

