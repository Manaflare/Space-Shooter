using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText RestartText;
    public GUIText GameOverText;
    public GUIText HPText;
    public GUIText PhaseText;
    public GameObject player;

    public bool Sound3DToggle;

    public bool gameOver;
    private bool restart;

    private int score;
    private int phase = 1;
    public GameObject soundController;
    public int GameTime = 0;
    private void Start()
    {
        phase = 1;
        GameTime = 0;
        restart = false;
        gameOver = false;

        RestartText.text = "";
        GameOverText.text = "";

        score = 0;
        UpdateScore();
        HPText.text = "HP : " + player.GetComponent<PlayerController>().HP;
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }    
    }

    IEnumerator SpawnWaves()
    {
        //start part
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            GameTime += 1;

            int startTime = (int)startWait;
            int remainTime = startTime - GameTime;
            PhaseText.text = "Phase : " + phase + " Time : " + remainTime;

            // 3seconds before starting wave 
            if (GameTime == startTime - 3)
            {
                //change the sound for combat in
                soundController.GetComponent<SoundController>().CombatStart();
            }

            if (GameTime >= startTime)
            {
                GameTime = 0;
                break;
            }

        }


        while(true)
        {
            for (int i = 0; i < ( hazardCount * phase ); i++)
            {
                if(player != null)
                {
                    Vector3 playerPos = player.transform.position;
                    //random spwan for hazard
                    Vector3 spawnPos = playerPos + Random.insideUnitSphere * 10;
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject tempThis = Instantiate(hazard, spawnPos, spawnRotation);
                
                    //change the direction to player
                    tempThis.GetComponent<Mover>().GoThisWay(spawnPos, playerPos);
                }
                
                yield return new WaitForSeconds(spawnWait);
            }


            //change the sound for combat out
            soundController.GetComponent<SoundController>().CombatOut();
            GameTime = 0;

            yield return new WaitForSeconds(waveWait);

            phase++;
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                GameTime += 1;

                int startTime = (int)waveWait;
                int remainTime = startTime - GameTime;
                PhaseText.text = "Phase : " + phase + " Time : " + remainTime;

                // 3seconds before starting wave 
                if (GameTime == startTime - 3)
                {
                    //change the sound for combat in
                    soundController.GetComponent<SoundController>().CombatStart();
                }

                //initialize gametime
                if (GameTime >= startTime)
                {
                    GameTime = 0;
                    break;
                }
            }
            
           

            if (gameOver)
            {
                RestartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }        
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }


    public bool DamageHP(int dmg)
    {
        player.GetComponent<PlayerController>().HP -= dmg;
        HPText.text = "HP : " + player.GetComponent<PlayerController>().HP;
        if (player.GetComponent<PlayerController>().HP <= 0)
        {
            HPText.text = "";
            GameOver();
            return true;
        }

        return false;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!!";
        gameOver = true;
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + score;
    }
}
