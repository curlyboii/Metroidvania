using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float bulletSpeed;
    public Rigidbody2D theRB;

    public Vector2 moveDir;



    // Update is called once per frame
    void Update()
    {
        theRB.velocity = moveDir * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
