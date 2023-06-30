using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtPatroller : MonoBehaviour
{

    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed, waitAtPoints;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D theRB; 


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoints;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
        {
            if(transform.position.x < patrolPoints[currentPoint].position.x)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            }


        }
    }
}
