using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

   // [HideInInspector]
    public int currentHealt;

    public int maxHealth;

    void Start()
    {

        currentHealt = maxHealth;

    }

    void Update()
    {
        
    }


    public void DamagePlayer(int damageAmount)
    {

        currentHealt -= damageAmount;

        if(currentHealt <= 0)
        {
            currentHealt = 0;

            gameObject.SetActive(false);
        }

    }
}
