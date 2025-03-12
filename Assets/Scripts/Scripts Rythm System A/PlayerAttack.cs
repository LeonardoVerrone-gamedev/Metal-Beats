using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    [SerializeField] private UnityEvent _event;

    [SerializeField] private AudioSource audio;

    //List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    [SerializeField] private int power = 1;
    public Transform CloserAttackPoint;
    public float AttackRange;
    public LayerMask enemyLayer;

    [SerializeField] private int comboNumber;
    [SerializeField] private int comboMax;
    public Animator anim;
    public bool _isAttacking;
    public bool ComboStarted;
    [SerializeField]private string attackButtomName;
    private bool P1;


    public GameObject punchEffect;

    

    // Start is called before the first frame update
    void Start()
    {
        audio = FindObjectOfType<AudioSource>();
        P1 = GetComponent<PlayerMove>().P1;
        if(GetComponent<PlayerMove>().P1 == true){
            attackButtomName = "Fire1";
        }else{
            attackButtomName = "Fire1B";
        }
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (noteRestriction == note.NoteName)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                //timeStamps.Add(audio.time + (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Attacking", _isAttacking);
        //anim.SetBool("ComboHaveStarted", ComboStarted);
        anim.SetInteger("ComboProgress", comboNumber);
        
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetButtonDown(attackButtomName))
            {
                if(!_isAttacking){
                    _isAttacking = true;
                }
                
                Collider[] hit = Physics.OverlapSphere(CloserAttackPoint.position, AttackRange, enemyLayer);
                    foreach(Collider hitted in hit){
                        Debug.Log("We hit" + hitted.name);
                        //cmManager.CameraSwitch();
                        hitted.gameObject.GetComponent<EnemyLife>().TakeDemage(power);
                    }

                if (Math.Abs(audioTime - timeStamp) <+ marginOfError)
                {
                    Hit();
                    Debug.Log($"Hit on {inputIndex} note");
                    //Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }else
                {
                    Debug.Log($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    Miss();
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log($"Missed {inputIndex} note");
                inputIndex++;
            }
        }       
    
    }
    private void Hit()
    {
        ComboStarted = true;
        Debug.Log("ACERTOU");
        //power = power * 2;
        Instantiate(punchEffect, CloserAttackPoint.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 90f)));
        if(comboNumber < comboMax){
            comboNumber+=1;
        }else{
            comboNumber = 0;
        }
    }
    private void Miss()
    {
        Debug.Log("ERROU");
        power = 1;
        _isAttacking = false;
        ComboStarted = false;
        
        comboNumber = 0;

        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in _enemies){
            enemy.GetComponent<EnemyMove>().Attack();
        }
    }

    void OnDrawGizmosSelected(){
        if(CloserAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(CloserAttackPoint.position, AttackRange);
    }
}