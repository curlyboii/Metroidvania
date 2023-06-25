using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = .5f;
    public GameObject explosion;
    private BombManager bombManager;


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

        }


    }
}
