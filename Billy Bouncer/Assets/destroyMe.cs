using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5);
		//StartCoroutine(DestroyIt(10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 //   IEnumerator DestroyIt(int waitTime)
   // {
    	// yield return new WaitForSeconds(waitTime);
    	// Destroy(gameObject);
    //}
}
