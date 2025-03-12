using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{

    public ParticleSystem particles;
    public EnemiesManager manager;
    public float rotationSpeed = 0;
    public Quaternion lastRotate;

    public Animator anim;
    public GameObject[] Possibletargets;
    public Transform target;
    public float moveSpeed;

    public float LineOfSite;

    public bool canFollow;
    public bool canAttack;

    public LayerMask playerLayer;
    public float AttackRange;
    public Transform attackPoint;
    public int power;

    public Transform sprite;

    public bool AttackCalled;

    [SerializeField] Transform P1;
    [SerializeField] Transform P2;
    
    void Start()
    {
        manager = FindObjectOfType<EnemiesManager>();
        Possibletargets = GameObject.FindGameObjectsWithTag("Player");
        target = Possibletargets[Random.Range(0, Possibletargets.Length)].GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(target == P1){
            if(manager.enemiesNearToPlayer1.Count < manager.maxEnemies){
                canFollow = true;
            }else{
                canFollow = false;
            }
        }
        
        float distanceToPlayer1 = Vector3.Distance(target.position, transform.position);

        if(canFollow && distanceToPlayer1 >= LineOfSite){
            transform.position = Vector3.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        if(distanceToPlayer1 <= LineOfSite && manager.enemiesNearToPlayer1.Contains(this.transform) == false){
            manager.enemiesNearToPlayer1.Add(this.transform);
        }
        if(distanceToPlayer1 > LineOfSite && manager.enemiesNearToPlayer1.Contains(this.transform)){
            manager.enemiesNearToPlayer1.Remove(this.transform);
        }
        
        //Quaternion toRotation = Quaternion.LookRotation(target.position, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        //lastRotate = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        this.gameObject.transform.LookAt(target);

        if(distanceToPlayer1 <= LineOfSite){
            canAttack = true;
            //StartCoroutine(Attack());
        }
    }

    public void Attack(){
        if(canAttack){
            anim.SetTrigger("Attack");
            particles.Play();
            Collider[] hit = Physics.OverlapSphere(attackPoint.position, AttackRange, playerLayer);
                foreach(Collider hitted in hit){
                    Debug.Log("We hit" + hitted.name);
                    hitted.gameObject.GetComponent<PlayerLife>().TakeDemage(power);
                }
        }else{
            return;
        }
    }

    /* public IEnumerator Attack(){
        if(!canAttack)
        {
            AttackCalled = false; 
            StartCoroutine(Attack());
        }

        while(canAttack){
            AttackCalled = true;
            Collider[] hit = Physics.OverlapSphere(attackPoint.position, AttackRange, playerLayer);
            foreach(Collider hitted in hit){
                Debug.Log("We hit" + hitted.name);
                hitted.gameObject.GetComponent<PlayerLife>().TakeDemage(power);
            }
            yield return new WaitForSeconds(Random.Range(0.75f, 3f));
        }
        AttackCalled = false; 
    } */
}
