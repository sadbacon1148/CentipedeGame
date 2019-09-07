using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    [SerializeField] private int requireBulletToDestroy = 3;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (requireBulletToDestroy > 0)
            {
                --requireBulletToDestroy;
            }
            else
            {
                Destroy(gameObject);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
