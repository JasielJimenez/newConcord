using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useKey : MonoBehaviour
{
    public GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Character")
        {
            Destroy(key);
        }
    }
}
