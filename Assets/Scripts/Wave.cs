using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(menuName = "WaveSystem/Wave")]
public class WaveData : ScriptableObject {
    public GameObject[] enemyPrefabs;
    public int SpawnAmount;
}
