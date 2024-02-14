using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject Point1, Point2, Point3, Point4;
    public GameObject LastPoint;

    public float Speed;

    // Use this for initialization
    void Awake ()
    {

        transform.position = Point1.transform.position;
        LastPoint = Point1;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //First point
        if(transform.position != Point2.transform.position && LastPoint == Point1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -90f);
            transform.position = Vector2.MoveTowards(transform.position, Point2.transform.position, Speed * Time.deltaTime);
        }
        if(transform.position == Point2.transform.position && LastPoint == Point1)
        {
            LastPoint = Point2;
        }
        //Second point
        if (transform.position != Point3.transform.position && LastPoint == Point2)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            transform.position = Vector2.MoveTowards(transform.position, Point3.transform.position, Speed * Time.deltaTime);
        }
        if (transform.position == Point3.transform.position && LastPoint == Point2)
        {
            LastPoint = Point3;
        }
        //Third point
        if (transform.position != Point4.transform.position && LastPoint == Point3)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
            transform.position = Vector2.MoveTowards(transform.position, Point4.transform.position, Speed * Time.deltaTime);
        }
        if (transform.position == Point4.transform.position && LastPoint == Point3)
        {
            LastPoint = Point4;
        }
        //Fourth point
        if (transform.position != Point1.transform.position && LastPoint == Point4)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, Point1.transform.position, Speed * Time.deltaTime);
        }
        if (transform.position == Point1.transform.position && LastPoint == Point4)
        {
            LastPoint = Point1;
        }
    }
}
