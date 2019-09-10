using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentipedeController : MonoBehaviour
{
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
    private bool canMove = true, moving = false;
    public bool centipedeHead = false;
    private Vector3 pos;
    public int centipedeSpeed = 5;


    public SpriteRenderer spriteRenderer;
    public Sprite headSprite;
    private Vector2 savePosition;
    private bool headLeft = false;
    private Rigidbody2D rb2D;
    private bool headUp = false;
    private bool headDown = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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
            
            CheckCentipedeDirection();   
            
         
            //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * centipedeSpeed);

        }

        if (moving)
        {
            Debug.Log("moving");
            if (transform.position == pos)
            {
                moving = false;
                canMove = true;

               
                CheckCentipedeDirection();
                
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * centipedeSpeed);
        }

        if (GameObject.Find("Player(Clone)") == null && GameObject.Find("Player") == null)
        {
            centipedeSpeed = 0;
            GameController.Instance.GameOver();
        }


    }

    public void CheckCentipedeDirection()   
    {
        if(headLeft)
        {
            MoveLeft();
            Debug.Log("move left");
        }
        else
        {
            MoveRight();
            Debug.Log("move right");

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

        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);

        }

        if(collision.tag == "RightCollider" || collision.tag == "LeftCollider" || collision.tag == "Mushroom")
        {
            if (headUp)
            {
                pos = gameObject.transform.position;
                pos += Vector3.up;
                Debug.Log("headUp");
            }
            if (headDown)
            {
                pos = gameObject.transform.position;
                pos += Vector3.down;
                Debug.Log("headDown");
            }

            headLeft = !headLeft;
        }



        if (collision.tag == "BottomCollider" && headDown)
        {
            headDown = false;
            headUp = true;
            pos = gameObject.transform.position;
            pos += Vector3.up;
        }

        if(collision.tag == "TopCollider" && headUp)
        {
         
            headUp = false;
            headDown = true;
            pos = gameObject.transform.position;
            pos += Vector3.down;
        }
    }
    
}
