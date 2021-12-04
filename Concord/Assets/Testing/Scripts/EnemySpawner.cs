using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn = null;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(EnemyToSpawn, this.transform);
    }

    void OnEnable()
    {
        Instantiate(EnemyToSpawn, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
