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

    public float invincibilityLenght;
    private float invincibilityCounter;

    public float flashLenght;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;



    void Start()
    {

        currentHealt = maxHealth;

        UIController.Instance.UpdateHealth(currentHealt,maxHealth);
    }

    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter-= Time.deltaTime;

            if(flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }

                flashCounter = flashLenght;

            }

            if(invincibilityCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
            }

        }
        
    }


    public void DamagePlayer(int damageAmount)
    {
        if (invincibilityCounter <= 0)
        {

            currentHealt -= damageAmount;

            if (currentHealt <= 0)
            {
                currentHealt = 0;

                gameObject.SetActive(false);
            }
            else
            {
                invincibilityCounter = invincibilityLenght;

            }

            UIController.Instance.UpdateHealth(currentHealt, maxHealth);
        }
    }
}
