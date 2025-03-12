using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int life;

    public GameManager gm;

    void Start()
    {
        
    }

    void Update()
    {
        if(gm == null){
            gm = FindObjectOfType<GameManager>();
        }
    }

    public void TakeDemage(int power){
        life-=power;
        if(life <= 0){
            gm.EnemyAmount -= 1;
            Destroy(this.gameObject);
        }
    }
}
