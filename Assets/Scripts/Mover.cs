using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public bool bRandom;

	void Start ()
    {
        if (bRandom == false)
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
	


	// Update is called once per frame
	void Update () {
      
	}

    public void GoThisWay(Vector3 Position, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - Position).normalized;
        Debug.DrawLine(Position, Position + direction * 10, Random.ColorHSV(), Mathf.Infinity);
        //transform.forward = direction;
        GetComponent<Rigidbody>().velocity = direction *  Random.Range(speed, speed + 5);
    }
}
