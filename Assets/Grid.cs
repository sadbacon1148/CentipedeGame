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

    void Start()
    {
        centipedeNumber = gameController.numOfCentipedeUnit;
        InitCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCells()
    {
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


                if (randomSpawn > 8 && row < rows - 2 && row > 2)
                {
                    instanceMushroom = Instantiate(mushroomPrefab, pos, Quaternion.identity) as GameObject;
                    //Debug.Log(row + "____" + rows);
                }

                if (row > rows -2)
                {
                    //Debug.Log(row + "CENTIPEDE" + rows);
                    if (centipedeNumber > 1 )
                    {
                        Instantiate(centipedeSprite,pos, Quaternion.identity);
                        centipedeNumber--;
                        //Debug.Log("centipede--");
                    }
                    else
                    {
                        if (centipedeNumber == 1)
                        {
                            GameObject instanceCentipede = Instantiate(centipedeSprite, pos, Quaternion.identity);
                            CentipedeController controller = instanceCentipede.GetComponent<CentipedeController>();
                            controller.head = true;
                            controller.spriteRenderer.sprite = controller.headSprite;
                            centipedeNumber--;
                        }
                        //Debug.Log("centipede else");
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
