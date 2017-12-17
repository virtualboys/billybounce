using UnityEngine;
using System.Collections;

public class BBRControls : MonoBehaviour
{
	public BillyGame game;
	public float zoneRange;
	public float buttonDist;
	public Transform enterGamePos;

	void Awake() {
	}

	// Use this for initialization
	void Start ()
	{
		PlayerController.singleton.gameControls.Add (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool IsInRange(Vector3 pos) {
		return Vec.IsInRangeIgnoreY (pos, transform.position, zoneRange);
	}

	public bool IsOutOfRange(Vector3 pos) {
		return !Vec.IsInRangeIgnoreY (pos, transform.position, zoneRange * 1.3f);
	}

	public Vector3 GetButtonPos(int x, int y) {
		return transform.position + transform.right * buttonDist * x + transform.forward * buttonDist * y;
	}
}

