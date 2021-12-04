using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject PlayerToSpawn = null;
    public Transform levelStart;
    public Transform bossFight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void placeAtFirstSpawn()
    {
        Instantiate(PlayerToSpawn, new Vector3(levelStart.position.x, levelStart.position.y, levelStart.position.z), this.transform.rotation);
    }

    public void placeAtBossSpawn()
    {
        Instantiate(PlayerToSpawn, new Vector3(bossFight.position.x, bossFight.position.y, bossFight.position.z), this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
