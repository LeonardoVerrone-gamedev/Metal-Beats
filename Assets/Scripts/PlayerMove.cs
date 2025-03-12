using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 move;
    public CharacterController charController;
    public Rigidbody rb;

    [SerializeField] public Transform cameraPosition;

    public GameObject AttackChild;

    public Animator anim;

    public float speed;

    public bool P1;

    public Vector3 moveDir;
    public float rotationSpeed;

    public Transform sprite;
    public bool isFacingRight;

    public Quaternion lastRotate;

    public bool flipped;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue;

        float verticalValue;

        Vector3 camForward = cameraPosition.forward;
        Vector3 camRight = cameraPosition.right;

        camForward.y = 0;
        camForward.y = 0;

        if(P1){
            horizontalValue = Input.GetAxisRaw("Horizontal");
            verticalValue = Input.GetAxisRaw("Vertical");
        }else{
            horizontalValue = Input.GetAxisRaw("Horizontal2");
            verticalValue = Input.GetAxis("Vertical2");
        }

        Vector3 forwardRelative = verticalValue * camForward;
        Vector3 rightRelative = horizontalValue * camRight;

        moveDir = forwardRelative + rightRelative;
        
    }

    void FixedUpdate(){
        //Vector3 move;
        if(P1){
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }else{
            move = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        }
        //charController.Move(move * Time.deltaTime * speed);
        rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);

        if(moveDir != Vector3.zero){
            Flip();
        }else{
            transform.rotation = lastRotate;
        }
        
        //lastRotate = transform.rotation;

        if(move == Vector3.zero){
            anim.SetFloat("Speed", 0f);
        }else{
            anim.SetFloat("Speed", 1f);
        }
    }

    protected virtual void Flip(){
        if(!flipped){
            flipped = true;
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            lastRotate = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        flipped = false;
    }
}
