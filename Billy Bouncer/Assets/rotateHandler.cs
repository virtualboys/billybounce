using UnityEngine;
using System.Collections;

public class rotateHandler : MonoBehaviour {

	public bool up,down,left,right,forward,back = false;
	public float speed;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.right * Time.deltaTime * 17);
		if (down == true) {
			transform.Rotate (Vector3.down * Time.deltaTime * speed);
		}
		if (up == true) {
			transform.Rotate (Vector3.up * Time.deltaTime * speed);
		}
		if (left == true) {
			transform.Rotate (Vector3.left * Time.deltaTime * speed);
		}
		if (right == true) {
			transform.Rotate (Vector3.right * Time.deltaTime * speed);
		}
		if (forward == true) {
			transform.Rotate (Vector3.forward * Time.deltaTime * speed);
		}
		if (back == true) {
			transform.Rotate (Vector3.back * Time.deltaTime * speed);
		}
	}
}
