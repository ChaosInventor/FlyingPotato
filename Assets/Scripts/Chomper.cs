using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : MonoBehaviour
{

    private float CurSpeed;
    public float Speed;
    public float StartingSpeed;
    public float SpeedUp;

	// Use this for initialization
	void Awake ()
    {
        CurSpeed = StartingSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.position += new Vector3(-CurSpeed * Time.deltaTime, 0f);
        if (CurSpeed < Speed)
        {
            CurSpeed += SpeedUp;
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.gameObject.tag == "Bad" && !coll.gameObject.GetComponentInChildren<StartJump>())
        {
            Destroy(coll.gameObject);
        }

    }

}
