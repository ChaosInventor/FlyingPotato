using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwPlayer : MonoBehaviour {

    public Transform Target;

    public float Offset;

	// Use this for initialization
	void Awake ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(Target.transform.position.x - Offset, transform.position.y, transform.position.z);
	}
}
