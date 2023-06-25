using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private static BombManager instance; // declares a private static field called instance, which will hold the reference to the singleton instance of the BombManager class
   
    public static BombManager Instance //public static property called Instance. It provides read-only access to the instance field,
                                       //allowing other scripts to retrieve the singleton instance of the BombManager class.
    {
        get { return instance; }
    }

    private int activeBombCount = 0;
    private int maxBombCount = 3;

    public GameObject bombPrefab;
    public Transform bombPoint;

    private void Awake() // The Awake() method is called when the script instance is initialized.
                         // This code ensures that only one instance of the BombManager class exists.
                         // If an instance already exists (instance != null), it destroys the newly created instance
                         // (Destroy(gameObject)) to enforce the singleton pattern. Otherwise, it assigns the current instance to the instance field.
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void SpawnBomb()
    {
        if (activeBombCount < maxBombCount)
        {
            GameObject newBomb = Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
            newBomb.tag = "Bomb";
            activeBombCount++;
        }
    }

    public void DecreaseActiveBombCount()
    {
        activeBombCount--;
    }
}
