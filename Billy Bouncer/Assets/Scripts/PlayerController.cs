using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private bool isInZone;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isInZone) {
			int x = (int)Input.GetAxisRaw ("Horizontal");
			int y = (int)Input.GetAxisRaw ("Vertical");

			Vector3 buttonpos = BBRControls.singleton.GetButtonPos (x, y);
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

			BillyController.singleton.Move (movex, movey);

		} else {
			
			if (BBRControls.singleton.IsInRange (transform.position)) {
				EnterControlZone ();
			}
		}
	}

	private void EnterControlZone() {
		isInZone = true;
		GetComponent<FirstPersonDrifter> ().enabled = false;
	}
}

