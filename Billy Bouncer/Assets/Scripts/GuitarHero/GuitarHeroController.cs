using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuitarHeroController : BillyGame
{
	// 0 is left, 1 is down, 2 is up, 3 is right

	public static GuitarHeroController singleton;

	public BillyGame otherBillyGame;

	public int numRows;
	public float bpm;

	public float noteSpread;

	public GameObject[] arrowInputs;
	public GameObject[] arrowPrefabs;
	public GameObject rowPrefab;
	public Transform rowParent;

	public Transform top;
	public Transform bottom;
	public Transform hitzone;

	private Queue<RowController> rows;

	public float speed { get; private set; }

	private float length;
	private float timeToSpawn;

	private float spawnTimer;

	private bool isRunning;

	private int[] arrowInds = { 1, 2, 3, 4 };

	void Awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start ()
	{
		length = top.localPosition.y - bottom.localPosition.y;
		rows = new Queue<RowController> ();
		RecomputeVars ();
	}

	public override void StartGame ()
	{
		isRunning = true;
		otherBillyGame.StartGame ();
	}

	public override void EndGame ()
	{
		isRunning = false;
		otherBillyGame.EndGame ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isRunning) {
			return;
		}

		spawnTimer += Time.deltaTime;
		if (spawnTimer > timeToSpawn) {
			SpawnRow ();
			spawnTimer = 0;
		}
	}

	public override void TakeInput (int x, int y)
	{
		if (x == -1) {
			TakeSingleInput (0);
		} else if (x == 1) {
			TakeSingleInput (3);
		}

		if (y == -1) {
			TakeSingleInput (1);
		} else if (y == 1) {
			TakeSingleInput (2);
		}

		otherBillyGame.TakeInput (x, y);
	}

	void TakeSingleInput(int ind) {
		InputAnim (ind);

		if (rows.Count != 0) {
			RowController rc = rows.Peek ();
			if (rc.IsInRange () && rc.TryHit (ind)) {
				Hit ();
			} else {
				Miss ();
			}
		}
	}

	void InputAnim(int ind) {
		iTween.PunchScale (arrowInputs [ind], 1.5f * Vector3.one, .2f);
	}

	void Hit() {

	}

	void Miss() {

	}

	void SpawnRow() {
		int numArrows = Random.Range (0, 2);

		if (numArrows == 0) {
			return;
		}

		RowController newRow = (GameObject.Instantiate (rowPrefab, top.position, Quaternion.identity, rowParent)
			as GameObject).GetComponent<RowController> ();
		
		rows.Enqueue (newRow);

		if (numArrows == 1) {
			int arrowInd = Random.Range (0, 4);
			Arrow newArrow = SpawnArrow (arrowInd, newRow.transform);
			newRow.AddArrow (newArrow);

		} else {
			//List<int> inds = new List<int> (arrowInds);
		}
	}

	Arrow SpawnArrow(int ind, Transform parent) {
		
		float offset;

		// left
		if (ind == 0) {
			offset = -1.5f * noteSpread;
		// down
		} else if (ind == 1) {
			offset = -.5f * noteSpread;
		// right
		} else if (ind == 3) {
			offset = 1.5f * noteSpread;
		// up
		} else {
			offset = .5f * noteSpread;
		}

		GameObject newArrow = GameObject.Instantiate(arrowPrefabs[ind], parent) as GameObject;
		//newArrow.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, rot));
		newArrow.transform.localPosition += offset * Vector3.right;

		return newArrow.GetComponent<Arrow> ();
	}

	void RecomputeVars() {
		timeToSpawn = 60.0f / bpm;
		speed = (length / numRows) / timeToSpawn;
	}

	public void DestroyRow() {
		RowController rc = rows.Dequeue ();
		GameObject.Destroy (rc.gameObject);
	}

}

