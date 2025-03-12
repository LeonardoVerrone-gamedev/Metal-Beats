using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<Transform> enemiesNearToPlayer1 = new List<Transform>();
    public List<Transform> enemiesNearToPlayer2 = new List<Transform>();
    public Transform player;
    public int maxEnemies;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
