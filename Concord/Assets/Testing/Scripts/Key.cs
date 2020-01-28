using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject bossDoor;
    // Start is called before the first frame update
    void Start()
    {
        
        bossDoor.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        bossDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
