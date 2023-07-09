using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Animator anim;

    public float distanceToOpen;

    private PlayerController thePlayer;

    private bool playerExiting;

    // Start is called before the first frame update
    void Start()
    {

        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {

            anim.SetBool("doorOpen", true);

        }
        else
        {

            anim.SetBool("doorOpen", false);

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!playerExiting)
            {
                thePlayer.canMove = false;

                StartCoroutine(UseDoorCo());

            }


        }

    }

    IEnumerator UseDoorCo()
    {

        yield return new WaitForSeconds(1.5f);

    }
}
