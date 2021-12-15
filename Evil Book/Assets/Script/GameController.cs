using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] enemies;
    public int enemiesAlived;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAmountEnemies();
    }

    public void UpdateAmountEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesAlived = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
