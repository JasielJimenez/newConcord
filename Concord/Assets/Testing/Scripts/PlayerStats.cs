using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //Animator anim;
    public float maxHealth = 1.0f;
    public float currHealth = 1.0f;
    public float attack = 1.0f;
    private float enemyPower = 1.0f;

    public bool playerDead = false;
    public GameObject slime;
    public GameObject player;
    public GameObject endDoor;
    public Transform healthBar;

    public float currExp = 0.0f;
    public float maxExp = 10.0f;
    public float level = 1.0f;
    public Transform expBar;
    public Text levelNum = null;

    // Start is called before the first frame update
    void Start()
    {
        //slimePower = slime.GetComponent<EnemyStats>().power;
        endDoor.SetActive(true);
        expBar.localScale = new Vector3(currExp / maxExp , 0.4f);
        levelNum.text = "1";
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hitbox")
        {
            if(player.GetComponent<PlayerMotion>().invulnerable == false)
            {
                //Debug.Log("HIT");
                enemyPower = other.gameObject.transform.parent.gameObject.GetComponent<EnemyStats>().power;
                currHealth = currHealth - enemyPower;
                if(currHealth > -1)
                {
                    healthBar.localScale = new Vector3(currHealth / maxHealth , 1.0f);
                }
                else
                {
                    healthBar.localScale = new Vector3(0.0f , 1.0f);
                }
                //Debug.Log(slimePower);
                //Debug.Log(currHealth);
            }
        }
        if(other.gameObject.name == "healthPack")
        {
            currHealth = maxHealth;
            healthBar.localScale = new Vector3(currHealth / maxHealth , 1.0f);
        }

        if(other.gameObject.name == "key")
        {
            endDoor.SetActive(false);
        }
    }

    public void gainExperience(float obtainedExp)
    {
        currExp = currExp + obtainedExp;
        Debug.Log("Exp gained: " + obtainedExp);
        if(currExp >= maxExp)
        {
            levelUp();
        }
        expBar.localScale = new Vector3(currExp / maxExp , 0.4f);
    }

    public void levelUp()
    {
        maxExp = maxExp + 5.0f;
        currExp = 0.0f;
        level = level + 1.0f;
        Debug.Log("level up! Level is now: " + level);
        attack = attack + 2;
        levelNum.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth <= 0.0f)
        {
            playerDead = true;
        }
    }
}
