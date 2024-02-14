using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trow : MonoBehaviour
{

    public GameObject TrowThis;
    public GameObject TrowFromHere;

    private Animator Anim;

    void Awake()
    {

        Anim = gameObject.GetComponent<Animator>();

    }

    public void StartTrow()
    {

        Anim.SetTrigger("Trow");

    }

    public void Troww()
    {

        Instantiate(TrowThis, TrowFromHere.transform.position, TrowFromHere.transform.rotation);

    }

}
