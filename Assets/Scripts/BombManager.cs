using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private static BombManager instance;
    public static BombManager Instance
    {
        get { return instance; }
    }

    private int activeBombCount = 0;
    private int maxBombCount = 3;

    public GameObject bombPrefab;
    public Transform bombPoint;

    private void Awake()
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
