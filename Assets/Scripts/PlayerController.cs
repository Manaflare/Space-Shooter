using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rigidbody;
    public float speed;
    public float tilt;
    public float shot_offset;
    public Boundary boundary;
    public GameObject shot;
    public GameObject shotSpawn;

    public int HP = 100;
    public float fireRate;
    float nextFire;

    public AudioClip shot_sound;
    private float volumeMin = 0.2f;
    private float volumeMax = 0.5f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.position = Random.insideUnitSphere * 10;
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
            Instantiate(shot, new Vector3(shotSpawn.transform.position.x - shot_offset, shotSpawn.transform.position.y, shotSpawn.transform.position.z - shot_offset), shotSpawn.transform.rotation);
            Instantiate(shot, new Vector3(shotSpawn.transform.position.x + shot_offset, shotSpawn.transform.position.y, shotSpawn.transform.position.z - shot_offset), shotSpawn.transform.rotation);


            //random sound
            float vol  = Random.Range(volumeMin, volumeMax);
            GetComponent<AudioSource>().PlayOneShot(shot_sound);
        }
        
    }


    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveUp = Input.GetAxis("Up");
        rigidbody.velocity = new Vector3(moveHorizontal * speed, moveUp * speed, moveVertical * speed);
      /*  rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        */
        rigidbody.rotation = Quaternion.Euler(moveUp * -tilt,  0.0f, moveHorizontal * -tilt);
        Camera.main.transform.rotation = Quaternion.Euler((moveUp ) + 20.0f, 0.0f, moveHorizontal );
    }
}
