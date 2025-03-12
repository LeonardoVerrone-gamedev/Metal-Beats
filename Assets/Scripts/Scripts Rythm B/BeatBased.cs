using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatBased : MonoBehaviour
{
    [SerializeField] private float BPM;


    [SerializeField] public AudioSource audio;
    [SerializeField] private Intervals[] _intervals;
    [SerializeField] public Animator[] anims;

    private void Update(){
        
        foreach(Intervals interval in _intervals){
            float sampledTime = (audio.timeSamples / (audio.clip.frequency * interval.GetIntervalLenght(BPM)));
            float AnimationTime = (audio.timeSamples / BPM);
            foreach(Animator anim in anims){
                if(anim.speed != AnimationTime){
                    anim.speed = AnimationTime;
                }
            }
            interval.CheckForIntervals(sampledTime);
        }

    }
}

[System.Serializable]
public class Intervals{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _event;
    private int lastInterval;

    public float GetIntervalLenght(float BPM){
        return 60f /  (BPM * _steps);
    }

    public void CheckForIntervals(float interval){

        if(Mathf.FloorToInt(interval) != lastInterval){
            lastInterval = Mathf.FloorToInt(interval);
            //_event.Invoke();
        }
    }
}
