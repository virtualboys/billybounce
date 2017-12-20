using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowController : MonoBehaviour
{
	public float hitRange;

	private List<Arrow> arrows;
	private int originalCount;

	private bool hasPlayedTestSound;
	private bool hasPopped;

	void Awake() {
		arrows = new List<Arrow> ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	public void AddArrow(Arrow arrow) {
		arrows.Add (arrow);
		originalCount++;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition += Time.deltaTime * GuitarHeroController.singleton.speed * Vector3.down;

		//if (!hasPlayedTestSound && transform.position.y <= GuitarHeroController.singleton.hitzone.position.y) {
		//	GuitarHeroController.singleton.PlayBPMCounter ();
		//	hasPlayedTestSound = true;
		//}

		if(!hasPopped && !IsInRange() && transform.position.y < GuitarHeroController.singleton.hitzone.position.y) {
			GuitarHeroController.singleton.PopRow ();
			GuitarHeroController.singleton.Miss ();
			hasPopped = true;
		}

		if (transform.position.y < GuitarHeroController.singleton.bottom.position.y) {
			GameObject.Destroy (gameObject);
		}
	}

	public bool IsInRange() {
		return Mathf.Abs (transform.position.y - GuitarHeroController.singleton.hitzone.position.y) < hitRange;
	}

	public bool HasArrow(int ind) {
		for (int i = 0; i < arrows.Count; i++) {
			if (arrows [i].ind == ind) {
				return true;
			}
		}

		return false;
	}

	public bool TryHit(int ind) {
		for (int i = 0; i < arrows.Count; i++) {
			if (arrows [i].ind == ind) {
				
				Arrow a = arrows [i];
				arrows.RemoveAt (i);
				GameObject.Destroy (a);

				if (arrows.Count == 0) {
					float d = Mathf.Abs (transform.position.y - GuitarHeroController.singleton.hitzone.position.y);
					float p = 1 - d / hitRange;
					p *= originalCount;
					BouncyBillyGame.forceMult = p + .3f;

					GuitarHeroController.singleton.PopRow ();
					GameObject.Destroy (gameObject);
				}

				return true;
			}
		}

		return false;
	}

	public int NumArrows() {
		return arrows.Count;
	}
}

