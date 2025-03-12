using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeComParede : MonoBehaviour
{
    public GameObject[] PRA_SUMIR;
    public GameObject[] PRA_APARECER;

    public void StartCamera(){
        foreach(GameObject some in PRA_SUMIR){
            some.SetActive(false);
        }
        foreach(GameObject aparece in PRA_APARECER){
            aparece.SetActive(true);
        }
    }
}
