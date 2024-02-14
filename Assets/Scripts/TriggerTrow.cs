using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrow : MonoBehaviour
{

    private Trow Trower;

	void Awake ()
    {

        Trower = gameObject.GetComponentInParent<Trow>();
        Trower.TrowFromHere = gameObject;

	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.gameObject.tag == "Player")
        {
            Trower.StartTrow();
        }

    }

}
