using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public Transform target;
    public Vector3 offset;
    public float time_speed;
    public GameObject gamecontroller;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       //transform.LookAt(target);
       if(gamecontroller.GetComponent<GameController>().gameOver == false)
        {
            Vector3 des = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, des, time_speed * Time.deltaTime);
        }
       

    }
}
