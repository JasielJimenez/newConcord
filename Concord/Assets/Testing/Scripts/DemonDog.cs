using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDog : MonoBehaviour
{
    Animator anim;

    //public int randomAttack;
    public GameObject bossHealthBar;
    public GameObject enemy;
    public bool deadCheck = false;
    public GameObject player;
    public GameObject front;
    public GameObject back;
    //public GameObject left;
    //public GameObject right;
    public GameObject center;

    public GameObject frontHitbox;
    public GameObject backHitbox;

    public Transform target;

    public float speed = 0.0f;
    public float runRange = 0.0f;
    public float walkRange = 0.0f;
    public float attackRange = 0.0f;
    public float playerHealth = 0.0f;

    public bool stillAttacking = false;
    
    // Start is called before the first frame update
    //Squares distances to 
    void Start()
    {
        anim = GetComponent<Animator>();
        runRange = runRange * runRange;
        walkRange = walkRange * walkRange;
        attackRange = attackRange * attackRange;
        bossHealthBar.SetActive(false);
    }

    //used to make sure the boss doesn't move while attacking
    public void attacking()
    {
        stillAttacking = true;
    }

    //used to make sure the boss doesn't move while attacking
    public void stopAttacking()
    {
        stillAttacking = false;
    }

    //Called in the run animation to set speed
    public void startRun()
    {
        speed = 7.0f;
    }

    //Called in the run animation to set speed
    public void slowDownRun()
    {
        speed = 3.0f;
    }

    public void backAttack()
    {
        backHitbox.SetActive(true);
    }

    public void endbackAttack()
    {
        backHitbox.SetActive(false);
        anim.SetBool("BackAttack", false);
    }

    public void forwardAttack()
    {
        frontHitbox.SetActive(true);
    }

    public void endforwardAttack()
    {
        frontHitbox.SetActive(false);
        anim.SetBool("FrontAttack", false);
    }

    //Called when demon dog is in range to attack
    //Calculates how close the player
    public void attackStance()
    {
        speed = 0.0f;
        float frontDistance = (target.position - front.transform.position).sqrMagnitude;
        float backDistance = (target.position - back.transform.position).sqrMagnitude;
        //float leftDistance = (target.position - left.transform.position).sqrMagnitude;
        //float rightDistance = (target.position - right.transform.position).sqrMagnitude;
        //randomAttack = Random.Range(0,2);
        if(backDistance < frontDistance)
        {
            anim.SetBool("BackAttack", true);
        //    anim.SetBool("SideAttack", false);
            anim.SetBool("FrontAttack", false);
        }
        else if(backDistance > frontDistance)
        {
            //if(randomAttack == 0)
            //{
                anim.SetBool("FrontAttack", true);
            //    anim.SetBool("SideAttack", false);
                anim.SetBool("BackAttack", false);
            //}
            //else if(randomAttack == 1)
            //{
            //    anim.SetBool("SideAttack", true);
            //    anim.SetBool("FrontAttack", false);
            //    anim.SetBool("BackAttack", false);
            //}

        }
    }

    public void rangeCheck()
    {
        float distance = (target.position - transform.position).sqrMagnitude;
        if(distance <= attackRange)
        {
            anim.SetBool("RunRange", false);
            anim.SetBool("WalkRange", false);
            anim.SetBool("AttackRange", true);
            attackStance();
        }
        else if(distance <= walkRange && stillAttacking == false)
        {
            anim.SetBool("RunRange", false);
            anim.SetBool("WalkRange", true);
            anim.SetBool("AttackRange", false);
            anim.SetBool("BackAttack", false);
            anim.SetBool("FrontAttack", false);
            speed = 3.0f;
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if(distance <= runRange && stillAttacking == false)
        {
            anim.SetBool("WalkRange", false);
            anim.SetBool("RunRange", true);
            anim.SetBool("AttackRange", false);
            anim.SetBool("BackAttack", false);
            anim.SetBool("FrontAttack", false);
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            bossHealthBar.SetActive(true);
        }
        else
        {
            anim.SetBool("RunRange", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerStats>().currHealth;
        deadCheck = enemy.GetComponent<EnemyStats>().dead;
        if(deadCheck)
        {
            anim.SetBool("isAlive",false);

        }
        else if(playerHealth <= 0.0f)
        {
            anim.SetBool("RunRange", false);
        }
        else
        {
            rangeCheck();
        }
    }
}
