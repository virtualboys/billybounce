using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowController : MonoBehaviour
{
	public float hitRange;

	private List<Arrow> arrows;

	void Awake() {
		arrows = new List<Arrow> ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	public void AddArrow(Arrow arrow) {
		arrows.Add (arrow);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition += Time.deltaTime * GuitarHeroController.singleton.speed * Vector3.down;

		if (transform.position.y < GuitarHeroController.singleton.bottom.position.y) {
			GuitarHeroController.singleton.DestroyRow ();
		}
	}

	public bool IsInRange() {
		return Mathf.Abs (transform.position.y - GuitarHeroController.singleton.hitzone.position.y) < hitRange;
	}

	public bool TryHit(int ind) {
		for (int i = 0; i < arrows.Count; i++) {
			if (arrows [i].ind == ind) {
				Arrow a = arrows [i];
				arrows.RemoveAt (i);
				GameObject.Destroy (a);

				if (arrows.Count == 0) {
					GuitarHeroController.singleton.DestroyRow ();
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

