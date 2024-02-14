using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float Speed;

	void Update ()
    {
        transform.position += new Vector3(-Speed * Time.deltaTime, 0f);
	}
}
