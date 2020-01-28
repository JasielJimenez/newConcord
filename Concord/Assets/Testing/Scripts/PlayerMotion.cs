using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    //public float moveSpeed;
    //private Vector3 moveDirection;
    //public Transform pivot;
    //public float rotateSpeed;
    //public CharacterController controller;

    Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    public float rotateSpeed = 0.0f;
    //private CharacterController controller;
    public Vector3 point;

    public float holdAttack;
    public float speed = 6.0f;
    private float trackSpeed = 1.0f;
    public float rollDistance = 1.0f;
    //public float rotateSpeed = 6.0f;
    private bool gamestart = false;
    private bool gamepaused = false;
    public bool midAttack = false;
    public bool continueAttack = false;
    //public bool swordGrip = false;
    public bool deadTest = false;
    public bool invulnerable = false;
    public bool isRolling = false;
    public GameObject gamemanager;
    public Transform cam;
    public GameObject player;
    public GameObject sword;
    public GameObject swordLight;
    public GameObject swordHitbox;
    public GameObject swordGrabRange;
    public GameObject hand;


    void Start()
    {
        anim = GetComponent<Animator>();
        point = transform.position;
        trackSpeed = speed;
        holdAttack = player.GetComponent<PlayerStats>().attack;
        //controller = GetComponent<CharacterController>();
        //controller = GetComponent<CharacterController>();
    }

    public void beginRoll()
    {
        invulnerable = true;
        isRolling = true;
        //transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }

    public void endRoll()
    {
        invulnerable = false;
        isRolling = false;
        anim.SetBool("roll", false);
        //ALLOW FOR ATTACKING IMMEDIATELY AFTERWARDS
    }

    public void swordStrike()
    {
        swordHitbox.SetActive(true);
    }

    public void endStrike()
    {
        swordHitbox.SetActive(false);
    }

    public void startStrongAttack()
    {
        speed = 0.0f;
        holdAttack = player.GetComponent<PlayerStats>().attack;
        player.GetComponent<PlayerStats>().attack = player.GetComponent<PlayerStats>().attack + 4;
    }

    public void endStrongAttack()
    {
        endStrike();
        speed = trackSpeed;
        anim.SetBool("strongAttack", false);
        player.GetComponent<PlayerStats>().attack = holdAttack;
    }

    void attackBegin()
    {
        midAttack = true;
        trackSpeed = speed;
        speed = 0.0f;
        continueAttack = false;
        anim.SetBool("continueCombo",false);
    }

    void attackEnd()
    {
        speed = trackSpeed;
        if(continueAttack == true)
        {
            anim.SetBool("continueCombo",true);
        }
        midAttack = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hitbox")
        {
            this.GetComponent<Rigidbody>().AddForce(transform.forward * -2.0f);
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    //Debug.Log("YO");
    //    if(other.gameObject.name == "SwordGrab")
    //    {
    //        //Debug.Log("HEY");
    //        if(Input.GetKeyDown("space"))
    //        {
    //            swordLight.SetActive(false);
    //            swordGrabRange.SetActive(false);
    //            sword.transform.SetParent(hand.transform);
    //            sword.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
    //            sword.transform.Rotate(0.0f, 90.0f, -45.0f);
    //            anim.SetBool("SwordHeld",true);
    //            swordGrip = true;
    //        }
    //    }
    //}

    void Update()
    {
        deadTest = player.GetComponent<PlayerStats>().playerDead;
        gamestart = gamemanager.GetComponent<gamemanager>().gameStart;
        gamepaused = gamemanager.GetComponent<gamemanager>().paused;
        if(deadTest == true)
        {
            anim.SetBool("isDead",true);
        }
        else if (gamestart == true && gamepaused == false)
        {
            if(isRolling == true)
            {
                transform.Translate(Vector3.forward * rollDistance * Time.deltaTime);
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Attack();
                }
                else if(Input.GetKeyDown("space"))
                {
                    anim.SetBool("roll", true);
                }
                else if(Input.GetMouseButtonDown(1))
                {
                    anim.SetBool("strongAttack", true);
                }
                else
                {
                    //anim.SetBool("punchAttack1", false);
                    anim.SetBool("swordAttack1", false);
                    ControlPlayer();
                }

                if(midAttack == true)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        continueAttack = true;
                    }
                }
            }
        }
    }

    void Attack()
    {
        //if(swordGrip == true)
        //{
            anim.SetBool("swordAttack1", true);
        //}
        //else{
        //    anim.SetBool("punchAttack1", true);
        //}
    }

    void ControlPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //TEST
        Vector3 groundForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 groundRight = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;
        Debug.DrawRay(transform.position, groundForward * 10, Color.red);
        Debug.DrawRay(transform.position, groundRight * 10, Color.green);

        

        
        //Vector3 distanceFromCam = cam.position - transform.position;
        Vector3 movement = groundForward * Input.GetAxisRaw("Vertical") + groundRight * Input.GetAxisRaw("Horizontal");
        movement = movement.normalized;
        if (movement != Vector3.zero)
        {/*
            Debug.Log("x: " + movement.x);
            Debug.Log("z: " + movement.z);
            if(movement.z == -1) //backward
            {
                movement = new Vector3(moveHorizontal - groundForward.x, 0.0f, moveVertical - groundForward.z);
            }
            else if(movement.z == 1) //forward
            {
                movement = new Vector3(camF.x - moveHorizontal, 0.0f, camF.z - moveVertical);
            }
            else if(movement.x == -1) //left
            {
                //movement = new Vector3(moveHorizontal - distanceFromCam.x, 0.0f, moveVertical);
            }
            else if(movement.x == 1) //right
            {

            }
            //else
            //{
            //     movement = new Vector3(moveHorizontal - distanceFromCam.x, 0.0f, moveVertical - distanceFromCam.z);
            //}
            */
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            anim.SetFloat("MoveSpeed",1.0f);
        }
        else{
            anim.SetFloat("MoveSpeed", 0.0f);
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        
        //-----------------------------------------------------------
        
        //transform.rotation = cam.rotation;
        
        //float yStore = moveDirection.y;
        //moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //moveDirection = moveDirection.normalized * moveSpeed;
        //moveDirection.y = yStore;

        //controller.Move(moveDirection * Time.deltaTime);
        //if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    Debug.Log("REEEE");
        //    anim.SetFloat("MoveSpeed",1.0f);
        //    transform.rotation = Quaternion.Euler(0f,pivot.rotation.eulerAngles.y,0f);
        //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
        //    transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,rotateSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    anim.SetFloat("MoveSpeed", 0.0f);
        //}

        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    Debug.Log("HELLO");

        //    if(Input.GetKey(KeyCode.A))
        //    {
        //        transform.RotateAround(point, Vector3.up, 20 * Time.deltaTime);
        //    }
        //    else if(Input.GetKey(KeyCode.D))
        //    {
        //        transform.RotateAround(point, -Vector3.up, 20 * Time.deltaTime);
        //    }
    }
}
