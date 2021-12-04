using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text objectiveText = null;
    public GameObject BossDoor;
    // Start is called before the first frame update
    void Start()
    {
        BossDoor.SetActive(true);
        objectiveText.text = "Find the golden key";
    }

    public void OpenBossDoor()
    {
        BossDoor.SetActive(false);
        objectiveText.text = "Head to the castle courtyard";
    }

    public void BossFightBegin()
    {
        objectiveText.text = "Defeat the boss!";
    }

    public void endGame()
    {
        objectiveText.text = "Enter the castle";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
