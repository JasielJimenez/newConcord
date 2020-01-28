using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthIcon;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(healthIcon);
        }
    }
}
