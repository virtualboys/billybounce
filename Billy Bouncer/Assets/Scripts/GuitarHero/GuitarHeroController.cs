using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuitarHeroController : BillyGame
{
	// 0 is left, 1 is down, 2 is up, 3 is right

	public static GuitarHeroController singleton;

	public BouncyBillyGame otherBillyGame;

	public float rowHeight;
	public float startingbpm;
	public float startDelay;

	public float noteSpread;

	public GameObject[] arrowInputs;
	public GameObject[] arrowPrefabs;
	public GameObject rowPrefab;
	public Transform rowParent;

	public AudioSource musicSource;
	public AudioSource sfxSource;

	public AudioClip beatSFX;

	public Transform top;
	public Transform bottom;
	public Transform hitzone;

	public Transform billyMeter;
	public int maxBillyScore;

	public Color glowColor;
	public Color regColor;

	private Queue<RowController> rows;

	public float speed { get; private set; }

	private float timeToSpawn;
	private float spawnTimer;
	private float startDelayTimer;
	public float bpm;

	private int billyScore;

	private bool isRunning;

	private int[] arrowInds = { 0, 1, 2, 3 };

	bool hit;

	void Awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start ()
	{
		rows = new Queue<RowController> ();

		//StartGame ();
	}

	public override void StartGame ()
	{
		isRunning = true;
		billyScore = 0;
		UpdateBillyMeter ();
		bpm = startingbpm;
		RecomputeVars ();
		musicSource.Stop ();
		musicSource.Play ();
		startDelayTimer = startDelay;
		SpawnRow ();

		otherBillyGame.StartGame ();
	}

	public override void EndGame ()
	{
		isRunning = false;

		while (rows.Count > 0) {
			GameObject.Destroy (rows.Dequeue ().gameObject);
		}

		otherBillyGame.EndGame ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isRunning) {
			return;
		}

		if (startDelayTimer > 0) {
			startDelayTimer -= Time.deltaTime;
			if (startDelay > 0) {
				return;
			}
		}

		RecomputeVars ();
			
		bool isInRange = rows.Peek ().IsInRange ();
		for (int i = 0; i < arrowInputs.Length; i++) {
			if (isInRange && rows.Peek().HasArrow(i)) {
				GlowInputArrow (i);
			} else {
				UnGlowInputArrow (i);
			}
		}


		spawnTimer += Time.deltaTime;
		if (spawnTimer > timeToSpawn) {
			SpawnRow ();
			spawnTimer = 0;
		}
	}

	public override void TakeInput (int x, int y)
	{
		if (x == 0 && y == 0) {
			return;
		}

		hit = true;
		if (x == -1 || x == 2) {
			TakeSingleInput (0);
		}
		if (x == 1 || x == 2) {
			TakeSingleInput (3);
		}

		if (y == -1 || y == 2) {
			TakeSingleInput (1);
		}
		if (y == 1 || y == 2) {
			TakeSingleInput (2);
		}

		if (hit) {
			otherBillyGame.TakeInput (x, y);
		}

		//otherBillyGame.TakeInput (x, y);
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
		hit = hit && true;
		if (billyScore >= maxBillyScore) {
			//u win
			return;
		}

		billyScore++;
		UpdateBillyMeter ();

	}

	public void Miss() {
		hit = false;
		billyScore -= 5;
		if (billyScore < 0) {
			billyScore = 0;
		}
		otherBillyGame.ReleaseNiceHit ();
		UpdateBillyMeter ();
	}

	void UpdateBillyMeter() {
		float billyPercent = ((float)billyScore) / maxBillyScore;
		billyMeter.localScale = Vec.SetX (billyMeter.localScale, 10.7f * billyPercent);
		ShakeCabinet.UpdateForce (billyPercent);
	}

	void SetBPM(float newbpm) {
		bpm = newbpm;

	}

	public void GlowInputArrow(int ind) {
		
		arrowInputs [ind].GetComponent<SpriteRenderer> ().color = glowColor;
	}

	public void UnGlowInputArrow(int ind) {
		arrowInputs [ind].GetComponent<SpriteRenderer> ().color = regColor;
	}

	void SpawnRow() {
		int numArrows = Random.Range (1, 2);

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
			List<int> inds = new List<int> (arrowInds);
			for (int i = 0; i < numArrows; i++) {
				int ind = Random.Range (0, inds.Count);
				Arrow newArrow = SpawnArrow (inds[ind], newRow.transform);
				newRow.AddArrow (newArrow);
				inds.RemoveAt (ind);
			}
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
		speed = rowHeight / timeToSpawn;
	}

	public void PopRow() {
		RowController rc = rows.Dequeue ();
	}

	// for testing
	public void PlayBPMCounter() {
		sfxSource.PlayOneShot (beatSFX);
	}
}

