using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.forward * speed;
        rb.velocity = Vector2.up * speed;


    }

    void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.up * speed;

        }
    }
}
