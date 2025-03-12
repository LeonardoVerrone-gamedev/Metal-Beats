using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public SongManager music;

    public GameObject FirstCam;
    public GameObject startScreenCam;
    public bool gameHasStarted;
    public GameObject[] StartScreenObjs;

    public CinemachineVirtualCamera[] Vcams;
    public CinemachineVirtualCamera cCam;
    public GameObject EnemyPrefab;

    int WaveI = 0;
    bool Started = false;

    //public WaveData[] wavesInOrder;
    public CinemachineVirtualCamera[] CamsInOrder;
    public Transform[] spawnsPoint;

    public PlayerMove playerMove;

    public int EnemyAmount;

    public  List<GameObject> spawnedEnemies = new List<GameObject>();

    public void WaveSpawn(){
        cCam = CamsInOrder[WaveI];
        cCam.gameObject.SetActive(true);
        cCam.gameObject.GetComponent<SomeComParede>().StartCamera();
        WaveI++;
        playerMove.cameraPosition = cCam.gameObject.transform;
        foreach(CinemachineVirtualCamera cams in Vcams){
            if(cams != cCam){
                cams.gameObject.SetActive(false);
            }
        }
        for (int i=0; i<4; i++){
            Transform _spawnPoint = spawnsPoint[Random.Range(0, spawnsPoint.Length)];
            Instantiate(EnemyPrefab, _spawnPoint);
            EnemyAmount++;
            
        }
    }

    void Update(){
        if(Started){
            //GameObject[] enemyFind = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach(GameObject enemy in enemyFind){
            //}

            if(EnemyAmount <= 0 && WaveI < 20){
                    WaveSpawn();
                }
        }

        if(!gameHasStarted){
            if(Input.anyKey){
                gameHasStarted = true;
                music.inGame = true;
                FirstCam.SetActive(true);
                startScreenCam.SetActive(false);
                foreach(GameObject obj in StartScreenObjs){
                    obj.SetActive(false);
                }
                music.GetDataFromMidi();
            }
        }
    }
    
    void Start()
    {
        Application.targetFrameRate = 24;
    }

    public void StartGame(){
         if(!Started){
            WaveSpawn();
            Started = true;
        }
    }

}

