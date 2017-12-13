using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
	public float thrust;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	    void OnCollisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
            if(col.gameObject.tag == "prop_billybouncer")
        	{
        	//GameObject billyB = col.gameObject;
            //billyB.GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
        	col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
            Debug.Log("Collision");
        	}
     	}
     }
}
