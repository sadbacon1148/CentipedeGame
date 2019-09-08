using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentipedeController : MonoBehaviour
{
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
    private bool canMove = true, moving = false;
    public bool head = false;


    public SpriteRenderer spriteRenderer;
    public Sprite headSprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);

        }

        Destroy(gameObject);
    }

}
