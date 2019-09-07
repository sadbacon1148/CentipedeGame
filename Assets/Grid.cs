using System.Collections;
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
    public BoxCollider2D boxCollider2D;
    [SerializeField] private float mushroomRadius;
    [SerializeField] private int numOfMushrooms;
    [SerializeField] private float randomSpawn;

    void Start()
    {
        InitCells();
        boxCollider2D = GetComponent<BoxCollider2D>();
        mushroomRadius = mushroomPrefab.GetComponent<CircleCollider2D>().radius;
        numOfMushrooms = (int)(0.5 * cols * rows);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitCells()
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
                if (randomSpawn > 8)
                {
                   instanceMushroom = Instantiate(mushroomPrefab, GetRandomPosition(), Quaternion.identity) as GameObject;
                }

                //set the parent of the cell to GRID so you can move the cells together with the grid
                instanceGridCell.transform.parent = transform;
                instanceMushroom.transform.parent = transform;
            }
            
        }
        for (int i = 0; i < numOfMushrooms; i++)
        {
            
            
            Debug.Log("mushroom");
        }

        //destroy the object used to instantiate the cells
        Destroy(cellObject);

        


        boxCollider2D.size = new Vector2(gridSize.x,1f);
        boxCollider2D.offset = new Vector2(0f, (gridSize.y / 2)+2f);
    }

    Vector2 GetRandomPosition()
    {
        return new Vector2((int)Random.Range(-gridSize.x / 2 + mushroomRadius , gridSize.x / 2 - mushroomRadius), (int)Random.Range(-gridSize.y / 2 + mushroomRadius  , gridSize.y / 2 - mushroomRadius ));

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
