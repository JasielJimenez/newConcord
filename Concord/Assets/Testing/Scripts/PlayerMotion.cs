using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMotion : MonoBehaviour
{
    #region variables
    Animator anim;
    public float holdAttack;
    public float speed = 6.0f;
    private float runSpeed = 0.0f;
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
    public bool isTakingDamage = false;
    public bool isAttacking = false;
    public GameObject gamemanager;
    public GameObject swordHitbox;
    public GameObject cam;
    public CharacterController controller;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        gamemanager = GameObject.Find("Gamemanager");
        cam = GameObject.Find("Main Camera");
        controller = GetComponent<CharacterController>();
        trackSpeed = speed;
        runSpeed = speed;
        holdAttack = this.GetComponent<PlayerStats>().attack;
        cam.GetComponent<CameraControl>().setCameraTarget();
    }

    #region animationFunctions
    public void beginRoll()
    {
        invulnerable = true;
        isRolling = true;
    }

    public void endRoll()
    {
        invulnerable = false;
        isRolling = false;
        anim.SetBool("Roll", false);
    }

    public void swordStrike()
    {
        swordHitbox.SetActive(true);
    }

    public void endStrike()
    {
        swordHitbox.SetActive(false);
    }

    /*
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
    */
    void attackBegin()
    {
        midAttack = true;
        trackSpeed = speed;
        speed = 0.0f;
        continueAttack = false;
        anim.SetBool("ContinueCombo",false);
    }

    void attackEnd()
    {
        speed = trackSpeed;
        if(continueAttack == true)
        {
            Debug.Log("ATTACK AGAIN");
            anim.SetBool("ContinueCombo",true);
        }
        else
        {
            isAttacking = false;
        }
        midAttack = false;
    }
    #endregion

    #region TakingDamage
    public void flinch()
    {
        anim.SetBool("IsHit", true);
        isTakingDamage = true;
    }

    public void stopFlinch()
    {
        anim.SetBool("IsHit", false);
        isTakingDamage = false;
    }
    #endregion

    void Update()
    {
        deadTest = this.GetComponent<PlayerStats>().playerDead;
        gamestart = gamemanager.GetComponent<gamemanager>().gameStart;
        gamepaused = gamemanager.GetComponent<gamemanager>().paused;
        if(deadTest == true)
        {
            anim.SetBool("IsDead",true);
            speed = 0;
            gamemanager.GetComponent<gamemanager>().GameOver();
        }
        else if (gamestart == true && gamepaused == false)
        {
            if(isRolling == true)
            {
                //transform.Translate(Vector3.forward * rollDistance * Time.deltaTime);
                controller.Move(transform.forward * rollDistance * Time.deltaTime);
            }
            else if(isTakingDamage == true)
            {
                //DO NOTHING
                //transform.Translate(-Vector3.forward * 200 * Time.deltaTime);
                controller.Move(-transform.forward * 200 * Time.deltaTime);
                anim.SetBool("IsHit", false);
                isTakingDamage = false;
            }
            else if(isAttacking == true)
            {

            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Attack();
                }
                else if(Input.GetKeyDown("space"))
                {
                    anim.Play("Roll");
                    anim.SetBool("Roll", true);
                    
                }
                else if(Input.GetButton("Guard"))
                {
                    anim.SetBool("IsGuarding", true);
                }
                else
                {
                    anim.SetBool("SwordAttack1", false);
                    anim.SetBool("IsGuarding", false);
                    ControlPlayer();
                    //PlayerMove();
                }

                if(midAttack == true)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        continueAttack = true;
                    }
                }
                if(Input.GetButtonDown("Heal"))
                {
                    this.GetComponent<PlayerStats>().healHealth();
                }
            }
        }
    }

    void Attack()
    {
            anim.SetBool("SwordAttack1", true);
            isAttacking = true;
    }

    void ControlPlayer()
    {
        Vector3 groundForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 groundRight = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

        //TEST
        //Debug.DrawRay(transform.position, groundForward * 10, Color.red);
        //Debug.DrawRay(transform.position, groundRight * 10, Color.green);

        Vector3 movement = groundForward * Input.GetAxisRaw("Vertical") + groundRight * Input.GetAxisRaw("Horizontal");
        movement = movement.normalized;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            anim.SetFloat("MoveSpeed",1.0f);
            speed = runSpeed;
        }
        else{
            anim.SetFloat("MoveSpeed", 0.0f);
        }
        //transform.Translate(movement * speed * Time.deltaTime, Space.World);
        controller.Move(movement * speed * Time.deltaTime);
    }
}
