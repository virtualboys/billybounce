using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{

	public int ind;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void HitArrow() {
		transform.SetParent (null);
		iTween.PunchScale (gameObject, iTween.Hash ("amount", 1.5f * Vector3.one, "time", 
			.1f, "oncomplete", "OnScaleComplete", "oncompletetarget", gameObject));
		iTween.FadeTo (gameObject, 0, .1f);
	}

	void OnScaleComplete() {
		GameObject.Destroy (gameObject);
	}
}

