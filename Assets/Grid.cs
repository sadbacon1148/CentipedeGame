﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    //grid specifics
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    public Vector2 gridSize;
    [SerializeField] private Vector2 gridOffset;

    //about cells
    [SerializeField]
    private Sprite cellSprite;
    public GameObject mushroomPrefab;
    private Vector2 cellSize;
    private Vector2 cellScale;

    [Header("Box Collider")]
    public BoxCollider2D topCollider;
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    public BoxCollider2D bottomCollider;

    [SerializeField] private float randomSpawn;

    [Header("Centipede Settings")]
    public GameObject centipedeSprite;
    public int centipedeNumber;

    public GameController gameController;
    //public List<GameObject> instanceCentipede = new List<GameObject>();


    void Start()
    {
        InitCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*TODO: 1. Create List to store instantiateCentipede
            2. When part of centipede body got shot it will return the part number to Split Function
            3. In Split Function, it will check if centipede is moving left or right
            4. compare the rest of number with the midpoint of the sceen to determine whether it's on the left or right of the screen
            4. then split the centipede to move towards left and right*/
    public void InitCells()
    {
        centipedeNumber = gameController.numOfCentipedeUnit;
        GameObject cellObject = new GameObject();
        GameObject instanceMushroom = new GameObject();
        //creates an empty object and adds a sprite renderer component -> set the sprite to cellSprite
        cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;
        //catch the size of the sprite
        cellSize = cellSprite.bounds.size;

        Vector2 newCellSite = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        //Get the scale so you can scale the cells and change their size to fit the grid
        cellScale.x = newCellSite.x / cellSize.x;
        cellScale.y = newCellSite.y / cellSize.y;

        cellSize = newCellSite; //the size will be replaced by the new computed size, we just use cellsize for computing the scale

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        //fix the cells to the grid by getting half of the grid and cells add and minus experiment
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;



        //fill the grid with cells by using Instantiate
        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                //add the cell size so that no two cells will have the same position of x and y
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);

                //Instantiate the game object, at position pos, with rotation set to identity
                GameObject instanceGridCell = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                randomSpawn = Random.Range(0f, 10f);


                if (randomSpawn > 8 && row < rows - 2f && row > 2f && col<cols-1f && col>0.5f)
                {
                    instanceMushroom = Instantiate(mushroomPrefab, pos, Quaternion.identity) as GameObject;
                }

                if (row > rows -2)
                {
                    if (centipedeNumber > 1 )
                    {
                        GameObject instanceCentipede = Instantiate(centipedeSprite,pos, Quaternion.identity);
                        //instanceCentipede.Add(tempCentipede);
                        centipedeNumber--;
                        
                    }
                    else
                    {
                        if (centipedeNumber == 1)
                        {
                            //instanceCentipede.Add(Instantiate(centipedeSprite, pos, Quaternion.identity) as GameObject);
                            GameObject instanceCentipede = Instantiate(centipedeSprite, pos, Quaternion.identity);
                            CentipedeController centipedeController = instanceCentipede.GetComponent<CentipedeController>();
                            centipedeController.centipedeHead = true;
                            centipedeController.spriteRenderer.sprite = centipedeController.headSprite;
                            centipedeNumber--;
                        }
                    }
                }

                //set the parent of the cell to GRID so you can move the cells together with the grid
                instanceGridCell.transform.parent = transform;
                instanceMushroom.transform.parent = transform;
            }
            
        }


        //destroy the object used to instantiate the cells
        Destroy(cellObject);

        topCollider.size = new Vector2(gridSize.x,1f);
        topCollider.offset = new Vector2(0f, (gridSize.y / 2)+0.2f);

        leftCollider.size = new Vector2(1f, gridSize.y);
        leftCollider.offset = new Vector2(-(gridSize.x / 2) -0.2f, 0f);

        rightCollider.size = new Vector2(1f, gridSize.y);
        rightCollider.offset = new Vector2((gridSize.x / 2) , 0f);

        bottomCollider.size = new Vector2(gridSize.x, 1f);
        bottomCollider.offset = new Vector2(0f, -(gridSize.y / 2));

    }



    //so you can see the width and height of the grid on editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("hit");
        }
            
    }
}
