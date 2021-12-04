using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject levelManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        levelManager.GetComponent<LevelManager>().OpenBossDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
