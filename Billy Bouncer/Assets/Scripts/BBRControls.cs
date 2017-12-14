using UnityEngine;
using System.Collections;

public class BBRControls : MonoBehaviour
{
	public static BBRControls singleton;

	public float zoneRange;
	public float buttonDist;

	void Awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool IsInRange(Vector3 pos) {
		return Vec.IsInRangeIgnoreY (pos, transform.position, zoneRange);
	}

	public Vector3 GetButtonPos(int x, int y) {
		return transform.position + new Vector3 (buttonDist * x, 0, buttonDist * y);
	}
}

