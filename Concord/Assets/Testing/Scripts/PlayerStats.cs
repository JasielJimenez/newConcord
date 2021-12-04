using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{

    #region variables
    //Animator anim;
    public float maxHealth = 1.0f;
    public float currHealth = 1.0f;
    public float attack = 1.0f;
    private float enemyPower = 1.0f;
    float force = 300;
    public int potionCount;

    public bool playerDead = false;
    public GameObject levelManager;
    public GameObject healthBar;
    public GameObject lostHealth;

    public float currExp = 0.0f;
    public float maxExp = 10.0f;
    public float level = 1.0f;
    public GameObject expBar;
    public Text levelNum = null;
    public Text potionNum = null;
    public ParticleSystem system;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Level_Manager");
        healthBar = GameObject.Find("BeginPlayerHealthBar");
        lostHealth = GameObject.Find("LostPlayerHealth");
        expBar = GameObject.Find("BeginExpBar");
        levelNum = GameObject.Find("PlayerLevel Num").GetComponent<Text>();
        potionNum = GameObject.Find("Potion Num").GetComponent<Text>();
        system = GameObject.Find("PlayerHitEffect").GetComponent<ParticleSystem>();
        expBar.transform.localScale = new Vector3(currExp / maxExp , 0.4f);
        levelNum.text = "1";
        potionCount = 3;
        potionNum.text = potionCount.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hitbox")
        {
            if(this.GetComponent<PlayerMotion>().invulnerable == false)
            {
                enemyPower = other.gameObject.transform.parent.gameObject.GetComponent<EnemyStats>().power;
                currHealth = currHealth - enemyPower;

                //FIX LAUNCH DIRECTION SO YOU DON'T FLY UPWARDS ----------------------------------------------------------------
                //Vector3 dir = transform.position - other.transform.position;
                //dir = -dir.normalized;
                //force = other.gameObject.transform.parent.gameObject.GetComponent<EnemyStats>().attackForce;
                //this.transform.LookAt(dir);
                //GetComponent<Rigidbody>().AddForce(dir*force);
                this.GetComponent<PlayerMotion>().flinch();

                if(currHealth > -1)
                {
                    healthBar.transform.localScale = new Vector3(currHealth / maxHealth , 1.0f);
                    lostHealth.transform.DOScaleX(currHealth / maxHealth , 2.0f);
                    emitHitEffect();
                }
                else
                {
                    healthBar.transform.localScale = new Vector3(0.0f , 1.0f);
                }
            }
        }
        if(other.gameObject.name == "healthPack")
        {
            potionCount++;
            potionNum.text = potionCount.ToString();
        }

        if(other.gameObject.name == "key")
        {
            levelManager.GetComponent<LevelManager>().OpenBossDoor();
            Destroy(other.gameObject);
        }
    }

    public void emitHitEffect()
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, 10);
    }

    public void healHealth()
    {
        if(potionCount > 0 && currHealth != maxHealth)
        {
            currHealth = maxHealth;
            healthBar.transform.DOScaleX(currHealth / maxHealth , 2.0f);
            lostHealth.transform.localScale = new Vector3(currHealth / maxHealth , 1.0f);
            potionCount--;
            potionNum.text = potionCount.ToString();
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
        expBar.transform.DOScaleX(currExp / maxExp , 1.0f);
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
