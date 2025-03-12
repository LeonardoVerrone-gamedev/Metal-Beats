using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cams;
    public CinemachineVirtualCamera walkingCam;
    public CinemachineVirtualCamera _currentCam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraSwitch(){
        Debug.Log("ENTROU NO CAMERA SWITCH");
        int i = Random.Range(0, cams.Length);
        cams[i].Priority = 1;
        _currentCam.Priority = 0;
        _currentCam = cams[i];
    }

    public void CameraReset(){
        _currentCam.Priority = 0;
        walkingCam.Priority = 1;
        _currentCam = walkingCam;
    }
}
