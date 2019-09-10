using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public int numOfCentipedeUnit = 15;
    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public int lives = 3;
    public GameObject playerPrefab;
    public PlayerController playerController;
    public CentipedeController centipedeController;
    public Grid grid;
    private bool gameOver = false;
    #region Singleton

    public static GameController Instance;

    public void Awake()
    {
        Instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            centipedeController = GameObject.Find("centipedeBody(Clone)").GetComponent<CentipedeController>();
            centipedeController.centipedeSpeed = 0;
            Debug.Log("centipede u should stop already");
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
                gameOver = false;
                gameOverText.gameObject.SetActive(false);
            }
        }

        GameObject[] centipedeToBeDetroyed;
        centipedeToBeDetroyed = GameObject.FindGameObjectsWithTag("Centipede");
        if(centipedeToBeDetroyed.Length == 0)
        {
            GameOver();
        }

    }

    public void DecreaseLivesAndInstantiate(GameObject playerToBeDestroyed)
    {
        lives--;

        if (lives >= 1)
        {
            Instantiate(playerPrefab, playerController.playerSpawnPosition, Quaternion.identity);
        }

        Debug.Log(lives);
        livesText.text = "Lives: " + lives.ToString();
        Destroy(playerToBeDestroyed);

        //Destroy all mushrooms and centipede body
        GameObject[] mushroomToBeDetroyed;
        mushroomToBeDetroyed = GameObject.FindGameObjectsWithTag("Mushroom");
        foreach (GameObject mushroom in mushroomToBeDetroyed)
        {
            Destroy(mushroom);
        }

        GameObject[] centipedeToBeDetroyed;
        centipedeToBeDetroyed = GameObject.FindGameObjectsWithTag("Centipede");
        foreach (GameObject centipedeBody in centipedeToBeDetroyed)
        {
            Destroy(centipedeBody);
        }

        grid.InitCells();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Debug.Log("gameover");
        gameOver = true;

    }
}