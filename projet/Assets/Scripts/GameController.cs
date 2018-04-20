using UnityEngine;
using System.Collections;
using UnityEngine.UI;       //to use "Text" type
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemy;        //référence à notre prefab Enemy
    public Vector3 spawnValues;

    public Text scoreText;      //displays the score
    public Text restartText;
    public Text gameOverText;

    private int score;           //holds the value of the current score
	
	private bool gameOver;
	private bool restart;
	
	public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private bool paused;

    void Start()
    {
        score = 0;
        UpdateScore();
		gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
		StartCoroutine (SpawnWaves ());
        paused = false;
    }
	
	void Update()
	{
        if (gameOver && !paused)
        {
            restartText.text = "Press 'R' to Restart";
            gameOverText.text = "Game Over !";
            restart = true;
            togglePause();
        }

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                togglePause();
            }
        }
	}

    //instantiates our enemy prefab
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    //displays our score
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
		SpawnWaves();
    }
	
	public void GameOver ()
    {
        gameOver = true;
    }

    public void togglePause()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
        }else{
            Time.timeScale = 0f;
            paused = true;
        }
    }
}