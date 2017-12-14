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

			BillyController.singleton.Move (x, y);

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

