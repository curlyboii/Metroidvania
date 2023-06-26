using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = .5f;
    public GameObject explosion;
    private BombManager bombManager;

    public float blastRange;
    public LayerMask whatIsDestructible;

   // public GameObject[] test; 



    // Start is called before the first frame update
    void Start()
    {
        bombManager = BombManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        timeToExplode -= Time.deltaTime;
        if(timeToExplode <= 0)
        {

            if (explosion != null)
            { 
            
                Instantiate(explosion, transform.position, transform.rotation);
            
            }

            Destroy(gameObject);
            bombManager.DecreaseActiveBombCount();

            // find all colliders within a circular area centered at the bomb's position
            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);
            //code block checks if there are any destructible objects (Collider2D objects) in the objectsToRemove array
            if (objectsToRemove.Length > 0)
            {

                foreach(Collider2D col in objectsToRemove)
                {

                    Destroy(col.gameObject);  

                }

            }
        }


    }
}
