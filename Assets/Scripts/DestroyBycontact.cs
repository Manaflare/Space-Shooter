using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBycontact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int ScoreValue;
    public int damage;
    private GameController gameController;
   

    // Use this for initialization
    public AudioClip crashHard;
    public AudioClip crashSoft;
    
    private float pitchLow = 0.75f;
    private float pitchHigh = 1.5f;

    private float velToVol = 0.2f;
    private float velocityClipSplit = 5f;

    private void Start()
    {
        GameObject gamecontrollerObj = GameObject.FindWithTag("GameController");
        if(gamecontrollerObj != null)
        {
            gameController = gamecontrollerObj.GetComponent<GameController>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
            return;

        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);

        AudioSource audioSource = exp.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(pitchLow, pitchHigh);

        float mag = GetComponent<Rigidbody>().velocity.magnitude;
        float hitVol = mag * velToVol;
        Debug.Log("mag : " + mag + " vol : " + hitVol);
        if(mag < velocityClipSplit)
        {
            audioSource.PlayOneShot(crashSoft, hitVol);
        }
        else
        {
            audioSource.PlayOneShot(crashHard, hitVol);
        }

        if(other.tag =="Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            bool gameover = gameController.DamageHP(damage);
            if (gameover == false)
            {
                Destroy(gameObject);
                return;
            }
                
        }
            

        gameController.AddScore(ScoreValue);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
