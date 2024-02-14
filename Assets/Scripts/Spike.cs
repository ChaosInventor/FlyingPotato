using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    private GameManager GM;
    private SpriteRenderer SR;

	// Use this for initialization
	void Awake ()
    {

        GM = GameObject.Find("Game manager").GetComponent<GameManager>();
        SR = gameObject.GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update ()
    {

        SR.size = new Vector2(GM.Dis - 20f, 0.2150611f);

	}
}
