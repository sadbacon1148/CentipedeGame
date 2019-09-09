using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentipedeController : MonoBehaviour
{
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
    private bool canMove = true, moving = false;
    public bool head = false;
    private Vector3 pos;
    [SerializeField] private int centipedeSpeed = 5;


    public SpriteRenderer spriteRenderer;
    public Sprite headSprite;
    private Vector2 savePosition;
    private bool checkPosition=true;
    private bool headLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canMove + "canmove");
        Debug.Log(moving + "moving");
        savePosition = new Vector2(transform.position.x, transform.position.y);

        if (canMove)
        {
            Debug.Log("canmove");
            pos = transform.position;
            //create condition here
            if (checkPosition)
            {
                CheckCentipedeDirection();   
            }
         
            //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * centipedeSpeed);

        }

        if (moving)
        {
            Debug.Log("moving");
            if (transform.position == pos)
            {
                moving = false;
                canMove = true;

                if (checkPosition)
                {
                    CheckCentipedeDirection();
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * centipedeSpeed);
        }

        
    }

    public void CheckCentipedeDirection()   
    {
        if(headLeft)
        //if (transform.position.x < savePosition.x) //moving from the right
        {
            MoveLeft();
            Debug.Log("move left");
        }
        else
        {
            MoveRight();
            Debug.Log("move right");

        }

        if (transform.position.y < savePosition.y)
        {
            Debug.Log("check y");
            checkPosition = true;
            if (transform.position.x < savePosition.x) 
            {
                MoveRight();
                Debug.Log("check y go right");
            }
            else
            {
                MoveLeft();
                Debug.Log("check y go left");

            }
        }
    }

    public void MoveRight()
    {
        canMove = false;
        moving = true;

        pos += Vector3.right;

    }

    public void MoveLeft()
    {
        canMove = false;
        moving = true;

        pos += Vector3.left;
        Debug.Log("moveleft");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canMove = false;
        moving = true;
        Debug.Log("OnTriggerEnter");
        Debug.Log(checkPosition + "in right collider");

        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);

        }

        if(collision.tag == "RightCollider" || collision.tag == "LeftCollider" || collision.tag == "Mushroom")
        {
           
            //checkPosition = false;
            pos += Vector3.down;
            //MoveLeft();
            headLeft = !headLeft;
            //Debug.Log("enter right");
        }

  

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
