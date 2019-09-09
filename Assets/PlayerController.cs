using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT }

    private bool canMove = true, moving = false;
    private int buttonCoolDown = 0;
    [SerializeField] private int playerSpeed = 5;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    public Grid grid;
    [SerializeField] private double yPlayerRange;
    [SerializeField] private float xPlayerRange;
    [SerializeField] private float yMinPlayerMove;
    [SerializeField] private float yMaxPlayerMove;
    [SerializeField] private float xMinPlayerMove;
    [SerializeField] private float xMaxPlayerMove;
    [SerializeField] private float playerOffset = 0.2f;
    private float nextFire;
    [SerializeField] private float fireRate;
    public Transform spawnPosition;
    public Text scoreText;
    public Text livesText;
    [SerializeField] private int lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        yPlayerRange = grid.gridSize.y * 0.15;
        xPlayerRange = grid.gridSize.x;
        yMinPlayerMove = -((grid.gridSize.y) / 2);
        yMaxPlayerMove = yMinPlayerMove + (float)yPlayerRange;
        xMinPlayerMove = -((xPlayerRange) / 2);
        xMaxPlayerMove = xPlayerRange/2;
        transform.position = new Vector3(0, yMinPlayerMove+playerOffset, 0);

        livesText.text = "Lives: " + lives.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //buttonCoolDown--;
        if (canMove)
        {
            pos = transform.position;
            Move();
        }

        if (moving)
        {
            if(transform.position == pos)
            {
                moving = false;
                canMove = true;

                Move();
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * playerSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        //if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ObjectPooler.Instance.SpawnFromPool("Bullet", spawnPosition.position, Quaternion.identity);
        }

    }

    private void LateUpdate()
    {
        //Vector3 playerPosition = transform.position;
        //playerPosition.x = Mathf.Clamp(playerPosition.x, xMinPlayerMove, xMaxPlayerMove);
        //playerPosition.y = Mathf.Clamp(playerPosition.y, yMinPlayerMove, yMaxPlayerMove);
        //transform.position = playerPosition;
    }

    private void Move()
    {
             if (Input.GetKey(KeyCode.UpArrow))
                {
                if(dir != DIRECTION.UP)
                {
                    //buttonCoolDown = 5;
                    dir = DIRECTION.UP;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += new Vector3(0f,1000f,0f);
                    pos.y = Mathf.Clamp(pos.y, yMinPlayerMove+playerOffset, yMaxPlayerMove);

                }
             }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (dir != DIRECTION.DOWN)
                {
                    //buttonCoolDown = 5;
                    dir = DIRECTION.DOWN;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.down;
                    pos.y = Mathf.Clamp(pos.y, yMinPlayerMove+playerOffset, yMaxPlayerMove);

                }

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (dir != DIRECTION.LEFT)
                {
                    //buttonCoolDown = 5;
                    dir = DIRECTION.LEFT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.left;
                    pos.x = Mathf.Clamp(pos.x, xMinPlayerMove+playerOffset, xMaxPlayerMove-playerOffset);
                }

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (dir != DIRECTION.RIGHT)
                {
                    //buttonCoolDown = 5;
                    dir = DIRECTION.RIGHT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.right;
                    pos.x = Mathf.Clamp(pos.x, xMinPlayerMove+playerOffset, xMaxPlayerMove-playerOffset);
                }

            }

        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Centipede")
        {
            lives--;
        }
    }


}
