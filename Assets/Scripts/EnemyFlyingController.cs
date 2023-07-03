using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    public float rangeToStartChase;
    private bool isChasing;

    public float moveSpeed, turnSpeed;

    private Transform player;



    // Start is called before the first frame update
    void Start()
    {

        player = PlayerHealthController.instance.transform;


    }

    // Update is called once per frame
    void Update()
    {
        if(!isChasing)
        {
            if(Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {

                isChasing = true;

            }
        }
        else
        {
            if(player.gameObject.activeSelf)
            {

                Vector3 diraction = transform.position - player.position;
                float angle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);


            }
        }

    }
}
