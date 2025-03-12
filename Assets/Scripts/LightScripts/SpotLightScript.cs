using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightScript : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.LookAt(player.position);
    }
}
