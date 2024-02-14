using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJump : MonoBehaviour
{

    public GameObject SpawnPoint;
    public GameObject Chomper;

    private bool HasJumped = false;

    private Animator Anim;

    void Awake()
    {

        Anim = gameObject.GetComponentInParent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Player" && !HasJumped)
        {
            Instantiate(Chomper, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            Anim.SetTrigger("Jump");
            HasJumped = true;
        }

    }

}
