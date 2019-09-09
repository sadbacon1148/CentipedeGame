using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int numOfCentipedeUnit = 15;
    public Text scoreText;
    public Text livesText;
    public int lives = 3;
    public GameObject playerPrefab;
    public PlayerController playerController;
    public CentipedeController centipedeController;
    public Grid grid;
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
        }
    }

    public void DecreaseLivesAndInstantiate(GameObject playerTobeDestroy)
    {
        if (lives != 0)
        {
            lives--;
            Instantiate(playerPrefab, playerController.playerSpawnPosition, Quaternion.identity);
        }

        Debug.Log(lives);
        livesText.text = "Lives: " + lives.ToString();
        GameObject mushroomToBeDetroyed = GameObject.Find("mario-sprite(Clone)");
        Destroy(playerTobeDestroy);
        Destroy(mushroomToBeDetroyed);
        grid.InitCells();
    }
}