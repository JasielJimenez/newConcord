using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public bool trackPlayer = true;
    public bool inRange = false;
    public bool deadCheck = false;
    public float range = 5;
    public float attackRange = 2;
    public GameObject target;
    public GameObject enemy;
    public GameObject mushroomParent; //Used to destroy parent collider
    public GameObject hitBox;
    private float playerHealth;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", true);
        target = GameObject.FindWithTag("Player");
        range = range * range;
        attackRange = attackRange * attackRange;
    }

    public void attack()
    {
        anim.SetBool("AttackRange",true);
        if(trackPlayer == true)
        {
            transform.LookAt(target.transform);
        }
    }

    public void FollowPlayer()
    {
        trackPlayer = true;
    }

    public void StopFollowPlayer()
    {
        trackPlayer = false;
    }

    public void startAttack()
    {
        hitBox.SetActive(true);
    }

    public void endAttack()
    {
        anim.SetBool("AttackRange",false);
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        deadCheck = enemy.GetComponent<EnemyStats>().dead;
        playerHealth = enemy.GetComponent<EnemyStats>().player.GetComponent<PlayerStats>().currHealth;
        if(deadCheck)
        {
            anim.SetBool("isAlive", false);
            endAttack();
            Destroy(mushroomParent, 3);
            Destroy(enemy, 3);
        }
        else if(playerHealth > 0.0f)
        {
            float distance = (target.transform.position - transform.position).sqrMagnitude;
            if(distance <= attackRange)
            {
                attack();
            }
        }
    }
}
