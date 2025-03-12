using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using URP;

public class RythmObjectBehaviour : MonoBehaviour
{

    public bool isLight;

    private Vector3 _startSize;
    [SerializeField] float _returnSpeed;
    [SerializeField] float _PulseSize;
    public float errorMarge;

    public Light light;
    public float minLightIntensity;
    public float maxLightIntensity;

    void Start()
    {   
        if(!isLight){
            _startSize = transform.localScale;
        }else{
            light = GetComponent<Light>();
        }
    }

    void Update()
    {
        if(!isLight){
            transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
        }else{
            light.intensity = Mathf.Lerp(light.intensity, minLightIntensity, Time.deltaTime * _returnSpeed);
        }
    }

    public void Pulse(){
        if(!isLight){
            transform.localScale = _startSize * _PulseSize;
        }else{
            light.intensity = maxLightIntensity;
        }
    }
}
