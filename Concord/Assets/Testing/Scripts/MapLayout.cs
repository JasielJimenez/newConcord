using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    public int mapSeed = 0;
    public GameObject Vwall_one;
    public GameObject Vwall_two;
    public GameObject Vwall_three;
    public GameObject Vwall_four;
    public GameObject Vwall_five;
    public GameObject Vwall_six;
    public GameObject Hwall_one;
    public GameObject Hwall_two;
    public GameObject Hwall_three;
    public GameObject Hwall_four;
    public GameObject Hwall_five;
    public GameObject Hwall_six;
    public GameObject keyZero;
    public GameObject keyOne;
    public GameObject keyTwo;
    public GameObject keyThree;
    public GameObject keyFour;
    // Start is called before the first frame update
    void Start()
    {
        Vwall_one.SetActive(true);
        Vwall_two.SetActive(true);
        Vwall_three.SetActive(true);
        Vwall_four.SetActive(true);
        Vwall_five.SetActive(true);
        Vwall_six.SetActive(true);
        Hwall_one.SetActive(true);
        Hwall_two.SetActive(true);
        Hwall_three.SetActive(true);
        Hwall_four.SetActive(true);
        Hwall_five.SetActive(true);
        Hwall_six.SetActive(true);
        keyZero.SetActive(false);
        keyOne.SetActive(false);
        keyTwo.SetActive(false);
        keyThree.SetActive(false);
        keyFour.SetActive(false);
        

        mapSeed = Random.Range(0,5);
        Debug.Log(mapSeed);
        if(mapSeed == 0)
        {
            spawnZero();
        }
        else if(mapSeed == 1)
        {
            spawnOne();
        }
        else if(mapSeed == 2)
        {
            spawnTwo();
        }
        else if(mapSeed == 3)
        {
            spawnThree();
        }
        else if(mapSeed == 4)
        {
            spawnFour();
        }
    }

    public void spawnZero()
    {
        Vwall_one.SetActive(false);
        Vwall_three.SetActive(false);
        Vwall_five.SetActive(false);
        Hwall_two.SetActive(false);
        Hwall_three.SetActive(false);
        Hwall_four.SetActive(false);
        keyZero.SetActive(true);
    }

    public void spawnOne()
    {
        Vwall_six.SetActive(false);
        Hwall_one.SetActive(false);
        Hwall_two.SetActive(false);
        Hwall_three.SetActive(false);
        Hwall_four.SetActive(false);
        Hwall_five.SetActive(false);
        keyOne.SetActive(true);
    }

    public void spawnTwo()
    {
        Vwall_one.SetActive(false);
        Vwall_six.SetActive(false);
        Hwall_one.SetActive(false);
        Hwall_two.SetActive(false);
        Hwall_three.SetActive(false);
        Hwall_five.SetActive(false);
        keyTwo.SetActive(true);
    }

    public void spawnThree()
    {
        Vwall_one.SetActive(false);
        Vwall_two.SetActive(false);
        Vwall_five.SetActive(false);
        Vwall_six.SetActive(false);
        Hwall_three.SetActive(false);
        Hwall_four.SetActive(false);
        Hwall_five.SetActive(false);
        Hwall_six.SetActive(false);
        keyThree.SetActive(true);
    }

    public void spawnFour()
    {
        Vwall_one.SetActive(false);
        Vwall_six.SetActive(false);
        Hwall_one.SetActive(false);
        Hwall_two.SetActive(false);
        Hwall_three.SetActive(false);
        Hwall_six.SetActive(false);
        keyFour.SetActive(true);
    }

}
