using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{

    public float Speed;

	void Update ()
    {

        transform.position += new Vector3(0f,Speed * Time.deltaTime);

	}
}
