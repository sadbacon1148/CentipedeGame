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
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (transform.position.x < savePosition.x) //moving from the right
        {
            MoveLeft();
            Debug.Log("move left");
        }
        else
        {
            MoveRight();
            Debug.Log("move right");

        }

        //if(transform.position.y == savePosition.y)
        //{
        //    MoveLeft();
        //}
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
        Debug.Log("OnTriggerEnter");
        Debug.Log(checkPosition + "in right collider");

        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);

        }

        if (collision.tag == "Mushroom")
        {
            if (transform.position.x > savePosition.x) //moving from the right
            {
                pos += Vector3.down;
                MoveLeft();
            }
            else
            {
                MoveRight();
            }

        }

        if(collision.tag == "RightCollider")
        {
            canMove = false;
            moving = true;
            checkPosition = false;
            pos += Vector3.down;
            MoveLeft();
            //Debug.Log("enter right");
        }

        if(collision.tag == "LeftCollider")
        {
            pos += Vector3.down;
            MoveRight();
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
