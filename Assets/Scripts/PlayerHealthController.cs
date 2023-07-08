using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        UIController.instance.UpdateHealth(currentHealt,maxHealth);
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

                //gameObject.SetActive(false);

                RespawnController.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLenght;

            }

            UIController.instance.UpdateHealth(currentHealt, maxHealth);
        }
    }

    public void FillHealth()
    {

        currentHealt = maxHealth;

        UIController.instance.UpdateHealth(currentHealt, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {

        currentHealt += healAmount;

        if(currentHealt > maxHealth)
        {

            currentHealt = maxHealth;

        }

        UIController.instance.UpdateHealth(currentHealt, maxHealth);

    }    
}
