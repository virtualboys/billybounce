using UnityEngine;
using System.Collections;

public class OldBillyGame : BillyGame
{
	public Transform billy;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void TakeInput (int x, int y)
	{
		billy.localPosition += new Vector3 (x, y, 0);
	}
}

